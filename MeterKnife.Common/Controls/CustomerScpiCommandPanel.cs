using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScpiKnife;

namespace MeterKnife.Common.Controls
{
    public partial class CustomerScpiCommandPanel : UserControl
    {
        public CustomerScpiCommandPanel()
        {
            InitializeComponent();
        }

        public ScpiCommandList GetCollectCommands()
        {
            return GetCommands("COLLECT");
        }

        public ScpiCommandList GetInitCommands()
        {
            return GetCommands("INIT");
        }

        private ScpiCommandList GetCommands(string groupName)
        {
            var commands = new ScpiCommandList();
            foreach (ListViewItem item in _ListView.Items)
            {
                if (item.Checked && item.Group.Name == groupName)
                {
                    commands.AddLast(new ScpiCommand()
                    {
                        Command = item.SubItems[0].Text,
                        Interval = int.Parse(item.SubItems[1].Text)
                    });
                }
            }
            return commands;
        }
    }
}
