﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace VBAGit.UI
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    public partial class SourceControlPanel : UserControl, ISourceControlView
    {
        public SourceControlPanel()
        {
            InitializeComponent();
        }

        public SourceControlPanel(IBranchesView branchesView, IChangesView changesView, IUnsyncedCommitsView commitsView, ISettingsView settingsView, IFailedMessageView failedActionView)
            :this()
        {
            SecondaryPanelVisible = false;

            ((Control)branchesView).Dock = DockStyle.Fill;
            ((Control)changesView).Dock = DockStyle.Fill;
            ((Control)commitsView).Dock = DockStyle.Fill;
            ((Control)settingsView).Dock = DockStyle.Fill;

            ((Control)failedActionView).Dock = DockStyle.Fill;

            this.BranchesTab.Controls.Add((Control)branchesView);
            this.ChangesTab.Controls.Add((Control)changesView);
            this.UnsyncedCommitsTab.Controls.Add((Control)commitsView);
            this.SettingsTab.Controls.Add((Control)settingsView);

            this.MainContainer.Panel1.Controls.Add((Control)failedActionView);

            SetText();
        }

        private void SetText()
        {
            RefreshButton.ToolTipText = VBAGit.SourceControl_RefreshButtonToolTip;
            OpenWorkingFolderButton.ToolTipText = VBAGit.SourceControl_OpenWorkingFolderToolTip;
            InitRepoButton.ToolTipText = VBAGit.SourceControl_InitRepoButtonToolTip;

            ChangesTab.Text = VBAGit.SourceControl_Changes;
            BranchesTab.Text = VBAGit.SourceControl_Branches;
            UnsyncedCommitsTab.Text = VBAGit.SourceControl_UnsyncedCommits;
            SettingsTab.Text = VBAGit.SourceControl_Settings;
        }

        public string ClassId
        {
            get { return "19A32FC9-4902-4385-9FE7-829D4F9C441D"; }
        }

        public string Caption
        {
            get { return VBAGit.SourceControlPanel_Caption; }
        }

        public string Status 
        {
            get { return this.StatusMessage.Text; }
            set { this.StatusMessage.Text = value; }
        }

        public bool SecondaryPanelVisible
        {
            get { return !this.MainContainer.Panel1Collapsed; }
            set { this.MainContainer.Panel1Collapsed = !value; }
        }

        public ISecondarySourceControlPanel SecondaryPanel
        {
            get
            {
                return (ISecondarySourceControlPanel)this.MainContainer.Panel1.Controls[0];
            }

            set
            {
                this.MainContainer.Panel1.Controls.Clear();

                ((Control)value).Dock = DockStyle.Fill;
                this.MainContainer.Panel1.Controls.Add((Control)value);
            }
        }

        public event EventHandler<EventArgs> RefreshData;
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RaiseGenericEvent(RefreshData, e);
        } 

        public event EventHandler<EventArgs> OpenWorkingDirectory;
        private void OpenWorkingFolderButton_Click(object sender, EventArgs e)
        {
            RaiseGenericEvent(OpenWorkingDirectory, e);
        }

        public event EventHandler<EventArgs> InitializeNewRepository;
        private void InitRepoButton_Click(object sender, EventArgs e)
        {
            RaiseGenericEvent(InitializeNewRepository, e);
        }

        private void RaiseGenericEvent(EventHandler<EventArgs> handler, EventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
