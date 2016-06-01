using Microsoft.Vbe.Interop;
using System.IO;
using VBAGitAddin.Settings;
using VBAGitAddin.SourceControl;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI
{         
    public sealed class UIApp
    {       
        private readonly VBE _vbe;
        private readonly IConfigurationService<SourceControlConfiguration> _configService;
        private readonly SourceControlConfiguration _config;
        
        internal UIApp(VBE vbe)
        {
            _vbe = vbe;

            _configService = new SourceControlConfigurationService();
            _config = _configService.LoadConfiguration();
        }
       
        public bool IsActiveProjectHasRepo
        {
            get
            {
                return GetVBProjectRepository(_vbe.ActiveVBProject) != null;
            }
        }

        public IRepository GetVBProjectRepository(VBProject project)
        {
            var projectRepoPath = GetVBProjectRepoPath(project);
            return (IRepository) _config.Repositories.Find(
                                       (Repository r) => (r.Name == project.Name &&
                                                          r.LocalLocation == projectRepoPath &&
                                                          Directory.Exists(r.LocalLocation)));
        }
         
        public static string GetVBProjectRepoPath(VBProject project)
        {
            var pathVBAGit = Path.Combine(Path.GetDirectoryName(project.FileName), VBAGitUI.VBAGitFolder);
            return Path.Combine(pathVBAGit, project.Name);
        }

        public void AddRepoToConfig(Repository repo)
        {
            if (!_config.Repositories.Exists((Repository r) => r.IsEqual(repo)))
            {
                _config.Repositories.Add(repo);
                _configService.SaveConfiguration(_config);
            }
        }

        public void CreateNewRepo()
        {
            using (var initCommand = new InitCommand(_vbe.ActiveVBProject))
            {
                initCommand.Execute();
                AddRepoToConfig((Repository)initCommand.Repository);
            }           
        }

        public void Commit()
        {
            var repo = GetVBProjectRepository(_vbe.ActiveVBProject);
            using (var commitCommand = new CommitCommand(_vbe.ActiveVBProject, repo))
            {
                commitCommand.Execute();
            }   
        }
    }
}
