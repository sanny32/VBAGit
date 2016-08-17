using System.Windows.Forms;

namespace VBAGitAddin.UI.Forms
{
    partial class PushForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PushForm));
            this.GroupRef = new System.Windows.Forms.GroupBox();
            this.SelectLocalBranch = new System.Windows.Forms.Button();
            this.SelectRemoteBranch = new System.Windows.Forms.Button();
            this.RemoteBranches = new System.Windows.Forms.ComboBox();
            this.LabelRemote = new System.Windows.Forms.Label();
            this.LocalBranches = new System.Windows.Forms.ComboBox();
            this.LabelLocal = new System.Windows.Forms.Label();
            this.PushAllBranches = new System.Windows.Forms.CheckBox();
            this.GroupDestination = new System.Windows.Forms.GroupBox();
            this.ArbitraryUrl = new System.Windows.Forms.TextBox();
            this.Manage = new System.Windows.Forms.Button();
            this.Remotes = new System.Windows.Forms.ComboBox();
            this.DestinationUrl = new System.Windows.Forms.RadioButton();
            this.DestinationRemote = new System.Windows.Forms.RadioButton();
            this.GroupOptions = new System.Windows.Forms.GroupBox();
            this.OptionSetUpstream = new System.Windows.Forms.CheckBox();
            this.OptionIncludeTags = new System.Windows.Forms.CheckBox();
            this.OptionUnknownChanges = new System.Windows.Forms.CheckBox();
            this.OptionKnownChanges = new System.Windows.Forms.CheckBox();
            this.LabelForce = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.GroupRef.SuspendLayout();
            this.GroupDestination.SuspendLayout();
            this.GroupOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupRef
            // 
            this.GroupRef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupRef.Controls.Add(this.SelectLocalBranch);
            this.GroupRef.Controls.Add(this.SelectRemoteBranch);
            this.GroupRef.Controls.Add(this.RemoteBranches);
            this.GroupRef.Controls.Add(this.LabelRemote);
            this.GroupRef.Controls.Add(this.LocalBranches);
            this.GroupRef.Controls.Add(this.LabelLocal);
            this.GroupRef.Controls.Add(this.PushAllBranches);
            this.GroupRef.Location = new System.Drawing.Point(12, 12);
            this.GroupRef.Name = "GroupRef";
            this.GroupRef.Size = new System.Drawing.Size(490, 125);
            this.GroupRef.TabIndex = 0;
            this.GroupRef.TabStop = false;
            this.GroupRef.Text = "Ref";
            // 
            // SelectLocalBranch
            // 
            this.SelectLocalBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectLocalBranch.Location = new System.Drawing.Point(458, 53);
            this.SelectLocalBranch.Name = "SelectLocalBranch";
            this.SelectLocalBranch.Size = new System.Drawing.Size(26, 25);
            this.SelectLocalBranch.TabIndex = 9;
            this.SelectLocalBranch.Text = "...";
            this.SelectLocalBranch.UseVisualStyleBackColor = true;
            this.SelectLocalBranch.Click += new System.EventHandler(this.SelectLocalBranch_Click);
            // 
            // SelectRemoteBranch
            // 
            this.SelectRemoteBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectRemoteBranch.Location = new System.Drawing.Point(458, 84);
            this.SelectRemoteBranch.Name = "SelectRemoteBranch";
            this.SelectRemoteBranch.Size = new System.Drawing.Size(26, 25);
            this.SelectRemoteBranch.TabIndex = 8;
            this.SelectRemoteBranch.Text = "...";
            this.SelectRemoteBranch.UseVisualStyleBackColor = true;
            this.SelectRemoteBranch.Click += new System.EventHandler(this.SelectRemoteBranch_Click);
            // 
            // RemoteBranches
            // 
            this.RemoteBranches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoteBranches.FormattingEnabled = true;
            this.RemoteBranches.Location = new System.Drawing.Point(131, 84);
            this.RemoteBranches.Name = "RemoteBranches";
            this.RemoteBranches.Size = new System.Drawing.Size(316, 23);
            this.RemoteBranches.TabIndex = 4;
            // 
            // LabelRemote
            // 
            this.LabelRemote.AutoSize = true;
            this.LabelRemote.Location = new System.Drawing.Point(6, 87);
            this.LabelRemote.Name = "LabelRemote";
            this.LabelRemote.Size = new System.Drawing.Size(54, 15);
            this.LabelRemote.TabIndex = 3;
            this.LabelRemote.Text = "Remote: ";
            // 
            // LocalBranches
            // 
            this.LocalBranches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalBranches.FormattingEnabled = true;
            this.LocalBranches.Location = new System.Drawing.Point(131, 55);
            this.LocalBranches.Name = "LocalBranches";
            this.LocalBranches.Size = new System.Drawing.Size(316, 23);
            this.LocalBranches.TabIndex = 2;
            // 
            // LabelLocal
            // 
            this.LabelLocal.AutoSize = true;
            this.LabelLocal.Location = new System.Drawing.Point(6, 58);
            this.LabelLocal.Name = "LabelLocal";
            this.LabelLocal.Size = new System.Drawing.Size(41, 15);
            this.LabelLocal.TabIndex = 1;
            this.LabelLocal.Text = "Local: ";
            // 
            // PushAllBranches
            // 
            this.PushAllBranches.AutoSize = true;
            this.PushAllBranches.Location = new System.Drawing.Point(6, 22);
            this.PushAllBranches.Name = "PushAllBranches";
            this.PushAllBranches.Size = new System.Drawing.Size(118, 19);
            this.PushAllBranches.TabIndex = 0;
            this.PushAllBranches.Text = "Push all branches";
            this.PushAllBranches.UseVisualStyleBackColor = true;
            // 
            // GroupDestination
            // 
            this.GroupDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupDestination.Controls.Add(this.ArbitraryUrl);
            this.GroupDestination.Controls.Add(this.Manage);
            this.GroupDestination.Controls.Add(this.Remotes);
            this.GroupDestination.Controls.Add(this.DestinationUrl);
            this.GroupDestination.Controls.Add(this.DestinationRemote);
            this.GroupDestination.Location = new System.Drawing.Point(12, 143);
            this.GroupDestination.Name = "GroupDestination";
            this.GroupDestination.Size = new System.Drawing.Size(490, 97);
            this.GroupDestination.TabIndex = 1;
            this.GroupDestination.TabStop = false;
            this.GroupDestination.Text = "Destination";
            // 
            // ArbitraryUrl
            // 
            this.ArbitraryUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArbitraryUrl.Location = new System.Drawing.Point(131, 56);
            this.ArbitraryUrl.Name = "ArbitraryUrl";
            this.ArbitraryUrl.Size = new System.Drawing.Size(353, 23);
            this.ArbitraryUrl.TabIndex = 4;
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Location = new System.Drawing.Point(415, 26);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(69, 25);
            this.Manage.TabIndex = 3;
            this.Manage.Text = "Manage";
            this.Manage.UseVisualStyleBackColor = true;
            // 
            // Remotes
            // 
            this.Remotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Remotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Remotes.FormattingEnabled = true;
            this.Remotes.Location = new System.Drawing.Point(131, 27);
            this.Remotes.Name = "Remotes";
            this.Remotes.Size = new System.Drawing.Size(278, 23);
            this.Remotes.TabIndex = 2;
            // 
            // DestinationUrl
            // 
            this.DestinationUrl.AutoSize = true;
            this.DestinationUrl.Location = new System.Drawing.Point(6, 57);
            this.DestinationUrl.Name = "DestinationUrl";
            this.DestinationUrl.Size = new System.Drawing.Size(101, 19);
            this.DestinationUrl.TabIndex = 1;
            this.DestinationUrl.Text = "Arbitrary URL: ";
            this.DestinationUrl.UseVisualStyleBackColor = true;
            // 
            // DestinationRemote
            // 
            this.DestinationRemote.AutoSize = true;
            this.DestinationRemote.Checked = true;
            this.DestinationRemote.Location = new System.Drawing.Point(6, 28);
            this.DestinationRemote.Name = "DestinationRemote";
            this.DestinationRemote.Size = new System.Drawing.Size(72, 19);
            this.DestinationRemote.TabIndex = 0;
            this.DestinationRemote.TabStop = true;
            this.DestinationRemote.Text = "Remote: ";
            this.DestinationRemote.UseVisualStyleBackColor = true;
            // 
            // GroupOptions
            // 
            this.GroupOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupOptions.Controls.Add(this.OptionSetUpstream);
            this.GroupOptions.Controls.Add(this.OptionIncludeTags);
            this.GroupOptions.Controls.Add(this.OptionUnknownChanges);
            this.GroupOptions.Controls.Add(this.OptionKnownChanges);
            this.GroupOptions.Controls.Add(this.LabelForce);
            this.GroupOptions.Location = new System.Drawing.Point(12, 246);
            this.GroupOptions.Name = "GroupOptions";
            this.GroupOptions.Size = new System.Drawing.Size(490, 107);
            this.GroupOptions.TabIndex = 2;
            this.GroupOptions.TabStop = false;
            this.GroupOptions.Text = "Options";
            // 
            // OptionSetUpstream
            // 
            this.OptionSetUpstream.AutoSize = true;
            this.OptionSetUpstream.Location = new System.Drawing.Point(9, 73);
            this.OptionSetUpstream.Name = "OptionSetUpstream";
            this.OptionSetUpstream.Size = new System.Drawing.Size(210, 19);
            this.OptionSetUpstream.TabIndex = 4;
            this.OptionSetUpstream.Text = "Set upstream/track remote branch ";
            this.OptionSetUpstream.UseVisualStyleBackColor = true;
            // 
            // OptionIncludeTags
            // 
            this.OptionIncludeTags.AutoSize = true;
            this.OptionIncludeTags.Location = new System.Drawing.Point(9, 48);
            this.OptionIncludeTags.Name = "OptionIncludeTags";
            this.OptionIncludeTags.Size = new System.Drawing.Size(92, 19);
            this.OptionIncludeTags.TabIndex = 3;
            this.OptionIncludeTags.Text = "Include Tags";
            this.OptionIncludeTags.UseVisualStyleBackColor = true;
            // 
            // OptionUnknownChanges
            // 
            this.OptionUnknownChanges.AutoSize = true;
            this.OptionUnknownChanges.Location = new System.Drawing.Point(339, 23);
            this.OptionUnknownChanges.Name = "OptionUnknownChanges";
            this.OptionUnknownChanges.Size = new System.Drawing.Size(123, 19);
            this.OptionUnknownChanges.TabIndex = 2;
            this.OptionUnknownChanges.Text = "unknown changes";
            this.OptionUnknownChanges.UseVisualStyleBackColor = true;
            // 
            // OptionKnownChanges
            // 
            this.OptionKnownChanges.AutoSize = true;
            this.OptionKnownChanges.Location = new System.Drawing.Point(179, 22);
            this.OptionKnownChanges.Name = "OptionKnownChanges";
            this.OptionKnownChanges.Size = new System.Drawing.Size(109, 19);
            this.OptionKnownChanges.TabIndex = 1;
            this.OptionKnownChanges.Text = "known changes";
            this.OptionKnownChanges.UseVisualStyleBackColor = true;
            // 
            // LabelForce
            // 
            this.LabelForce.AutoSize = true;
            this.LabelForce.Location = new System.Drawing.Point(6, 23);
            this.LabelForce.Name = "LabelForce";
            this.LabelForce.Size = new System.Drawing.Size(106, 15);
            this.LabelForce.TabIndex = 0;
            this.LabelForce.Text = "Force: May discard";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(415, 362);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(87, 27);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.Location = new System.Drawing.Point(322, 362);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(87, 27);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // PushForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 401);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.GroupOptions);
            this.Controls.Add(this.GroupDestination);
            this.Controls.Add(this.GroupRef);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1920, 440);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(530, 440);
            this.Name = "PushForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Push - VBAGit";
            this.GroupRef.ResumeLayout(false);
            this.GroupRef.PerformLayout();
            this.GroupDestination.ResumeLayout(false);
            this.GroupDestination.PerformLayout();
            this.GroupOptions.ResumeLayout(false);
            this.GroupOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupRef;
        private System.Windows.Forms.Label LabelLocal;
        private System.Windows.Forms.CheckBox PushAllBranches;
        private System.Windows.Forms.ComboBox LocalBranches;
        private System.Windows.Forms.ComboBox RemoteBranches;
        private System.Windows.Forms.Label LabelRemote;
        private System.Windows.Forms.Button SelectRemoteBranch;
        private System.Windows.Forms.Button SelectLocalBranch;
        private System.Windows.Forms.GroupBox GroupDestination;
        private System.Windows.Forms.Button Manage;
        private System.Windows.Forms.ComboBox Remotes;
        private System.Windows.Forms.RadioButton DestinationUrl;
        private System.Windows.Forms.RadioButton DestinationRemote;
        private System.Windows.Forms.TextBox ArbitraryUrl;
        private System.Windows.Forms.GroupBox GroupOptions;
        private System.Windows.Forms.CheckBox OptionUnknownChanges;
        private System.Windows.Forms.CheckBox OptionKnownChanges;
        private System.Windows.Forms.Label LabelForce;
        private System.Windows.Forms.CheckBox OptionIncludeTags;
        private System.Windows.Forms.CheckBox OptionSetUpstream;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
    }
}