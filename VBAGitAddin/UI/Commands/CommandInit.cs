using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using VBAGitAddin.Git;
using VBAGitAddin.UI.Forms;
using VBAGitAddin.VBEditor.Extensions;
using LibGit2Sharp;


namespace VBAGitAddin.UI.Commands
{
    public class CommandInit : CommandBase, IGitCommand
    {
        private IRepository _repositroty;

        public CommandInit(VBProject project)
            :base(project)
        {
            Provider = new GitProvider(VBProject);
        }

        public override IRepository Repository
        {
            get
            {
                return _repositroty;
            }
        }
       
        public override string Name
        {
            get
            {
                return "Git Init";
            }
        }
              
        public override void Execute()
        {            
            using (var progressForm = new ProgressForm(this))
            {
                progressForm.Shown += ProgressForm_Shown;
                progressForm.ShowDialog();
            };
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            RunCommandAsync();
        }

        protected override void OnExectute(DoWorkEventArgs e)
        {          
            var path = VBAGitAddinApp.GetVBProjectRepoPath(VBProject);

            if(path == null)
            {
                throw new Exception("You must save project before create git repository.");
            }

            _repositroty = Provider.Init(path, false);

            int progress = 0;
            int count = VBProject.VBComponents.Count;

            foreach (VBComponent component in VBProject.VBComponents)
            {
                ReportProgress(100 * ++progress / count, VBAGitUI.ProgressInfo_ExportingFiles);

                if (CancellationPending)
                {
                    e.Cancel = true;              
                    return;
                }

                component.ExportAsSourceFile(path);
            }
        }                    
    }
}
