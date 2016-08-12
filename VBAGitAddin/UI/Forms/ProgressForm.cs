using System;
using System.Windows.Forms;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.Diagnostics;

namespace VBAGitAddin.UI.Forms
{
    public partial class ProgressForm : PersistentForm
    {
        private IGitCommand _gitCommand;
        private RichTextBoxTraceListener _tracer;

        public ProgressForm(IGitCommand gitCommand)
        {
            InitializeComponent();

            _gitCommand = gitCommand;
            _gitCommand.CommandProgress += _gitCommand_CommandProgress;
            _gitCommand.CommandSuccess += _gitCommand_CommandSuccess;
            _gitCommand.CommandAborted += _gitCommand_CommandAborted;
            _gitCommand.CommandFailed += _gitCommand_CommandFailed;
            
            _tracer = new RichTextBoxTraceListener(LogBox);
            Trace.AutoFlush = true;
            Trace.Listeners.Add(_tracer);            
        }

        public new void ShowDialog()
        {
            Close.Enabled = false;
            LogBox.Text = string.Empty;

            Text = string.Format(VBAGitUI.ProgressForm_Text, _gitCommand.Name);
            Animation.AnimatedImage = _gitCommand.ProgressImage;

            Animation.AnimateImage();           

            base.ShowDialog();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Trace.Listeners.Remove(_tracer);

            _gitCommand.CommandProgress -= _gitCommand_CommandProgress;
            _gitCommand.CommandSuccess -= _gitCommand_CommandSuccess;
            _gitCommand.CommandAborted -= _gitCommand_CommandAborted;
            _gitCommand.CommandFailed -= _gitCommand_CommandFailed;

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void _gitCommand_CommandAborted(object sender, EventArgs e)
        {
            Close();
        }

        private void _gitCommand_CommandSuccess(object sender, EventArgs e)
        {
            ProgressBar.Value = 100;
            Abort.Enabled = false;
            Close.Enabled = true;
            Animation.StopAnimate();            

            Trace.TraceOperationStop("Success ({0} ms @ {1})", 
                Convert.ToInt64(_gitCommand.LastExecutionDuration.TotalMilliseconds), DateTime.Now);                               
        }

        private void _gitCommand_CommandFailed(object sender, ErrorEventArgs e)
        {
            ProgressBar.Value = 100;
            Abort.Enabled = false;
            Close.Enabled = true;
            Animation.StopAnimate();            

            if (e.Error.InnerException != null)
            {
                Trace.TraceInformation("fatal: --{0} ({1})", e.Error.Message, e.Error.InnerException.Message);
            }
            else
            {
                Trace.TraceInformation("fatal: --{0}", e.Error.Message);
            }

            Trace.TraceError("Failed ({0} ms @ {1})",
                Convert.ToInt64(_gitCommand.LastExecutionDuration.TotalMilliseconds), DateTime.Now);
        }

        private void _gitCommand_CommandProgress(object sender, ProgressEventArgs e)
        {
            ProgressInfo.Text = e.Info;

            if (e.Progress < ProgressBar.Minimum)
            {
                ProgressBar.Value = ProgressBar.Minimum;
                return;
            }

            if(e.Progress > ProgressBar.Maximum)
            {
                ProgressBar.Value = ProgressBar.Maximum;
                return;
            }

            ProgressBar.Value = e.Progress;            
        }       

        private void Abort_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            _gitCommand.Abort();
            UseWaitCursor = false;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (_gitCommand.Status == CommandStatus.InProgress);
        }
    }
}
