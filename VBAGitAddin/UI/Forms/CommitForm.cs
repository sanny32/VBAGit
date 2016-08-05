using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using VBAGitAddin.Git.Extensions;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.VBEditor.Extensions;
using LibGit2Sharp;


namespace VBAGitAddin.UI.Forms
{
    public partial class CommitForm : PersistentForm
    {
        internal class ListViewItemObject
        {
            public ListViewItemObject()
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

        private readonly CommandCommit _gitCommand;        
        private List<ListViewItem> _items;
        private List<ListViewGroup> _groups;

        public CommitForm(CommandCommit gitCommand)
        {
            InitializeComponent();

            _gitCommand = gitCommand;
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

            Author.Text = _gitCommand.Author;
            CommitBranch.Tag = string.Empty;
            CommitBranch.Text = _gitCommand.CurrentBranch;

            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            UpdateLabelSelectedText();
            UpdateCommitButtonState();
        }

        public new void ShowDialog()
        {
            Text = string.Format(VBAGitUI.CommitForm_Text, _gitCommand.Repository.Info.WorkingDirectory);
            
            UseWaitCursor = true;

            base.ShowDialog();
        }

        private void CommitForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            _backgroundWorker.RunWorkerAsync();
        }

        private void AddItems()
        {
            Action append = delegate ()
            {
                CommitList.Items.Clear();

                if (ShowUnversionedFiles.Checked)
                {
                    _items.ForEach(item => item.Group = (item.Tag as ListViewItemObject)?.Group);
                    CommitList.Items.AddRange(_items.ToArray());                   
                }
                else
                {
                    var versionedItems = _items.Where(item => (item.Tag as ListViewItemObject)?.FileStatus != FileStatus.NewInWorkdir);
                    versionedItems.AsParallel().ForAll(item => item.Group = (item.Tag as ListViewItemObject)?.Group);
                    CommitList.Items.AddRange(versionedItems.ToArray());
                }

                EmptyCommitList.Text = VBAGitUI.CommitForm_EmptyCommitList;
                EmptyCommitList.Visible = (CommitList.Items.Count == 0);

                CheckUnversioned.Enabled = _items.Exists(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.NewInWorkdir);
                CheckVersioned.Enabled = _items.Exists(item => (item.Tag as ListViewItemObject)?.FileStatus != FileStatus.NewInWorkdir);
                CheckAdded.Enabled = _items.Exists(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.NewInIndex);
                CheckDeleted.Enabled = _items.Exists(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.DeletedFromIndex);
                CheckModified.Enabled = _items.Exists(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.ModifiedInWorkdir);
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
            var checkedItems = items.Where(item => item?.Checked == true).Count();
            LabelSelected.Text = string.Format(VBAGitUI.CommitForm_LabelSelected, checkedItems, items.Count());
        }

        private void UpdateCommitButtonState()
        {
            IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
            var checkedItemsCount = items.Where(item => item?.Checked == true).Count();
            Commit.Enabled = !string.IsNullOrEmpty(CommitMessage.Text) && (checkedItemsCount > 0 || MessageOnly.Checked);
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
                items.ToList().ForEach(item => item.Checked = true);
            }
        }

        private void CheckNone_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
            }
        }

        private void CheckUnversioned_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.NewInWorkdir).ToList().ForEach(item => item.Checked = true);
            }
        }

        private void CheckVersioned_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemObject)?.FileStatus != FileStatus.NewInWorkdir).ToList().ForEach(item => item.Checked = true);
            }
        }

        private void CheckAdded_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.NewInIndex).ToList().ForEach(item => item.Checked = true);
            }
        }

        private void CheckDeleted_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.DeletedFromIndex).ToList().ForEach(item => item.Checked = true);
            }
        }

        private void CheckModified_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                items.ToList().ForEach(item => item.Checked = false);
                items.Where(item => (item.Tag as ListViewItemObject)?.FileStatus == FileStatus.ModifiedInWorkdir).ToList().ForEach(item => item.Checked = true);
            }
        }

        private void ShowUnversionedFiles_CheckedChanged(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                AddItems();
            }
        }

        private void AddSignedoffby_Click(object sender, EventArgs e)
        {
            string signedOfBy = string.Format(VBAGitUI.CommitForm_Signedofby, _gitCommand.Author);
            if (!CommitMessage.Text.Contains(signedOfBy))
            {
                CommitMessage.Text += Environment.NewLine + Environment.NewLine + signedOfBy;
            }
        }

        private void SetAuthorDate_CheckedChanged(object sender, EventArgs e)
        {
            AuthorDate.Value = DateTime.Now;
            AuthorTime.Value = DateTime.Now;
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
            CommitBranch.Text = (NewBranch.Checked) ? CommitBranch.Tag.ToString() : _gitCommand.CurrentBranch;
            ErrorProvider.SetError(CommitBranch, "");
        }

        private void CommitBranch_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).ContainsFocus)
            {
                CommitBranch.Tag = CommitBranch.Text;
                ErrorProvider.SetError(CommitBranch, "");
            }
        }

        private void CommitBranch_Validating(object sender, CancelEventArgs e)
        {                        
            if(!Reference.IsValidName("refs/heads/" + CommitBranch.Text))
            {
                ErrorProvider.SetError(CommitBranch, VBAGitUI.Git_InvalidBranchName);
                return;
            }    
            
            if(_gitCommand.Repository.Branches[CommitBranch.Text] !=null)
            {
                ErrorProvider.SetError(CommitBranch, VBAGitUI.Git_BranchExists);
                return;
            }
                 
            ErrorProvider.SetError(CommitBranch, "");
        }      

        private void MessageOnly_CheckedChanged(object sender, EventArgs e)
        {
            CommitList.Enabled = !MessageOnly.Checked;
            EmptyCommitList.BackColor = (MessageOnly.Checked) ? System.Drawing.Color.Transparent : CommitList.BackColor;      
        }

        private void Commit_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();

            if(ErrorProvider.GetError(CommitBranch) != "")
            {
                return;
            }           

            List<string> files = new List<string>();
            if (!MessageOnly.Checked)
            {
                IEnumerable<ListViewItem> items = CommitList.Items.Cast<ListViewItem>();
                var checkedItems = items.ToList().Where(item => item.Checked);
                checkedItems.ToList().ForEach(item => files.AddRange((item.Tag as ListViewItemObject).Files));               
            }

            DateTime when = DateTime.Now;
            if (SetAuthorDate.Checked)
            {
                when = new DateTime(AuthorDate.Value.Year, AuthorDate.Value.Month, AuthorDate.Value.Day,
                                              AuthorTime.Value.Hour, AuthorTime.Value.Minute, AuthorTime.Value.Second);
            }

            string author = _gitCommand.Author;
            if(SetAuthor.Checked)
            {
                author = Author.Text;
            }
        
            _gitCommand.Commit(CommitBranch.Text, CommitMessage.Text, author, when, files);

            if (_gitCommand.Status == CommandStatus.Success)
            {
                Close();
            }
        }
       
        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _gitCommand.FileList;                         
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var stat in e.Result as IList<StatusEntry>)
            {
                if (stat.State == FileStatus.Ignored)
                {
                    continue;
                }

                ListViewItem item = new ListViewItem();
                ListViewItemObject itemTag = new ListViewItemObject();

                var ext = Path.GetExtension(stat.FilePath);
                var componentName = Path.GetFileNameWithoutExtension(stat.FilePath);

                switch (ext)
                {
                    case VBComponentExtensions.DocClassExtension:
                        item.ImageIndex = 3;
                        item.Group = _groups.ElementAt(0);
                        itemTag.Files.Add(stat.FilePath);
                        break;

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
                    item.SubItems.Add(stat.State.AsString());

                    itemTag.Group = item.Group;
                    itemTag.FileStatus = stat.State;
                    item.Tag = itemTag;

                    _items.Add(item);
                 }
            }

            AddItems();
            UseWaitCursor = false;
        }       
    }
}
