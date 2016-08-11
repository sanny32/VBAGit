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
                _scSync = AddButton(_menu, VBAGitUI.VBAGitMenu_Sync, false, OnGitSync, "git_sync");               
                _scCommit = AddButton(_menu, VBAGitUI.VBAGitMenu_Commit, false, OnGitCommit, "git_commit");                
                _scPull = AddButton(_menu, VBAGitUI.VBAGitMenu_Pull, true, OnGitPull, "git_pull");                
                _scFetch = AddButton(_menu, VBAGitUI.VBAGitMenu_Fecth, false, OnGitFetch, "git_pull");                
                _scPush = AddButton(_menu, VBAGitUI.VBAGitMenu_Push, false, OnGitPush, "git_push");

                AddVBAGitMenu(true);
            }
            else
            {
                _scCreate = AddButton(_menu, VBAGitUI.VBAGitMenu_Create, false, OnGitCreate, "create_repo");

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
                _scDiff = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Diff, false, OnGitDiff, "VBAGit_diff");

                _scShowLog = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_ShowLog, true, OnGitShowLog, "VBAGit_showlog");
                _scRepoBrowser = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_RepoBrowser, false, OnGitRepoBrowser, "VBAGit_repobrowser");
                _scCheckMod = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CheckForModifications, false, OnGitCheckForModifications, "VBAGit_checkmod");
                _scRebase = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Rebase, false, OnGitRebase, "VBAGit_rebase");

                _scResolve = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Resolve, true, OnGitResolve, "VBAGit_resolve");
                _scDelete = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Delete, false, OnGitDelete, "VBAGit_delete");
                _scRevert = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Revert, false, OnGitRevert, "VBAGit_revert");
                _scCleanUp = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CleanUp, false, OnGitCleanUp, "VBAGit_cleanup");

                _scCheckout = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Checkout, true, OnGitCheckout, "VBAGit_checkout");
                _scMerge = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Merge, false, OnGitMerge, "VBAGit_merge");
                _scCreateBranch = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CreateBranch, false, OnGitCreateBranch, "VBAGit_createbranch");
                _scExport = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Export, false, OnGitExport, "VBAGit_export");

            }

            _scSettings = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Settings, true, OnGitSettings, "VBAGit_settings");
            _scAbout = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_About, false, OnGitAbout, "VBAGit_about");
        }

        private void OnGitSync(CommandBarButton Ctrl, ref bool CancelDefault)
        {
         
        }

        private void OnGitCommit(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _app.Commit(_app.IDE.ActiveVBProject);            
        }

        private void OnGitPull(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitFetch(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitPush(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitCreate(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var project = _app.IDE.ActiveVBProject;
            _app.CreateNewRepo(project);
            RecreateMenu(project);
        }

        private void OnGitDiff(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitShowLog(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitRepoBrowser(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitCheckForModifications(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitRebase(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitResolve(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitDelete(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitRevert(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _app.Revert(_app.IDE.ActiveVBProject);
        }

        private void OnGitCleanUp(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitCheckout(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitMerge(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitCreateBranch(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            _app.CreateBranch(_app.IDE.ActiveVBProject);
        }

        private void OnGitExport(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitSettings(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        private void OnGitAbout(CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }        

        private void UnsubsribeCommandBarButtonClickEvents()
        {
            if (_scCreate != null)
                _scCreate.Click -= OnGitCreate;

            if (_scSync != null)
                _scSync.Click -= OnGitSync;
            if (_scCommit != null)
                _scCommit.Click -= OnGitCommit;

            if (_scPull != null)
                _scPull.Click -= OnGitPull;
            if (_scFetch != null)
                _scFetch.Click -= OnGitFetch;
            if (_scPush != null)
                _scPush.Click -= OnGitPush;

            if (_scDiff != null)
                _scDiff.Click -= OnGitDiff;
            if (_scShowLog != null)
                _scShowLog.Click -= OnGitShowLog;
            if (_scRepoBrowser != null)
                _scRepoBrowser.Click -= OnGitRepoBrowser;
            if (_scCheckMod != null)
                _scCheckMod.Click -= OnGitCheckForModifications;
            if (_scRebase != null)
                _scRebase.Click -= OnGitRebase;
            if (_scResolve != null)
                _scResolve.Click -= OnGitResolve;
            if (_scDelete != null)
                _scDelete.Click -= OnGitDelete;
            if (_scRevert != null)
                _scRevert.Click -= OnGitRevert;
            if (_scCleanUp != null)
                _scCleanUp.Click -= OnGitCleanUp;
            if (_scCheckout != null)
                _scCheckout.Click -= OnGitCheckout;
            if(_scMerge != null)
                _scMerge.Click -= OnGitMerge;
            if (_scCreateBranch != null)
                _scCreateBranch.Click -= OnGitCreateBranch;
            if (_scExport != null)
                _scExport.Click -= OnGitExport;

            if (_scSettings != null)
                _scSettings.Click -= OnGitSettings;
            if (_scAbout != null)
                _scAbout.Click -= OnGitAbout;
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