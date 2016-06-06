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
            public CommitInfo(string message, Signature author, IEnumerable<string> files)
            {
                Message = message;
                Author = author;
                Files = files;
            }
            public string Message { get; private set;}
            public Signature Author { get; private set; }
            public IEnumerable<string> Files { get; private set; }
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

        //public string Author
        //{
        //    get
        //    {
        //        _provider.
        //    }
        //}

        public void Commit(string message, Signature author, IEnumerable<string> files)
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {                   
                    RunCommandAsync(new CommitInfo(message, author, files));
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

            _provider.Commit(commitInfo.Message, commitInfo.Author, options);
        }


        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Commit", _repository.LocalLocation);
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
