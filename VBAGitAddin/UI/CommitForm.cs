using Microsoft.Vbe.Interop;
using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.SourceControl;
using VBAGitAddin.VBEditor.Extensions;


namespace VBAGitAddin.UI
{
    public partial class CommitForm : Form
    {
        private readonly CommitCommand _scCommand;
        private readonly VBProject _project;
        private readonly IRepository _repository;

        public CommitForm(CommitCommand scCommand)
        {
            InitializeComponent();

            _scCommand = scCommand;
            _project = _scCommand.VBProject;
            _repository = _scCommand.Repository;

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

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.CommitForm_Text, _repository.LocalLocation);
            
            UseWaitCursor = true;

            _backgroundWorker.RunWorkerAsync();

            base.ShowDialog();
        }

        private void CommitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _backgroundWorker.IsBusy;
        }

        private void CheckAll_Click(object sender, EventArgs e)
        {
            if(!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = true);                
            }
        }

        private void CheckNone_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = false);
            }
        }

        private void CheckUnversioned_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = false);
                items.AsParallel().Where(item => item.Tag.Equals(FileStatus.Unversioned)).ForAll(item => item.Checked = true);
            }
        }

        private void CheckVersioned_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = false);
                items.AsParallel().Where(item => !item.Tag.Equals(FileStatus.Unversioned)).ForAll(item => item.Checked = true);
            }
        }

        private void CheckAdded_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = false);
                items.AsParallel().Where(item => item.Tag.Equals(FileStatus.Added)).ForAll(item => item.Checked = true);
            }
        }

        private void CheckDeleted_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = false);
                items.AsParallel().Where(item => item.Tag.Equals(FileStatus.Deleted)).ForAll(item => item.Checked = true);
            }
        }

        private void CheckModified_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.AsParallel().ForAll(item => item.Checked = false);
                items.AsParallel().Where(item => item.Tag.Equals(FileStatus.Modified)).ForAll(item => item.Checked = true);
            }
        }

        private void SetAuthorDate_CheckedChanged(object sender, EventArgs e)
        {
            AuthorDate.Visible = SetAuthorDate.Checked;
            AuthorTime.Visible = SetAuthorDate.Checked;
        }

        private void SetAuthor_CheckedChanged(object sender, EventArgs e)
        {
            Author.Visible = SetAuthor.Checked;
        }

        private void NewBranch_CheckedChanged(object sender, EventArgs e)
        {            
            CommitBranch.ReadOnly = !NewBranch.Checked;
            CommitBranch.BorderStyle = (NewBranch.Checked) ? BorderStyle.Fixed3D : BorderStyle.None;
            CommitBranch.Top += (NewBranch.Checked) ? -3: 3;
            CommitBranch.Left += (NewBranch.Checked) ? -3 : 3;
        }

        private void CommitMessage_TextChanged(object sender, EventArgs e)
        {
            Commit.Enabled = !string.IsNullOrEmpty(CommitMessage.Text);
        }

        private void Commit_Click(object sender, EventArgs e)
        {
            _scCommand.Commit(CommitMessage.Text);
        }
       
        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {                       
            var fileStats = _scCommand.FileList;
            foreach (var stat in fileStats)
            {
                if(stat.FileStatus == FileStatus.Ignored)
                {
                    continue;
                }

                ListViewItem item = new ListViewItem();

                var ext = Path.GetExtension(stat.FilePath);
                var componentName =  Path.GetFileNameWithoutExtension(stat.FilePath);
                var component = _scCommand.VBProject.VBComponents.Find(componentName);

                switch(ext)
                {
                    case VBComponentExtensions.ClassExtesnion:
                        item.ImageIndex = 2;
                        item.Group = CommitList.Groups["VBClassModules"];
                        break;

                    case VBComponentExtensions.FormExtension:
                    case VBComponentExtensions.FormBinaryExtension:
                        ext = string.Format("{0}, {1}", VBComponentExtensions.FormExtension, VBComponentExtensions.FormBinaryExtension);
                        item.ImageIndex = 0;
                        item.Group = CommitList.Groups["VBForms"];
                        break;

                    case VBComponentExtensions.StandardExtension:
                        item.ImageIndex = 1;
                        item.Group = CommitList.Groups["VBModules"];
                        break;
                }

                if (!CommitList.Items.ContainsKey(componentName))
                {
                    item.Name = componentName;
                    item.Text = componentName;
                    item.Tag = stat.FileStatus;
                    item.SubItems.Add(ext);
                    item.SubItems.Add(stat.FileStatus.ToString());

                    Action append = delegate ()
                    {
                        CommitList.Items.Add(item);
                    };

                    if (CommitList.InvokeRequired)
                    {
                        var result = CommitList.BeginInvoke(append);
                        CommitList.EndInvoke(result);
                    }
                    else
                    {
                        append();
                    }
                }                
            }
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UseWaitCursor = false;
        }       
    }
}
