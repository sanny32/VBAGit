using System;
using System.Windows.Forms;

namespace VBAGitAddin.VBEditor
{
    public class SubClassingWindowEventArgs : EventArgs
    {
        private readonly Message _msg;

        public Message Message
        {
            get { return _msg; }
        }

        public SubClassingWindowEventArgs(Message msg)
        {
            _msg = msg;
        }
    }
}