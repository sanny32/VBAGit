using System;
using System.Windows.Forms;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.Diagnostics;

namespace VBAGitAddin.UI
{
    public partial class ProgressForm : Form
    {
        private ISourceControlCommand _scCommand;

        public ProgressForm(ISourceControlCommand command)
        {
            InitializeComponent();

            _scCommand = command;
            _scCommand.CommandProgress += _scCommand_CommandProgress;
            _scCommand.CommandSuccess += _scCommand_CommandSuccess;
            _scCommand.CommandAborted += _scCommand_CommandAborted;
            _scCommand.CommandFailed += _scCommand_CommandFailed;

            this.Text = string.Format(VBAGitUI.ProgressForm_Text, _scCommand.Name);

            Trace.AutoFlush = true;
            Trace.Listeners.Add(new RichTextBoxTraceListener(LogBox));            
        }

        public new void ShowDialog()
        {
            Close.Enabled = false;

            base.ShowDialog();
        }

        private void SetProgress(int progress)
        {
            ProgressBar.Value = progress;
        }       

        private void _scCommand_CommandAborted(object sender, EventArgs e)
        {
            Close();
        }

        private void _scCommand_CommandSuccess(object sender, EventArgs e)
        {
            Abort.Enabled = false;
            Close.Enabled = true;

            Trace.TraceOperationStop("Success ({0} ms @ {1})", 
                Convert.ToInt64(_scCommand.LastExecutionDuration.TotalMilliseconds), DateTime.Now);                               
        }

        private void _scCommand_CommandFailed(object sender, ErrorEventArgs e)
        {
            Abort.Enabled = false;
            Close.Enabled = true;

            Trace.TraceError(e.Error.Message);
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
