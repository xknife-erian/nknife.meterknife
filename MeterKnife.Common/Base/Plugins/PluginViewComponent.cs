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
        private readonly List<Control> _Containers = new List<Control>();

        /// <summary>
        /// 插件的功能所能绑定的相应的菜单与工具条
        /// </summary>
        public ToolStripItemCollection StripItemCollection { get; private set; }

        /// <summary>
        /// 描述插件功能的可用性的变化状态的事件
        /// </summary>
        public Action<int, bool, bool, bool> SetAction { get; set; }

        /// <summary>
        /// 插件的功能界面的容器
        /// </summary>
        public Control[] Containers => _Containers.ToArray();

        public void Set(ToolStripItemCollection toolStripItemCollection)
        {
            StripItemCollection = toolStripItemCollection;
        }

        public void Add(Control control)
        {
            _Containers.Add(control);
        }
    }
}