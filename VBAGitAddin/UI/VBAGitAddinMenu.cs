using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;

namespace VBAGitAddin.UI
{
    internal class VBAGitAddinMenu : Menu
    {       
        private readonly VBAGitAddinApp _app;

        private CommandBarPopup _menu;

        private CommandBarButton _scCreate;
        private CommandBarButton _scSync;
        private CommandBarButton _scCommit;
        private CommandBarButton _scPull;
        private CommandBarButton _scFetch;
        private CommandBarButton _scPush;

        private CommandBarPopup _vbaGitMenu;
        private CommandBarButton _scDiff;
        private CommandBarButton _scShowLog;
        private CommandBarButton _scRepoBrowser;
        private CommandBarButton _scCheckMod;
        private CommandBarButton _scRebase;
        private CommandBarButton _scResolve;
        private CommandBarButton _scDelete;
        private CommandBarButton _scRevert;
        private CommandBarButton _scCleanUp;
        private CommandBarButton _scCheckout;
        private CommandBarButton _scMerge;
        private CommandBarButton _scCreateBranch;
        private CommandBarButton _scExport;
        private CommandBarButton _scSettings;
        private CommandBarButton _scAbout;

        public VBAGitAddinMenu(VBAGitAddinApp app)
            : base()
        {
            _app = app;
        }
        
        public void Initialize()
        {
            RecreateMenu(_app.IDE.ActiveVBProject);
        }

        public void RecreateMenu(VBProject project)
        {
            int windowMenuId = 30038;
            var menuBarControls = _app.IDE.CommandBars[1].Controls;
            var beforeIndex = FindMenuInsertionIndex(menuBarControls, windowMenuId);

            if (_menu != null)
            {                
                UnsubsribeCommandBarButtonClickEvents();
                beforeIndex = _menu.Index;
                _menu.Delete();                              
            }
            
            _menu = menuBarControls.Add(MsoControlType.msoControlPopup, Before: beforeIndex, Temporary: true) as CommandBarPopup;
            _menu.Tag = "VBAGit";       
            _menu.Caption = VBAGitUI.VBAGitMenu;

            if (_app.GetVBProjectRepository(project) != null)
            {
                _scSync = AddButton(_menu, VBAGitUI.VBAGitMenu_Sync, false, OnSourceControlSync, "git_sync");               
                _scCommit = AddButton(_menu, VBAGitUI.VBAGitMenu_Commit, false, OnSourceControlCommit, "git_commit");                
                _scPull = AddButton(_menu, VBAGitUI.VBAGitMenu_Pull, true, OnSourceControlPull, "git_pull");                
                _scFetch = AddButton(_menu, VBAGitUI.VBAGitMenu_Fecth, false, OnSourceControlFetch, "git_pull");                
                _scPush = AddButton(_menu, VBAGitUI.VBAGitMenu_Push, false, OnSourceControlPush, "git_push");

                AddVBAGitMenu(true);
            }
            else
            {
                _scCreate = AddButton(_menu, VBAGitUI.VBAGitMenu_Create, false, OnSourceControlCreate, "create_repo");

                AddVBAGitMenu(false);
            }           
        }

        private void AddVBAGitMenu(bool hasRepo)
        {
            _vbaGitMenu = _menu.Controls.Add(MsoControlType.msoControlPopup, Temporary: true) as CommandBarPopup;
            _vbaGitMenu.BeginGroup = true;
            _vbaGitMenu.Caption = VBAGitUI.VBAGitMenu_VBAGit;

            if (hasRepo)
            {                
                _scDiff = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Diff, false, OnSourceControlDiff, "VBAGit_diff");

                _scShowLog = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_ShowLog, true, OnSourceControlShowLog, "VBAGit_showlog");
                _scRepoBrowser = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_RepoBrowser, false, OnSourceControlRepoBrowser, "VBAGit_repobrowser");
                _scCheckMod = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CheckForModifications, false, OnSourceControlCheckForModifications, "VBAGit_checkmod");
                _scRebase = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Rebase, false, OnSourceControlRebase, "VBAGit_rebase");

                _scResolve = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Resolve, true, OnSourceControlResolve, "VBAGit_resolve");
                _scDelete = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Delete, false, OnSourceControlDelete, "VBAGit_delete");
                _scRevert = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Revert, false, OnSourceControlRevert, "VBAGit_revert");
                _scCleanUp = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CleanUp, false, OnSourceControlCleanUp, "VBAGit_cleanup");

                _scCheckout = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Checkout, true, OnSourceControlCheckout, "VBAGit_checkout");
                _scMerge = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Merge, false, OnSourceControlMerge, "VBAGit_merge");
                _scCreateBranch = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CreateBranch, false, OnSourceControlCreateBranch, "VBAGit_createbranch");
                _scExport = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Export, false, OnSourceControlExport, "VBAGit_export");

            }

            _scSettings = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Settings, true, OnSourceControlSettings, "VBAGit_settings");
            _scAbout = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_About, false, OnSourceControlAbout, "VBAGit_about");
        }

        private void OnSourceControlSync(CommandBarButton Ctrl, ref bool CancelDefault)
        {
         
        }

        private void OnSourceControlCommit(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _app.Commit(_app.IDE.ActiveVBProject);            
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
            var project = _app.IDE.ActiveVBProject;
            _app.CreateNewRepo(project);
            RecreateMenu(project);
        }

        private void OnSourceControlDiff(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlShowLog(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlRepoBrowser(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlCheckForModifications(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlRebase(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlResolve(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlDelete(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlRevert(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlCleanUp(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlCheckout(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlMerge(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlCreateBranch(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _app.CreateBranch(_app.IDE.ActiveVBProject);
        }

        private void OnSourceControlExport(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlSettings(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnSourceControlAbout(CommandBarButton Ctrl, ref bool CancelDefault)
        {

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

            if (_scDiff != null)
                _scDiff.Click -= OnSourceControlDiff;
            if (_scShowLog != null)
                _scShowLog.Click -= OnSourceControlShowLog;
            if (_scRepoBrowser != null)
                _scRepoBrowser.Click -= OnSourceControlRepoBrowser;
            if (_scCheckMod != null)
                _scCheckMod.Click -= OnSourceControlCheckForModifications;
            if (_scRebase != null)
                _scRebase.Click -= OnSourceControlRebase;
            if (_scResolve != null)
                _scResolve.Click -= OnSourceControlResolve;
            if (_scDelete != null)
                _scDelete.Click -= OnSourceControlDelete;
            if (_scRevert != null)
                _scRevert.Click -= OnSourceControlRevert;
            if (_scCleanUp != null)
                _scCleanUp.Click -= OnSourceControlCleanUp;
            if (_scCheckout != null)
                _scCheckout.Click -= OnSourceControlCheckout;
            if(_scMerge != null)
                _scMerge.Click -= OnSourceControlMerge;
            if (_scCreateBranch != null)
                _scCreateBranch.Click -= OnSourceControlCreateBranch;
            if (_scExport != null)
                _scExport.Click -= OnSourceControlExport;

            if (_scSettings != null)
                _scSettings.Click -= OnSourceControlSettings;
            if (_scAbout != null)
                _scAbout.Click -= OnSourceControlAbout;
    }

        private bool _disposed;
        protected override void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            UnsubsribeCommandBarButtonClickEvents();

            if(_app != null)
            {
                _app.Dispose();
            }

            if (_menu != null)
            {
                var menuBarControls = _app.IDE.CommandBars[1].Controls;
                var control = menuBarControls.Parent.FindControl(_menu.Type, _menu.Id, _menu.Tag, _menu.Visible);
                if (control != null) control.Delete();
            }

            _disposed = true;
            base.Dispose(true);
        }
    }
}