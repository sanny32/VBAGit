﻿using System.ComponentModel;
using System.Windows.Forms;

namespace VBAGit.UI
{
    partial class BranchesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BranchesPanel = new System.Windows.Forms.Panel();
            this.DeleteBranchButton = new System.Windows.Forms.Button();
            this.PublishedBranchesBox = new System.Windows.Forms.GroupBox();
            this.PublishedBranchesList = new System.Windows.Forms.ListBox();
            this.MergeBranchButton = new System.Windows.Forms.Button();
            this.UnpublishedBranchesBox = new System.Windows.Forms.GroupBox();
            this.UnpublishedBranchesList = new System.Windows.Forms.ListBox();
            this.CurrentBranchSelector = new System.Windows.Forms.ComboBox();
            this.CurrentBranch = new System.Windows.Forms.Label();
            this.NewBranchButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BranchesPanel.SuspendLayout();
            this.PublishedBranchesBox.SuspendLayout();
            this.UnpublishedBranchesBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BranchesPanel
            // 
            this.BranchesPanel.AutoScroll = true;
            this.BranchesPanel.Controls.Add(this.flowLayoutPanel1);
            this.BranchesPanel.Controls.Add(this.PublishedBranchesBox);
            this.BranchesPanel.Controls.Add(this.UnpublishedBranchesBox);
            this.BranchesPanel.Controls.Add(this.CurrentBranchSelector);
            this.BranchesPanel.Controls.Add(this.CurrentBranch);
            this.BranchesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BranchesPanel.Location = new System.Drawing.Point(0, 0);
            this.BranchesPanel.Name = "BranchesPanel";
            this.BranchesPanel.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.BranchesPanel.Size = new System.Drawing.Size(362, 459);
            this.BranchesPanel.TabIndex = 1;
            // 
            // DeleteBranchButton
            // 
            this.DeleteBranchButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DeleteBranchButton.AutoSize = true;
            this.DeleteBranchButton.Image = global::VBAGitUI.Properties.Resources.cross_script;
            this.DeleteBranchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteBranchButton.Location = new System.Drawing.Point(195, 3);
            this.DeleteBranchButton.Name = "DeleteBranchButton";
            this.DeleteBranchButton.Size = new System.Drawing.Size(101, 23);
            this.DeleteBranchButton.TabIndex = 17;
            this.DeleteBranchButton.Text = "Delete Branch";
            this.DeleteBranchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteBranchButton.UseVisualStyleBackColor = true;
            this.DeleteBranchButton.Click += new System.EventHandler(this.OnDeleteBranch);
            // 
            // PublishedBranchesBox
            // 
            this.PublishedBranchesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PublishedBranchesBox.Controls.Add(this.PublishedBranchesList);
            this.PublishedBranchesBox.Location = new System.Drawing.Point(9, 76);
            this.PublishedBranchesBox.Name = "PublishedBranchesBox";
            this.PublishedBranchesBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.PublishedBranchesBox.Size = new System.Drawing.Size(347, 141);
            this.PublishedBranchesBox.TabIndex = 15;
            this.PublishedBranchesBox.TabStop = false;
            this.PublishedBranchesBox.Text = "Published Branches";
            // 
            // PublishedBranchesList
            // 
            this.PublishedBranchesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PublishedBranchesList.FormattingEnabled = true;
            this.PublishedBranchesList.Location = new System.Drawing.Point(6, 19);
            this.PublishedBranchesList.Name = "PublishedBranchesList";
            this.PublishedBranchesList.Size = new System.Drawing.Size(335, 116);
            this.PublishedBranchesList.TabIndex = 1;
            // 
            // MergeBranchButton
            // 
            this.MergeBranchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MergeBranchButton.AutoSize = true;
            this.MergeBranchButton.Image = global::VBAGitUI.Properties.Resources.arrow_merge_090;
            this.MergeBranchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MergeBranchButton.Location = new System.Drawing.Point(101, 3);
            this.MergeBranchButton.Name = "MergeBranchButton";
            this.MergeBranchButton.Size = new System.Drawing.Size(88, 23);
            this.MergeBranchButton.TabIndex = 14;
            this.MergeBranchButton.Text = "Merge";
            this.MergeBranchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.MergeBranchButton.UseVisualStyleBackColor = true;
            this.MergeBranchButton.Click += new System.EventHandler(this.OnMerge);
            // 
            // UnpublishedBranchesBox
            // 
            this.UnpublishedBranchesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnpublishedBranchesBox.Controls.Add(this.UnpublishedBranchesList);
            this.UnpublishedBranchesBox.Location = new System.Drawing.Point(9, 223);
            this.UnpublishedBranchesBox.Name = "UnpublishedBranchesBox";
            this.UnpublishedBranchesBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.UnpublishedBranchesBox.Size = new System.Drawing.Size(347, 141);
            this.UnpublishedBranchesBox.TabIndex = 16;
            this.UnpublishedBranchesBox.TabStop = false;
            this.UnpublishedBranchesBox.Text = "Unpublished Branches";
            // 
            // UnpublishedBranchesList
            // 
            this.UnpublishedBranchesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnpublishedBranchesList.FormattingEnabled = true;
            this.UnpublishedBranchesList.Location = new System.Drawing.Point(6, 19);
            this.UnpublishedBranchesList.Name = "UnpublishedBranchesList";
            this.UnpublishedBranchesList.Size = new System.Drawing.Size(335, 116);
            this.UnpublishedBranchesList.TabIndex = 0;
            // 
            // CurrentBranchSelector
            // 
            this.CurrentBranchSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentBranchSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrentBranchSelector.FormattingEnabled = true;
            this.CurrentBranchSelector.Location = new System.Drawing.Point(56, 11);
            this.CurrentBranchSelector.Name = "CurrentBranchSelector";
            this.CurrentBranchSelector.Size = new System.Drawing.Size(300, 21);
            this.CurrentBranchSelector.TabIndex = 12;
            this.CurrentBranchSelector.SelectedIndexChanged += new System.EventHandler(this.OnSelectedBranchChanged);
            // 
            // CurrentBranch
            // 
            this.CurrentBranch.AutoSize = true;
            this.CurrentBranch.Location = new System.Drawing.Point(6, 14);
            this.CurrentBranch.Name = "CurrentBranch";
            this.CurrentBranch.Size = new System.Drawing.Size(44, 13);
            this.CurrentBranch.TabIndex = 11;
            this.CurrentBranch.Text = "Branch:";
            // 
            // NewBranchButton
            // 
            this.NewBranchButton.AutoSize = true;
            this.NewBranchButton.Image = global::VBAGitUI.Properties.Resources.arrow_branch_090;
            this.NewBranchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NewBranchButton.Location = new System.Drawing.Point(3, 3);
            this.NewBranchButton.Name = "NewBranchButton";
            this.NewBranchButton.Size = new System.Drawing.Size(92, 23);
            this.NewBranchButton.TabIndex = 13;
            this.NewBranchButton.Text = "New Branch";
            this.NewBranchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewBranchButton.UseVisualStyleBackColor = true;
            this.NewBranchButton.Click += new System.EventHandler(this.OnCreateBranch);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.NewBranchButton);
            this.flowLayoutPanel1.Controls.Add(this.MergeBranchButton);
            this.flowLayoutPanel1.Controls.Add(this.DeleteBranchButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 38);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(335, 32);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // BranchesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BranchesPanel);
            this.Name = "BranchesControl";
            this.Size = new System.Drawing.Size(362, 459);
            this.BranchesPanel.ResumeLayout(false);
            this.BranchesPanel.PerformLayout();
            this.PublishedBranchesBox.ResumeLayout(false);
            this.UnpublishedBranchesBox.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel BranchesPanel;
        private GroupBox PublishedBranchesBox;
        private ListBox PublishedBranchesList;
        private Button MergeBranchButton;
        private GroupBox UnpublishedBranchesBox;
        private ListBox UnpublishedBranchesList;
        private ComboBox CurrentBranchSelector;
        private Label CurrentBranch;
        private Button NewBranchButton;
        private Button DeleteBranchButton;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
