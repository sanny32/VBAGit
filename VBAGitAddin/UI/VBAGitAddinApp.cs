using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VBAGitAddin.Configuration;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.UI.Forms;
using VBAGitAddin.VBEditor.Extensions;

namespace VBAGitAddin.UI
{         
    public sealed class VBAGitAddinApp : IDisposable
    {       
        private readonly VBE _vbe;
        private readonly IConfigurationService<GitConfiguration> _configService;
        private readonly GitConfiguration _config;
        private List<FileSystemWatcher> _fsWatchers;
        private List<string> _changedFiles;
        private IGitCommand _gitCommand;
        private Timer _timer;

        internal VBAGitAddinApp(VBE vbe)
        {
            _vbe = vbe;
            _gitCommand = null;

            _configService = new VBAGitConfigurationService();
            _config = _configService.LoadConfiguration();

            _fsWatchers = new List<FileSystemWatcher>();
            _changedFiles = new List<string>();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();

            foreach (VBProject prj in _vbe.VBProjects)
            {
                var repo = GetVBProjectRepository(prj);
                if (repo != null)
                {
                    var fsWatcher = new FileSystemWatcher();
                    fsWatcher.NotifyFilter = NotifyFilters.LastWrite;                    
                    fsWatcher.InternalBufferSize = 4096;
                    fsWatcher.IncludeSubdirectories = false;
                    fsWatcher.Changed += fsWatcher_Changed;
                    fsWatcher.Path = repo.LocalPath;
                    fsWatcher.EnableRaisingEvents = true;                    

                    _fsWatchers.Add(fsWatcher);
                }
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Stop();

                lock (_changedFiles)
                {
                    if (_changedFiles.Count == 0)
                    {
                        return;
                    }

                    //Because refreshing removes components, we need to store the current selection,
                    // so we can correctly reset it once the files are imported from the repository.
                    var selection = _vbe.ActiveCodePane.GetSelection();
                    string name = null;
                    if (selection.QualifiedName.Component != null)
                    {
                        name = selection.QualifiedName.Component.Name;
                    }

                    ReloadVBComponents();

                    _vbe.SetSelection(selection.QualifiedName.Project, selection.Selection, name);
                }
            }
            finally
            {
                _timer.Start();
            }
        }

        private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {           
            if(e.ChangeType == WatcherChangeTypes.Changed)
            {                               
                lock(_changedFiles)
                {
                    if (!_changedFiles.Contains(e.FullPath))
                    {
                        _changedFiles.Add(e.FullPath);
                    }
                }
            }
        }

        private void ReloadVBComponents()
        {
            DialogResultEx result = DialogResultEx.None;
            _changedFiles.ForEach(filePath =>
            {
                var fileInfo = new FileInfo(filePath);
                var name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                switch (fileInfo.Extension)
                {
                    case VBComponentExtensions.ClassExtesnion:
                    case VBComponentExtensions.DocClassExtension:
                    case VBComponentExtensions.FormExtension:
                    case VBComponentExtensions.StandardExtension:
                        {
                            switch (result)
                            {
                                case DialogResultEx.Yes:
                                case DialogResultEx.No:
                                case DialogResultEx.None:
                                    {
                                        using (ReloadFileForm form = new ReloadFileForm(filePath))
                                        {
                                            NativeWindow window = new NativeWindow();
                                            window.AssignHandle(new IntPtr(_vbe.MainWindow.HWnd));

                                            result = form.ShowDialog(window);
                                            switch (result)
                                            {
                                                case DialogResultEx.Yes:
                                                case DialogResultEx.YesToAll:
                                                    ReloadVBComponent(name, fileInfo.FullName);
                                                    break;
                                            }

                                            window.ReleaseHandle();
                                        }
                                    }
                                    break;

                                case DialogResultEx.YesToAll:
                                    ReloadVBComponent(name, fileInfo.FullName);
                                    break;
                            }

                        }
                        break;
                }
            });

            _changedFiles.Clear();
        }

        private void ReloadVBComponent(string name, string filePath)
        {           
            _vbe.ActiveVBProject.RemoveComponent(name);
            _vbe.ActiveVBProject.ImportSourceFile(filePath);           
        }

        public bool EnableFileSystemWatcher
        {
            set
            {
                _fsWatchers.ForEach(fsWatcher => fsWatcher.EnableRaisingEvents = value);
            }
        }

        public bool IsActiveProjectHasRepo
        {
            get
            {
                return GetVBProjectRepository(_vbe.ActiveVBProject) != null;
            }
        }       

        public RepositorySettings GetVBProjectRepository(VBProject project)
        {
            var projectRepoPath = GetVBProjectRepoPath(project);
            return _config.Repositories.Find(r => (r.Name == project.Name &&
                                                   NormalizePath(r.LocalPath) == NormalizePath(projectRepoPath) &&
                                                   Directory.Exists(r.LocalPath)));
        }

        public string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        public static string GetVBProjectRepoPath(VBProject project)
        {
            var pathVBAGit = Path.Combine(Path.GetDirectoryName(project.FileName), VBAGitUI.VBAGitFolder);
            return Path.Combine(pathVBAGit, project.Name);
        }

        public void AddRepoToConfig(RepositorySettings repo)
        {
            if (!_config.Repositories.Exists(r => (r.Name == repo.Name &&
                                                   r.LocalPath == repo.LocalPath &&
                                                   r.RemotePath == repo.RemotePath)))
            {
                _config.Repositories.Add(repo);
                _configService.SaveConfiguration(_config);
            }
        }

        public void CreateNewRepo()
        {
            try
            {
                EnableFileSystemWatcher = false;

                var project = _vbe.ActiveVBProject;
                using (_gitCommand = new CommandInit(project))
                {
                    _gitCommand.Execute();

                    RepositorySettings repoSetting = new RepositorySettings();
                    repoSetting.Name = project.Name;
                    repoSetting.LocalPath = _gitCommand.Repository.Info.WorkingDirectory;

                    AddRepoToConfig(repoSetting);
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        public void Commit()
        {
            try
            {
                EnableFileSystemWatcher = false;

                var repo = GetVBProjectRepository(_vbe.ActiveVBProject);
                using (_gitCommand = new CommandCommit(_vbe.ActiveVBProject, repo))
                {
                    _gitCommand.Execute();
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        public void CreateBranch()
        {
            try
            {
                EnableFileSystemWatcher = false;

                var repo = GetVBProjectRepository(_vbe.ActiveVBProject);
                using (_gitCommand = new CommandCreateBranch(_vbe.ActiveVBProject, repo))
                {
                    _gitCommand.Execute();
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        public void Dispose()
        {
            _fsWatchers.ForEach(fsWatcher =>
            {
                fsWatcher.EnableRaisingEvents = false;
                fsWatcher.Changed -= fsWatcher_Changed;
                fsWatcher.Dispose();
            });
            _fsWatchers.Clear();
        }
    }
}
