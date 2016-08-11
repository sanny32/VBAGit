using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using LibGit2Sharp;
using Microsoft.Vbe.Interop;
using VBAGitAddin.Configuration;
using VBAGitAddin.Git;
using VBAGitAddin.UI.Forms;


namespace VBAGitAddin.UI.Commands
{
    public class CommandRevert : CommandBase, IGitCommand
    {
        private readonly VBProject _project;
        private readonly GitProvider _provider;

        public CommandRevert(VBProject project, RepositorySettings repoSettings)
        {
            _project = project;
            _provider = new GitProvider(_project, repoSettings);
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Revert", this.Repository.Info.WorkingDirectory);
            }
        }

        public override Bitmap ProgressImage
        {
            get
            {
                return null;
            }
        }

        public IList<StatusEntry> FileList
        {
            get
            {
                return _provider.Status().ToList();
            }
        }

        public override IRepository Repository
        {
            get
            {
                return _provider.Repository;
            }
        }

        public GitProvider Provider
        {
            get
            {
                return _provider;
            }
        }

        public void Revert(IEnumerable<string> files)
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {
                    RunCommandAsync(files);
                };
                progressForm.ShowDialog();
            };
        }

        public override void Execute()
        {
            using (var revertForm = new RevertForm(this))
            {
                revertForm.ShowDialog();
            }
        }

        protected override void OnExectute(DoWorkEventArgs e)
        {
            ReportProgress(0, VBAGitUI.ProgressInfo_Revert);

            var files = e.Argument as IEnumerable<string>;

            //_provider.Revert()            
        }
    }
}
