using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.App.Tray.Properties;
using MeterKnife.Interfaces;

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
        private readonly ContextMenu _NotifyContextMenu = new ContextMenu();

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            _Notifyicon.Icon = Resources.mk_main;
            _Notifyicon.Visible = true;
            return true;
        }

        public bool CloseService()
        {
            _Notifyicon.Visible = false;
            return true;
        }

        public int Order { get; } = 0;
        public string Description { get; } = "程序托盘服务";

        #endregion


        /// <summary>
        /// 方法名称：notifyIconShow_SizeChanged(窗体大小改变后事件)
        /// 方法作用：隐藏任务栏图标，显示托盘图标
        /// 完成日期：2010年5月16日
        /// 作者：心语
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIconShow_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
//            if (WindowState == FormWindowState.Minimized)
//            {
//                //托盘显示图标等于托盘图标对象
//                //注意notifyIcon1是控件的名字而不是对象的名字
//                notifyIcon1.Icon = ico;
//                //隐藏任务栏区图标
//                this.ShowInTaskbar = false;
//                //图标显示在托盘区
//                _Notifyicon.Visible = true;
//            }
        }

    }
}
