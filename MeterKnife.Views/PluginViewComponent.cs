using System.Collections.Generic;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Views
{
    public class PluginViewComponent : IPluginViewComponent
    {
        private readonly List<ToolStripItemCollection> _ToolStripItemCollections = new List<ToolStripItemCollection>();
        private readonly List<Control> _Containers = new List<Control>();

        #region Implementation of IPluginViewComponent

        /// <summary>
        /// 插件的功能所能绑定的相应的菜单与工具条
        /// </summary>
        public ToolStripItemCollection[] ToolStripItemCollections => _ToolStripItemCollections.ToArray();

        /// <summary>
        /// 插件的功能界面的容器
        /// </summary>
        public Control[] Containers => _Containers.ToArray();

        #endregion

        public void Add(ToolStripItemCollection toolStripItemCollection)
        {
            _ToolStripItemCollections.Add(toolStripItemCollection);
        }

        public void Add(Control control)
        {
            _Containers.Add(control);
        }
    }
}