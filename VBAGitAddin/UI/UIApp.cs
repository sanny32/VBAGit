using Microsoft.Vbe.Interop;
using System;
using System.IO;
using System.ComponentModel;
using VBAGitAddin.SourceControl;
using VBAGitAddin.Settings;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.UI.Extensions;

namespace VBAGitAddin.UI
{         
    public sealed class UIApp
    {       
        private readonly VBE _vbe;
        private readonly AddIn _addIn;
        private readonly IConfigurationService<SourceControlConfiguration> _configService;
        private readonly SourceControlConfiguration _config;
        
        internal UIApp(VBE vbe, AddIn addIn)
        {
            _vbe = vbe;
            _addIn = addIn;

            _configService = new SourceControlConfigurationService();
            _config = _configService.LoadConfiguration();
        }
       
        public bool IsActiveProjectHasRepo
        {
            get
            {
                var project = _vbe.ActiveVBProject;
                var projectRepoPath = GetVBProjectRepoPath(project);
                return _config.Repositories.Exists(
                    (Repository r) => (r.Name == project.Name && 
                                       r.LocalLocation == projectRepoPath && 
                                       Directory.Exists(r.LocalLocation)));                
            }
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
            var task = new InitCommand(_vbe.ActiveVBProject);                      
            task.Execute();

            AddRepoToConfig((Repository)task.Repository);
        }

        public void Commit()
        {
            new CommitForm().ShowDialog();    
        }
    }
}
