using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using VBAGitAddin.VBEditor;
using VBAGitAddin.Settings;

namespace VBAGitAddin.UI
{
    internal class VBAGitAddinMenu : Menu
    {       
        private readonly AddIn _addIn;      
        private CommandBarButton _sourceControl;

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

            _sourceControl = AddButton(_menu, VBAGitUI.VBAGitMenu_SourceControl, false, OnSourceControlClick);            
        }

        private App _sourceControlApp;
        //I'm not the one with the bad name, MS is. Signature must match delegate definition.
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void OnSourceControlClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (_sourceControlApp == null)
            {
                _sourceControlApp = new App(this.IDE, this.AddIn, new SourceControlConfigurationService(), 
                                                                new ChangesControl(), new UnSyncedCommitsControl(),
                                                                new SettingsControl(), new BranchesControl(),
                                                                new CreateBranchForm(), new DeleteBranchForm(),
                                                                new MergeForm());
            }

            _sourceControlApp.ShowWindow();
        }
       

        private bool _disposed;
        private CommandBarPopup _menu;

        protected override void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }
          
            _sourceControl.Click -= OnSourceControlClick;

            var menuBarControls = IDE.CommandBars[1].Controls;
            menuBarControls.Parent.FindControl(_menu.Type, _menu.Id, _menu.Tag, _menu.Visible).Delete();

            _disposed = true;
            base.Dispose(true);
        }
    }
}