using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench
{
    /// <summary>
    /// 本应用程序实现的程序托盘的功能
    /// </summary>
    public class AppTrayService : IAppTrayService
    {
        /// <summary>
        /// 创建NotifyIcon对象
        /// </summary>
        private readonly NotifyIcon _notifyIcon = new NotifyIcon();

        private readonly IWorkbench _workbench;

        public AppTrayService(IWorkbench workbench, TrayMenuStrip notifyMenu)
        {
            _workbench = workbench;
            _notifyIcon.Icon = Resources.meterknife_24px;
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip = notifyMenu;
            _notifyIcon.ContextMenuStrip.Show();
            _notifyIcon.ContextMenuStrip.Close();
            _notifyIcon.MouseClick += NotifyIconMouseClick;
            _notifyIcon.MouseDoubleClick += NotifyIconMouseDoubleClick;
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        private void NotifyIconMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var form = (Form)_workbench;
                form.ShowInTaskbar = true;  //显示在系统任务栏
                form.WindowState = FormWindowState.Normal;  //还原窗体
                form.Show();
                form.Activate();
            }
        }

        private void NotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _notifyIcon.ContextMenuStrip.Show();
        }

        public bool CloseService()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            return true;
        }

        public int Order { get; } = 1;
        public string Description { get; } = "程序托盘服务";

        #endregion
    }
}
