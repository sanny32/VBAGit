using Microsoft.Vbe.Interop;
using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using VBAGitAddin.SourceControl;
using VBAGitAddin.UI.Extensions;
using System.ComponentModel;

namespace VBAGitAddin.UI.Commands
{
    public class CommitCommand : CommandBase
    {
        private class CommitInfo
        {
            public string message;
            public Signature author;
            public IEnumerable<string> files;
        }

        private readonly VBProject _project;
        private readonly IRepository _repository;
        private readonly ISourceControlProvider _provider;

        public CommitCommand(VBProject project, IRepository repo)
        {
            _project = project;
            _repository = repo;

            var path = UIApp.GetVBProjectRepoPath(_project);
            var providerFactory = new SourceControlProviderFactory();
           _provider = providerFactory.CreateProvider(_project, _repository);
        }      
        
        public IList<IFileStatusEntry> FileList
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

        public void Commit(string message, Signature author, IEnumerable<string> files)
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {
                    var commitInfo = new CommitInfo();
                    commitInfo.message = message;
                    commitInfo.author = author;
                    commitInfo.files = files;

                    RunCommandAsync(commitInfo);
                };
                progressForm.ShowDialog();
            };
        }
       

        protected override void OnExectute(DoWorkEventArgs e)
        {
            var commitInfo = e.Argument as CommitInfo;

            var options = new CommitOptions();
            options.AllowEmptyCommit = true;

            if (commitInfo.files?.Count() > 0)
            {
                options.AllowEmptyCommit = false;
                _provider.Stage(commitInfo.files);
            }

            _provider.Commit(commitInfo.message, commitInfo.author, options);
        }


        public override string Name
        {
            get
            {
                return "Git Commit";
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
            using (var commitForm = new CommitForm(this))
            {
                commitForm.ShowDialog();
            }
        }      
    }
}
