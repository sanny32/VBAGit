using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VBAGitAddin.Git.Extensions;

namespace VBAGitAddin.UI.Forms
{
    public partial class BrowseReferencesForm : PersistentForm
    {
        private readonly IRepository _repo;
        private readonly TreeNode _refsNode;
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
            TreeNode headsNode = null;
            TreeNode tagsNode = null;
            TreeNode remotesNode = null;

            foreach (var r in _repo.Refs)
            {
                if(r.CanonicalName.StartsWith("refs/heads/"))
                {
                    if (headsNode == null)
                    {
                        headsNode = _refsNode.Nodes.Add("heads");
                    }
                }

                if (r.CanonicalName.StartsWith("refs/tags/"))
                {
                    if (tagsNode == null)
                    {
                        tagsNode =_refsNode.Nodes.Add("tags");
                    }
                }

                if (r.CanonicalName.StartsWith("refs/remotes/"))
                {
                    if (remotesNode == null)
                    {
                        remotesNode = _refsNode.Nodes.Add("remotes");
                    }
                }
            }
           
            _refsNode.ExpandAll();            

            if (headsNode != null)
            {
                RefsTree.SelectedNode = headsNode;
            }
            else
            {
                RefsTree.SelectedNode = _refsNode;
            }
        }  

        public object SelectedReference
        {
            get;
            private set;           
        }

        
        public DialogResult ShowDialog(Branch branch)
        {
            SelectedReference = branch;
            return ShowDialog();
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

        private void ShowRefs()
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
                ListViewItem lvi = new ListViewItem();
                if (r.IsLocalBranch)
                {
                    var branch = _repo.Branches[r.CanonicalName];

                    lvi.Tag = branch;
                    lvi.Name = branch.FriendlyName;
                    lvi.Text = r.CanonicalName.Replace("refs/", string.Empty);                  
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

        private void ShowHeads()
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

            foreach (var r in _repo.Refs)
            {
                if (r.IsLocalBranch)
                {
                    var branch = _repo.Branches[r.CanonicalName];

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
            switch (e.Node.Text)
            {
                case "refs":
                    ShowRefs();
                    break;

                case "heads":
                    ShowHeads();
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
