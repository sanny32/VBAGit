using Microsoft.Vbe.Interop;
using System;
using System.IO;
using VBAGitAddin.Configuration;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI
{         
    public sealed class VBAGitAddinApp
    {       
        private readonly VBE _vbe;
        private readonly IConfigurationService<GitConfiguration> _configService;
        private readonly GitConfiguration _config;
        
        internal VBAGitAddinApp(VBE vbe)
        {
            _vbe = vbe;

            _configService = new VBAGitConfigurationService();
            _config = _configService.LoadConfiguration();
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
            var project = _vbe.ActiveVBProject;
            using (var initCommand = new CommandInit(project))
            {
                initCommand.Execute();

                RepositorySettings repoSetting = new RepositorySettings();
                repoSetting.Name = project.Name;
                repoSetting.LocalPath = initCommand.Repository.Info.WorkingDirectory;

                AddRepoToConfig(repoSetting);
            }           
        }

        public void Commit()
        {
            var repo = GetVBProjectRepository(_vbe.ActiveVBProject);
            using (var commitCommand = new CommandCommit(_vbe.ActiveVBProject, repo))
            {
                commitCommand.Execute();
            }   
        }

        public void CreateBranch()
        {
            var repo = GetVBProjectRepository(_vbe.ActiveVBProject);
            using (var createBranchCommand = new CommandCreateBranch(_vbe.ActiveVBProject, repo))
            {
                createBranchCommand.Execute();
            }
        }
    }
}
