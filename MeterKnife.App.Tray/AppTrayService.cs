﻿using System;
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
    public class AppTrayService : IAppTrayService
    {
        /// <summary>
        /// 创建NotifyIcon对象
        /// </summary>
        private readonly NotifyIcon _Notifyicon = new NotifyIcon();

        /// <summary>
        /// 创建托盘菜单对象
        /// </summary>
        private readonly ContextMenuStrip _NotifyContextMenu = DI.Get<TrayMenuStrip>();

        public AppTrayService()
        {
            _Notifyicon.Icon = Resources.mk_main;
            _Notifyicon.Visible = true;
            _Notifyicon.ContextMenuStrip = _NotifyContextMenu;
            _Notifyicon.MouseClick += _Notifyicon_MouseClick;
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        private void _Notifyicon_MouseClick(object sender, MouseEventArgs e)
        {
            _Notifyicon.ContextMenuStrip.Show();
        }

        public bool CloseService()
        {
            _Notifyicon.Visible = false;
            return true;
        }

        public int Order { get; } = 0;
        public string Description { get; } = "程序托盘服务";

        #endregion
    }
}
