using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    /// 描述插件的功能所能绑定的相应的菜单与工具条；插件的功能界面的容器。
    /// </summary>
    public interface IPluginViewComponent
    {
        /// <summary>
        /// 插件的功能所能绑定的相应的菜单与工具条
        /// </summary>
        ToolStripItemCollection[] ToolStripItemCollections { get; }

        /// <summary>
        /// 插件的功能界面的容器
        /// </summary>
        Control[] Containers { get; }
    }
}
