using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI.Forms
{
    public partial class ReloadFileForm : Form
    {
        public ReloadFileForm(string filePath)
        {
            InitializeComponent();

            LabelInfo.Text = string.Format(VBAGitUI.ReloadFileForm_Info, filePath);
        }

        private void ReloadFileForm_Shown(object sender, EventArgs e)
        {
            BringToFront();

        }
    }
}
