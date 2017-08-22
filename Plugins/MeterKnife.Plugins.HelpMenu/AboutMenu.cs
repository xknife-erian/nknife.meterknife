using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.HelpMenu
{
    /// <summary>
    /// 插件："新建测量"功能；
    /// </summary>
    public class AboutMenu : PluginBase
    {
        public AboutMenu()
        {
            _StripItem.Text = "关于(&A)";
            _StripItem.Order = 100F;
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
        public override PluginStyle PluginStyle { get; } = PluginStyle.HelpMenu;

        /// <summary>
        ///     插件的详细描述
        /// </summary>
        public override PluginDetail Detail { get; } = new PluginDetailKnife();

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        protected override bool OnProviderRegistered()
        {
            return true;
        }

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
