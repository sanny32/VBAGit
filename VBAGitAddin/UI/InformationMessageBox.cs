using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public class InformationMessageBox
    {      
        public static void Show(string msg)
        {
            MessageBox.Show(msg, VBAGitUI.VBAGitCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
