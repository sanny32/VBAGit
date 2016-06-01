using Microsoft.Vbe.Interop;
using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using VBAGitAddin.SourceControl;

namespace VBAGitAddin.UI.Commands
{
    public class CommitCommand : ISourceControlCommand, IDisposable
    {
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

        public void Commit(string message)
        {

        }
        
        public TimeSpan LastExecutionDuration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Bitmap ProgressImage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository Repository
        {
            get
            {
                return _repository;
            }
        }

        public event EventHandler CommandAborted;
        public event EventHandler<ErrorEventArgs> CommandFailed;
        public event EventHandler<ProgressEventArgs> CommandProgress;
        public event EventHandler CommandSuccess;

        public void Abort()
        {
            throw new NotImplementedException();
        }
       
        public void Execute()
        {
            using (var commitForm = new CommitForm(this))
            {
                commitForm.ShowDialog();
            }
        }

        public void Dispose()
        {
            
        }

    }
}
