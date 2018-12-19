using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Base.Plugins
{
    /// <summary>
    /// 描述插件的功能所能绑定的相应的菜单与工具条；插件的功能界面的容器。
    /// </summary>
    public class PluginViewComponent
    {
        private readonly List<Control> _containers = new List<Control>();

        /// <summary>
        /// 插件的功能所能绑定的相应的菜单与工具条
        /// </summary>
        public ToolStripItemCollection StripItemCollection { get; private set; }

        /// <summary>
        /// 程序托盘菜单
        /// </summary>
        public ContextMenuStrip TrayMenu { get; private set; }

        /// <summary>
        /// 插件的功能界面的容器
        /// </summary>
        public Control[] Containers => _containers.ToArray();

        public void Set(ToolStripItemCollection collection, ContextMenuStrip contextMenu)
        {
            StripItemCollection = collection;
            TrayMenu = contextMenu;
        }

        public void Add(Control control)
        {
            _containers.Add(control);
        }
    }
}