using System.Windows.Forms;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.FileMenu.New
{
    /// <summary>
    /// 插件："新建测量"功能；
    /// </summary>
    public class NewMeasure : IPlugIn
    {
        private readonly ToolStripMenuItem _StripItem = new ToolStripMenuItem("新建测量(&N)");
        private PluginViewComponent _ViewComponent;
        private IExtenderProvider _ExtenderProvider;

        public NewMeasure()
        {
            _StripItem.Click += (s, e) =>
            {
                var view = new MeasureView();
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
