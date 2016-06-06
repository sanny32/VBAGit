using Microsoft.Vbe.Interop;
using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using VBAGitAddin.SourceControl;
using VBAGitAddin.UI.Extensions;

namespace VBAGitAddin.UI.Commands
{
    public class CommitCommand : ISourceControlCommand, IDisposable
    {
        private readonly VBProject _project;
        private readonly IRepository _repository;
        private readonly ISourceControlProvider _provider;
        private Stopwatch _watch;

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
                    _watch = Stopwatch.StartNew();

                    Exception error = null;
                    try
                    {
                        var options = new CommitOptions();

                        if (files?.Count() > 0)
                        {
                            options.AllowEmptyCommit = true;
                            _provider.Stage(files);
                        }

                        _provider.Commit(message, author, options);
                    }
                    catch(Exception ex)
                    {
                        error = ex;
                    }

                    _watch.Stop();

                    if (error != null)
                    {
                        CommandFailed?.Raise(this, new ErrorEventArgs(error));
                    }
                    else
                    {
                        CommandSuccess?.Raise(this, new EventArgs());
                    }
                };

                progressForm.ShowDialog();
            };
        }  

        public TimeSpan LastExecutionDuration
        {
            get
            {
                return _watch.Elapsed;
            }
        }

        public string Name
        {
            get
            {
                return "Git Commit";
            }
        }

        public Bitmap ProgressImage
        {
            get
            {
                return null;
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
