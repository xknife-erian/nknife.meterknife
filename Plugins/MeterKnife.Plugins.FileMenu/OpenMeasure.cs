using System;
using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu
{
    public class OpenMeasure : IPlugIn
    {
        private readonly OrderToolStripMenuItem _StripItem = new OrderToolStripMenuItem("打开测量(&O)");

        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;
        public PluginDetail Detail { get; } = new PluginDetailKnife();

        public OpenMeasure()
        {
            _StripItem.Order = 1;
            _StripItem.ShortcutKeys = Keys.Control | Keys.O;
        }

        public void BindViewComponent(PluginViewComponent component)
        {
            var collection = component.StripItemCollection;
            collection.Add(_StripItem);
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