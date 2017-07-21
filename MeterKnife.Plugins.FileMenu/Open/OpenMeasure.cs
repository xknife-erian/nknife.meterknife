using System;
using System.Windows.Forms;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu.Open
{
    public class OpenMeasure : IPlugIn
    {
        private readonly ToolStripMenuItem _StripItem = new ToolStripMenuItem("打开测量(&O)");

        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;
        public PluginDetail Detail { get; } = new PluginDetailKnife();

        public void BindViewComponent(PluginViewComponent component)
        {
            component.StripItemCollection.Add(_StripItem);
        }

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        public bool Register(ref IExtenderProvider provider)
        {
            return true;
        }

        /// <summary>
        ///     从扩展模组回收核心扩展供给器。
        /// </summary>
        public bool UnRegister()
        {
            throw new NotImplementedException();
        }
    }
}