using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VBAGitAddin.UI.Extensions;

namespace VBAGitAddin.UI
{    
    internal class RichTextBoxTraceListener : TraceListener
    {
        private readonly RichTextBox _richTextBox;

        public RichTextBoxTraceListener(RichTextBox richTextBox)
        {
            this.Name = "Trace";            
            _richTextBox = richTextBox;
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            switch (eventType)
            {
                case TraceEventType.Error:
                case TraceEventType.Critical:
                    Write(Environment.NewLine + message + Environment.NewLine, Color.Red);
                    break;

                case TraceEventType.Start:
                case TraceEventType.Stop:
                    Write(Environment.NewLine + message + Environment.NewLine, Color.Blue);
                    break;

                case TraceEventType.Warning:
                    Write(Environment.NewLine + message + Environment.NewLine, Color.Yellow);
                    break;

                default:
                    this.WriteLine(message);
                    break;
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            TraceEvent(eventCache, source, eventType, id, string.Format(format, args));
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            WriteLine(string.Empty);
        }                       

        public override void Write(string message)
        {
            Write(message, Color.Black);
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        private void Write(string message, Color color)
        {
            Action append = delegate ()
            {
                _richTextBox.AppendText(message, color);
            };

            if (_richTextBox.InvokeRequired)
            {
                var result = _richTextBox.BeginInvoke(append);
                _richTextBox.EndInvoke(result);
            }
            else
            {
                append();
            }
        }
    }
}
