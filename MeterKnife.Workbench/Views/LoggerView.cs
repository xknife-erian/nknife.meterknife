using System.Drawing;
using System.Windows.Forms;
using NKnife.NLog.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Workbench.Views
{
    public partial class LoggerView : DockContent
    {
        public LoggerView()
        {
            InitializeComponent();

            SuspendLayout();
            LogPanel logPanel = LogPanel.Instance;

            // 
            // logPanel1
            // 
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.HeaderStyle = ColumnHeaderStyle.Clickable;
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "logPanel1";
            logPanel.Size = new Size(784, 262);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            // 
            // LoggerView
            // 
            Controls.Add(logPanel);
            ResumeLayout(false);
        }
    }
}