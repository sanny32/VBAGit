using Microsoft.Vbe.Interop;
using System;
using System.IO;
using System.Windows.Forms;
using VBAGitAddin.SourceControl;
using VBAGitAddin.Settings;

namespace VBAGitAddin.UI
{ 
    public class RepositoryEventArgs: EventArgs
    {
        public RepositoryEventArgs(IRepository repo)
        {
            Repository = repo;
        }

        public IRepository Repository { get; private set; }
    }
    
    public sealed class UIApp
    {       
        private readonly VBE _vbe;
        private readonly AddIn _addIn;
        private readonly IConfigurationService<SourceControlConfiguration> _configService;
        private readonly SourceControlConfiguration _config;

        public event EventHandler<RepositoryEventArgs> NewRepositoryCreated;
        
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
                return _config.Repositories.Exists(
                    (Repository r) => (r.Name == project.Name && Directory.Exists(r.LocalLocation)) ||
                                      (r.Name == project.Name + Repository.BareExt && Directory.Exists(r.RemoteLocation)));                
            }
        }

        public void AddRepoToConfig(Repository repo)
        {
            if (!_config.Repositories.Exists((Repository r) => r.IsEqual(repo)))
            {
                _config.Repositories.Add(repo);
                _configService.SaveConfiguration(_config);
            }
        }

        public void CreateNewRepo(bool bare)
        {
            var project = _vbe.ActiveVBProject;
            var pathVBAGit = Path.Combine(Path.GetDirectoryName(project.FileName), VBAGitUI.VBAGitFolder);
            var pathRepo = Path.Combine(pathVBAGit, project.Name);

            var providerFactory = new SourceControlProviderFactory();
            var provider = providerFactory.CreateProvider(project);
            var repo = (Repository)provider.Init(pathRepo, bare);
            AddRepoToConfig(repo);

            NewRepositoryCreated?.Invoke(this, new RepositoryEventArgs(repo));
        }

        public void Commit(string message)
        {
            var project = _vbe.ActiveVBProject;
            var providerFactory = new SourceControlProviderFactory();
            var provider = providerFactory.CreateProvider(project);
            provider.Commit(message);
        }      

    }
}
