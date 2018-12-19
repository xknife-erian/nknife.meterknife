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
        private readonly NotifyIcon _notifyicon = new NotifyIcon();

        /// <summary>
        /// 创建托盘菜单对象
        /// </summary>
        private readonly ContextMenuStrip _notifyContextMenu = DI.Get<TrayMenuStrip>();

        public AppTrayService()
        {
            _notifyicon.Icon = Resources.mk_main;
            _notifyicon.Visible = true;
            _notifyicon.ContextMenuStrip = _notifyContextMenu;
            _notifyicon.ContextMenuStrip.Show();
            _notifyicon.ContextMenuStrip.Close();
            _notifyicon.MouseClick += _Notifyicon_MouseClick;
            _notifyicon.MouseDoubleClick += _Notifyicon_MouseDoubleClick;
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        private void _Notifyicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var form = (Form) DI.Get<IWorkbench>();
                form.ShowInTaskbar = true;  //显示在系统任务栏
                form.WindowState = FormWindowState.Normal;  //还原窗体
                form.Activate();
            }
        }

        private void _Notifyicon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _notifyicon.ContextMenuStrip.Show();
        }

        public bool CloseService()
        {
            _notifyicon.Visible = false;
            return true;
        }

        public int Order { get; } = 0;
        public string Description { get; } = "程序托盘服务";

        #endregion
    }
}
