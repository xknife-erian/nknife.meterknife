using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Views.InstrumentsDiscovery;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.FileMenu
{
    public class InstrumentsDiscovery : IPlugIn
    {
        private readonly OrderToolStripMenuItem _StripItem = new OrderToolStripMenuItem("仪器管理(&I)");
        private PluginViewComponent _ViewComponent;
        private IExtenderProvider _ExtenderProvider;

        public InstrumentsDiscovery()
        {
            _StripItem.Order = 100F;
            _StripItem.ShortcutKeys = Keys.Control | Keys.I;
            _StripItem.Click += (s, e) =>
            {
                var view = new InstrumentsDiscoveryView();
                view.SetProvider(_ExtenderProvider);
                foreach (var container in _ViewComponent.Containers)
                {
                    var panel = container as DockPanel;
                    if (panel != null)
                    {
                        var dockpanel = panel;
                        view.Show(dockpanel, DockState.Document);
                    }
                }
            };
        }

        #region Implementation of IPlugIn

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;

        /// <summary>
        ///     插件的详细描述
        /// </summary>
        public PluginDetail Detail { get; } = new PluginDetailKnife();

        /// <summary>
        ///     将本插件的功能绑定于相应的菜单与工具条上，绑定需要呈现的控件到相应的界面组件上。
        /// </summary>
        /// <param name="component"></param>
        public void BindViewComponent(PluginViewComponent component)
        {
            _ViewComponent = component;
            component.StripItemCollection.Add(_StripItem);
        }

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        public bool Register(ref IExtenderProvider provider)
        {
            _ExtenderProvider = provider;
            return true;
        }

        /// <summary>
        ///     从扩展模组回收核心扩展供给器。
        /// </summary>
        public bool UnRegister()
        {
            return true;
        }

        #endregion
    }
}
