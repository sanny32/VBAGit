using System.Windows.Forms;

namespace VBAGitAddin.UI.Forms
{
    partial class RevertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevertForm));
            this.RevertList = new System.Windows.Forms.ListView();
            this.ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VBComponentsImageList = new System.Windows.Forms.ImageList(this.components);
            this.SelectAll = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RevertList
            // 
            this.RevertList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RevertList.CheckBoxes = true;
            this.RevertList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnName,
            this.ColumnExtension,
            this.ColumnStatus});
            this.RevertList.FullRowSelect = true;
            this.RevertList.Location = new System.Drawing.Point(14, 14);
            this.RevertList.Name = "RevertList";
            this.RevertList.Size = new System.Drawing.Size(476, 325);
            this.RevertList.SmallImageList = this.VBComponentsImageList;
            this.RevertList.TabIndex = 0;
            this.RevertList.UseCompatibleStateImageBehavior = false;
            this.RevertList.View = System.Windows.Forms.View.Details;
            // 
            // ColumnName
            // 
            this.ColumnName.Text = "Name";
            this.ColumnName.Width = 277;
            // 
            // ColumnExtension
            // 
            this.ColumnExtension.Text = "Extension";
            this.ColumnExtension.Width = 76;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.Text = "Status";
            this.ColumnStatus.Width = 91;
            // 
            // VBComponentsImageList
            // 
            this.VBComponentsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("VBComponentsImageList.ImageStream")));
            this.VBComponentsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.VBComponentsImageList.Images.SetKeyName(0, "VBForm.png");
            this.VBComponentsImageList.Images.SetKeyName(1, "VBModule.png");
            this.VBComponentsImageList.Images.SetKeyName(2, "VBClassModule.png");
            this.VBComponentsImageList.Images.SetKeyName(3, "VBDocument.png");
            // 
            // SelectAll
            // 
            this.SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectAll.AutoCheck = false;
            this.SelectAll.AutoSize = true;
            this.SelectAll.Location = new System.Drawing.Point(14, 345);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(120, 19);
            this.SelectAll.TabIndex = 1;
            this.SelectAll.Text = "Select/deselect all";
            this.SelectAll.ThreeState = true;
            this.SelectAll.UseVisualStyleBackColor = true;
            this.SelectAll.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(403, 372);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(87, 27);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.Location = new System.Drawing.Point(310, 372);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(87, 27);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // RevertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 411);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.SelectAll);
            this.Controls.Add(this.RevertList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(520, 450);
            this.Name = "RevertForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Revert - VBAGit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView RevertList;
        private System.Windows.Forms.CheckBox SelectAll;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.ColumnHeader ColumnExtension;
        private System.Windows.Forms.ColumnHeader ColumnStatus;
        private ImageList VBComponentsImageList;
    }
}