using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public class ExceptionMessageBox
    {        
        public static void Show(Exception ex)
        {
            MessageBox.Show(ex.Message, VBAGitUI.VBAGitCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
