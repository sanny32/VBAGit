using System.Linq;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI
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
            
        }

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.CreateBranchForm_Text, _gitCommand.Repository.Info.WorkingDirectory);

            UseWaitCursor = true;

            base.ShowDialog();
        }
    }
}
