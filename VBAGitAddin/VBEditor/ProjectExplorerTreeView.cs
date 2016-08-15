using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VBAGitAddin.UI.Extensions;

namespace VBAGitAddin.VBEditor
{
    public class ProjectExplorerTreeView : NativeWindow, IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessageTVI(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref TVITEM tvi);

        [DllImport("kernel32.dll")]
        static extern IntPtr LocalAlloc(uint flags, uint cb);

        [DllImport("kernel32.dll")]
        static extern IntPtr LocalFree(IntPtr p);

        private const int TV_FIRST = 0x1100;

        private const int TVGN_ROOT = 0x0;
        private const int TVGN_NEXT = 0x1;
        private const int TVGN_CHILD = 0x4;
        private const int TVGN_FIRSTVISIBLE = 0x5;
        private const int TVGN_NEXTVISIBLE = 0x6;
        private const int TVGN_CARET = 0x9;

        private const int TVM_SELECTITEM = (TV_FIRST + 11);
        private const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        private const int TVM_GETITEM = (TV_FIRST + 12);

        private const int TVIF_TEXT = 0x1;

        private const int WM_CONTEXTMENU = 0x007B;
        private const int WM_RBUTTONDOWN = 0x0204;

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        private struct TVITEM
        {
            public uint mask;
            public IntPtr hItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }     

        public const string Node_Forms = "Forms";
        public const string Node_Modules = "Modules";
        public const string Node_ClassModules = "Class Modules";
        public const string Node_References = "References";       

        private readonly VBE _vbe;

        public event EventHandler OnContextMenu;

        public ProjectExplorerTreeView(VBE vbe)
        {
            _vbe = vbe;

            NativeWindow window = new NativeWindow();
            window.AssignHandle(new IntPtr(vbe.MainWindow.HWnd));

            var hTreeView = window.FindChildWindow("SysTreeView32");
            AssignHandle(hTreeView);

            window.ReleaseHandle();
        }

        protected override void WndProc(ref Message m)
        {          
            switch (m.Msg)
            {               
                case WM_CONTEXTMENU:
                    OnContextMenu.Raise(this, new EventArgs());
                    break;
            }

            base.WndProc(ref m);
        }

        public string GetSelectedItemText()
        {
            IntPtr selectedItem = SendMessage(Handle, TVM_GETNEXTITEM, new IntPtr(TVGN_CARET), IntPtr.Zero);

            TVITEM tvi = new TVITEM();

            const int maxSize = 260;
            IntPtr pszText = LocalAlloc(0x40, maxSize);

            tvi.mask = TVIF_TEXT;
            tvi.hItem = selectedItem;
            tvi.cchTextMax = 260;
            tvi.pszText = pszText;

            SendMessageTVI(Handle, TVM_GETITEM, IntPtr.Zero, ref tvi);
            string nodeText = Marshal.PtrToStringAnsi(tvi.pszText, maxSize);

            LocalFree(pszText);

            return nodeText?.TrimEnd('\0');
        }

        public IEnumerable<string> GetComponents()
        {
            List<string> items = new List<string>();

            string selectedItem = GetSelectedItemText();
            switch(selectedItem)
            {
                case Node_Forms:
                    break;

                case Node_Modules:
                    break;

                case Node_ClassModules:
                    break;
            }

            return items;
        }

        public void Dispose()
        {
            ReleaseHandle();
        }
    }
}
