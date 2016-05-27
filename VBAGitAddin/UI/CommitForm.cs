using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public partial class CommitForm : Form
    {
        private readonly UIApp _app;

        public CommitForm(UIApp app)
        {
            _app = app;

            InitializeComponent();

            LabelCommit.Text = VBAGitUI.CommitForm_LabelCommit;
            NewBranch.Text = VBAGitUI.CommitForm_NewBranch;
            GroupMessage.Text = VBAGitUI.CommitForm_GroupMessage;
            SetAuthorDate.Text = VBAGitUI.CommitForm_SetAuthorDate;
            SetAuthor.Text = VBAGitUI.CommitForm_SetAuthor;
            AddSignedoffby.Text = VBAGitUI.CommitForm_AddSignedoffby;

            GroupChanges.Text = VBAGitUI.CommitForm_GroupChanges;
            LabelCheck.Text = VBAGitUI.CommitForm_LabelCheck;
            CheckAll.Text = VBAGitUI.CommitForm_CheckAll;
            CheckNone.Text = VBAGitUI.CommitForm_CheckNone;
            CheckUnversioned.Text = VBAGitUI.CommitForm_CheckUnversioned;
            CheckVersioned.Text = VBAGitUI.CommitForm_CheckVersioned;
            CheckAdded.Text = VBAGitUI.CommitForm_CheckAdded;
            CheckDeleted.Text = VBAGitUI.CommitForm_CheckDeleted;
            CheckModified.Text = VBAGitUI.CommitForm_CheckModified;
            ShowUnversionedFiles.Text = VBAGitUI.CommitForm_ShowUnversionedFiles;
            LabelSelected.Text = string.Format(VBAGitUI.CommitForm_LabelSelected, 0, 0);
            MessageOnly.Text = VBAGitUI.CommitForm_MessageOnly;

            Commit.Text = VBAGitUI.CommitForm_Commit;
            Cancel.Text = VBAGitUI.Cancel;
        }

        public DialogResult ShowDialog(string repoPath)
        {
            Text = string.Format(VBAGitUI.CommitForm_Text, repoPath);
            return ShowDialog();
        }

        private void CheckAll_Click(object sender, System.EventArgs e)
        {

        }

        private void CheckNone_Click(object sender, System.EventArgs e)
        {

        }

        private void CheckUnversioned_Click(object sender, System.EventArgs e)
        {

        }

        private void CheckVersioned_Click(object sender, System.EventArgs e)
        {

        }

        private void CheckAdded_Click(object sender, System.EventArgs e)
        {

        }

        private void CheckDeleted_Click(object sender, System.EventArgs e)
        {

        }

        private void CheckModified_Click(object sender, System.EventArgs e)
        {

        }

        private void SetAuthorDate_CheckedChanged(object sender, System.EventArgs e)
        {
            AuthorDate.Visible = SetAuthorDate.Checked;
            AuthorTime.Visible = SetAuthorDate.Checked;
        }

        private void SetAuthor_CheckedChanged(object sender, System.EventArgs e)
        {
            Author.Visible = SetAuthor.Checked;
        }

        private void NewBranch_CheckedChanged(object sender, System.EventArgs e)
        {
            CommitBranch.ReadOnly = !NewBranch.Checked;
        }

        private void CommitMessage_TextChanged(object sender, System.EventArgs e)
        {
            Commit.Enabled = !string.IsNullOrEmpty(CommitMessage.Text);
        }
    }
}
