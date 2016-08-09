using System.Windows.Forms;

namespace VBAGitAddin.UI.Forms
{
    partial class CreateBranchForm
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
            Application.Idle -= Application_Idle;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateBranchForm));
            this.GroupName = new System.Windows.Forms.GroupBox();
            this.BranchName = new System.Windows.Forms.TextBox();
            this.LabelBranch = new System.Windows.Forms.Label();
            this.GroupBaseOn = new System.Windows.Forms.GroupBox();
            this.SelectCommit = new System.Windows.Forms.Button();
            this.SelectBranch = new System.Windows.Forms.Button();
            this.Commits = new System.Windows.Forms.ComboBox();
            this.Tags = new System.Windows.Forms.ComboBox();
            this.Branches = new System.Windows.Forms.ComboBox();
            this.BaseOnCommit = new System.Windows.Forms.RadioButton();
            this.BaseOnTag = new System.Windows.Forms.RadioButton();
            this.BaseOnBranch = new System.Windows.Forms.RadioButton();
            this.BaseOnHead = new System.Windows.Forms.RadioButton();
            this.GroupOptions = new System.Windows.Forms.GroupBox();
            this.SwitchOption = new System.Windows.Forms.CheckBox();
            this.ForceOption = new System.Windows.Forms.CheckBox();
            this.TrackOption = new System.Windows.Forms.CheckBox();
            this.GroupDescription = new System.Windows.Forms.GroupBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.GroupName.SuspendLayout();
            this.GroupBaseOn.SuspendLayout();
            this.GroupOptions.SuspendLayout();
            this.GroupDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupName
            // 
            this.GroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupName.Controls.Add(this.BranchName);
            this.GroupName.Controls.Add(this.LabelBranch);
            this.GroupName.Location = new System.Drawing.Point(12, 12);
            this.GroupName.Name = "GroupName";
            this.GroupName.Size = new System.Drawing.Size(544, 64);
            this.GroupName.TabIndex = 0;
            this.GroupName.TabStop = false;
            this.GroupName.Text = "Name";
            // 
            // BranchName
            // 
            this.BranchName.Location = new System.Drawing.Point(160, 25);
            this.BranchName.Name = "BranchName";
            this.BranchName.Size = new System.Drawing.Size(236, 23);
            this.BranchName.TabIndex = 1;
            this.BranchName.TextChanged += new System.EventHandler(this.BranchName_TextChanged);
            this.BranchName.Validating += new System.ComponentModel.CancelEventHandler(this.BranchName_Validating);
            // 
            // LabelBranch
            // 
            this.LabelBranch.AutoSize = true;
            this.LabelBranch.Location = new System.Drawing.Point(16, 28);
            this.LabelBranch.Name = "LabelBranch";
            this.LabelBranch.Size = new System.Drawing.Size(44, 15);
            this.LabelBranch.TabIndex = 0;
            this.LabelBranch.Text = "Branch";
            // 
            // GroupBaseOn
            // 
            this.GroupBaseOn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBaseOn.Controls.Add(this.SelectCommit);
            this.GroupBaseOn.Controls.Add(this.SelectBranch);
            this.GroupBaseOn.Controls.Add(this.Commits);
            this.GroupBaseOn.Controls.Add(this.Tags);
            this.GroupBaseOn.Controls.Add(this.Branches);
            this.GroupBaseOn.Controls.Add(this.BaseOnCommit);
            this.GroupBaseOn.Controls.Add(this.BaseOnTag);
            this.GroupBaseOn.Controls.Add(this.BaseOnBranch);
            this.GroupBaseOn.Controls.Add(this.BaseOnHead);
            this.GroupBaseOn.Location = new System.Drawing.Point(12, 82);
            this.GroupBaseOn.Name = "GroupBaseOn";
            this.GroupBaseOn.Size = new System.Drawing.Size(544, 133);
            this.GroupBaseOn.TabIndex = 1;
            this.GroupBaseOn.TabStop = false;
            this.GroupBaseOn.Text = "Base On";
            // 
            // SelectCommit
            // 
            this.SelectCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectCommit.Location = new System.Drawing.Point(498, 92);
            this.SelectCommit.Name = "SelectCommit";
            this.SelectCommit.Size = new System.Drawing.Size(26, 25);
            this.SelectCommit.TabIndex = 8;
            this.SelectCommit.Text = "...";
            this.SelectCommit.UseVisualStyleBackColor = true;
            // 
            // SelectBranch
            // 
            this.SelectBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectBranch.Location = new System.Drawing.Point(498, 42);
            this.SelectBranch.Name = "SelectBranch";
            this.SelectBranch.Size = new System.Drawing.Size(26, 25);
            this.SelectBranch.TabIndex = 7;
            this.SelectBranch.Text = "...";
            this.SelectBranch.UseVisualStyleBackColor = true;
            this.SelectBranch.Click += new System.EventHandler(this.SelectBranch_Click);
            // 
            // Commits
            // 
            this.Commits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Commits.FormattingEnabled = true;
            this.Commits.Location = new System.Drawing.Point(160, 93);
            this.Commits.Name = "Commits";
            this.Commits.Size = new System.Drawing.Size(327, 23);
            this.Commits.TabIndex = 6;
            // 
            // Tags
            // 
            this.Tags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tags.FormattingEnabled = true;
            this.Tags.Location = new System.Drawing.Point(160, 68);
            this.Tags.Name = "Tags";
            this.Tags.Size = new System.Drawing.Size(327, 23);
            this.Tags.TabIndex = 5;
            // 
            // Branches
            // 
            this.Branches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Branches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Branches.FormattingEnabled = true;
            this.Branches.Location = new System.Drawing.Point(160, 43);
            this.Branches.Name = "Branches";
            this.Branches.Size = new System.Drawing.Size(327, 23);
            this.Branches.TabIndex = 4;
            // 
            // BaseOnCommit
            // 
            this.BaseOnCommit.AutoSize = true;
            this.BaseOnCommit.Location = new System.Drawing.Point(19, 97);
            this.BaseOnCommit.Name = "BaseOnCommit";
            this.BaseOnCommit.Size = new System.Drawing.Size(69, 19);
            this.BaseOnCommit.TabIndex = 3;
            this.BaseOnCommit.Text = "Commit";
            this.BaseOnCommit.UseVisualStyleBackColor = true;
            this.BaseOnCommit.CheckedChanged += new System.EventHandler(this.BaseOnCommit_CheckedChanged);
            // 
            // BaseOnTag
            // 
            this.BaseOnTag.AutoSize = true;
            this.BaseOnTag.Location = new System.Drawing.Point(19, 72);
            this.BaseOnTag.Name = "BaseOnTag";
            this.BaseOnTag.Size = new System.Drawing.Size(44, 19);
            this.BaseOnTag.TabIndex = 2;
            this.BaseOnTag.Text = "Tag";
            this.BaseOnTag.UseVisualStyleBackColor = true;
            this.BaseOnTag.CheckedChanged += new System.EventHandler(this.BaseOnTag_CheckedChanged);
            // 
            // BaseOnBranch
            // 
            this.BaseOnBranch.AutoSize = true;
            this.BaseOnBranch.Location = new System.Drawing.Point(19, 47);
            this.BaseOnBranch.Name = "BaseOnBranch";
            this.BaseOnBranch.Size = new System.Drawing.Size(62, 19);
            this.BaseOnBranch.TabIndex = 1;
            this.BaseOnBranch.Text = "Branch";
            this.BaseOnBranch.UseVisualStyleBackColor = true;
            this.BaseOnBranch.CheckedChanged += new System.EventHandler(this.BaseOnBranch_CheckedChanged);
            // 
            // BaseOnHead
            // 
            this.BaseOnHead.AutoSize = true;
            this.BaseOnHead.Checked = true;
            this.BaseOnHead.Location = new System.Drawing.Point(19, 22);
            this.BaseOnHead.Name = "BaseOnHead";
            this.BaseOnHead.Size = new System.Drawing.Size(56, 19);
            this.BaseOnHead.TabIndex = 0;
            this.BaseOnHead.TabStop = true;
            this.BaseOnHead.Text = "HEAD";
            this.BaseOnHead.UseVisualStyleBackColor = true;
            this.BaseOnHead.CheckedChanged += new System.EventHandler(this.BaseOnHead_CheckedChanged);
            // 
            // GroupOptions
            // 
            this.GroupOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupOptions.Controls.Add(this.SwitchOption);
            this.GroupOptions.Controls.Add(this.ForceOption);
            this.GroupOptions.Controls.Add(this.TrackOption);
            this.GroupOptions.Location = new System.Drawing.Point(12, 221);
            this.GroupOptions.Name = "GroupOptions";
            this.GroupOptions.Size = new System.Drawing.Size(544, 72);
            this.GroupOptions.TabIndex = 2;
            this.GroupOptions.TabStop = false;
            this.GroupOptions.Text = "Options";
            // 
            // SwitchOption
            // 
            this.SwitchOption.AutoSize = true;
            this.SwitchOption.Location = new System.Drawing.Point(296, 31);
            this.SwitchOption.Name = "SwitchOption";
            this.SwitchOption.Size = new System.Drawing.Size(140, 19);
            this.SwitchOption.TabIndex = 2;
            this.SwitchOption.Text = "Switch to new branch";
            this.SwitchOption.UseVisualStyleBackColor = true;
            this.SwitchOption.CheckedChanged += new System.EventHandler(this.SwitchOption_CheckedChanged);
            // 
            // ForceOption
            // 
            this.ForceOption.AutoSize = true;
            this.ForceOption.Location = new System.Drawing.Point(157, 31);
            this.ForceOption.Name = "ForceOption";
            this.ForceOption.Size = new System.Drawing.Size(55, 19);
            this.ForceOption.TabIndex = 1;
            this.ForceOption.Text = "Force";
            this.ForceOption.UseVisualStyleBackColor = true;
            this.ForceOption.CheckedChanged += new System.EventHandler(this.ForceOption_CheckedChanged);
            // 
            // TrackOption
            // 
            this.TrackOption.AutoSize = true;
            this.TrackOption.Checked = true;
            this.TrackOption.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.TrackOption.Enabled = false;
            this.TrackOption.Location = new System.Drawing.Point(19, 31);
            this.TrackOption.Name = "TrackOption";
            this.TrackOption.Size = new System.Drawing.Size(54, 19);
            this.TrackOption.TabIndex = 0;
            this.TrackOption.Text = "Track";
            this.TrackOption.ThreeState = true;
            this.TrackOption.UseVisualStyleBackColor = true;
            this.TrackOption.CheckedChanged += new System.EventHandler(this.TrackOption_CheckedChanged);
            // 
            // GroupDescription
            // 
            this.GroupDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupDescription.Controls.Add(this.Description);
            this.GroupDescription.Location = new System.Drawing.Point(12, 299);
            this.GroupDescription.Name = "GroupDescription";
            this.GroupDescription.Size = new System.Drawing.Size(544, 125);
            this.GroupDescription.TabIndex = 3;
            this.GroupDescription.TabStop = false;
            this.GroupDescription.Text = "Description";
            // 
            // Description
            // 
            this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Description.Location = new System.Drawing.Point(19, 25);
            this.Description.Multiline = true;
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(506, 83);
            this.Description.TabIndex = 0;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(469, 430);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(87, 27);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.Location = new System.Drawing.Point(376, 430);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(87, 27);
            this.Ok.TabIndex = 4;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // CreateBranchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 469);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.GroupDescription);
            this.Controls.Add(this.GroupOptions);
            this.Controls.Add(this.GroupBaseOn);
            this.Controls.Add(this.GroupName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(584, 508);
            this.Name = "CreateBranchForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Branch - VBAGit";
            this.GroupName.ResumeLayout(false);
            this.GroupName.PerformLayout();
            this.GroupBaseOn.ResumeLayout(false);
            this.GroupBaseOn.PerformLayout();
            this.GroupOptions.ResumeLayout(false);
            this.GroupOptions.PerformLayout();
            this.GroupDescription.ResumeLayout(false);
            this.GroupDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupName;
        private System.Windows.Forms.TextBox BranchName;
        private System.Windows.Forms.Label LabelBranch;
        private System.Windows.Forms.GroupBox GroupBaseOn;
        private System.Windows.Forms.RadioButton BaseOnCommit;
        private System.Windows.Forms.RadioButton BaseOnTag;
        private System.Windows.Forms.RadioButton BaseOnBranch;
        private System.Windows.Forms.RadioButton BaseOnHead;
        private System.Windows.Forms.GroupBox GroupOptions;
        private System.Windows.Forms.CheckBox SwitchOption;
        private System.Windows.Forms.CheckBox ForceOption;
        private System.Windows.Forms.CheckBox TrackOption;
        private System.Windows.Forms.GroupBox GroupDescription;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button SelectCommit;
        private System.Windows.Forms.Button SelectBranch;
        private System.Windows.Forms.ComboBox Commits;
        private System.Windows.Forms.ComboBox Tags;
        private System.Windows.Forms.ComboBox Branches;
        private ErrorProvider ErrorProvider;
    }
}