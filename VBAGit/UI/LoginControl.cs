﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace VBAGit.UI
{
    [ExcludeFromCodeCoverage]
    public partial class LoginControl : UserControl, ILoginView
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        public string Username
        {
            get { return this.UsernameBox.Text; }
            set { this.UsernameBox.Text = value; }
        }

        public string Password
        {
            get { return this.PasswordBox.Text; }
            set { this.PasswordBox.Text = value; }
        }

        public event EventHandler Confirm;
        public event EventHandler Cancel;
        public event EventHandler<EventArgs> DismissSecondaryPanel;

        private void OkButton_Click(object sender, EventArgs e)
        {
            var handler = Confirm;
            if (handler != null)
            {
                handler(this, e);
            }

            var dismiss = DismissSecondaryPanel;
            if (dismiss != null)
            {
                dismiss(this, e);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var handler = Confirm;
            if (handler != null)
            {
                handler(this, e);
            }

            var dismiss = DismissSecondaryPanel;
            if (dismiss != null)
            {
                dismiss(this, e);
            }
        }
    }
}
