using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using VBAGitAddin.Settings;

namespace VBAGitAddin.UI
{
    [ExcludeFromCodeCoverage]
    public partial class SettingsControl : UserControl, ISettingsView
    {
        public SettingsControl()
        {
            InitializeComponent();

            SetText();
        }

        private void SetText()
        {
            GlobalSettingsBox.Text = VBAGitUI.SourceControl_GlobalSettings;
            UserNameLabel.Text = VBAGitUI.SourceControl_UserNameLabel;
            EmailAddressLabel.Text = VBAGitUI.SourceControl_EmailAddressLabel;
            DefaultRepositoryLocationLabel.Text = VBAGitUI.SourceControl_DefaultRepoLocationLabel;
            UpdateGlobalSettingsButton.Text = VBAGitUI.SourceControl_UpdateGlobalSettings;
            CancelGlobalSettingsButton.Text = VBAGitUI.SourceControl_CancelGlobalSettings;

            RepositorySettingsBox.Text = VBAGitUI.SourceControl_RespositorySettings;
            EditIgnoreFileButton.Text = VBAGitUI.SourceControl_IgnoreFile;
            EditAttributeFileButton.Text = VBAGitUI.SourceControl_AttributesFile;
        }

        string ISourceControlUserSettings.UserName
        {
            get { return this.UserNameTextBox.Text; }
            set { this.UserNameTextBox.Text = value; }
        }

        string ISourceControlUserSettings.EmailAddress
        {
            get { return this.EmailAddressTextBox.Text; }
            set { this.EmailAddressTextBox.Text = value; }
        }

        string ISourceControlUserSettings.DefaultRepositoryLocation
        {
            get { return this.DefaultRepositoryLocationTextBox.Text; }
            set { this.DefaultRepositoryLocationTextBox.Text = value; }
        }

        public event EventHandler<EventArgs> BrowseDefaultRepositoryLocation;
        private void BrowseDefaultRepositoryLocationButton_Click(object sender, EventArgs e)
        {
            var handler = BrowseDefaultRepositoryLocation;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<EventArgs> Save;
        private void UpdateGlobalSettingsButton_Click(object sender, EventArgs e)
        {
            var handler = Save;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<EventArgs> Cancel;

        private void CancelGlobalSettingsButton_Click(object sender, EventArgs e)
        {
            var handler = Cancel;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<EventArgs> EditIgnoreFile;
        private void EditIgnoreFileButton_Click(object sender, EventArgs e)
        {
            var handler = EditIgnoreFile;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<EventArgs> EditAttributesFile;
        private void EditAttributeButton_Click(object sender, EventArgs e)
        {
            var handler = EditAttributesFile;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
