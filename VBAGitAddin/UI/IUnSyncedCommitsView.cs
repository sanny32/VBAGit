using System;
using System.Collections.Generic;
using VBAGitAddin.SourceControl;

namespace VBAGitAddin.UI
{
    public interface IUnsyncedCommitsView
    {
        event EventHandler<EventArgs> Fetch;
        event EventHandler<EventArgs> Pull;
        event EventHandler<EventArgs> Push;
        event EventHandler<EventArgs> Sync;

        string CurrentBranch { get; set; }
        IList<ICommit> IncomingCommits { get; set; }
        IList<ICommit> OutgoingCommits { get; set; } 
    }
}
