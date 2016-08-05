using LibGit2Sharp;
using System.Linq;
using System.Windows.Forms;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI.Forms
{
    public partial class CreateBranchForm : PersistentForm
    {
        private readonly CommandCreateBranch _gitCommand;

        public CreateBranchForm(CommandCreateBranch gitCommand)
        {
            InitializeComponent();

            _gitCommand = gitCommand;

            GroupName.Text = VBAGitUI.CreateBranchForm_Name;
            LabelBranch.Text = VBAGitUI.CreateBranchForm_Branch;

            GroupBaseOn.Text = VBAGitUI.CreateBranchForm_BaseOn;
            BaseOnHead.Text = string.Format(VBAGitUI.CreateBranchForm_Head, _gitCommand.CurrentBranch);
            BaseOnBranch.Text = VBAGitUI.CreateBranchForm_Branch;
            BaseOnTag.Text = VBAGitUI.CreateBranchForm_Tag;
            BaseOnCommit.Text = VBAGitUI.CreateBranchForm_Commit;

            GroupOptions.Text = VBAGitUI.CreateBranchForm_Options;
            TrackOption.Text = VBAGitUI.CreateBranchForm_Track;
            ForceOption.Text = VBAGitUI.CreateBranchForm_Force;
            SwitchOption.Text = VBAGitUI.CreateBranchForm_Switch;

            GroupDescription.Text = VBAGitUI.CreateBranchForm_Description;

            Ok.Text = VBAGitUI.OK;
            Cancel.Text = VBAGitUI.Cancel;

            // populate combobox with branches
            Branches.Items.AddRange(_gitCommand.Provider.Branches.Select(b => b.FriendlyName).ToArray());

            // populate combobox with tags
            Tags.Items.AddRange(_gitCommand.Repository.Tags.Select(t => t.FriendlyName).ToArray());

            Application.Idle += Application_Idle;
        }
   
        private void Application_Idle(object sender, System.EventArgs e)
        {
            Branches.Enabled = BaseOnBranch.Checked;
            SelectBranch.Enabled = BaseOnBranch.Checked;

            Tags.Enabled = BaseOnTag.Checked;

            Commits.Enabled = BaseOnCommit.Checked;
            SelectCommit.Enabled = BaseOnCommit.Checked;
        }

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.CreateBranchForm_Text, _gitCommand.Repository.Info.WorkingDirectory);

            base.ShowDialog();
        }      

        private void BranchName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Reference.IsValidName("refs/heads/" + BranchName.Text))
            {
                ErrorProvider.SetError(BranchName, VBAGitUI.Git_InvalidBranchName);
                return;
            }

            if (_gitCommand.Repository.Branches[BranchName.Text] != null)
            {
                ErrorProvider.SetError(BranchName, VBAGitUI.Git_BranchExists);
                return;
            }

            ErrorProvider.SetError(BranchName, "");
        }

        private void BranchName_TextChanged(object sender, System.EventArgs e)
        {
            if (((TextBox)sender).ContainsFocus)
            {
                ErrorProvider.SetError(BranchName, "");
            }
        }

        private void Ok_Click(object sender, System.EventArgs e)
        {
            this.ValidateChildren();

            if (ErrorProvider.GetError(BranchName) != "")
            {
                return;
            }
        }        
    }
}
