using System.Linq;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI
{
    public partial class CreateBranchForm : PersistentForm
    {
        private readonly CommandCreateBranch _scCommand;

        public CreateBranchForm(CommandCreateBranch scCommand)
        {
            InitializeComponent();

            _scCommand = scCommand;

            GroupName.Text = VBAGitUI.CreateBranchForm_Name;
            LabelBranch.Text = VBAGitUI.CreateBranchForm_Branch;

            GroupBaseOn.Text = VBAGitUI.CreateBranchForm_BaseOn;
            BaseOnHead.Text = string.Format(VBAGitUI.CreateBranchForm_Head, _scCommand.CurrentBranch);
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
            Branches.Items.AddRange(_scCommand.Provider.Branches.Select(b => b.FriendlyName).ToArray());

            //Tags.Items.AddRange(_scCommand.Provider.t)
        }
    }
}
