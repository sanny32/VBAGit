using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public class InformationMessageBox
    {
        private readonly string _msg;
        public InformationMessageBox(string msg)
        {
            _msg = msg;
        }

        public void Show()
        {
            MessageBox.Show(_msg, VBAGitUI.VBAGitCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
