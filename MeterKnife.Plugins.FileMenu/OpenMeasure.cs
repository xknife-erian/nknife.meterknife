using System;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu
{
    public class OpenMeasure : IPlugIn
    {
        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;
        public PluginDetail Detail { get; }
        public void BindViewComponent(IPluginViewComponent component)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        public bool Register(ref IExtenderProvider provider)
        {
            throw new NotImplementedException();
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
