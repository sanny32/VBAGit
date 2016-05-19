using System;
using VBEGit;

namespace VBAGitUI
{
    public interface IProviderPresenter
    {
        ISourceControlProvider Provider { get; set; }
        event EventHandler<ActionFailedEventArgs> ActionFailed;
    }

    public abstract class ProviderPresenterBase : IProviderPresenter
    {
        public virtual ISourceControlProvider Provider { get; set; }

        public event EventHandler<ActionFailedEventArgs> ActionFailed;
        protected virtual void RaiseActionFailedEvent(SourceControlException ex)
        {
            var handler = ActionFailed;
            if (handler != null)
            {
                handler(this, new ActionFailedEventArgs(ex));
            }
        }
    }
}
