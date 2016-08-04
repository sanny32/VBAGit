using Microsoft.Vbe.Interop;
using System;
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using LibGit2Sharp;
using VBAGitAddin.Git;
using VBAGitAddin.Configuration;

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

        private readonly VBProject _project;
        private readonly GitProvider _provider;

        public CommandCommit(VBProject project, RepositorySettings repoSettings)
        {
            _project = project;
            _provider = new GitProvider(_project, repoSettings);
        }      
        
        public IList<StatusEntry> FileList
        {
            get
            {
                return _provider.Status().ToList();
            }
        }

        public VBProject VBProject
        {
            get
            {
                return _project;
            }
        }

        public string Author
        {
            get
            {
                return _provider.Author;
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
            var commitInfo = e.Argument as CommitInfo;

            var options = new CommitOptions();
            options.AllowEmptyCommit = true;

            if (commitInfo.Files?.Count() > 0)
            {
                options.AllowEmptyCommit = false;
                _provider.Stage(commitInfo.Files);
            }

            if(commitInfo.Branch != "master" &&
               commitInfo.Branch != this.CurrentBranch)           
            {
                _provider.CreateBranch(commitInfo.Branch);
            }

            _provider.Commit(commitInfo.Message, commitInfo.Author, commitInfo.When, options);            
        }


        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Commit", this.Repository.Info.WorkingDirectory);
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
       
        public override void Execute()
        {
            using (var commitForm = new CommitForm(this))
            {
                commitForm.ShowDialog();
            }
        }      
    }
}
