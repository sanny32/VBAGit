namespace VBAGitAddin.UI
{
    partial class ProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.Close = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.ProgressInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(12, 62);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(570, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.BackColor = System.Drawing.SystemColors.Window;
            this.LogBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LogBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogBox.Location = new System.Drawing.Point(12, 100);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(570, 205);
            this.LogBox.TabIndex = 1;
            this.LogBox.Text = "";
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close.Location = new System.Drawing.Point(426, 323);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 2;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            // 
            // Abort
            // 
            this.Abort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Abort.Location = new System.Drawing.Point(507, 323);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(75, 23);
            this.Abort.TabIndex = 3;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            this.Abort.Click += new System.EventHandler(this.Abort_Click);
            // 
            // ProgressInfo
            // 
            this.ProgressInfo.AutoSize = true;
            this.ProgressInfo.Location = new System.Drawing.Point(9, 44);
            this.ProgressInfo.Name = "ProgressInfo";
            this.ProgressInfo.Size = new System.Drawing.Size(0, 13);
            this.ProgressInfo.TabIndex = 4;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 358);
            this.Controls.Add(this.ProgressInfo);
            this.Controls.Add(this.Abort);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.ProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.RichTextBox LogBox;
        private new System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.Label ProgressInfo;
    }
}