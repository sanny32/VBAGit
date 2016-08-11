using System;
using System.Linq;
using System.Windows.Forms;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI.Forms
{
    public partial class RevertForm : PersistentForm
    {
        private readonly CommandRevert _gitCommand;

        public RevertForm(CommandRevert gitCommand)
        {
            InitializeComponent();

            _gitCommand = gitCommand;

            ColumnName.Text = VBAGitUI.Name;
            ColumnExtension.Text = VBAGitUI.Extension;
            ColumnStatus.Text = VBAGitUI.Status;

            SelectAll.Text = VBAGitUI.RevertForm_SelectAll;

            Ok.Text = VBAGitUI.OK;
            Cancel.Text = VBAGitUI.Cancel;

            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            
        }

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.RevertForm_Text, _gitCommand.Repository.Info.WorkingDirectory);

            base.ShowDialog();
        }

        private void Ok_Click(object sender, EventArgs e)
        {

        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
