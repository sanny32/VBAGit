using LibGit2Sharp;
using Microsoft.Vbe.Interop;
using System.ComponentModel;
using System.Drawing;
using VBAGitAddin.Configuration;
using VBAGitAddin.Git;

namespace VBAGitAddin.UI.Commands
{
    public class CommandCreateBranch : CommandBase, IGitCommand
    {
        private readonly VBProject _project;
        private readonly GitProvider _provider;

        public CommandCreateBranch(VBProject project, RepositorySettings repoSettings)
        {
            _project = project;
            _provider = new GitProvider(_project, repoSettings);
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Create Branch", this.Repository.Info.WorkingDirectory);
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
                return _provider.Repository;
            }
        }

        public string CurrentBranch
        {
            get
            {
                return (_provider.CurrentBranch == null) ? "master" : _provider.CurrentBranch.FriendlyName;
            }
        }

        public GitProvider Provider
        {
            get
            {
                return _provider;
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
