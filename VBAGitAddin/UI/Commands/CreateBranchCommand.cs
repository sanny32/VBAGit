using Microsoft.Vbe.Interop;
using System.ComponentModel;
using System.Drawing;
using VBAGitAddin.SourceControl;

namespace VBAGitAddin.UI.Commands
{
    public class CreateBranchCommand : CommandBase, ISourceControlCommand
    {
        private readonly VBProject _project;
        private readonly IRepository _repository;
        private readonly ISourceControlProvider _provider;

        public CreateBranchCommand(VBProject project, IRepository repo)
        {
            _project = project;
            _repository = repo;

            var providerFactory = new SourceControlProviderFactory();
            _provider = providerFactory.CreateProvider(_project, _repository);
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Create Branch", _repository.LocalLocation);
            }
        }

        public override Bitmap ProgressImage
        {
            get
            {
                return null;
            }
        }

        public override IRepository Repository
        {
            get
            {
                return _repository;
            }
        }

        public override void Execute()
        {
            
        }

        protected override void OnExectute(DoWorkEventArgs e)
        {
            
        }
    }
}
