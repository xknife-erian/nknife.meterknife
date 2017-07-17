﻿using System;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu
{
    public class OpenMeasure : IPlugIn
    {
        private readonly ToolStripMenuItem _StripItem = new ToolStripMenuItem("打开测量(&O)");

        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;
        public PluginDetail Detail { get; }

        public void BindViewComponent(IPluginViewComponent component)
        {
            foreach (ToolStripItemCollection collection in component.ToolStripItemCollections)
            {
                collection.Add(_StripItem);
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
