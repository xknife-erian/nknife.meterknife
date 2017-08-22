using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu
{
    public class OpenMeasure : PluginBase
    {
        public OpenMeasure()
        {
            _StripItem.Text = "打开测量(&O)";
            _StripItem.Order = 1F;
            _StripItem.ShortcutKeys = Keys.Control | Keys.O;
        }

        public override PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;
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
    }
}