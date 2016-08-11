using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VBAGitAddin.Git.Extensions;
using VBAGitAddin.UI.Commands;
using VBAGitAddin.VBEditor.Extensions;

namespace VBAGitAddin.UI.Forms
{
    public partial class RevertForm : PersistentForm
    {
        private class ListViewItemObject
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

        private readonly CommandRevert _gitCommand;
        private List<ListViewGroup> _groups;

        public RevertForm(CommandRevert gitCommand)
        {
            InitializeComponent();

            _gitCommand = gitCommand;
            _groups = new List<ListViewGroup>()
            {
                new ListViewGroup("VBDocuments", "Documents"),
                new ListViewGroup("VBForms", "Forms"),
                new ListViewGroup("VBModules", "Modules"),
                new ListViewGroup("VBClassModules", "Class Modules")
            };
            RevertList.Groups.AddRange(_groups.ToArray());
            ColumnName.Text = VBAGitUI.Name;
            ColumnExtension.Text = VBAGitUI.Extension;
            ColumnStatus.Text = VBAGitUI.Status;

            SelectAll.Text = VBAGitUI.RevertForm_SelectAll;

            Ok.Text = VBAGitUI.OK;
            Cancel.Text = VBAGitUI.Cancel;
           
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            UpdateSelectAllCheckState();
        }

        public new void ShowDialog()
        {
            FillRevertList();

            Text = string.Format(VBAGitUI.RevertForm_Text, _gitCommand.Repository.Info.WorkingDirectory);

            base.ShowDialog();
        }

        private void FillRevertList()
        {
            RevertList.Items.Clear();

            _gitCommand.FileList.ToList().ForEach(stat =>
            {
                if (stat.State == FileStatus.NewInIndex ||
                    stat.State == FileStatus.ModifiedInWorkdir)
                {
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

                    item.Name = componentName;
                    item.Text = componentName;
                    item.SubItems.Add(ext);
                    item.SubItems.Add(stat.State.AsString());

                    itemTag.Group = item.Group;
                    itemTag.FileStatus = stat.State;
                    item.Tag = itemTag;

                    RevertList.Items.Add(item);
                }
            });
        }

        private void Ok_Click(object sender, EventArgs e)
        {

        }

        private void UpdateSelectAllCheckState()
        {
            var checkedCount = RevertList.Items.Cast<ListViewItem>().Count(item => item.Checked);
            if (checkedCount == RevertList.Items.Count)
            {
                SelectAll.CheckState = CheckState.Checked;
            }
            else
            {
                if (checkedCount == 0)
                {
                    SelectAll.CheckState = CheckState.Unchecked;
                }
                else
                {
                    SelectAll.CheckState = CheckState.Indeterminate;
                }
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            switch(SelectAll.CheckState)
            {
                case CheckState.Checked:
                case CheckState.Indeterminate:
                    SelectAll.CheckState = CheckState.Unchecked;
                    break;

                case CheckState.Unchecked:
                    SelectAll.CheckState = CheckState.Checked;
                    break;
            }

            RevertList.Items.Cast<ListViewItem>().
                       ToList().ForEach(item => item.Checked = SelectAll.CheckState == CheckState.Checked);
        }
    }
}
