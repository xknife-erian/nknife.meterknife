using MeterKnife.Interfaces.Plugins;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Base.Plugins
{
    public abstract class PluginBase : IPlugIn
    {
        protected readonly OrderToolStripMenuItem _StripItem = new OrderToolStripMenuItem();
        protected PluginViewComponent _ViewComponent;
        protected IExtenderProvider _ExtenderProvider;

        #region Implementation of IPlugIn

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public abstract PluginStyle PluginStyle { get; }

        /// <summary>
        ///     插件的详细描述
        /// </summary>
        public abstract PluginDetail Detail { get; }

        /// <summary>
        ///     将本插件的功能绑定于相应的菜单与工具条上，绑定需要呈现的控件到相应的界面组件上。
        /// </summary>
        /// <param name="component"></param>
        public virtual void BindViewComponent(PluginViewComponent component)
        {
            _ViewComponent = component;
            _ViewComponent.StripItemCollection.Add(_StripItem);
        }

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        public virtual bool Register(ref IExtenderProvider provider)
        {
            _ExtenderProvider = provider;
            return OnProviderRegistered();
        }

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        protected abstract bool OnProviderRegistered();

        /// <summary>
        ///     从扩展模组回收核心扩展供给器。
        /// </summary>
        public abstract bool UnRegister();

        #endregion

        protected void ShowAtDockPanel(DockContent view)
        {
            foreach (var container in _ViewComponent.Containers)
            {
                var panel = container as DockPanel;
                if (panel != null)
                {
                    var dockpanel = panel;
                    view.Show(dockpanel, DockState.Document);
                }
            }
        }
    }
}