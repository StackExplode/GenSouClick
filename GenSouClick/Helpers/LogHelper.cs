using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenSouClick.Helpers
{
    class LogHelper
    {
        private RichTextBox _textbox;
        public LogHelper(RichTextBox txt)
        {
            _textbox = txt;
        }

        protected void AppendText(string s, Color cl, bool isdate = true)
        {
            _textbox.SelectionColor = cl;
            if (isdate)
            {
                _textbox.AppendText("[");
                _textbox.AppendText(DateTime.Now.ToLongTimeString());
                _textbox.AppendText("] ");
            }
            _textbox.AppendText(s);
            _textbox.AppendText(Environment.NewLine);
            _textbox.ScrollToCaret();
        }

        public void WriteInfo(string s)
        {
            AppendText(s, Color.White);
        }

        public void WriteWarnning(string s)
        {
            AppendText(s, Color.Orange);
        }

        public void WriteError(string s)
        {
            AppendText(s, Color.Red);
        }

        public void WriteDebug(string s)
        {
            AppendText(s, Color.Gray);
        }

        public void Write(string s, int level)
        {
            Color cl;
            switch (level)
            {
                case 1: cl = Color.White; break;
                case 2: cl = Color.Orange; break;
                case 3: cl = Color.Red; break;
                case 4: cl = Color.Gray; break;
                default: cl = Color.Pink; break;
            }
            AppendText(s, cl);
        }
    }
}
