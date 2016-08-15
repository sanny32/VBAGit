using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using VBAGitAddin.VBEditor;

namespace VBAGitAddin.UI
{
    public class ContextMenu : Menu
    {
        private readonly VBAGitAddinApp _app;
        private readonly ProjectExplorerTreeView _projectExplorer;

        private CommandBarButton _gitCommit;
        private CommandBarButton _gitRevert;

        public ContextMenu(VBAGitAddinApp app)
        {
            _app = app;
            _projectExplorer = new ProjectExplorerTreeView(_app.IDE);
            _projectExplorer.OnContextMenu += _projectExplorer_OnContextMenu;
        }

        private void _projectExplorer_OnContextMenu(object sender, EventArgs e)
        {
            string selectedItem = _projectExplorer.GetSelectedItemText();
            EnableButtons(selectedItem != ProjectExplorerTreeView.Node_References);
        }

        public void Initialize()
        {
            RecreateMenu(_app.IDE.ActiveVBProject);
        }

        public void RecreateMenu(VBProject project)
        {
            RemoveButtons();

            var commandBar = _app.IDE.CommandBars["Project Window"];
            var beforeItem = commandBar.Controls.Cast<CommandBarControl>().First(control => control.Id == 2578).Index;            

            if (_app.GetVBProjectRepository(project) != null)
            {               
                _gitCommit = AddButton(commandBar, beforeItem, VBAGitUI.VBAGitMenu_Commit, true, _gitCommit_Click, "git_commit");                
                _gitRevert = AddButton(commandBar, beforeItem + 1, VBAGitUI.VBAGitMenu_Revert, false, _gitRevert_Click, "VBAGit_revert");               
            }           
        }

        private void _gitRevert_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if(_app.IDE.SelectedVBComponent != null)
            {
                _app.Revert(_app.IDE.ActiveVBProject, new List<string>() { _app.IDE.SelectedVBComponent.Name });
            }
            else
            {                               
                //string selectedItem = _projectExplorer.GetSelectedItemText();                
            }
            
        }

        private void _gitCommit_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (_app.IDE.SelectedVBComponent != null)
            {
                _app.Commit(_app.IDE.ActiveVBProject, new List<string>() { _app.IDE.SelectedVBComponent.Name });
            }
        }

        private void EnableButtons(bool enable)
        {
            if (_gitCommit != null)
            {
                _gitCommit.Enabled = enable;
            }

            if (_gitRevert != null)
            {
                _gitRevert.Enabled = enable;
            }
        }

        private void RemoveButtons()
        {
            if (_gitCommit != null)
            {
                _gitCommit.Click -= _gitCommit_Click;
                _gitCommit.Delete();

                _gitCommit = null;
            }

            if (_gitRevert != null)
            {
                _gitRevert.Click -= _gitRevert_Click;
                _gitRevert.Delete();

                _gitRevert = null;
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

            if (_projectExplorer != null)
            {
                _projectExplorer.Dispose();
            }

            _disposed = true;
            base.Dispose(true);
        }
    }
}
