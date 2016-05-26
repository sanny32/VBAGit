using Microsoft.Vbe.Interop;
using System.IO;
using System.Windows.Forms;
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
                return _config.Repositories.Exists(
                    (Repository r) => (r.Name == project.Name && Directory.Exists(r.LocalLocation)) ||
                                      (r.Name == project.Name + Repository.BareExt && Directory.Exists(r.RemoteLocation)));                
            }
        }

        public bool CreateNewRepo()
        {
            using (var initForm = new InitForm())
            {
                var project = _vbe.ActiveVBProject;                
                var pathVBAGit = Path.Combine(Path.GetDirectoryName(project.FileName), VBAGitUI.VBAGitFolder);
                var pathRepo = Path.Combine(pathVBAGit, project.Name);

                if (initForm.ShowDialog(pathRepo) == DialogResult.OK)
                {
                    var providerFactory = new SourceControlProviderFactory();
                    var provider = providerFactory.CreateProvider(project);
                    var repo = (Repository)provider.Init(pathRepo, false);
                    AddRepoToConfig(repo);

                    return true;
                }
            }

            return false;
        }

        public void Commit()
        {
            using (var commitForm = new CommitForm())
            {
                //var project = _vbe.ActiveVBProject;
                //var providerFactory = new SourceControlProviderFactory();
                //var provider = providerFactory.CreateProvider(project);
                
                commitForm.ShowDialog();
            }
        }

        private void AddRepoToConfig(Repository repo)
        {
            if (!_config.Repositories.Exists((Repository r) => r.IsEqual(repo)))
            {
                _config.Repositories.Add(repo);
                _configService.SaveConfiguration(_config);
            }
        }

    }
}
