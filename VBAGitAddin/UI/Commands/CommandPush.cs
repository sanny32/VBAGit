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
        private class PushInfo
        {
            public PushInfo(string remote, IEnumerable<string> pushRefSpecs)
            {
                Remote = remote;
                PushRefs = pushRefSpecs;
            }
            public string Remote { get; private set; }           
            public IEnumerable<string> PushRefs { get; private set; }
        }

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

        public void Push(string remote, IEnumerable<string> pushRefSpecs)
        {
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += delegate (object sender, EventArgs e)
                {                 
                    RunCommandAsync(new PushInfo(remote, pushRefSpecs));
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
            var pushInfo = e.Argument as PushInfo;

            var remote = Repository.Network.Remotes.FirstOrDefault(r => r.Name == pushInfo.Remote);
            Provider.Push(remote, pushInfo.PushRefs);
        }
    }
}
