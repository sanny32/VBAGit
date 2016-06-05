using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using System.Diagnostics;
using VBAGitAddin.SourceControl;
using VBAGitAddin.UI.Extensions;
using VBAGitAddin.VBEditor.Extensions;
using System.Drawing;

namespace VBAGitAddin.UI.Commands
{
    public class InitCommand : ISourceControlCommand, IDisposable
    {
        private readonly VBProject _project;
        private BackgroundWorker _bgw;
        private Stopwatch _watch;

        public InitCommand(VBProject project)
        {
            _project = project;

            _bgw = new BackgroundWorker();            
            _bgw.DoWork += _bgw_DoWork;
            _bgw.ProgressChanged += _bgw_ProgressChanged;
            _bgw.RunWorkerCompleted += _bgw_RunWorkerCompleted;
            _bgw.WorkerReportsProgress = true;
            _bgw.WorkerSupportsCancellation = true;

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

        public Bitmap ProgressImage
        {
            get
            {
                return Properties.Resources.tshell32_160;
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
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += ProgressForm_Shown;
                progressForm.ShowDialog();
            };
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            _bgw.RunWorkerAsync();
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
                if (_bgw.CancellationPending)
                {
                    e.Cancel = true;              
                    return;
                }

                component.ExportAsSourceFile(path);

                _bgw.ReportProgress(100 * ++progress / count);
            }
        }

        private void _bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommandProgress?.Raise(this, new ProgressEventArgs(e.ProgressPercentage, VBAGitUI.ProgressInfo_ExportingFiles));
        }


        private void _bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _watch.Stop();

            if (e.Cancelled)
            {
                CommandAborted?.Raise(this, new EventArgs());
            }
            else
            {
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

        public void Dispose()
        {
            _bgw.Dispose();
        }
    }
}
