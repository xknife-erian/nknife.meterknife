using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.IoC;
using NKnife.NLog3.Properties;

namespace NKnife.NLog3.Controls
{
    public partial class NLogForm : Form
    {
        public NLogForm()
        {
            InitializeComponent();
            Icon = OwnResources.NLogForm;
            Padding = new Padding(3);
            var logPanel = LogPanel.Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.HeaderStyle = ColumnHeaderStyle.Clickable;
            Controls.Add(logPanel);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int h = Screen.PrimaryScreen.Bounds.Height;
            Top = h - Height - 40;

            int w = Screen.PrimaryScreen.Bounds.Width;
            Left = w - Width - 2;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}