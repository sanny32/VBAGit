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
        private CommandBarButton _scSync;
        private CommandBarButton _scCommit;
        private CommandBarButton _scPull;
        private CommandBarButton _scFetch;
        private CommandBarButton _scPush;
        private CommandBarPopup _vbaGitMenu;

        public VBAGitAddinMenu(VBE vbe, AddIn addIn, IActiveCodePaneEditor editor)
            : base(vbe, addIn)
        {
            _addIn = addIn;                      
        }       

        public void Initialize()
        {
            const int windowMenuId = 30009;
            var menuBarControls = IDE.CommandBars[1].Controls;
            var beforeIndex = FindMenuInsertionIndex(menuBarControls, windowMenuId);
            _menu = menuBarControls.Add(MsoControlType.msoControlPopup, Before: beforeIndex, Temporary: true) as CommandBarPopup;

            _menu.Caption = VBAGitUI.VBAGitMenu;

            _scSync = AddButton(_menu, VBAGitUI.VBAGitMenu_Sync, false, OnSourceControlSync, VBAGitAddin.Properties.Resources.arrow_circle_double);
            _scCommit = AddButton(_menu, VBAGitUI.VBAGitMenu_Commit, false, OnSourceControlCommit, VBAGitAddin.Properties.Resources.arrow1);

            _scPull = AddButton(_menu, VBAGitUI.VBAGitMenu_Pull, true, OnSourceControlPull, VBAGitAddin.Properties.Resources.arrow_270);
            _scFetch = AddButton(_menu, VBAGitUI.VBAGitMenu_Fecth, false, OnSourceControlFetch, VBAGitAddin.Properties.Resources.arrow_270);
            _scPush = AddButton(_menu, VBAGitUI.VBAGitMenu_Push, false, OnSourceControlPush, VBAGitAddin.Properties.Resources.arrow_090);

            _vbaGitMenu = _menu.Controls.Add(MsoControlType.msoControlPopup, Temporary: true) as CommandBarPopup;
            _vbaGitMenu.BeginGroup = true;
            _vbaGitMenu.Caption = VBAGitUI.VBAGitMenu_VBAGit;
        }

        private App _sourceControlApp;       
        private void OnSourceControlSync(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            //if (_sourceControlApp == null)
            //{
            //    _sourceControlApp = new App(this.IDE, this.AddIn, new SourceControlConfigurationService(), 
            //                                                    new ChangesControl(), new UnSyncedCommitsControl(),
            //                                                    new SettingsControl(), new BranchesControl(),
            //                                                    new CreateBranchForm(), new DeleteBranchForm(),
            //                                                    new MergeForm());
            //}

            //_sourceControlApp.ShowWindow();
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

        private bool _disposed;
        private CommandBarPopup _menu;

        protected override void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            _scSync.Click -= OnSourceControlSync;
            _scCommit.Click -= OnSourceControlCommit;
            _scPull.Click -= OnSourceControlPull;
            _scFetch.Click -= OnSourceControlFetch;
            _scPush.Click -= OnSourceControlPush;

            var menuBarControls = IDE.CommandBars[1].Controls;
            menuBarControls.Parent.FindControl(_menu.Type, _menu.Id, _menu.Tag, _menu.Visible).Delete();

            _disposed = true;
            base.Dispose(true);
        }
    }
}