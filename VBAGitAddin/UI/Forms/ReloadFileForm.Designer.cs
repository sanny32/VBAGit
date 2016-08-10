namespace VBAGitAddin.UI.Forms
{
    partial class ReloadFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReloadFileForm));
            this.Yes = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.No = new System.Windows.Forms.Button();
            this.YesToAll = new System.Windows.Forms.Button();
            this.NoToAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(231, 108);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(69, 23);
            this.Yes.TabIndex = 9;
            this.Yes.Text = "Yes";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.LabelInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Yes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.No, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.YesToAll, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.NoToAll, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 141);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LabelInfo, 5);
            this.LabelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelInfo.Location = new System.Drawing.Point(9, 6);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Padding = new System.Windows.Forms.Padding(6);
            this.LabelInfo.Size = new System.Drawing.Size(516, 99);
            this.LabelInfo.TabIndex = 8;
            this.LabelInfo.Text = "{0}\r\n\r\nThis file has been modified outside of the source editor.\r\nDo you want to " +
    "reload it?";
            // 
            // No
            // 
            this.No.Location = new System.Drawing.Point(381, 108);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(69, 23);
            this.No.TabIndex = 10;
            this.No.Text = "No";
            this.No.UseVisualStyleBackColor = true;
            this.No.Click += new System.EventHandler(this.No_Click);
            // 
            // YesToAll
            // 
            this.YesToAll.Location = new System.Drawing.Point(306, 108);
            this.YesToAll.Name = "YesToAll";
            this.YesToAll.Size = new System.Drawing.Size(69, 23);
            this.YesToAll.TabIndex = 11;
            this.YesToAll.Text = "Yes To All";
            this.YesToAll.UseVisualStyleBackColor = true;
            this.YesToAll.Click += new System.EventHandler(this.YesToAll_Click);
            // 
            // NoToAll
            // 
            this.NoToAll.Location = new System.Drawing.Point(456, 108);
            this.NoToAll.Name = "NoToAll";
            this.NoToAll.Size = new System.Drawing.Size(69, 23);
            this.NoToAll.TabIndex = 12;
            this.NoToAll.Text = "No To All";
            this.NoToAll.UseVisualStyleBackColor = true;
            this.NoToAll.Click += new System.EventHandler(this.NoToAll_Click);
            // 
            // ReloadFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 141);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 180);
            this.MinimizeBox = false;
            this.Name = "ReloadFileForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File modified - VBAGit";
            this.Shown += new System.EventHandler(this.ReloadFileForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Button No;
        private System.Windows.Forms.Button YesToAll;
        private System.Windows.Forms.Button NoToAll;
    }
}