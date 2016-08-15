using System.Linq;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using System.Drawing;
using System.Collections.Generic;

namespace VBAGitAddin.UI
{
    public class ContextMenu : Menu
    {
        private readonly VBAGitAddinApp _app;

        private CommandBarButton _gitCommit;
        private CommandBarButton _gitRevert;

        public ContextMenu(VBAGitAddinApp app)
        {
            _app = app;
        }

        public void Initialize()
        {
            RecreateMenu(_app.IDE.ActiveVBProject);
        }

        public void RecreateMenu(VBProject project)
        {
            var commandBar = _app.IDE.CommandBars["Project Window"];
            var beforeItem = commandBar.Controls.Cast<CommandBarControl>().First(control => control.Id == 2578).Index;            

            if (_app.GetVBProjectRepository(project) != null)
            {               
                _gitCommit = AddButton(commandBar, beforeItem, VBAGitUI.VBAGitMenu_Commit, true, _gitCommit_Click, "git_commit");                
                _gitRevert = AddButton(commandBar, beforeItem + 1, VBAGitUI.VBAGitMenu_Revert, false, _gitRevert_Click, "VBAGit_revert");               
            }
            else
            {
                RemoveButtons();
            }
        }

        private void _gitRevert_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if(_app.IDE.SelectedVBComponent != null)
            {
                _app.Revert(_app.IDE.ActiveVBProject, new List<string>() { _app.IDE.SelectedVBComponent.Name });
            }
        }

        private void _gitCommit_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (_app.IDE.SelectedVBComponent != null)
            {
                _app.Commit(_app.IDE.ActiveVBProject, new List<string>() { _app.IDE.SelectedVBComponent.Name });
            }
        }

        private void RemoveButtons()
        {
            if (_gitCommit != null)
            {
                _gitCommit.Click -= _gitCommit_Click;
                _gitCommit.Delete();
            }

            if (_gitRevert != null)
            {
                _gitRevert.Click -= _gitRevert_Click;
                _gitRevert.Delete();
            }
        }

        bool _disposed;
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            RemoveButtons();

            _disposed = true;
            base.Dispose(true);
        }
    }
}
