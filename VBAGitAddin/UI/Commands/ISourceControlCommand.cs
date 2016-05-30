using System;
using VBAGitAddin.SourceControl;

namespace VBAGitAddin.UI.Commands
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(int progress, string info)
        {
            Info = info;
            Progress = progress;
        }

        public int Progress
        {
            get;
            private set;
        }

        public string Info
        {
            get;
            private set;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(Exception ex)
        {
            Error = ex;
        }

        public Exception Error
        {
            get;
            private set;
        }
    }

    public interface ISourceControlCommand
    {
        string Name { get; }
        IRepository Repository { get; }
        TimeSpan LastExecutionDuration { get; }

        event EventHandler<ProgressEventArgs> CommandProgress;
        event EventHandler CommandAborted;
        event EventHandler CommandSuccess;
        event EventHandler<ErrorEventArgs> CommandFailed;

        void Abort();
        void Execute();        
    }
}
