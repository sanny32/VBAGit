using Microsoft.Vbe.Interop;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VBAGitAddin.UI.Extensions;
using VBAGitAddin.VBEditor.Extensions;

namespace VBAGitAddin.VBEditor
{
    public enum ProjectFolder
    {
        None = 0,
        Objects,
        Forms,
        Modules,
        ClassModules,
        References
    }

    public class ProjectExplorerTreeViewItem
    {
        public ProjectExplorerTreeViewItem()
        {         
        }

        public IntPtr Handle { get; set; }
        public string Text { get; set; }
        public ProjectFolder Folder { get; set; }              
        public IEnumerable<string> SelectedComponents { get; set; }
    }

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
        private const int TVGN_PARENT = 0x3;
        private const int TVGN_CHILD = 0x4;
        private const int TVGN_FIRSTVISIBLE = 0x5;
        private const int TVGN_NEXTVISIBLE = 0x6;
        private const int TVGN_CARET = 0x9;

        private const int TVM_SELECTITEM = (TV_FIRST + 11);
        private const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        private const int TVM_GETITEM = (TV_FIRST + 12);

        private const int NM_FIRST = 0;
        private const int NM_RCLICK = (NM_FIRST - 5);

        private const int TVN_FIRST = -400;
        private const int TVN_SELCHANGING = (TVN_FIRST - 1);
        private const int TVN_SELCHANGED = (TVN_FIRST - 2);

        private const int TVIF_TEXT = 0x1;

        private const int WM_CONTEXTMENU = 0x007B;        
        private const int WM_NOTIFY = 0x004E;
        private const int WM_REFLECT = 0x2000;
        

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
      
        private const string Node_Forms = "Forms";
        private const string Node_Modules = "Modules";
        private const string Node_ClassModules = "Class Modules";
        private const string Node_References = "References";       

        private readonly VBE _vbe;
        private readonly IntPtr _hTreeView;

        public event EventHandler OnContextMenu;
        public event EventHandler OnSelectionChanged;

        public ProjectExplorerTreeView(VBE vbe)
        {
            _vbe = vbe;

            NativeWindow window = new NativeWindow();
            window.AssignHandle(new IntPtr(vbe.MainWindow.HWnd));

            var hPROJECT = window.FindChildWindow("PROJECT");
            AssignHandle(hPROJECT);

            window.ReleaseHandle();

            _hTreeView = this.FindChildWindow("SysTreeView32");            
        }

        public new IntPtr Handle
        {
            get
            {
                return _hTreeView;
            }
        }

        public string Node_Project
        {
            get
            {
                var project = _vbe.ActiveVBProject;
                return string.Format("{0} ({1})", project.Name, project.BuildFileName);
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NOTIFY:
                    var nmhdr = (NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NMHDR));
                    switch(nmhdr.code)
                    {
                        case NM_RCLICK:
                            OnContextMenu.Raise(this, new EventArgs());
                            break;

                       case TVN_SELCHANGED:
                            OnSelectionChanged.Raise(this, new EventArgs());
                            break;
                    }                   
                    break;

                case WM_CONTEXTMENU:
                    OnContextMenu.Raise(this, new EventArgs());
                    break;
            }

            base.WndProc(ref m);
        }

        public string GetItemText(IntPtr handle)
        {            
            TVITEM tvi = new TVITEM();

            const int maxSize = 260;
            IntPtr pszText = LocalAlloc(0x40, maxSize);

            tvi.mask = TVIF_TEXT;
            tvi.hItem = handle;
            tvi.cchTextMax = 260;
            tvi.pszText = pszText;

            SendMessageTVI(Handle, TVM_GETITEM, IntPtr.Zero, ref tvi);
            string nodeText = Marshal.PtrToStringAnsi(tvi.pszText, maxSize);

            LocalFree(pszText);

            return nodeText?.TrimEnd('\0');
        }

        public ProjectExplorerTreeViewItem GetSelectedItem()
        {
            ProjectExplorerTreeViewItem item = new ProjectExplorerTreeViewItem();

            item.Handle = SendMessage(Handle, TVM_GETNEXTITEM, new IntPtr(TVGN_CARET), IntPtr.Zero);            
            item.Text = GetItemText(item.Handle);           
            item.Folder = GetItemFolder(item.Text);

            List<string> listOfComponents = new List<string>();

            if (item.Folder == ProjectFolder.None)
            {
                IntPtr hParent = SendMessage(Handle, TVM_GETNEXTITEM, new IntPtr(TVGN_PARENT), item.Handle);
                string parentText = GetItemText(hParent);
                item.Folder = GetItemFolder(parentText);

                if (item.Folder != ProjectFolder.None)
                {
                    listOfComponents.Add(item.Text);
                    item.SelectedComponents = listOfComponents;
                }
            }
            else
            {
                IEnumerable<VBComponent> components = null;
                switch (item.Folder)
                {
                    case ProjectFolder.None:
                        break;

                    case ProjectFolder.Objects:
                        components = _vbe.ActiveVBProject.SelectComponents(vbext_ComponentType.vbext_ct_Document);
                        break;

                    case ProjectFolder.Forms:
                        components = _vbe.ActiveVBProject.SelectComponents(vbext_ComponentType.vbext_ct_MSForm);
                        break;

                    case ProjectFolder.Modules:
                        components = _vbe.ActiveVBProject.SelectComponents(vbext_ComponentType.vbext_ct_StdModule);
                        break;

                    case ProjectFolder.ClassModules:
                        components = _vbe.ActiveVBProject.SelectComponents(vbext_ComponentType.vbext_ct_ClassModule);
                        break;
                }

                if (components != null)
                {
                    listOfComponents.AddRange(components.Select(c => c.Name));
                    item.SelectedComponents = listOfComponents;
                }
            }                               

            return item;
        }        
      
        private ProjectFolder GetItemFolder(string text)
        {
            if (text.Contains("Objects"))
            {
                return ProjectFolder.Objects;
            }
            else
            {
                switch (text)
                {
                    case Node_Forms:
                        return ProjectFolder.Forms;

                    case Node_Modules:
                        return ProjectFolder.Modules;

                    case Node_ClassModules:
                        return ProjectFolder.ClassModules;

                    case Node_References:
                        return ProjectFolder.References;

                    default:
                        return ProjectFolder.None;
                }
            }
        }

        public void Dispose()
        {
            ReleaseHandle();
        }
    }
}
