using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VBAGitAddin.Git.Extensions;

namespace VBAGitAddin.UI.Forms
{
    public partial class BrowseReferencesForm : PersistentForm
    {
        private readonly IRepository _repo;
        private readonly TreeNode _refsNode;
        private TreeNode _headsNode;
        private TreeNode _tagsNode;
        private TreeNode _notesNode;
        private TreeNode _remotesNode;
        private List<ListViewItem> _items;
        
        public BrowseReferencesForm(IRepository repo)
        {
            InitializeComponent();

            _repo = repo;
            _items = new List<ListViewItem>();

            LabelFilter.Text = VBAGitUI.BrowseReferencesForm_Filter;
            NestedRefs.Text = VBAGitUI.BrowseReferencesForm_ShowNestedRefs;
            CurrentBranch.Text = VBAGitUI.BrowseReferencesForm_CurrentBranch;
            Filter.Text = VBAGitUI.BrowseReferencesForm_FilterTooltip;

            _refsNode = RefsTree.Nodes[0];                     
        }  

        public object SelectedReference
        {
            get;
            private set;           
        }
            

        public DialogResult ShowHeads([Optional]Branch branch)
        {
            if (branch!= null && !branch.IsRemote)
            {
                SelectedReference = branch;
            }
            else
            {
                SelectedReference = null;
            }

            Clear();
            AddHeads();
            _refsNode.ExpandAll();

            if (_headsNode != null)
            {
                RefsTree.SelectedNode = _headsNode;
            }
            else
            {
                RefsTree.SelectedNode = _refsNode;
            }

            return ShowDialog();
        }

        public DialogResult ShowRemotes([Optional]Branch branch)
        {
            if (branch != null && branch.IsRemote)
            {
                SelectedReference = branch;
            }
            else
            {
                SelectedReference = null;
            }

            Clear();
            AddRemotes();
            _refsNode.ExpandAll();

            if (_remotesNode != null)
            {
                RefsTree.SelectedNode = _remotesNode;
            }
            else
            {
                RefsTree.SelectedNode = _refsNode;
            }

            return ShowDialog();
        }

        public DialogResult ShowTags([Optional]Tag tag)
        {
            SelectedReference = tag;

            Clear();
            AddTags();
            _refsNode.ExpandAll();

            if (_tagsNode != null)
            {
                RefsTree.SelectedNode = _tagsNode;
            }
            else
            {
                RefsTree.SelectedNode = _refsNode;
            }

            return ShowDialog();
        }

        public DialogResult ShowRefs([Optional]object reference)
        {
            SelectedReference = reference;

            Clear();
            AddHeads();
            AddTags();
            AddRemotes();
            _refsNode.ExpandAll();

            if (_headsNode != null)
            {
                RefsTree.SelectedNode = _headsNode;
            }
            else if (_tagsNode != null)
            {
                RefsTree.SelectedNode = _tagsNode;
            }
            else if (_remotesNode != null)
            {
                RefsTree.SelectedNode = _remotesNode;
            }

            return ShowDialog();
        }

        private void Clear()
        {
            _refsNode.Nodes.Clear();
            _headsNode = null;
            _remotesNode = null;
            _tagsNode = null;
            _notesNode = null;
        }

        private void AddHeads()
        {
            if (_repo.Head != null)
            {
                var name = "heads";
                _headsNode = _refsNode.Nodes.Add(name, name);                
            }
        }

        private void AddTags()
        {
            if (_repo.Tags.Count() > 0)
            {
                var name = "tags";
                _tagsNode = _refsNode.Nodes.Add(name, name);
            }
        }

        private void AddRemotes()
        {
            if (_repo.Network.Remotes.Count() > 0)
            {
                var name = "remotes";
                _remotesNode = _refsNode.Nodes.Add(name, name);
                _remotesNode.Nodes.AddRange(_repo.Network.Remotes.Select(r =>
                                        {
                                            var node = new TreeNode(r.Name);
                                            node.Tag = r;
                                            return node;

                                        }).ToArray());
            }
        }

        private void BrowseReferencesForm_Shown(object sender, EventArgs e)
        {
            RefList_SetFocus();
        }        

        private void RefList_SetFocus()
        {
            if (SelectedReference is Branch)
            {
                var item = RefsList.Items[(SelectedReference as Branch).FriendlyName];
                if (item != null)
                {
                    item.Focused = true;
                    item.Selected = true;
                    RefsList.Focus();
                }
            }
        }

        private void DisplayRefs()
        {
            _items.Clear();
            RefsList.Clear();            

            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_BranchName, 150);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_DateLastCommit, 120);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastCommit, 200);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastAuthor, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_SHA1, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_Description, 100);
           
            foreach (var r in _repo.Refs)
            {
                if (r.IsLocalBranch && _headsNode != null || 
                    r.IsRemoteTrackingBranch && _remotesNode != null || 
                    r.IsTag && _tagsNode != null ||
                    r.IsNote && _notesNode != null)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Tag = r;
                    lvi.Name = r.CanonicalName;
                    lvi.Text = r.CanonicalName.Replace("refs/", string.Empty);

                    Commit commit = null;
                    string description = string.Empty;

                    if (r.IsTag)
                    {
                        var tag = _repo.Tags[r.CanonicalName];
                        commit = tag?.Target as Commit;
                        description = tag?.Annotation?.Message;
                    }

                    if(r.IsNote)
                    {
                        var note = _repo.Notes[r.CanonicalName];
                    }

                    if (r.IsLocalBranch || r.IsRemoteTrackingBranch)
                    {
                        var branch = _repo.Branches[r.CanonicalName];
                        commit = branch?.Tip;
                        description = _repo.GetBranchDescription(branch?.FriendlyName);
                    }

                    lvi.SubItems.Add(commit?.Author.When.ToString("G"));
                    lvi.SubItems.Add(commit?.MessageShort);
                    lvi.SubItems.Add(commit?.Author.Name);
                    lvi.SubItems.Add(commit?.Sha);
                    lvi.SubItems.Add(description);

                    _items.Add(lvi);
                }
            }

            RefsList.Items.Clear();
            RefsList.Items.AddRange(_items.AsParallel().Where(i => IsItemFiltered(i)).ToArray());
        }

        private void DisplayHeads()
        {
            _items.Clear();
            RefsList.Clear();

            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_BranchName, 150);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_TrackedBranch, 120);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_DateLastCommit, 120);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastCommit, 200);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastAuthor, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_SHA1, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_Description, 100);     
                              

            foreach (var branch in _repo.Branches)
            {
                if (!branch.IsRemote)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = branch;
                    lvi.Name = branch.FriendlyName;
                    lvi.Text = branch.FriendlyName;
                    lvi.SubItems.Add(branch.TrackedBranch?.FriendlyName);
                    lvi.SubItems.Add(branch.Tip?.Author.When.ToString("G"));
                    lvi.SubItems.Add(branch.Tip?.MessageShort);
                    lvi.SubItems.Add(branch.Tip?.Author.Name);
                    lvi.SubItems.Add(branch.Tip?.Sha);
                    lvi.SubItems.Add(_repo.GetBranchDescription(branch.FriendlyName));

                    _items.Add(lvi);
                }
            }

            RefsList.Items.Clear();
            RefsList.Items.AddRange(_items.AsParallel().Where(i => IsItemFiltered(i)).ToArray());
        }

        private void DisplayTags()
        {
            _items.Clear();
            RefsList.Clear();

            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_BranchName, 150);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_DateLastCommit, 120);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastCommit, 200);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastAuthor, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_SHA1, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_Description, 100);

            foreach (var tag in _repo.Tags)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = tag;
                lvi.Name = tag.FriendlyName;
                lvi.Text = tag.FriendlyName;

                var commit = tag.Target as Commit;                
                lvi.SubItems.Add(commit?.Author.When.ToString("G"));
                lvi.SubItems.Add(commit?.MessageShort);
                lvi.SubItems.Add(commit?.Author.Name);
                lvi.SubItems.Add(commit?.Sha);
                lvi.SubItems.Add(tag.Annotation?.Message);

                _items.Add(lvi);
            }

            RefsList.Items.Clear();
            RefsList.Items.AddRange(_items.AsParallel().Where(i => IsItemFiltered(i)).ToArray());
        }

        private void DisplayRemotes([Optional]Remote remote)
        {
            _items.Clear();
            RefsList.Clear();

            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_BranchName, 150);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_TrackedBranch, 120);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_DateLastCommit, 120);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastCommit, 200);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_LastAuthor, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_SHA1, 80);
            RefsList.Columns.Add(VBAGitUI.BrowseReferencesForm_Description, 100);

            var branches = (remote == null) ? _repo.Branches : _repo.Branches.Where(b => b.Remote == remote);
            foreach (var branch in branches)
            {
                if (branch.IsRemote)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = branch;
                    lvi.Name = branch.FriendlyName;
                    lvi.Text = (remote == null) ? branch.FriendlyName : branch.FriendlyName.Replace(remote.Name + "/", string.Empty);
                    lvi.SubItems.Add(branch.TrackedBranch?.FriendlyName);
                    lvi.SubItems.Add(branch.Tip?.Author.When.ToString("G"));
                    lvi.SubItems.Add(branch.Tip?.MessageShort);
                    lvi.SubItems.Add(branch.Tip?.Author.Name);
                    lvi.SubItems.Add(branch.Tip?.Sha);
                    lvi.SubItems.Add(_repo.GetBranchDescription(branch.FriendlyName));

                    _items.Add(lvi);
                }
            }

            RefsList.Items.Clear();
            RefsList.Items.AddRange(_items.AsParallel().Where(i => IsItemFiltered(i)).ToArray());
        }

        private bool IsItemFiltered(ListViewItem lvi)
        {
            if(string.IsNullOrEmpty(Filter.Text) ||
                Filter.Text == VBAGitUI.BrowseReferencesForm_FilterTooltip)
            {
                return true;
            }

            if(lvi.Text.Contains(Filter.Text))
            {
                return true;
            }

            if(lvi.SubItems.Cast<ListViewItem.ListViewSubItem>().
                            AsParallel().Any(i => i.Text.Contains(Filter.Text)))
            {
                return true;
            }

            return false;
        }

        private void RefsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "refs":
                    DisplayRefs();
                    break;

                case "heads":
                    DisplayHeads();
                    break;

                case "tags":
                    DisplayTags();
                    break;

                case "remotes":
                    DisplayRemotes();
                    break;

                default:
                    RefsList.Items.Clear();
                    if (e.Node.Tag is Remote)
                    {
                        var remote = e.Node.Tag as Remote;
                        DisplayRemotes(remote);
                    }
                    break;
            }
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SelectedReference = e.Item.Tag;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CurrentBranch_Click(object sender, EventArgs e)
        {
            SelectedReference = _repo.Head;

            Close();
        }

        private void Filter_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).ContainsFocus)
            {
                RefsList.Items.Clear();
                RefsList.Items.AddRange(_items.AsParallel().Where(i => IsItemFiltered(i)).ToArray());
            }
        }

        private void Filter_Enter(object sender, EventArgs e)
        {
            if (Filter.Text == VBAGitUI.BrowseReferencesForm_FilterTooltip)
            {
                Filter.Text = string.Empty;
                Filter.ForeColor = Color.Black;
            }
        }

        private void Filter_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Filter.Text))
            {
                FilterPicture.Focus();
                Filter.ForeColor = Color.Gray;
                Filter.Text = VBAGitUI.BrowseReferencesForm_FilterTooltip;
            }
        }

        private void Filter_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Filter.Text = string.Empty;
                RefList_SetFocus();

                RefsList.Items.Clear();
                RefsList.Items.AddRange(_items.AsParallel().Where(i => IsItemFiltered(i)).ToArray());
            }
        }
    }
}
