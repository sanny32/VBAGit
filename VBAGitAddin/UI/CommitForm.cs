using Microsoft.Vbe.Interop;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public partial class CommitForm : Form
    {
        public CommitForm()
        {
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

        public new DialogResult ShowDialog()
        {
            //Text = string.Format(VBAGitUI.CommitForm_Text, _app.ActiveProjectRepoPath);
            //foreach(VBComponent component in _app.IDE.ActiveVBProject.VBComponents)
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.Name = component.Name;
            //    item.Text = component.Name;
               
            //    switch (component.Type)
            //    {
            //        case vbext_ComponentType.vbext_ct_Document:
            //            item.ImageIndex = 2;
            //            item.Group = CommitList.Groups["VBDocuments"];
            //        break;

            //        case vbext_ComponentType.vbext_ct_MSForm:
            //            item.ImageIndex = 0;
            //            item.Group = CommitList.Groups["VBForms"];
            //        break;

            //        case vbext_ComponentType.vbext_ct_StdModule:
            //            item.ImageIndex = 1;
            //            item.Group = CommitList.Groups["VBModules"];
            //        break;

            //        case vbext_ComponentType.vbext_ct_ClassModule:
            //            item.ImageIndex = 2;
            //            item.Group = CommitList.Groups["VBClassModules"];
            //       break;
            //    }

            //    CommitList.Items.Add(item);           
            //}

            return base.ShowDialog();
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
