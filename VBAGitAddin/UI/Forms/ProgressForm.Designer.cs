using VBAGitAddin.UI.Controls;

namespace VBAGitAddin.UI.Forms
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;       

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.Close = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.ProgressInfo = new System.Windows.Forms.Label();
            this.Action = new System.Windows.Forms.Button();
            this.Animation = new VBAGitAddin.UI.Controls.AnimationControl();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(16, 82);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(456, 27);
            this.ProgressBar.TabIndex = 0;
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.BackColor = System.Drawing.SystemColors.Window;
            this.LogBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LogBox.Location = new System.Drawing.Point(16, 115);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(456, 101);
            this.LogBox.TabIndex = 1;
            this.LogBox.Text = "";
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close.Location = new System.Drawing.Point(292, 222);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(87, 27);
            this.Close.TabIndex = 2;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            // 
            // Abort
            // 
            this.Abort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Abort.Location = new System.Drawing.Point(385, 222);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(87, 27);
            this.Abort.TabIndex = 3;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            this.Abort.Click += new System.EventHandler(this.Abort_Click);
            // 
            // ProgressInfo
            // 
            this.ProgressInfo.AutoSize = true;
            this.ProgressInfo.Location = new System.Drawing.Point(13, 58);
            this.ProgressInfo.Name = "ProgressInfo";
            this.ProgressInfo.Size = new System.Drawing.Size(0, 15);
            this.ProgressInfo.TabIndex = 4;
            // 
            // Action
            // 
            this.Action.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Action.Location = new System.Drawing.Point(16, 222);
            this.Action.Name = "Action";
            this.Action.Size = new System.Drawing.Size(181, 27);
            this.Action.TabIndex = 5;
            this.Action.Text = "Action";
            this.Action.UseVisualStyleBackColor = true;
            this.Action.Visible = false;
            // 
            // Animation
            // 
            this.Animation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Animation.AnimatedImage = null;
            this.Animation.Location = new System.Drawing.Point(14, 6);
            this.Animation.Name = "Animation";
            this.Animation.Size = new System.Drawing.Size(456, 49);
            this.Animation.TabIndex = 6;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.Animation);
            this.Controls.Add(this.Action);
            this.Controls.Add(this.ProgressInfo);
            this.Controls.Add(this.Abort);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.ProgressBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.RichTextBox LogBox;
        private new System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.Label ProgressInfo;
        private System.Windows.Forms.Button Action;
        private AnimationControl Animation;
    }
}