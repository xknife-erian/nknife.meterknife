using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.ViewMenu.Loggers
{
    public class LoggerPanelView : PluginBase
    {
        public LoggerPanelView()
        {
            var view = DI.Get<LoggerView>();
            _StripItem.Text = "日志(&L)";
            _StripItem.Order = 99999990F;
            _StripItem.Click += (s, e) =>
            {
                foreach (var container in _ViewComponent.Containers)
                {
                    var panel = container as DockPanel;
                    if (panel != null)
                    {
                        var dockpanel = panel;
                        if (_StripItem.CheckState != CheckState.Checked)
                        {
                            view.Show(dockpanel, DockState.DockBottom);
                            _StripItem.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            view.Show(dockpanel, DockState.DockBottomAutoHide);
                            _StripItem.CheckState = CheckState.Unchecked;
                        }
                    }
                }
                view.FormClosing += (n, v) =>
                {
                    _StripItem.CheckState = CheckState.Unchecked;
                };
            };
        }

        #region Implementation of IPlugIn

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public override PluginStyle PluginStyle { get; } = PluginStyle.ViewMenu;

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
