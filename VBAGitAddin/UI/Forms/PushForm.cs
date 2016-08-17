using LibGit2Sharp;
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
        private class ComboBoxItem
        {
            public ComboBoxItem(string name, object tag)
            {
                Name = name;
                Tag = tag;
            }

            public string Name { get; }
            public object Tag { get; }

            public override string ToString()
            {
                return Name;
            }
        }

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

            var branches = _gitCommand.Provider.Branches.Where(b => !b.IsRemote);
            LocalBranches.Items.AddRange(branches.Select(b=> new ComboBoxItem(b.FriendlyName, b)).ToArray());
            LocalBranches.SelectedItem = LocalBranches.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Name == _gitCommand.Repository.Head?.FriendlyName);

            var remotes = _gitCommand.Repository.Network.Remotes;
            Remotes.Items.AddRange(remotes.Select(r => r.Name).ToArray());
            Remotes.SelectedIndex = Remotes.Items.Count > 0 ? 0 : -1;

            Application.Idle += Application_Idle;
        }

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.PushForm_Text, _gitCommand.Repository.Info.WorkingDirectory);

            base.ShowDialog();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            UpdateRefsState();
            UpdateDestinationState();
            UpdateOptionsState();
        }

        private void UpdateOptionsState()
        {
            OptionKnownChanges.Enabled = !OptionUnknownChanges.Checked;
            OptionUnknownChanges.Enabled = !OptionKnownChanges.Checked;
            OptionIncludeTags.Enabled = !OptionKnownChanges.Checked;
        }

        private void UpdateRefsState()
        {
            LocalBranches.Enabled = !PushAllBranches.Checked;
            RemoteBranches.Enabled = !PushAllBranches.Checked;
            SelectLocalBranch.Enabled = !PushAllBranches.Checked;
            SelectRemoteBranch.Enabled = !PushAllBranches.Checked;
        }

        private void UpdateDestinationState()
        {
            Remotes.Enabled = DestinationRemote.Checked;
            Manage.Enabled = DestinationRemote.Checked;
            ArbitraryUrl.Enabled = DestinationUrl.Checked;
        }

        private void SelectLocalBranch_Click(object sender, EventArgs e)
        {
            using (BrowseReferencesForm browsRefsForm = new BrowseReferencesForm(_gitCommand.Repository))
            {
                var branch = _gitCommand.Provider.Branches.FirstOrDefault(b => b.FriendlyName == LocalBranches.Text);
                if (branch != null && browsRefsForm.ShowHeads(branch) == DialogResult.OK)
                {
                    if (browsRefsForm.SelectedReference is Branch)
                    {
                        var selectedBranch = browsRefsForm.SelectedReference as Branch;
                        if (!selectedBranch.IsRemote)
                        {
                            LocalBranches.Text = selectedBranch?.FriendlyName;
                        }
                    }
                }
            }
        }

        private void SelectRemoteBranch_Click(object sender, EventArgs e)
        {
            using (BrowseReferencesForm browsRefsForm = new BrowseReferencesForm(_gitCommand.Repository))
            {
                var branch = _gitCommand.Provider.Branches.FirstOrDefault(b => b.FriendlyName == RemoteBranches.Text);
                if (browsRefsForm.ShowRemotes(branch) == DialogResult.OK)
                {
                    if (browsRefsForm.SelectedReference is Branch)
                    {
                        var selectedBranch = browsRefsForm.SelectedReference as Branch;
                        if (selectedBranch.IsRemote)
                        {
                            RemoteBranches.Text = selectedBranch.FriendlyName;
                        }
                    }
                }
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            List<string> listRefs = new List<string>();

            if(PushAllBranches.Checked)
            {
                var branches = _gitCommand.Repository.Branches.Where(b => !b.IsRemote);
                listRefs.AddRange(branches.Select(b => b.CanonicalName).ToArray());
            }
            else
            {
                var item = LocalBranches.SelectedItem as ComboBoxItem;
                var branch = item?.Tag as Branch;
                listRefs.Add(branch?.CanonicalName);                
            }

            _gitCommand.Push(Remotes.Text, listRefs);

            if(_gitCommand.Status == CommandStatus.Success)
            {
                Close();
            }
        }
    }
}
