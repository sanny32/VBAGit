using Microsoft.Vbe.Interop;
using System.IO;
using VBAGitAddin.SourceControl;
using VBAGitAddin.Settings;

namespace VBAGitAddin.UI
{
    class App
    {
        private readonly VBE _vbe;
        private readonly AddIn _addIn;
        private readonly IConfigurationService<SourceControlConfiguration> _configService;
        private readonly SourceControlConfiguration _config;

        internal App(VBE vbe, AddIn addIn)
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
                return _config.Repositories.Exists((Repository r) => r.Name == project.Name);                
            }
        }

        public void CreateNewRepo()
        {
            var project = _vbe.ActiveVBProject;
            var providerFactory = new SourceControlProviderFactory();
            var provider = providerFactory.CreateProvider(project);
            var pathVBAGit = Path.Combine(Path.GetDirectoryName(project.FileName), VBAGitUI.VBAGitFolder);
            var repo = provider.Init(Path.Combine(pathVBAGit, project.Name));

            AddRepoToConfig((Repository)repo);
        }

        private void AddRepoToConfig(Repository repo)
        {
            _config.Repositories.Add(repo);
            _configService.SaveConfiguration(_config);
        }

    }
}
