using System;
using System.Windows.Forms;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.Diagnostics;

namespace VBAGitAddin.UI
{
    public partial class ProgressForm : Form
    {
        private ISourceControlCommand _scCommand;
        private RichTextBoxTraceListener _tracer;

        public ProgressForm(ISourceControlCommand command)
        {
            InitializeComponent();

            _scCommand = command;
            _scCommand.CommandProgress += _scCommand_CommandProgress;
            _scCommand.CommandSuccess += _scCommand_CommandSuccess;
            _scCommand.CommandAborted += _scCommand_CommandAborted;
            _scCommand.CommandFailed += _scCommand_CommandFailed;

            Animation.AnimatedImage = _scCommand.ProgressImage;
            Text = string.Format(VBAGitUI.ProgressForm_Text, _scCommand.Name);
            
            _tracer = new RichTextBoxTraceListener(LogBox);
            Trace.AutoFlush = true;
            Trace.Listeners.Add(_tracer);            
        }

        public new void ShowDialog()
        {
            Close.Enabled = false;

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

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void _scCommand_CommandAborted(object sender, EventArgs e)
        {
            Close();
        }

        private void _scCommand_CommandSuccess(object sender, EventArgs e)
        {
            ProgressBar.Value = 100;
            Abort.Enabled = false;
            Close.Enabled = true;
            Animation.StopAnimate();            

            Trace.TraceOperationStop("Success ({0} ms @ {1})", 
                Convert.ToInt64(_scCommand.LastExecutionDuration.TotalMilliseconds), DateTime.Now);                               
        }

        private void _scCommand_CommandFailed(object sender, ErrorEventArgs e)
        {
            Abort.Enabled = false;
            Close.Enabled = true;
            Animation.StopAnimate();

            if (e.Error.InnerException != null)
            {
                Trace.TraceError("{0} ({1})", e.Error.Message, e.Error.InnerException.Message);
            }
            else
            {
                Trace.TraceError(e.Error.Message);
            }
        }

        private void _scCommand_CommandProgress(object sender, ProgressEventArgs e)
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
            _scCommand.Abort();
            UseWaitCursor = false;
        }
    }
}
