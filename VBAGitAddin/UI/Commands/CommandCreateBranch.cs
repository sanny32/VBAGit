using LibGit2Sharp;
using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using System.Drawing;
using VBAGitAddin.Configuration;
using VBAGitAddin.Git;
using VBAGitAddin.UI.Forms;

namespace VBAGitAddin.UI.Commands
{
    public class CommandCreateBranch : CommandBase, IGitCommand
    {
        private class CreateBranchInfo
        {
            public CreateBranchInfo(string branch, string description, CreateBranchOptions options)
            {
                Branch = branch;
                Description = description;
                Options = options;              
            }
            public string Branch { get; private set; }
            public string Description { get; private set; }
            public CreateBranchOptions Options { get; private set; }
        }

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

        public void CreateBranch(string branch, string description, CreateBranchOptions options)
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {
                    RunCommandAsync(new CreateBranchInfo(branch, description, options));
                };
                progressForm.ShowDialog();
            };
        }

        public override void Execute()
        {
            using (var createBranchForm = new CreateBranchForm(this))
            {
                createBranchForm.ShowDialog();
            }
        }

        protected override void OnExectute(DoWorkEventArgs e)
        {
            ReportProgress(100, VBAGitUI.ProgressInfo_CreateBranch);

            var branchInfo = e.Argument as CreateBranchInfo;
            _provider.CreateBranch(branchInfo.Branch, branchInfo.Description, branchInfo.Options);            
        }
    }
}
