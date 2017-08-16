using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.NLog.WinForm;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.ViewMenu.Loggers
{
    public partial class LoggerView : DockContent
    {
        public LoggerView()
        {
            InitializeComponent();
            var logPanel = LoggerListView.Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.Location = new Point(0, 0);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            logPanel.SetDebugMode(true);
            Controls.Add(logPanel);
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Closing" /> 事件。</summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.ComponentModel.CancelEventArgs" />。</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        #endregion
    }
}
