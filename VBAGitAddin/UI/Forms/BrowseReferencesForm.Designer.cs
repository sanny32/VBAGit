namespace VBAGitAddin.UI.Forms
{
    partial class BrowseReferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("refs");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseReferencesForm));
            this.RefsTree = new System.Windows.Forms.TreeView();
            this.RefsList = new System.Windows.Forms.ListView();
            this.NestedRefs = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.LabelFilter = new System.Windows.Forms.Label();
            this.Filter = new System.Windows.Forms.TextBox();
            this.CurrentBranch = new System.Windows.Forms.Button();
            this.FilterPanel = new System.Windows.Forms.Panel();
            this.FilterPicture = new System.Windows.Forms.PictureBox();
            this.FilterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RefsTree
            // 
            this.RefsTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RefsTree.HideSelection = false;
            this.RefsTree.Location = new System.Drawing.Point(14, 37);
            this.RefsTree.Name = "RefsTree";
            treeNode1.Name = "Node0";
            treeNode1.Text = "refs";
            this.RefsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.RefsTree.ShowPlusMinus = false;
            this.RefsTree.Size = new System.Drawing.Size(205, 329);
            this.RefsTree.TabIndex = 0;
            this.RefsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RefsTree_AfterSelect);
            // 
            // RefsList
            // 
            this.RefsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RefsList.FullRowSelect = true;
            this.RefsList.HideSelection = false;
            this.RefsList.Location = new System.Drawing.Point(225, 37);
            this.RefsList.MultiSelect = false;
            this.RefsList.Name = "RefsList";
            this.RefsList.Size = new System.Drawing.Size(447, 329);
            this.RefsList.TabIndex = 1;
            this.RefsList.UseCompatibleStateImageBehavior = false;
            this.RefsList.View = System.Windows.Forms.View.Details;
            this.RefsList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemSelectionChanged);
            // 
            // NestedRefs
            // 
            this.NestedRefs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NestedRefs.AutoSize = true;
            this.NestedRefs.Location = new System.Drawing.Point(14, 380);
            this.NestedRefs.Name = "NestedRefs";
            this.NestedRefs.Size = new System.Drawing.Size(115, 19);
            this.NestedRefs.TabIndex = 2;
            this.NestedRefs.Text = "Show nested refs";
            this.NestedRefs.UseVisualStyleBackColor = true;
            this.NestedRefs.Visible = false;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(585, 372);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(87, 27);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(492, 372);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(87, 27);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // LabelFilter
            // 
            this.LabelFilter.AutoSize = true;
            this.LabelFilter.Location = new System.Drawing.Point(222, 13);
            this.LabelFilter.Name = "LabelFilter";
            this.LabelFilter.Size = new System.Drawing.Size(39, 15);
            this.LabelFilter.TabIndex = 8;
            this.LabelFilter.Text = "Filter: ";
            // 
            // Filter
            // 
            this.Filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Filter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Filter.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Filter.Location = new System.Drawing.Point(1, 1);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(382, 16);
            this.Filter.TabIndex = 9;
            this.Filter.Text = "Filter by refname, Subject, Authors, SHA-1";
            this.Filter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Filter.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            this.Filter.Enter += new System.EventHandler(this.Filter_Enter);
            this.Filter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Filter_KeyUp);
            this.Filter.Leave += new System.EventHandler(this.Filter_Leave);
            // 
            // CurrentBranch
            // 
            this.CurrentBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentBranch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CurrentBranch.Location = new System.Drawing.Point(374, 372);
            this.CurrentBranch.Name = "CurrentBranch";
            this.CurrentBranch.Size = new System.Drawing.Size(112, 27);
            this.CurrentBranch.TabIndex = 10;
            this.CurrentBranch.Text = "Current Branch";
            this.CurrentBranch.UseVisualStyleBackColor = true;
            this.CurrentBranch.Click += new System.EventHandler(this.CurrentBranch_Click);
            // 
            // FilterPanel
            // 
            this.FilterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilterPanel.Controls.Add(this.FilterPicture);
            this.FilterPanel.Controls.Add(this.Filter);
            this.FilterPanel.Location = new System.Drawing.Point(271, 12);
            this.FilterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(401, 20);
            this.FilterPanel.TabIndex = 11;
            // 
            // FilterPicture
            // 
            this.FilterPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterPicture.BackColor = System.Drawing.SystemColors.Window;
            this.FilterPicture.Image = global::VBAGitAddin.Properties.Resources.search;
            this.FilterPicture.Location = new System.Drawing.Point(383, 1);
            this.FilterPicture.Name = "FilterPicture";
            this.FilterPicture.Size = new System.Drawing.Size(16, 16);
            this.FilterPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FilterPicture.TabIndex = 10;
            this.FilterPicture.TabStop = false;
            // 
            // BrowseReferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.FilterPanel);
            this.Controls.Add(this.CurrentBranch);
            this.Controls.Add(this.LabelFilter);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.NestedRefs);
            this.Controls.Add(this.RefsList);
            this.Controls.Add(this.RefsTree);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "BrowseReferencesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browse references - VBAGit";
            this.Shown += new System.EventHandler(this.BrowseReferencesForm_Shown);
            this.FilterPanel.ResumeLayout(false);
            this.FilterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView RefsTree;
        private System.Windows.Forms.ListView RefsList;
        private System.Windows.Forms.CheckBox NestedRefs;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label LabelFilter;
        private System.Windows.Forms.TextBox Filter;
        private System.Windows.Forms.Button CurrentBranch;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.PictureBox FilterPicture;
    }
}