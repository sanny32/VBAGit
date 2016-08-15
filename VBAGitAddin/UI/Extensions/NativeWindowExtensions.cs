using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace VBAGitAddin.UI.Extensions
{
    public static class NativeWindowExtensions
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="window"></param>
        /// <returns>List of child windows</returns>
        public static List<IntPtr> GetChildWindows(this NativeWindow window)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc((IntPtr handle, IntPtr pointer) =>
                {
                    GCHandle gch = GCHandle.FromIntPtr(pointer);
                    List<IntPtr> list = gch.Target as List<IntPtr>;
                    if (list == null)
                    {
                        throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
                    }
                    list.Add(handle);
                    //  You can modify this to check to see if you want to cancel the operation, then return a null here
                    return true;
                });

                EnumChildWindows(window.Handle, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        /// <summary>
        /// Find child window by class name
        /// </summary>
        /// <param name="window"></param>
        /// <param name="className">Class name of finding window</param>
        /// <returns></returns>
        public static IntPtr FindChildWindow(this NativeWindow window, string className)
        {
            IntPtr ptrWindow = IntPtr.Zero;

            try
            {
                EnumWindowProc childProc = new EnumWindowProc((IntPtr handle, IntPtr pointer) =>
                {
                    NativeWindow wnd = new NativeWindow();
                    wnd.AssignHandle(handle);

                    if (wnd.GetClassName() == className)
                    {
                        ptrWindow = handle;
                    }                 

                    wnd.ReleaseHandle();

                    return ptrWindow != handle;
                });

                EnumChildWindows(window.Handle, childProc, IntPtr.Zero);                
            }
            finally
            {               
            }

            return ptrWindow;
        }

        public static string GetClassName(this NativeWindow window)
        {
            // Pre-allocate 256 characters, since this is the maximum class name length.
            StringBuilder className = new StringBuilder(256);

            //Get the window class name
            int nRet = GetClassName(window.Handle, className, className.Capacity);
            if (nRet == 0)
            {
                throw new InvalidOperationException("GetClassName returns an error.");
            }

            return className.ToString();
        }              

        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);
    }
}
