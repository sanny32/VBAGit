using System;
using System.Drawing;
using VBAGitAddin.SourceControl;

namespace VBAGitAddin.UI.Commands
{
    public class CommitCommand : ISourceControlCommand, IDisposable
    {

        public CommitCommand()
        {

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
                throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}
