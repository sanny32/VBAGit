using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using VBAGitAddin.SourceControl;
using VBAGitAddin.VBEditor.Extensions;
using System.Drawing;

namespace VBAGitAddin.UI.Commands
{
    public class InitCommand : CommandBase
    {
        private readonly VBProject _project;
        private IRepository _repositroty;

        public InitCommand(VBProject project)
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
                return Properties.Resources.tshell32_160;
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
            var path = UIApp.GetVBProjectRepoPath(_project);
            var providerFactory = new SourceControlProviderFactory();
            var provider = providerFactory.CreateProvider(_project);

            _repositroty = provider.Init(path, false);

            int progress = 0;
            int count = _project.VBComponents.Count;
            foreach (VBComponent component in _project.VBComponents)
            {
                if (CancellationPending)
                {
                    e.Cancel = true;              
                    return;
                }

                component.ExportAsSourceFile(path);

                ReportProgress(100 * ++progress / count, VBAGitUI.ProgressInfo_ExportingFiles);
            }
        }                    
    }
}
