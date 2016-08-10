using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace VBAGitAddin.UI.Forms
{
    public partial class ReloadFileForm : Form
    {
        private DialogResultEx _dialogResult;

        public ReloadFileForm(string filePath)
        {
            InitializeComponent();

            LabelInfo.Text = string.Format(VBAGitUI.ReloadFileForm_Info, filePath);
            Yes.Text = VBAGitUI.Yes;
            YesToAll.Text = VBAGitUI.YesToAll;
            No.Text = VBAGitUI.No;
            NoToAll.Text = VBAGitUI.NoToAll;

            _dialogResult = DialogResultEx.None;
        }

        public new DialogResultEx ShowDialog(IWin32Window owner)
        {            
            base.ShowDialog(owner);
            return _dialogResult;
        }

        private void ReloadFileForm_Shown(object sender, EventArgs e)
        {            
            BringToFront();
            FlashWindow.Flash((Owner != null) ? Owner.Handle : IntPtr.Zero, 1);
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResultEx.Yes;
            Close();
        }

        private void YesToAll_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResultEx.YesToAll;
            Close();
        }

        private void No_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResultEx.No;
            Close();
        }

        private void NoToAll_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResultEx.NoToAll;
            Close();
        }
    }
}
