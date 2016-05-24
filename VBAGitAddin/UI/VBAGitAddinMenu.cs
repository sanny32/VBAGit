using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using VBAGitAddin.VBEditor;

namespace VBAGitAddin.UI
{
    internal class VBAGitAddinMenu : Menu
    {       
        private readonly AddIn _addIn;

        private CommandBarPopup _menu;

        private CommandBarButton _scCreate;
        private CommandBarButton _scSync;
        private CommandBarButton _scCommit;
        private CommandBarButton _scPull;
        private CommandBarButton _scFetch;
        private CommandBarButton _scPush;
        private CommandBarPopup _vbaGitMenu;

        private bool _activeProjectHasRepo;

        public VBAGitAddinMenu(VBE vbe, AddIn addIn, IActiveCodePaneEditor editor)
            : base(vbe, addIn)
        {
            _addIn = addIn;
            _activeProjectHasRepo = false;
        }       

        public void Initialize()
        {
            RecreateMenu();
        }

        public void RecreateMenu()
        {
            if (_menu != null)
            {
                UnsubsribeCommandBarButtonClickEvents();
                _menu.Delete();
            }

            const int windowMenuId = 30009;
            var menuBarControls = IDE.CommandBars[1].Controls;
            var beforeIndex = FindMenuInsertionIndex(menuBarControls, windowMenuId);

            _menu = menuBarControls.Add(MsoControlType.msoControlPopup, Before: beforeIndex, Temporary: true) as CommandBarPopup;
            _menu.Caption = VBAGitUI.VBAGitMenu;

            if (_activeProjectHasRepo)
            {
                _scSync = AddButton(_menu, VBAGitUI.VBAGitMenu_Sync, false, OnSourceControlSync);
                SetButtonImage(_scSync, VBAGitAddin.Properties.Resources.git_sync, VBAGitAddin.Properties.Resources.git_sync_mask);

                _scCommit = AddButton(_menu, VBAGitUI.VBAGitMenu_Commit, false, OnSourceControlCommit);
                SetButtonImage(_scCommit, VBAGitAddin.Properties.Resources.git_commit, VBAGitAddin.Properties.Resources.git_commit_mask);

                _scPull = AddButton(_menu, VBAGitUI.VBAGitMenu_Pull, true, OnSourceControlPull);
                SetButtonImage(_scPull, VBAGitAddin.Properties.Resources.git_pull, VBAGitAddin.Properties.Resources.git_pull_mask);

                _scFetch = AddButton(_menu, VBAGitUI.VBAGitMenu_Fecth, false, OnSourceControlFetch);
                SetButtonImage(_scFetch, VBAGitAddin.Properties.Resources.git_pull, VBAGitAddin.Properties.Resources.git_pull_mask);

                _scPush = AddButton(_menu, VBAGitUI.VBAGitMenu_Push, false, OnSourceControlPush);
                SetButtonImage(_scPush, VBAGitAddin.Properties.Resources.git_push, VBAGitAddin.Properties.Resources.git_push_mask);

                _vbaGitMenu = _menu.Controls.Add(MsoControlType.msoControlPopup, Temporary: true) as CommandBarPopup;
                _vbaGitMenu.BeginGroup = true;
                _vbaGitMenu.Caption = VBAGitUI.VBAGitMenu_VBAGit;
            }
            else
            {
                _scCreate = AddButton(_menu, VBAGitUI.VBAGitMenu_Create, false, OnSourceControlCreate);
                SetButtonImage(_scCreate, VBAGitAddin.Properties.Resources.create_repo, VBAGitAddin.Properties.Resources.create_repo_mask);
            }
        }

        private void OnSourceControlSync(CommandBarButton Ctrl, ref bool CancelDefault)
        {
         
        }

        private void OnSourceControlCommit(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlPull(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlFetch(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlPush(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlCreate(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _activeProjectHasRepo = true;
            RecreateMenu();
        }

        private void UnsubsribeCommandBarButtonClickEvents()
        {
            if (_scCreate != null)
                _scCreate.Click -= OnSourceControlCreate;

            if (_scSync != null)
                _scSync.Click -= OnSourceControlSync;
            if (_scCommit != null)
                _scCommit.Click -= OnSourceControlCommit;

            if (_scPull != null)
                _scPull.Click -= OnSourceControlPull;
            if (_scFetch != null)
                _scFetch.Click -= OnSourceControlFetch;
            if (_scPush != null)
                _scPush.Click -= OnSourceControlPush;
        }

        private bool _disposed;
        protected override void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            UnsubsribeCommandBarButtonClickEvents();

            if (_menu != null)
            {
                var menuBarControls = IDE.CommandBars[1].Controls;
                var control = menuBarControls.Parent.FindControl(_menu.Type, _menu.Id, _menu.Tag, _menu.Visible);
                if (control != null) control.Delete();
            }

            _disposed = true;
            base.Dispose(true);
        }
    }
}