using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Instruments.Common
{
    public partial class UserScpiCommandPanel : UserControl
    {
        public UserScpiCommandPanel()
        {
            InitializeComponent();
        }

        public List<string> InitCommands
        {
            get
            {
                var commands = new List<string>(6);
                string text = GetText(_CommandTextBox1);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CommandTextBox2);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CommandTextBox3);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CommandTextBox4);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CommandTextBox5);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CommandTextBox6);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                return commands;
            }
        }

        public List<string> CollectCommands
        {
            get
            {
                var commands = new List<string>(3);
                string text = GetText(_CollectCommandBox1);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CollectCommandBox2);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                text = GetText(_CollectCommandBox3);
                if (!string.IsNullOrEmpty(text))
                    commands.Add(text);
                return commands;
            }
        }

        private string GetText(Control control)
        {
            string text = "";
            this.ThreadSafeInvoke(() => text = control.Text);
            return text;
        }

    }
}
