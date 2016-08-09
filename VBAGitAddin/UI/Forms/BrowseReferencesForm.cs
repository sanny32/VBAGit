using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VBAGitAddin.Git.Extensions;

namespace VBAGitAddin.UI.Forms
{
    public partial class BrowseReferencesForm : PersistentForm
    {
        private readonly IRepository _repo;
        private readonly TreeNode _refsNode;
        public BrowseReferencesForm(IRepository repo)
        {
            InitializeComponent();

            _repo = repo;

            LabelFilter.Text = VBAGitUI.BrowseReferencesForm_Filter;
            NestedRefs.Text = VBAGitUI.BrowseReferencesForm_ShowNestedRefs;
            CurrentBranch.Text = VBAGitUI.BrowseReferencesForm_CurrentBranch;
            Filter.Text = VBAGitUI.BrowseReferencesForm_FilterTooltip;

            _refsNode = RefsTree.Nodes[0];

            foreach (var r in _repo.Refs)
            {
                if(r.CanonicalName.StartsWith("refs/heads/"))
                {
                    if (!_refsNode.Nodes.Cast<TreeNode>().
                                        Any(n => n.Text == "heads"))
                    {
                        _refsNode.Nodes.Add("heads");
                    }
                }

                if (r.CanonicalName.StartsWith("refs/tags/"))
                {
                    if (!_refsNode.Nodes.Cast<TreeNode>().
                                       Any(n => n.Text == "tags"))
                    {
                        _refsNode.Nodes.Add("tags");
                    }
                }

                if (r.CanonicalName.StartsWith("refs/remotes/"))
                {
                    if (!_refsNode.Nodes.Cast<TreeNode>().
                                       Any(n => n.Text == "remotes"))
                    {
                        _refsNode.Nodes.Add("remotes");
                    }
                }
            }
           
            _refsNode.ExpandAll();

            ShowRefs();
        }       

        private void ShowRefs()
        {
            ListView.Clear();
            
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_BranchName, 150);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_DateLastCommit, 120);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_LastCommit, 200);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_LastAuthor, 80);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_SHA1, 80);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_Description, 100);

            foreach (var r in _repo.Refs)
            {
                if (r.IsLocalBranch)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = r.CanonicalName;
                    var branch = _repo.Branches[r.CanonicalName];
                    var commit = branch.Tip;
                    lvi.SubItems.Add(commit?.Author.When.ToString("G"));
                    lvi.SubItems.Add(commit?.MessageShort);
                    lvi.SubItems.Add(commit?.Author.Name);
                    lvi.SubItems.Add(commit?.Sha);
                    lvi.SubItems.Add(_repo.GetBranchDescription(branch.FriendlyName));                    

                    ListView.Items.Add(lvi);
                }
            }
        }

        private void ShowHeads()
        {
            ListView.Clear();

            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_BranchName, 150);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_TrackedBranch, 120);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_DateLastCommit, 120);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_LastCommit, 200);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_LastAuthor, 80);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_SHA1, 80);
            ListView.Columns.Add(VBAGitUI.BrowseReferencesForm_Description, 100);                 

            foreach (var r in _repo.Refs)
            {
            }
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

        }      
    }
}
