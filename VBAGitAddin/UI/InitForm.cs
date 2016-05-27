using Microsoft.Vbe.Interop;
using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public partial class InitForm : Form
    {
        private readonly UIApp _app;

        public InitForm(UIApp app)
        {
            _app = app;

            InitializeComponent();

            OK.Text = VBAGitUI.OK;
            Cancel.Text = VBAGitUI.Cancel;
            MakeBare.Text = VBAGitUI.InitForm_MakeBare;
            LabelInfo.Text = VBAGitUI.InitForm_LabelInfo;
        }                    

        public DialogResult ShowDialog(string initPath)
        {
            Text = string.Format(VBAGitUI.InitForm_Text, initPath);
            return ShowDialog();
        }

        private void OK_Click(object sender, EventArgs e)
        {            
            try
            {
                _app.CreateNewRepo(this.MakeBare.Checked);
            }
            catch(Exception ex)
            {
                new ExceptionMessageBox(ex).Show();
            }
        }
    }
}
