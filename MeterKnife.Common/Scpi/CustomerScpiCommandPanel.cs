using System;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Common.Scpi
{
    public partial class CustomerScpiCommandPanel : UserControl
    {
        public int GpibAddress { get; set; }

        public CustomerScpiCommandPanel()
        {
            GpibAddress = 23;//测试使用
            InitializeComponent();
            var kernel = DI.Get<IMeterKernel>();
            kernel.Collected += (s, e) =>
            {
                if (e.GpibAddress == GpibAddress)
                    SetToolStripState(e.IsCollected);
            };
        }

        public ScpiCommandList GetCollectCommands()
        {
            return GetCommands("COLLECT");
        }

        public ScpiCommandList GetInitCommands()
        {
            return GetCommands("INIT");
        }

        protected virtual ScpiCommandList GetCommands(string groupName)
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

        protected void SetToolStripState(bool state)
        {
            _ListView.Enabled = !state;
            _ToolStrip.Enabled = !state;
        }

        private void _LoadButton_Click(object sender, EventArgs e)
        {

        }

        private void _SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void _AddInitButton_Click(object sender, EventArgs e)
        {

        }

        private void _AddCollectButton_Click(object sender, EventArgs e)
        {

        }

        private void _DeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void _EditButton_Click(object sender, EventArgs e)
        {

        }

        private void _DownButton_Click(object sender, EventArgs e)
        {

        }

        private void _UpButton_Click(object sender, EventArgs e)
        {

        }
    }
}
