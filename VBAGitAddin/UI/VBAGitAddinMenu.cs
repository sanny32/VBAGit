using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;

namespace VBAGitAddin.UI
{
    internal class VBAGitAddinMenu : Menu
    {       
        private readonly VBAGitAddinApp _app;

        private CommandBarPopup _menu;

        private CommandBarButton _gitCreate;
        private CommandBarButton _gitSync;
        private CommandBarButton _gitCommit;
        private CommandBarButton _gitPull;
        private CommandBarButton _gitFetch;
        private CommandBarButton _gitPush;

        private CommandBarPopup _vbaGitMenu;
        private CommandBarButton _gitDiff;
        private CommandBarButton _gitShowLog;
        private CommandBarButton _gitRepoBrowser;
        private CommandBarButton _gitCheckMod;
        private CommandBarButton _gitRebase;
        private CommandBarButton _gitResolve;
        private CommandBarButton _gitRevert;
        private CommandBarButton _gitCleanUp;
        private CommandBarButton _gitCheckout;
        private CommandBarButton _gitMerge;
        private CommandBarButton _gitCreateBranch;
        private CommandBarButton _gitExport;
        private CommandBarButton _gitSettings;
        private CommandBarButton _gitAbout;

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
                _gitSync = AddButton(_menu, VBAGitUI.VBAGitMenu_Sync, false, OnGitSync, "git_sync");               
                _gitCommit = AddButton(_menu, VBAGitUI.VBAGitMenu_Commit, false, OnGitCommit, "git_commit");                
                _gitPull = AddButton(_menu, VBAGitUI.VBAGitMenu_Pull, true, OnGitPull, "git_pull");                
                _gitFetch = AddButton(_menu, VBAGitUI.VBAGitMenu_Fecth, false, OnGitFetch, "git_pull");                
                _gitPush = AddButton(_menu, VBAGitUI.VBAGitMenu_Push, false, OnGitPush, "git_push");

                AddVBAGitMenu(true);
            }
            else
            {
                _gitCreate = AddButton(_menu, VBAGitUI.VBAGitMenu_Create, false, OnGitCreate, "create_repo");

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
                _gitDiff = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Diff, false, OnGitDiff, "VBAGit_diff");

                _gitShowLog = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_ShowLog, true, OnGitShowLog, "VBAGit_showlog");
                _gitRepoBrowser = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_RepoBrowser, false, OnGitRepoBrowser, "VBAGit_repobrowser");
                _gitCheckMod = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CheckForModifications, false, OnGitCheckForModifications, "VBAGit_checkmod");
                _gitRebase = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Rebase, false, OnGitRebase, "VBAGit_rebase");

                _gitResolve = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Resolve, true, OnGitResolve, "VBAGit_resolve");
                _gitRevert = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Revert, false, OnGitRevert, "VBAGit_revert");
                _gitCleanUp = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CleanUp, false, OnGitCleanUp, "VBAGit_cleanup");

                _gitCheckout = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Checkout, true, OnGitCheckout, "VBAGit_checkout");
                _gitMerge = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Merge, false, OnGitMerge, "VBAGit_merge");
                _gitCreateBranch = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_CreateBranch, false, OnGitCreateBranch, "VBAGit_createbranch");
                _gitExport = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Export, false, OnGitExport, "VBAGit_export");

            }

            _gitSettings = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_Settings, true, OnGitSettings, "VBAGit_settings");
            _gitAbout = AddButton(_vbaGitMenu, VBAGitUI.VBAGitMenu_About, false, OnGitAbout, "VBAGit_about");
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
            _app.Push(_app.IDE.ActiveVBProject);
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
            if (_gitCreate != null)
                _gitCreate.Click -= OnGitCreate;

            if (_gitSync != null)
                _gitSync.Click -= OnGitSync;
            if (_gitCommit != null)
                _gitCommit.Click -= OnGitCommit;

            if (_gitPull != null)
                _gitPull.Click -= OnGitPull;
            if (_gitFetch != null)
                _gitFetch.Click -= OnGitFetch;
            if (_gitPush != null)
                _gitPush.Click -= OnGitPush;

            if (_gitDiff != null)
                _gitDiff.Click -= OnGitDiff;
            if (_gitShowLog != null)
                _gitShowLog.Click -= OnGitShowLog;
            if (_gitRepoBrowser != null)
                _gitRepoBrowser.Click -= OnGitRepoBrowser;
            if (_gitCheckMod != null)
                _gitCheckMod.Click -= OnGitCheckForModifications;
            if (_gitRebase != null)
                _gitRebase.Click -= OnGitRebase;
            if (_gitResolve != null)
                _gitResolve.Click -= OnGitResolve;           
            if (_gitRevert != null)
                _gitRevert.Click -= OnGitRevert;
            if (_gitCleanUp != null)
                _gitCleanUp.Click -= OnGitCleanUp;
            if (_gitCheckout != null)
                _gitCheckout.Click -= OnGitCheckout;
            if(_gitMerge != null)
                _gitMerge.Click -= OnGitMerge;
            if (_gitCreateBranch != null)
                _gitCreateBranch.Click -= OnGitCreateBranch;
            if (_gitExport != null)
                _gitExport.Click -= OnGitExport;

            if (_gitSettings != null)
                _gitSettings.Click -= OnGitSettings;
            if (_gitAbout != null)
                _gitAbout.Click -= OnGitAbout;
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