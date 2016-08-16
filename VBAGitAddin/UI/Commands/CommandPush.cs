using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using VBAGitAddin.Configuration;
using VBAGitAddin.Git;
using VBAGitAddin.UI.Forms;

namespace VBAGitAddin.UI.Commands
{
    public class CommandPush : CommandBase
    {
        public CommandPush(VBProject project, RepositorySettings repoSettings)
            :base(project)
        {
            Provider = new GitProvider(project, repoSettings);
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} - Git Push", Repository.Info.WorkingDirectory);
            }
        }

        public void Push()
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {
                    RunCommandAsync();
                };
                progressForm.ShowDialog();
            };
        }

        public override void Execute()
        {
            using (var pushForm = new PushForm(this))
            {
                pushForm.ShowDialog();
            }
        }

        protected override void OnExectute(DoWorkEventArgs e)
        {
            
        }
    }
}
