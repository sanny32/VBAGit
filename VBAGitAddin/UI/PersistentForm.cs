using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Win32;

namespace VBAGitAddin.UI
{
    public class PersistentForm : Form
    {
        public PersistWindowState windowState;

        public PersistentForm() : base()
        {
            windowState = new PersistWindowState(this, @"SOFTWARE\Microsoft\VBA\VBE\6.0\Addins\" + _Extension.ProgId + @"\Forms");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PersistentForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "PersistentForm";
            this.ResumeLayout(false);

        }
    }

    public class PersistWindowState : Component
    {
		// event info that allows form to persist extra window state data
		public delegate void WindowStateDelegate(object sender, RegistryKey key);
		public event WindowStateDelegate LoadStateEvent;
		public event WindowStateDelegate SaveStateEvent;

		private Form m_parent;
		private string m_regPath;
		private int m_normalLeft;
		private int m_normalTop;
		private int m_normalWidth;
		private int m_normalHeight;
		private FormWindowState m_windowState;
		private bool m_allowSaveMinimized = false;

		public PersistWindowState() : this(null, "", null, null)
		{
		}

        public PersistWindowState(Form parent, String registryPath) : this(parent, registryPath, null, null)
        {
        }

        public PersistWindowState(Form parent, String registryPath, WindowStateDelegate loadStateEvent, WindowStateDelegate saveStateEvent)
        {
            Parent = parent;
            RegistryPath = registryPath;
            LoadStateEvent = loadStateEvent;
            SaveStateEvent = saveStateEvent;
        }

		public Form Parent
		{
			set
			{
				m_parent = value;

				// subscribe to parent form's events
				m_parent.Closing += new CancelEventHandler(OnClosing);
				m_parent.Resize += new EventHandler(OnResize);
				m_parent.Move += new EventHandler(OnMove);
				m_parent.Load += new EventHandler(OnLoad);

				// get initial width and height in case form is never resized
				m_normalWidth = m_parent.Width;
				m_normalHeight = m_parent.Height;
			}
			get
			{
				return m_parent;
			}
		}

		// registry key should be set in parent form's constructor
		public string RegistryPath
		{
			set
			{
				m_regPath = value;		
			}
			get
			{
				return m_regPath;
			}
		}

		public bool AllowSaveMinimized
		{
			set
			{
				m_allowSaveMinimized = value;
			}
		}

		private void OnResize(object sender, EventArgs e)
		{
			// save width and height
			if(m_parent.WindowState == FormWindowState.Normal)
			{
				m_normalWidth = m_parent.Width;
				m_normalHeight = m_parent.Height;
			}
		}

		private void OnMove(object sender, EventArgs e)
		{
			// save position
			if(m_parent.WindowState == FormWindowState.Normal)
			{
				m_normalLeft = m_parent.Left;
				m_normalTop = m_parent.Top;
			}
			// save state
			m_windowState = m_parent.WindowState;
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
            if (!DesignMode)
            {
                // save position, size and state
                RegistryKey key = Registry.CurrentUser.CreateSubKey(m_regPath);
                key.SetValue(m_parent.Name + ".Left", m_normalLeft);
                key.SetValue(m_parent.Name + ".Top", m_normalTop);
                key.SetValue(m_parent.Name + ".Width", m_normalWidth);
                key.SetValue(m_parent.Name + ".Height", m_normalHeight);

                // check if we are allowed to save the state as minimized (not normally)
                if (!m_allowSaveMinimized)
                {
                    if (m_windowState == FormWindowState.Minimized)
                        m_windowState = FormWindowState.Normal;
                }

                key.SetValue(m_parent.Name + ".WindowState", (int)m_windowState);

                // Save the position of any split containers
                SaveSplitterDistance(m_parent, key);

                // fire SaveState event
                if (SaveStateEvent != null)
                    SaveStateEvent(this, key);
            }
		}

        private void OnLoad(object sender, EventArgs e)
		{
            if (!DesignMode)
            {
                // attempt to read state from registry
                RegistryKey key = Registry.CurrentUser.OpenSubKey(m_regPath);
                if (key != null)
                {
                    int left = (int)key.GetValue(m_parent.Name + ".Left", m_parent.Left);
                    int top = (int)key.GetValue(m_parent.Name + ".Top", m_parent.Top);
                    int width = (int)key.GetValue(m_parent.Name + ".Width", m_parent.Width);
                    int height = (int)key.GetValue(m_parent.Name + ".Height", m_parent.Height);
                    FormWindowState windowState = (FormWindowState)key.GetValue(m_parent.Name + ".WindowState", (int)m_parent.WindowState);

                    m_parent.Location = new Point(left, top);
                    m_parent.Size = new Size(width, height);
                    m_parent.WindowState = windowState;

                    // Restore the position of any split containers
                    LoadSplitterDistance(m_parent, key);

                    // fire LoadState event
                    if (LoadStateEvent != null)
                        LoadStateEvent(this, key);
                }
            }
		}

        /// <summary>
        /// Recursive function that walks the control tree saving any nested SplitContainers
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="key"></param>
        private void SaveSplitterDistance(Control parentControl, RegistryKey key)
        {
            if (parentControl is SplitContainer)
            {
                key.SetValue(m_parent.Name + "." + parentControl.Name + ".SplitterDistance", ((SplitContainer)(parentControl)).SplitterDistance);
            }
            
            if (parentControl.Controls.Count > 0)
            {
                foreach (Control childControl in parentControl.Controls)
                {
                    SaveSplitterDistance(childControl, key);
                }
            }
        }

        /// <summary>
        /// Recursive function that walks the control tree restoring any nested SplitContainers
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="key"></param>
        private void LoadSplitterDistance(Control parentControl, RegistryKey key)
        {
            if (parentControl is SplitContainer)
            {
                ((SplitContainer)(parentControl)).SplitterDistance = (int)key.GetValue(m_parent.Name + "." + parentControl.Name + ".SplitterDistance", ((SplitContainer)(parentControl)).SplitterDistance);
            }

            if (parentControl.Controls.Count > 0)
            {
                foreach (Control childControl in parentControl.Controls)
                {
                    LoadSplitterDistance(childControl, key);
                }
            }
        }

    }
}
