using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace VBAGit.UI
{
    [ExcludeFromCodeCoverage]
    public partial class DeleteBranchForm : Form, IDeleteBranchView
    {
        public DeleteBranchForm()
        {
            InitializeComponent();

            Branches = new List<string>();

            Text = VBAGitUI.SourceControl_DeleteBranchCaption;
            TitleLabel.Text = VBAGitUI.SourceControl_DeleteBranchTitleLable;
            InstructionsLabel.Text = VBAGitUI.SourceControl_DeleteBranchInstructionsLabel;
            OkButton.Text = VBAGitUI.OK_AllCaps;

            OkButton.Click += OkButton_Click;
            CancelButton.Text = VBAGitUI.CancelButtonText;
            CancelButton.Click += CancelButton_Click;
            BranchList.SelectedValueChanged += BranchList_SelectedValueChanged;
        }

        public event EventHandler<BranchDeleteArgs> SelectionChanged;
        private void BranchList_SelectedValueChanged(object sender, EventArgs e)
        {
            var handler = SelectionChanged;
            if (handler != null)
            {
                handler(this, new BranchDeleteArgs(this.BranchList.SelectedItem.ToString()));
            }
        }

        public bool OkButtonEnabled
        {
            get { return this.OkButton.Enabled; }
            set { this.OkButton.Enabled = value; }
        }

        private IList<string> _branches;
        public IList<string> Branches
        {
            get { return _branches; }
            set
            {
                _branches = value;
                BranchList.DataSource = Branches;
                BranchList.Refresh();
           }
        }

        public event EventHandler<BranchDeleteArgs> Confirm;
        private void OkButton_Click(object sender, EventArgs e)
        {
            var handler = Confirm;
            if (handler != null)
            {
                handler(this, new BranchDeleteArgs(this.BranchList.SelectedItem.ToString()));
            }
        }

        public event EventHandler<EventArgs> Cancel;
        private void CancelButton_Click(object sender, EventArgs e)
        {
            var handler = Cancel;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
