using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Views;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.FileMenu
{
    public class InstrumentsDiscovery : PluginBase
    {
        public InstrumentsDiscovery()
        {
            _StripItem.Text = "仪器管理(&I)";
            _StripItem.Order = 100F;
            _StripItem.ShortcutKeys = Keys.Control | Keys.I;
            _StripItem.Click += (s, e) =>
            {
                var view = (DockContent)DI.Get<IViewsManager>().InstrumentsDiscoveryView;
                ShowAtDockPanel(view);
            };
        }

        #region Implementation of IPlugIn

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public override PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;

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
