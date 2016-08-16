using Microsoft.Vbe.Interop;
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using LibGit2Sharp;
using VBAGitAddin.Git;
using VBAGitAddin.Configuration;
using VBAGitAddin.UI.Forms;

namespace VBAGitAddin.UI.Commands
{
    public class CommandCommit : CommandBase
    {
        private class CommitInfo
        {
            public CommitInfo(string branch, string message, string author, DateTimeOffset when, IEnumerable<string> files)
            {
                Branch = branch;
                Message = message;
                Author = author;
                When = when;
                Files = files;
            }
            public string Branch { get; private set; }
            public string Message { get; private set;}
            public string Author { get; private set; }
            public DateTimeOffset When { get; private set; }
            public IEnumerable<string> Files { get; private set; }
        }

        
        public CommandCommit(VBProject project, RepositorySettings repoSettings, IEnumerable<string> components)
            :base(project)
        {
            Provider = new GitProvider(project, repoSettings);
            Components = components;
        }

        public IEnumerable<string> Components
        {
            get;
            private set;
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Commit", Repository.Info.WorkingDirectory);
            }
        }

        public string Author
        {
            get
            {
                return Provider.Author;
            }
        }       

        public string CurrentBranch
        {
            get
            {
                return (Provider.CurrentBranch == null) ? "master" : Provider.CurrentBranch.FriendlyName;
            }
        }        

        public void Commit(string branch, string message, string author, DateTimeOffset when, IEnumerable<string> files)
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {                   
                    RunCommandAsync(new CommitInfo(branch, message, author, when, files));
                };
                progressForm.ShowDialog();
            };
        }
       

        protected override void OnExectute(DoWorkEventArgs e)
        {
            ReportProgress(0, VBAGitUI.ProgressInfo_Commit);

            var commitInfo = e.Argument as CommitInfo;

            var options = new CommitOptions();
            options.AllowEmptyCommit = true;

            if (commitInfo.Files?.Count() > 0)
            {
                options.AllowEmptyCommit = false;
                Provider.Stage(commitInfo.Files);
            }

            if(commitInfo.Branch != "master" &&
               commitInfo.Branch != this.CurrentBranch)           
            {
                ReportProgress(50, VBAGitUI.ProgressInfo_CreateBranch);
                Provider.CreateBranch(commitInfo.Branch);                
            }

            ReportProgress(60, VBAGitUI.ProgressInfo_Commit);
            Provider.Commit(commitInfo.Message, commitInfo.Author, commitInfo.When, options);            
        }

       
        public override void Execute()
        {
            using (var commitForm = new CommitForm(this))
            {
                commitForm.ShowDialog();
            }
        }      
    }
}
