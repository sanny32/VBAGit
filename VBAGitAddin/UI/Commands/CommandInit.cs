using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using VBAGitAddin.Git;
using VBAGitAddin.UI.Forms;
using VBAGitAddin.VBEditor.Extensions;
using System.Drawing;
using LibGit2Sharp;


namespace VBAGitAddin.UI.Commands
{
    public class CommandInit : CommandBase, IGitCommand
    {
        private readonly VBProject _project;
        private IRepository _repositroty;

        public CommandInit(VBProject project)
        {
            _project = project;            
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

        public override Bitmap ProgressImage
        {
            get
            {
                return null;
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
            var provider = new GitProvider(_project);
            var path = VBAGitAddinApp.GetVBProjectRepoPath(_project);

            _repositroty = provider.Init(path, false);

            int progress = 0;
            int count = _project.VBComponents.Count;

            foreach (VBComponent component in _project.VBComponents)
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
