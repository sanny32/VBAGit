using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using System.Diagnostics;
using VBAGitAddin.SourceControl;
using VBAGitAddin.UI.Extensions;
using VBAGitAddin.VBEditor.Extensions;

namespace VBAGitAddin.UI.Commands
{
    public class InitCommand : ISourceControlCommand
    {
        private readonly VBProject _project;
        private BackgroundWorker _bgw;
        private Stopwatch _watch;

        public InitCommand(VBProject project)
        {
            _project = project;

            _bgw = new BackgroundWorker();
            _bgw.DoWork += _bgw_DoWork;
            _bgw.RunWorkerCompleted += _bgw_RunWorkerCompleted;

            _watch = new Stopwatch();
        }

        public IRepository Repository
        {
            get;
            private set;
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
                return "Git Init";
            }
        }

        public event EventHandler CommandAborted;
        public event EventHandler CommandSuccess;
        public event EventHandler<ProgressEventArgs> CommandProgress;
        public event EventHandler<ErrorEventArgs> CommandFailed;

        public void Abort()
        {
            _bgw.CancelAsync();
        }

        public void Execute()
        {
            _bgw.RunWorkerAsync();

            using (var progressForm = new ProgressForm(this))
            {
                progressForm.ShowDialog();
            };
        }

        private void _bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            _watch = Stopwatch.StartNew();

            var path = UIApp.GetVBProjectRepoPath(_project);
            var providerFactory = new SourceControlProviderFactory();
            var provider = providerFactory.CreateProvider(_project);

            Repository = provider.Init(path, false);

            int progress = 0;
            int count = _project.VBComponents.Count;
            foreach (VBComponent component in _project.VBComponents)
            {
                if (e.Cancel)
                {
                    _watch.Stop();
                    CommandAborted?.Raise(this, new EventArgs());
                    return;
                }

                CommandProgress?.Raise(this, new ProgressEventArgs(100 * ++progress / count, VBAGitUI.ProgressInfo_ExportingFiles));

                component.ExportAsSourceFile(path);
            }

            CommandProgress?.Raise(this, new ProgressEventArgs(100, string.Empty));
        }


        private void _bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _watch.Stop();
            if (e.Error != null)
            {
                CommandFailed?.Raise(this, new ErrorEventArgs(e.Error));
            }
            else
            {
                CommandSuccess?.Raise(this, new EventArgs());
            }
        }
    }
}
