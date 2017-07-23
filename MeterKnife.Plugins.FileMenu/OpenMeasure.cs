using System;
using System.Windows.Forms;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu
{
    public class OpenMeasure : IPlugIn
    {
        private readonly ToolStripMenuItem _StripItem = new ToolStripMenuItem("打开测量(&O)");

        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;
        public PluginDetail Detail { get; } = new PluginDetailKnife();

        public OpenMeasure()
        {
            _StripItem.ShortcutKeys = Keys.Control | Keys.O;
        }

        public void BindViewComponent(PluginViewComponent component)
        {
            var collection = component.StripItemCollection;
            if (collection.Count > 2)
            {
                var item = collection[0];
                collection.Insert(item.Text.Contains("N") ? 1 : 0, _StripItem);
            }
            else
            {
                collection.Insert(0, _StripItem);
            }
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