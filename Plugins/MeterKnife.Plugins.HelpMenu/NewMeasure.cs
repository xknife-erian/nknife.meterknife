using System.Windows.Forms;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.HelpMenu
{
    /// <summary>
    /// 插件："新建测量"功能；
    /// </summary>
    public class About : IPlugIn
    {
        private readonly ToolStripMenuItem _StripItem = new ToolStripMenuItem("关于(&A)");
        private PluginViewComponent _ViewComponent;
        private IExtenderProvider _ExtenderProvider;

        public About()
        {
            _StripItem.Click += (s, e) =>
            {
                var dialog = new AboutDialog();
                dialog.ShowDialog();
            };
        }

        #region Implementation of IPlugIn
            
        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public PluginStyle PluginStyle { get; } = PluginStyle.HelpMenu;

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
            var collection = component.StripItemCollection;
            if (collection.Count > 0)
                collection.Add(new ToolStripSeparator());
            collection.Add(_StripItem);
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
