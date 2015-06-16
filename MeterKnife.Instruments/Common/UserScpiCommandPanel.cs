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
                if (!string.IsNullOrEmpty(_CommandTextBox1.Text))
                    commands.Add(_CommandTextBox1.Text);
                if (!string.IsNullOrEmpty(_CommandTextBox2.Text))
                    commands.Add(_CommandTextBox2.Text);
                if (!string.IsNullOrEmpty(_CommandTextBox3.Text))
                    commands.Add(_CommandTextBox3.Text);
                if (!string.IsNullOrEmpty(_CommandTextBox4.Text))
                    commands.Add(_CommandTextBox4.Text);
                if (!string.IsNullOrEmpty(_CommandTextBox5.Text))
                    commands.Add(_CommandTextBox5.Text);
                if (!string.IsNullOrEmpty(_CommandTextBox6.Text))
                    commands.Add(_CommandTextBox6.Text);
                return commands;
            }
        }

        public List<string> CollectCommand
        {
            get
            {
                var commands = new List<string>(3);
                if (!string.IsNullOrEmpty(_CollectCommandBox1.Text))
                    commands.Add(_CollectCommandBox1.Text);
                if (!string.IsNullOrEmpty(_CollectCommandBox2.Text))
                    commands.Add(_CollectCommandBox2.Text);
                if (!string.IsNullOrEmpty(_CollectCommandBox3.Text))
                    commands.Add(_CollectCommandBox3.Text);
                return commands;
            }
        }

    }
}
