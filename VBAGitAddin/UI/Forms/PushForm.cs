using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VBAGitAddin.UI.Commands;

namespace VBAGitAddin.UI.Forms
{
    public partial class PushForm : PersistentForm
    {
        private readonly CommandPush _gitCommand;

        public PushForm(CommandPush gitCommand)
        {
            InitializeComponent();

            _gitCommand = gitCommand;

            GroupRef.Text = VBAGitUI.PushForm_GroupRef;
            PushAllBranches.Text = VBAGitUI.PushForm_PushAllBranches;
            LabelLocal.Text = VBAGitUI.PushForm_LabelLocal;
            LabelRemote.Text = VBAGitUI.PushForm_LabelRemote;

            GroupDestination.Text = VBAGitUI.PushForm_GroupDestinantion;
            DestinationRemote.Text = VBAGitUI.PushForm_DestinationRemote;
            DestinationUrl.Text = VBAGitUI.PushForm_DestinationUrl;
            Manage.Text = VBAGitUI.PushForm_Manage;

            GroupOptions.Text = VBAGitUI.PushForm_GroupOptions;
            LabelForce.Text = VBAGitUI.PushForm_LableForce;
            OptionIncludeTags.Text = VBAGitUI.PushForm_OptionIncludeTags;
            OptionKnownChanges.Text = VBAGitUI.PushForm_OptionKnownChanges;
            OptionUnknownChanges.Text = VBAGitUI.PushForm_OptionUnknownChanges;
            OptionSetUpstream.Text = VBAGitUI.PushForm_OptionSetUpstream;

            Ok.Text = VBAGitUI.OK;
            Cancel.Text = VBAGitUI.Cancel;

            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            UpdateOptionsState();
        }

        private void UpdateOptionsState()
        {
            OptionKnownChanges.Enabled = !OptionUnknownChanges.Checked;
            OptionUnknownChanges.Enabled = !OptionKnownChanges.Checked;
        }
    }
}
