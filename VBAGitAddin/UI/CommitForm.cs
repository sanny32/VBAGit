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
        internal class ListViewItemTag
        {
            public ListViewItemTag()
            {
                Files = new List<string>();
            }

           
            public List<string> Files
            {
                get;
                private set;
            }
          
            public FileStatus FileStatus
            {
                get;
                set;
            }

            public ListViewGroup Group
            {
                get;
                set;
            }
        }

        private readonly CommitCommand _scCommand;
        private readonly VBProject _project;
        private readonly IRepository _repository;

        private List<ListViewItem> _items;
        private List<ListViewGroup> _groups;

        public CommitForm(CommitCommand scCommand)
        {
            InitializeComponent();

            _scCommand = scCommand;
            _project = _scCommand.VBProject;
            _repository = _scCommand.Repository;
            _items = new List<ListViewItem>();
            _groups = new List<ListViewGroup>()
            {
                new ListViewGroup("VBDocuments", "Documents"),
                new ListViewGroup("VBForms", "Forms"),
                new ListViewGroup("VBModules", "Modules"),
                new ListViewGroup("VBClassModules", "Class Modules")
            };
            CommitList.Groups.AddRange(_groups.ToArray());

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
            EmptyCommitList.Text = VBAGitUI.CommitForm_WaitForUpdate;

            Commit.Text = VBAGitUI.CommitForm_Commit;
            Cancel.Text = VBAGitUI.Cancel;
        }

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.CommitForm_Text, _repository.LocalLocation);
            
            UseWaitCursor = true;

            base.ShowDialog();
        }

        private void CommitForm_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            _backgroundWorker.RunWorkerAsync();
        }

        private void AddItems()
        {
            Action append = delegate ()
            {
                CommitList.Items.Clear();

                if (ShowUnversionedFiles.Checked)
                {
                    _items.ForEach(item => item.Group = (item.Tag as ListViewItemTag)?.Group);
                    CommitList.Items.AddRange(_items.ToArray());                   
                }
                else
                {
                    var versionedItems = _items.Where(item => (item.Tag as ListViewItemTag)?.FileStatus != FileStatus.Unversioned);
                    versionedItems.AsParallel().ForAll(item => item.Group = (item.Tag as ListViewItemTag)?.Group);
                    CommitList.Items.AddRange(versionedItems.ToArray());
                }

                EmptyCommitList.Text = VBAGitUI.CommitForm_EmptyCommitList;
                EmptyCommitList.Visible = (CommitList.Items.Count == 0);
                UpdateLabelSelectedText();
            };

            if (CommitList.InvokeRequired)
            {
                CommitList.BeginInvoke(append);
            }
            else
            {
                append();
            }

        }

        private void UpdateLabelSelectedText()
        {
            IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
            var checkedItems = items.Where(item => item.Checked == true).Count();
            LabelSelected.Text = string.Format(VBAGitUI.CommitForm_LabelSelected, checkedItems, items.Count());
        }

        private void CommitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _backgroundWorker.IsBusy;
        }

        private void CommitList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateLabelSelectedText();
        }

        private void CheckAll_Click(object sender, EventArgs e)
        {
            if(!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = true);
                UpdateLabelSelectedText();
            }
        }

        private void CheckNone_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                UpdateLabelSelectedText();
            }
        }

        private void CheckUnversioned_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemTag)?.FileStatus == FileStatus.Unversioned).ToList().ForEach(item => item.Checked = true);
                UpdateLabelSelectedText();
            }
        }

        private void CheckVersioned_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemTag)?.FileStatus != FileStatus.Unversioned).ToList().ForEach(item => item.Checked = true);
                UpdateLabelSelectedText();
            }
        }

        private void CheckAdded_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemTag)?.FileStatus == FileStatus.Added).ToList().ForEach(item => item.Checked = true);
                UpdateLabelSelectedText();
            }
        }

        private void CheckDeleted_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemTag)?.FileStatus == FileStatus.Deleted).ToList().ForEach(item => item.Checked = true);
                UpdateLabelSelectedText();
            }
        }

        private void CheckModified_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemTag)?.FileStatus == FileStatus.Modified).ToList().ForEach(item => item.Checked = true);
                UpdateLabelSelectedText();
            }
        }

        private void ShowUnversionedFiles_CheckedChanged(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                AddItems();
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
            Commit.Enabled = !string.IsNullOrEmpty(CommitMessage.Text) && CommitList.Items.Count > 0;
        }

        private void Commit_Click(object sender, EventArgs e)
        {
            IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
            var checkedItems = items.ToList().Where(item => item.Checked);

            List<string> files = new List<string>();
            foreach(var item in checkedItems)
            {
                var itemTag = item.Tag as ListViewItemTag;
                if (itemTag != null)
                {
                    files.AddRange(itemTag.Files);
                }
            }

            _scCommand.Commit(CommitMessage.Text, files);

            Close();
        }
       
        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _scCommand.FileList;                         
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var stat in e.Result as IList<IFileStatusEntry>)
            {
                if (stat.FileStatus == FileStatus.Ignored)
                {
                    continue;
                }

                ListViewItem item = new ListViewItem();
                ListViewItemTag itemTag = new ListViewItemTag();

                var ext = Path.GetExtension(stat.FilePath);
                var componentName = Path.GetFileNameWithoutExtension(stat.FilePath);

                switch (ext)
                {
                    case VBComponentExtensions.ClassExtesnion:
                        item.ImageIndex = 2;
                        item.Group = _groups.ElementAt(3);
                        itemTag.Files.Add(stat.FilePath);
                        break;

                    case VBComponentExtensions.FormExtension:
                    case VBComponentExtensions.FormBinaryExtension:
                        ext = string.Format("{0}, {1}", VBComponentExtensions.FormExtension, VBComponentExtensions.FormBinaryExtension);
                        item.ImageIndex = 0;
                        item.Group = _groups.ElementAt(1);
                        itemTag.Files.Add(componentName + VBComponentExtensions.FormExtension);
                        itemTag.Files.Add(componentName + VBComponentExtensions.FormBinaryExtension);
                        break;

                    case VBComponentExtensions.StandardExtension:
                        item.ImageIndex = 1;
                        item.Group = _groups.ElementAt(2);
                        itemTag.Files.Add(stat.FilePath);
                        break;
                }

                if (!_items.Exists(_item => _item.Name == componentName))
                {
                    item.Name = componentName;
                    item.Text = componentName;                                
                    item.SubItems.Add(ext);
                    item.SubItems.Add(stat.FileStatus.ToString());

                    itemTag.Group = item.Group;
                    itemTag.FileStatus = stat.FileStatus;
                    item.Tag = itemTag;

                    _items.Add(item);
                 }
            }

            AddItems();
            UseWaitCursor = false;
        }
    }
}
