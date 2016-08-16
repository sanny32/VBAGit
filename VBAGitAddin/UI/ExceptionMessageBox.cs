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

        public static void Show(IWin32Window owner, Exception ex)
        {
            MessageBox.Show(owner, ex.Message, VBAGitUI.VBAGitCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Show(IntPtr handle, Exception ex)
        {
            NativeWindow window = new NativeWindow();
            window.AssignHandle(handle);

            MessageBox.Show(window, ex.Message, VBAGitUI.VBAGitCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            window.ReleaseHandle();
        }
    }
}
