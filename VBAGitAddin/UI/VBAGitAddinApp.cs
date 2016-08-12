using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VBAGitAddin.Configuration;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.UI.Forms;
using VBAGitAddin.VBEditor.Extensions;

namespace VBAGitAddin.UI
{         
    public sealed class VBAGitAddinApp : IDisposable
    {       
        private class RepositoryFile : IEquatable<RepositoryFile>
        {
            public RepositoryFile(string filePath, RepositorySettings repo, VBProject project)
            {
                FilePath = filePath;
                Repository = repo;
                Project = project;
            }

            public string FilePath { get; }
            public RepositorySettings Repository { get; }
            public VBProject Project { get; }

            public bool Equals(RepositoryFile other)
            {
                return FilePath == other.FilePath &&
                    Project == other.Project &&
                    Repository.Name == other.Repository.Name &&
                    Repository.LocalPath == other.Repository.LocalPath &&
                    Repository.RemotePath == other.Repository.RemotePath;
            }
        }

        private readonly VBE _vbe;
        private readonly IConfigurationService<GitConfiguration> _configService;
        private readonly GitConfiguration _config;

        private Timer _timer;
        private List<RepositoryFile> _changedFiles;
        private List<RepositoryFileWatcher> _repoWatchers;              

        internal VBAGitAddinApp(VBE vbe)
        {
            _vbe = vbe;

            _configService = new VBAGitConfigurationService();
            _config = _configService.LoadConfiguration();

            _changedFiles = new List<RepositoryFile>();
            _repoWatchers = new List<RepositoryFileWatcher>();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();

            _vbe.VBProjects.Cast<VBProject>()
                .ToList().ForEach(p => AddVBProject(p));           
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                lock (_changedFiles)
                {
                    if (_changedFiles.Count == 0)
                    {
                        return;
                    }

                    _timer.Stop();

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
                    var repoWatcher = (RepositoryFileWatcher)sender;
                    var repoFile = new RepositoryFile(e.FullPath, repoWatcher.Repository, repoWatcher.Project);
                    if (!_changedFiles.Contains(repoFile))
                    {
                        _changedFiles.Add(repoFile);
                    }
                }
            }
        }

        private void ReloadVBComponents()
        {
            DialogResultEx result = DialogResultEx.None;
            _changedFiles.ForEach(repoFile =>
            {
                var fileInfo = new FileInfo(repoFile.FilePath);
                var name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                var project = repoFile.Project;

                if (project != null && 
                    project.Contains(name))
                {
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
                                            using (ReloadFileForm form = new ReloadFileForm(fileInfo.FullName))
                                            {
                                                NativeWindow window = new NativeWindow();
                                                window.AssignHandle(new IntPtr(_vbe.MainWindow.HWnd));

                                                result = form.ShowDialog(window);
                                                switch (result)
                                                {
                                                    case DialogResultEx.Yes:
                                                    case DialogResultEx.YesToAll:
                                                        ReloadVBComponent(project, name, fileInfo.FullName);
                                                        break;
                                                }

                                                window.ReleaseHandle();
                                            }
                                        }
                                        break;

                                    case DialogResultEx.YesToAll:
                                        ReloadVBComponent(project, name, fileInfo.FullName);
                                        break;
                                }

                            }
                            break;
                    }
                }
            });

            _changedFiles.Clear();
        }

        private void ReloadVBComponent(VBProject project, string name, string filePath)
        {
            project.RemoveComponent(name);
            project.ImportSourceFile(filePath);            
        }

        /// <summary>
        /// Visual Basic for Applications IDE
        /// </summary>
        public VBE IDE
        {
            get
            {
                return _vbe;
            }
        }

        /// <summary>
        /// Enable or disable repository file watcher
        /// </summary>
        public bool EnableFileSystemWatcher
        {
            set
            {
                _repoWatchers.ForEach(fsWatcher => fsWatcher.EnableRaisingEvents = value);
            }
        }

        /// <summary>
        /// Add VBProject to watch repository files
        /// </summary>
        /// <param name="project">VBProject</param>
        public void AddVBProject(VBProject project)
        {
            RemoveVBProject(project);

            var repo = GetVBProjectRepository(project);
            if (repo != null)
            {
                var repoWatcher = new RepositoryFileWatcher();
                repoWatcher.NotifyFilter = NotifyFilters.LastWrite;
                repoWatcher.InternalBufferSize = 4096;
                repoWatcher.IncludeSubdirectories = false;
                repoWatcher.Changed += fsWatcher_Changed;
                repoWatcher.Repository = repo;
                repoWatcher.Project = project;
                repoWatcher.Path = repo.LocalPath;
                repoWatcher.EnableRaisingEvents = true;

                _repoWatchers.Add(repoWatcher);
            }
        }

        /// <summary>
        /// Remove VBProject form watching files
        /// </summary>
        /// <param name="project">VBProject</param>
        public void RemoveVBProject(VBProject project)
        {
            var repoWatcher = _repoWatchers.Find(w => w.Project == project);
            if(repoWatcher != null)
            {
                repoWatcher.EnableRaisingEvents = false;
                repoWatcher.Changed -= fsWatcher_Changed;
                repoWatcher.Dispose();

                _repoWatchers.Remove(repoWatcher);
            }
        }

        private string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        /// <summary>
        /// Returns repository settings for project
        /// </summary>
        /// <param name="project">VBProject</param>
        /// <returns></returns>
        public RepositorySettings GetVBProjectRepository(VBProject project)
        {
            var projectRepoPath = GetVBProjectRepoPath(project);            
            return _config.Repositories.Find(r => (r.Name == project.GetRepoName() &&
                                                   NormalizePath(r.LocalPath) == NormalizePath(projectRepoPath) &&
                                                   Directory.Exists(r.LocalPath)));
        }

        /// <summary>
        /// Returns folder contains repository path for project
        /// </summary>
        /// <param name="project">VBProject</param>
        /// <returns></returns>
        public static string GetVBProjectRepoPath(VBProject project)
        {
            var fileName = project.GetFileName();
            if (fileName != null)
            {
                var pathVBAGit = Path.Combine(Path.GetDirectoryName(fileName), VBAGitUI.VBAGitFolder);
                return Path.Combine(pathVBAGit, project.GetRepoName());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create new repository for project
        /// </summary>
        /// <param name="project">VBProject</param>
        public void CreateNewRepo(VBProject project)
        {
            try
            {
                EnableFileSystemWatcher = false;

                using (var gitCommand = new CommandInit(project))
                {
                    gitCommand.Execute();

                    if (gitCommand.Status == CommandStatus.Success)
                    {
                        RepositorySettings repo = new RepositorySettings();
                        repo.Name = project.GetRepoName();
                        repo.LocalPath = gitCommand.Repository.Info.WorkingDirectory;

                        if (!_config.Repositories.Exists(r => (r.Name == repo.Name &&
                                                    r.LocalPath == repo.LocalPath &&
                                                    r.RemotePath == repo.RemotePath)))
                        {
                            _config.Repositories.Add(repo);
                            _configService.SaveConfiguration(_config);
                        }

                        AddVBProject(project);
                    }
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        /// <summary>
        /// Commit changes to repository for project
        /// </summary>
        /// <param name="project">VBProject</param>
        public void Commit(VBProject project)
        {
            try
            {
                EnableFileSystemWatcher = false;

                var repo = GetVBProjectRepository(project);
                using (var gitCommand = new CommandCommit(project, repo))
                {
                    gitCommand.Execute();
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        /// <summary>
        /// Create new branch for repository
        /// </summary>
        /// <param name="project">VBProject</param>
        public void CreateBranch(VBProject project)
        {
            try
            {
                EnableFileSystemWatcher = false;

                var repo = GetVBProjectRepository(project);
                using (var gitCommand = new CommandCreateBranch(project, repo))
                {
                    gitCommand.Execute();
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        /// <summary>
        /// Revert files in repository
        /// </summary>
        /// <param name="project">VBProject</param>
        public void Revert(VBProject project)
        {
            try
            {
                EnableFileSystemWatcher = false;

                var repo = GetVBProjectRepository(project);
                using (var gitCommand = new CommandRevert(project, repo))
                {
                    gitCommand.Execute();
                }
            }
            finally
            {
                EnableFileSystemWatcher = true;
            }
        }

        /// <summary>
        /// Release resources 
        /// </summary>
        public void Dispose()
        {
            _repoWatchers.ForEach(repoWatcher =>
            {
                repoWatcher.EnableRaisingEvents = false;
                repoWatcher.Changed -= fsWatcher_Changed;
                repoWatcher.Dispose();
            });
            _repoWatchers.Clear();

            ///////////////////////////////////////////

            {
                _timer.Stop();
                _timer.Tick -= _timer_Tick;
                _timer.Dispose();
            }
        }
    }
}
