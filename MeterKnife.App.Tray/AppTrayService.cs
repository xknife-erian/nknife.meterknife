using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.App.Tray.Properties;
using MeterKnife.Base;
using MeterKnife.Interfaces;
using NKnife.IoC;

namespace MeterKnife.App.Tray
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

        /// <summary>
        /// 创建托盘菜单对象
        /// </summary>
        private readonly ContextMenuStrip _notifyContextMenu = DI.Get<TrayMenuStrip>();

        public AppTrayService()
        {
            _notifyIcon.Icon = Resources.mk_main;
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip = _notifyContextMenu;
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
                var form = (Form) DI.Get<IWorkbench>();
                form.ShowInTaskbar = true;  //显示在系统任务栏
                form.WindowState = FormWindowState.Normal;  //还原窗体
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
            return true;
        }

        public int Order { get; } = 0;
        public string Description { get; } = "程序托盘服务";

        #endregion
    }
}
