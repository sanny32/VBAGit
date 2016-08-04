using LibGit2Sharp;
using System;
using System.Drawing;

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

    public enum CommandStatus
    {
        Success = 0,
        InProgress = 1,
        Aborted = 2,
        Error = 3,
        NotExecuted = 4
    }

    public interface IGitCommand
    {
        string Name { get; }
        IRepository Repository { get; }
        TimeSpan LastExecutionDuration { get; }
        CommandStatus Status { get; }
        Bitmap ProgressImage { get; }

        event EventHandler<ProgressEventArgs> CommandProgress;
        event EventHandler CommandAborted;
        event EventHandler CommandSuccess;
        event EventHandler<ErrorEventArgs> CommandFailed;

        void Abort();
        void Execute();        
    }
}
