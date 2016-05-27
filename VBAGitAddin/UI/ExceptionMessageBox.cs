using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public class ExceptionMessageBox
    {
        private readonly Exception _ex;
        public ExceptionMessageBox(Exception ex)
        {
            _ex = ex;
        }

        public void Show()
        {
            MessageBox.Show(_ex.Message, VBAGitUI.VBAGitCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
