using System;
using System.Drawing;
using Microsoft.Vbe.Interop;
using VBAGitAddin.UI;
using VBAGitAddin.VBEditor;

namespace VBAGitAddin
{
    public class App : IDisposable
    {
        private readonly VBE _vbe;
        private readonly AddIn _addIn;
        private VBAGitAddinMenu _menu;
        private readonly ActiveCodePaneEditor _editor;

        private bool displayToolbar = false;
        private Point toolbarCoords = new Point(-1, -1);

        public App(VBE vbe, AddIn addIn)
        {
            _vbe = vbe;
            _addIn = addIn;            

            _editor = new ActiveCodePaneEditor(vbe);

            CleanUp();

            Setup();
        }

        private void _configService_SettingsChanged(object sender, EventArgs e)
        {
            CleanUp();

            Setup();
        }        

        private void Setup()
        {            
            _menu = new VBAGitAddinMenu(_vbe, _addIn, _editor);
            _menu.Initialize();
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) { return; }

            CleanUp();
        }

        private void CleanUp()
        {
            if (_menu != null)
            {
                _menu.Dispose();
            }          
        }
    }
}
