using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;
using NKnife.ControlKnife;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.ToolsMenu.Updates
{
    public class UpdateManager : PluginBase
    {
        public UpdateManager()
        {
            _StripItem.Text = "扩展与更新(&U)";
            _StripItem.Order = 0F;
            _StripItem.Click += (s, e) =>
            {
                var view = new UpdatesForm();
                Form parentForm = null;
                foreach (var container in _ViewComponent.Containers)
                {
                    var panel = container as DockPanel;
                    if (panel != null)
                    {
                        parentForm = panel.FindForm();
                        break;
                    }
                }
                DialogResult dr = (parentForm != null) ? view.ShowDialog(parentForm) : view.ShowDialog();
                if (dr == DialogResult.OK)
                {
                }
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
