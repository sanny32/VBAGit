using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Extensibility;
using Microsoft.Vbe.Interop;
using VBAGitAddin.UI;

namespace VBAGitAddin
{
    [ComVisible(true)]
    [Guid(ClassId)]
    [ProgId(ProgId)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    // ReSharper disable once InconsistentNaming
    public class _Extension : IDTExtensibility2, IDisposable
    {
        private const string ClassId = "C723A557-FD00-4C16-AE87-E8D31DC164F9";
        private const string ProgId = "VBAGitAddin.Connect";

        private App _app;

        public void OnAddInsUpdate(ref Array custom)
        {
        }

        public void OnBeginShutdown(ref Array custom)
        {
        }

        public void OnConnection(object Application, ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
        {
            try
            {
                _app = new App((VBE)Application, (AddIn)AddInInst);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, VBAGitUI.VBAGitLoadFailure, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnStartupComplete(ref Array custom)
        {

        }

        public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom)
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing & _app != null)
            {
                _app.Dispose();
            }
        }
    }
}
