using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.ToolsMenu
{
    public class KeysightChannelTool : PluginBase
    {
        public KeysightChannelTool()
        {
            _StripItem.Text = "Aglient 82357x工具(&G)";
            _StripItem.Order = 10F;
            _StripItem.Click += (s, e) =>
            {
                var view = new KeysightChannelToolView();
                ShowAtDockPanel(view);
            };
        }

        #region Overrides of PluginBase

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public override PluginStyle PluginStyle { get; } = PluginStyle.ToolMenu;

        /// <summary>
        ///     插件的详细描述
        /// </summary>
        public override PluginDetail Detail { get; } = new PluginDetailKnife();

        /// <summary>
        ///     从扩展模组回收核心扩展供给器。
        /// </summary>
        public override bool UnRegister()
        {
            return true;
        }

        #endregion
    }
}
