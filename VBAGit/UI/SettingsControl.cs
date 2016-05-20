using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace VBAGit.UI
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
            GlobalSettingsBox.Text = VBAGit.SourceControl_GlobalSettings;
            UserNameLabel.Text = VBAGit.SourceControl_UserNameLabel;
            EmailAddressLabel.Text = VBAGit.SourceControl_EmailAddressLabel;
            DefaultRepositoryLocationLabel.Text = VBAGit.SourceControl_DefaultRepoLocationLabel;
            UpdateGlobalSettingsButton.Text = VBAGit.SourceControl_UpdateGlobalSettings;
            CancelGlobalSettingsButton.Text = VBAGit.SourceControl_CancelGlobalSettings;

            RepositorySettingsBox.Text = VBAGit.SourceControl_RespositorySettings;
            EditIgnoreFileButton.Text = VBAGit.SourceControl_IgnoreFile;
            EditAttributeFileButton.Text = VBAGit.SourceControl_AttributesFile;
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
