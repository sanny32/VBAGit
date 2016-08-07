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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseReferencesForm));
            this.RefsTree = new System.Windows.Forms.TreeView();
            this.BranchesView = new System.Windows.Forms.ListView();
            this.NestedRefs = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.LabelFilter = new System.Windows.Forms.Label();
            this.Filter = new System.Windows.Forms.TextBox();
            this.CurrentBranch = new System.Windows.Forms.Button();
            this.FilterPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FilterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // RefsTree
            // 
            this.RefsTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RefsTree.Location = new System.Drawing.Point(14, 37);
            this.RefsTree.Name = "RefsTree";
            this.RefsTree.Size = new System.Drawing.Size(247, 379);
            this.RefsTree.TabIndex = 0;
            // 
            // BranchesView
            // 
            this.BranchesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BranchesView.Location = new System.Drawing.Point(271, 37);
            this.BranchesView.Name = "BranchesView";
            this.BranchesView.Size = new System.Drawing.Size(501, 379);
            this.BranchesView.TabIndex = 1;
            this.BranchesView.UseCompatibleStateImageBehavior = false;
            // 
            // NestedRefs
            // 
            this.NestedRefs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NestedRefs.AutoSize = true;
            this.NestedRefs.Location = new System.Drawing.Point(14, 430);
            this.NestedRefs.Name = "NestedRefs";
            this.NestedRefs.Size = new System.Drawing.Size(115, 19);
            this.NestedRefs.TabIndex = 2;
            this.NestedRefs.Text = "Show nested refs";
            this.NestedRefs.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(685, 422);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(87, 27);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.Location = new System.Drawing.Point(592, 422);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(87, 27);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // LabelFilter
            // 
            this.LabelFilter.AutoSize = true;
            this.LabelFilter.Location = new System.Drawing.Point(268, 15);
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
            this.Filter.Location = new System.Drawing.Point(3, 3);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(426, 16);
            this.Filter.TabIndex = 9;
            this.Filter.Text = "Filter by refname, Subject, Authors, SHA-1";
            this.Filter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CurrentBranch
            // 
            this.CurrentBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentBranch.Location = new System.Drawing.Point(474, 422);
            this.CurrentBranch.Name = "CurrentBranch";
            this.CurrentBranch.Size = new System.Drawing.Size(112, 27);
            this.CurrentBranch.TabIndex = 10;
            this.CurrentBranch.Text = "Current Branch";
            this.CurrentBranch.UseVisualStyleBackColor = true;
            // 
            // FilterPanel
            // 
            this.FilterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterPanel.Controls.Add(this.pictureBox1);
            this.FilterPanel.Controls.Add(this.Filter);
            this.FilterPanel.Location = new System.Drawing.Point(323, 12);
            this.FilterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(449, 21);
            this.FilterPanel.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(430, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // BrowseReferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.FilterPanel);
            this.Controls.Add(this.CurrentBranch);
            this.Controls.Add(this.LabelFilter);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.NestedRefs);
            this.Controls.Add(this.BranchesView);
            this.Controls.Add(this.RefsTree);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "BrowseReferencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browse references - VBAGit";
            this.FilterPanel.ResumeLayout(false);
            this.FilterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView RefsTree;
        private System.Windows.Forms.ListView BranchesView;
        private System.Windows.Forms.CheckBox NestedRefs;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label LabelFilter;
        private System.Windows.Forms.TextBox Filter;
        private System.Windows.Forms.Button CurrentBranch;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}