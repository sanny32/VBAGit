namespace VBAGitAddin.UI
{
    partial class CommitForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommitForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GroupMessage = new System.Windows.Forms.GroupBox();
            this.AddSignedoffby = new System.Windows.Forms.Button();
            this.Author = new System.Windows.Forms.TextBox();
            this.SetAuthor = new System.Windows.Forms.CheckBox();
            this.AuthorTime = new System.Windows.Forms.DateTimePicker();
            this.AuthorDate = new System.Windows.Forms.DateTimePicker();
            this.SetAuthorDate = new System.Windows.Forms.CheckBox();
            this.CommitMessage = new System.Windows.Forms.TextBox();
            this.NewBranch = new System.Windows.Forms.CheckBox();
            this.CommitBranch = new System.Windows.Forms.TextBox();
            this.LabelCommit = new System.Windows.Forms.Label();
            this.MessageOnly = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Commit = new System.Windows.Forms.Button();
            this.GroupChanges = new System.Windows.Forms.GroupBox();
            this.EmptyCommitList = new System.Windows.Forms.Label();
            this.LabelSelected = new System.Windows.Forms.Label();
            this.ShowUnversionedFiles = new System.Windows.Forms.CheckBox();
            this.CommitList = new System.Windows.Forms.ListView();
            this.ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VBComponentsImageList = new System.Windows.Forms.ImageList(this.components);
            this.CheckModified = new System.Windows.Forms.Label();
            this.CheckDeleted = new System.Windows.Forms.Label();
            this.CheckAdded = new System.Windows.Forms.Label();
            this.CheckVersioned = new System.Windows.Forms.Label();
            this.CheckUnversioned = new System.Windows.Forms.Label();
            this.CheckNone = new System.Windows.Forms.Label();
            this.CheckAll = new System.Windows.Forms.Label();
            this.LabelCheck = new System.Windows.Forms.Label();
            this._backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.GroupMessage.SuspendLayout();
            this.GroupChanges.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GroupMessage);
            this.splitContainer1.Panel1.Controls.Add(this.NewBranch);
            this.splitContainer1.Panel1.Controls.Add(this.CommitBranch);
            this.splitContainer1.Panel1.Controls.Add(this.LabelCommit);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MessageOnly);
            this.splitContainer1.Panel2.Controls.Add(this.Cancel);
            this.splitContainer1.Panel2.Controls.Add(this.Commit);
            this.splitContainer1.Panel2.Controls.Add(this.GroupChanges);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(617, 511);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 5;
            // 
            // GroupMessage
            // 
            this.GroupMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupMessage.Controls.Add(this.AddSignedoffby);
            this.GroupMessage.Controls.Add(this.Author);
            this.GroupMessage.Controls.Add(this.SetAuthor);
            this.GroupMessage.Controls.Add(this.AuthorTime);
            this.GroupMessage.Controls.Add(this.AuthorDate);
            this.GroupMessage.Controls.Add(this.SetAuthorDate);
            this.GroupMessage.Controls.Add(this.CommitMessage);
            this.GroupMessage.Location = new System.Drawing.Point(11, 32);
            this.GroupMessage.Name = "GroupMessage";
            this.GroupMessage.Size = new System.Drawing.Size(597, 165);
            this.GroupMessage.TabIndex = 7;
            this.GroupMessage.TabStop = false;
            this.GroupMessage.Text = "Message: ";
            // 
            // AddSignedoffby
            // 
            this.AddSignedoffby.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddSignedoffby.Location = new System.Drawing.Point(469, 134);
            this.AddSignedoffby.Name = "AddSignedoffby";
            this.AddSignedoffby.Size = new System.Drawing.Size(121, 23);
            this.AddSignedoffby.TabIndex = 6;
            this.AddSignedoffby.Text = "Add Signed-off-by";
            this.AddSignedoffby.UseVisualStyleBackColor = true;
            // 
            // Author
            // 
            this.Author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Author.Location = new System.Drawing.Point(152, 136);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(284, 20);
            this.Author.TabIndex = 5;
            this.Author.Visible = false;
            // 
            // SetAuthor
            // 
            this.SetAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetAuthor.AutoSize = true;
            this.SetAuthor.Location = new System.Drawing.Point(6, 138);
            this.SetAuthor.Name = "SetAuthor";
            this.SetAuthor.Size = new System.Drawing.Size(75, 17);
            this.SetAuthor.TabIndex = 4;
            this.SetAuthor.Text = "Set author";
            this.SetAuthor.UseVisualStyleBackColor = true;
            this.SetAuthor.CheckedChanged += new System.EventHandler(this.SetAuthor_CheckedChanged);
            // 
            // AuthorTime
            // 
            this.AuthorTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AuthorTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.AuthorTime.Location = new System.Drawing.Point(254, 110);
            this.AuthorTime.Name = "AuthorTime";
            this.AuthorTime.ShowUpDown = true;
            this.AuthorTime.Size = new System.Drawing.Size(80, 20);
            this.AuthorTime.TabIndex = 3;
            this.AuthorTime.Visible = false;
            // 
            // AuthorDate
            // 
            this.AuthorDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AuthorDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.AuthorDate.Location = new System.Drawing.Point(152, 110);
            this.AuthorDate.Name = "AuthorDate";
            this.AuthorDate.Size = new System.Drawing.Size(96, 20);
            this.AuthorDate.TabIndex = 2;
            this.AuthorDate.Visible = false;
            // 
            // SetAuthorDate
            // 
            this.SetAuthorDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetAuthorDate.AutoSize = true;
            this.SetAuthorDate.Location = new System.Drawing.Point(6, 112);
            this.SetAuthorDate.Name = "SetAuthorDate";
            this.SetAuthorDate.Size = new System.Drawing.Size(99, 17);
            this.SetAuthorDate.TabIndex = 1;
            this.SetAuthorDate.Text = "Set author date";
            this.SetAuthorDate.UseVisualStyleBackColor = true;
            this.SetAuthorDate.CheckedChanged += new System.EventHandler(this.SetAuthorDate_CheckedChanged);
            // 
            // CommitMessage
            // 
            this.CommitMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommitMessage.Location = new System.Drawing.Point(6, 19);
            this.CommitMessage.Multiline = true;
            this.CommitMessage.Name = "CommitMessage";
            this.CommitMessage.Size = new System.Drawing.Size(584, 79);
            this.CommitMessage.TabIndex = 0;
            this.CommitMessage.TextChanged += new System.EventHandler(this.CommitMessage_TextChanged);
            // 
            // NewBranch
            // 
            this.NewBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewBranch.AutoSize = true;
            this.NewBranch.Location = new System.Drawing.Point(358, 9);
            this.NewBranch.Name = "NewBranch";
            this.NewBranch.Size = new System.Drawing.Size(82, 17);
            this.NewBranch.TabIndex = 6;
            this.NewBranch.Text = "new branch";
            this.NewBranch.UseVisualStyleBackColor = true;
            this.NewBranch.CheckedChanged += new System.EventHandler(this.NewBranch_CheckedChanged);
            // 
            // CommitBranch
            // 
            this.CommitBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommitBranch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommitBranch.Location = new System.Drawing.Point(70, 10);
            this.CommitBranch.Name = "CommitBranch";
            this.CommitBranch.ReadOnly = true;
            this.CommitBranch.Size = new System.Drawing.Size(282, 13);
            this.CommitBranch.TabIndex = 5;
            this.CommitBranch.TabStop = false;
            this.CommitBranch.Text = "master";
            // 
            // LabelCommit
            // 
            this.LabelCommit.AutoSize = true;
            this.LabelCommit.Location = new System.Drawing.Point(8, 10);
            this.LabelCommit.Name = "LabelCommit";
            this.LabelCommit.Size = new System.Drawing.Size(56, 13);
            this.LabelCommit.TabIndex = 4;
            this.LabelCommit.Text = "Commit to:";
            // 
            // MessageOnly
            // 
            this.MessageOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MessageOnly.AutoSize = true;
            this.MessageOnly.Location = new System.Drawing.Point(17, 277);
            this.MessageOnly.Name = "MessageOnly";
            this.MessageOnly.Size = new System.Drawing.Size(91, 17);
            this.MessageOnly.TabIndex = 3;
            this.MessageOnly.Text = "Message only";
            this.MessageOnly.UseVisualStyleBackColor = true;
            this.MessageOnly.CheckedChanged += new System.EventHandler(this.MessageOnly_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(530, 273);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Commit
            // 
            this.Commit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Commit.Enabled = false;
            this.Commit.Location = new System.Drawing.Point(449, 273);
            this.Commit.Name = "Commit";
            this.Commit.Size = new System.Drawing.Size(75, 23);
            this.Commit.TabIndex = 1;
            this.Commit.Text = "Commit";
            this.Commit.UseVisualStyleBackColor = true;
            this.Commit.Click += new System.EventHandler(this.Commit_Click);
            // 
            // GroupChanges
            // 
            this.GroupChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupChanges.Controls.Add(this.EmptyCommitList);
            this.GroupChanges.Controls.Add(this.LabelSelected);
            this.GroupChanges.Controls.Add(this.ShowUnversionedFiles);
            this.GroupChanges.Controls.Add(this.CommitList);
            this.GroupChanges.Controls.Add(this.CheckModified);
            this.GroupChanges.Controls.Add(this.CheckDeleted);
            this.GroupChanges.Controls.Add(this.CheckAdded);
            this.GroupChanges.Controls.Add(this.CheckVersioned);
            this.GroupChanges.Controls.Add(this.CheckUnversioned);
            this.GroupChanges.Controls.Add(this.CheckNone);
            this.GroupChanges.Controls.Add(this.CheckAll);
            this.GroupChanges.Controls.Add(this.LabelCheck);
            this.GroupChanges.Location = new System.Drawing.Point(11, 3);
            this.GroupChanges.Name = "GroupChanges";
            this.GroupChanges.Size = new System.Drawing.Size(597, 264);
            this.GroupChanges.TabIndex = 0;
            this.GroupChanges.TabStop = false;
            this.GroupChanges.Text = "Changes made (double-click on file for diff): ";
            // 
            // EmptyCommitList
            // 
            this.EmptyCommitList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.EmptyCommitList.BackColor = System.Drawing.SystemColors.Window;
            this.EmptyCommitList.Location = new System.Drawing.Point(200, 76);
            this.EmptyCommitList.Name = "EmptyCommitList";
            this.EmptyCommitList.Size = new System.Drawing.Size(197, 33);
            this.EmptyCommitList.TabIndex = 12;
            this.EmptyCommitList.Text = "Please wait while updating files status...";
            this.EmptyCommitList.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LabelSelected
            // 
            this.LabelSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSelected.Location = new System.Drawing.Point(375, 233);
            this.LabelSelected.Name = "LabelSelected";
            this.LabelSelected.Size = new System.Drawing.Size(217, 13);
            this.LabelSelected.TabIndex = 11;
            this.LabelSelected.Text = "0 selected, 0 total";
            this.LabelSelected.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShowUnversionedFiles
            // 
            this.ShowUnversionedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowUnversionedFiles.AutoSize = true;
            this.ShowUnversionedFiles.Checked = true;
            this.ShowUnversionedFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowUnversionedFiles.Location = new System.Drawing.Point(6, 241);
            this.ShowUnversionedFiles.Name = "ShowUnversionedFiles";
            this.ShowUnversionedFiles.Size = new System.Drawing.Size(135, 17);
            this.ShowUnversionedFiles.TabIndex = 10;
            this.ShowUnversionedFiles.Text = "Show Unverioned Files";
            this.ShowUnversionedFiles.UseVisualStyleBackColor = true;
            this.ShowUnversionedFiles.CheckedChanged += new System.EventHandler(this.ShowUnversionedFiles_CheckedChanged);
            // 
            // CommitList
            // 
            this.CommitList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommitList.CheckBoxes = true;
            this.CommitList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnName,
            this.ColumnExtension,
            this.ColumnStatus});
            this.CommitList.FullRowSelect = true;
            this.CommitList.Location = new System.Drawing.Point(6, 40);
            this.CommitList.Name = "CommitList";
            this.CommitList.Size = new System.Drawing.Size(584, 190);
            this.CommitList.SmallImageList = this.VBComponentsImageList;
            this.CommitList.TabIndex = 9;
            this.CommitList.UseCompatibleStateImageBehavior = false;
            this.CommitList.View = System.Windows.Forms.View.Details;
            this.CommitList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.CommitList_ItemChecked);
            // 
            // ColumnName
            // 
            this.ColumnName.Text = "Name";
            this.ColumnName.Width = 332;
            // 
            // ColumnExtension
            // 
            this.ColumnExtension.Text = "Extension";
            this.ColumnExtension.Width = 87;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.Text = "Status";
            this.ColumnStatus.Width = 132;
            // 
            // VBComponentsImageList
            // 
            this.VBComponentsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("VBComponentsImageList.ImageStream")));
            this.VBComponentsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.VBComponentsImageList.Images.SetKeyName(0, "VBForm.png");
            this.VBComponentsImageList.Images.SetKeyName(1, "VBModule.png");
            this.VBComponentsImageList.Images.SetKeyName(2, "VBClassModule.png");
            // 
            // CheckModified
            // 
            this.CheckModified.AutoSize = true;
            this.CheckModified.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckModified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckModified.Location = new System.Drawing.Point(386, 21);
            this.CheckModified.Name = "CheckModified";
            this.CheckModified.Size = new System.Drawing.Size(55, 13);
            this.CheckModified.TabIndex = 7;
            this.CheckModified.Text = "Modified";
            this.CheckModified.Click += new System.EventHandler(this.CheckModified_Click);
            // 
            // CheckDeleted
            // 
            this.CheckDeleted.AutoSize = true;
            this.CheckDeleted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckDeleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckDeleted.Location = new System.Drawing.Point(329, 21);
            this.CheckDeleted.Name = "CheckDeleted";
            this.CheckDeleted.Size = new System.Drawing.Size(51, 13);
            this.CheckDeleted.TabIndex = 6;
            this.CheckDeleted.Text = "Deleted";
            this.CheckDeleted.Click += new System.EventHandler(this.CheckDeleted_Click);
            // 
            // CheckAdded
            // 
            this.CheckAdded.AutoSize = true;
            this.CheckAdded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckAdded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckAdded.Location = new System.Drawing.Point(280, 21);
            this.CheckAdded.Name = "CheckAdded";
            this.CheckAdded.Size = new System.Drawing.Size(43, 13);
            this.CheckAdded.TabIndex = 5;
            this.CheckAdded.Text = "Added";
            this.CheckAdded.Click += new System.EventHandler(this.CheckAdded_Click);
            // 
            // CheckVersioned
            // 
            this.CheckVersioned.AutoSize = true;
            this.CheckVersioned.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckVersioned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckVersioned.Location = new System.Drawing.Point(211, 21);
            this.CheckVersioned.Name = "CheckVersioned";
            this.CheckVersioned.Size = new System.Drawing.Size(63, 13);
            this.CheckVersioned.TabIndex = 4;
            this.CheckVersioned.Text = "Versioned";
            this.CheckVersioned.Click += new System.EventHandler(this.CheckVersioned_Click);
            // 
            // CheckUnversioned
            // 
            this.CheckUnversioned.AutoSize = true;
            this.CheckUnversioned.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckUnversioned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckUnversioned.Location = new System.Drawing.Point(127, 21);
            this.CheckUnversioned.Name = "CheckUnversioned";
            this.CheckUnversioned.Size = new System.Drawing.Size(78, 13);
            this.CheckUnversioned.TabIndex = 3;
            this.CheckUnversioned.Text = "Unversioned";
            this.CheckUnversioned.Click += new System.EventHandler(this.CheckUnversioned_Click);
            // 
            // CheckNone
            // 
            this.CheckNone.AutoSize = true;
            this.CheckNone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckNone.Location = new System.Drawing.Point(84, 21);
            this.CheckNone.Name = "CheckNone";
            this.CheckNone.Size = new System.Drawing.Size(37, 13);
            this.CheckNone.TabIndex = 2;
            this.CheckNone.Text = "None";
            this.CheckNone.Click += new System.EventHandler(this.CheckNone_Click);
            // 
            // CheckAll
            // 
            this.CheckAll.AutoSize = true;
            this.CheckAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckAll.Location = new System.Drawing.Point(57, 21);
            this.CheckAll.Name = "CheckAll";
            this.CheckAll.Size = new System.Drawing.Size(21, 13);
            this.CheckAll.TabIndex = 1;
            this.CheckAll.Text = "All";
            this.CheckAll.Click += new System.EventHandler(this.CheckAll_Click);
            // 
            // LabelCheck
            // 
            this.LabelCheck.AutoSize = true;
            this.LabelCheck.Location = new System.Drawing.Point(7, 21);
            this.LabelCheck.Name = "LabelCheck";
            this.LabelCheck.Size = new System.Drawing.Size(44, 13);
            this.LabelCheck.TabIndex = 0;
            this.LabelCheck.Text = "Check: ";
            // 
            // _backgroundWorker
            // 
            this._backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._backgroundWorker_DoWork);
            this._backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            // 
            // CommitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 511);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(633, 550);
            this.Name = "CommitForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Commit - VBAGit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommitForm_FormClosing);
            this.Shown += new System.EventHandler(this.CommitForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.GroupMessage.ResumeLayout(false);
            this.GroupMessage.PerformLayout();
            this.GroupChanges.ResumeLayout(false);
            this.GroupChanges.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox GroupMessage;
        private System.Windows.Forms.Button AddSignedoffby;
        private System.Windows.Forms.TextBox Author;
        private System.Windows.Forms.CheckBox SetAuthor;
        private System.Windows.Forms.DateTimePicker AuthorTime;
        private System.Windows.Forms.DateTimePicker AuthorDate;
        private System.Windows.Forms.CheckBox SetAuthorDate;
        private System.Windows.Forms.TextBox CommitMessage;
        private System.Windows.Forms.CheckBox NewBranch;
        private System.Windows.Forms.TextBox CommitBranch;
        private System.Windows.Forms.Label LabelCommit;
        private System.Windows.Forms.CheckBox MessageOnly;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Commit;
        private System.Windows.Forms.GroupBox GroupChanges;
        private System.Windows.Forms.CheckBox ShowUnversionedFiles;
        private System.Windows.Forms.ListView CommitList;
        private System.Windows.Forms.Label CheckModified;
        private System.Windows.Forms.Label CheckDeleted;
        private System.Windows.Forms.Label CheckAdded;
        private System.Windows.Forms.Label CheckVersioned;
        private System.Windows.Forms.Label CheckUnversioned;
        private System.Windows.Forms.Label CheckNone;
        private System.Windows.Forms.Label CheckAll;
        private System.Windows.Forms.Label LabelCheck;
        private System.Windows.Forms.Label LabelSelected;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.ColumnHeader ColumnExtension;
        private System.Windows.Forms.ColumnHeader ColumnStatus;
        private System.Windows.Forms.ImageList VBComponentsImageList;
        private System.ComponentModel.BackgroundWorker _backgroundWorker;
        private System.Windows.Forms.Label EmptyCommitList;
    }
}