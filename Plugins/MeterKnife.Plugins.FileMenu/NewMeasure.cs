using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Views;
using MeterKnife.Views.Measures;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.FileMenu
{
    /// <summary>
    /// 插件："新建测量"功能；
    /// </summary>
    public class NewMeasure : PluginBase
    {
        public NewMeasure()
        {
            _StripItem.Text = "新建测量(&N)";
            _StripItem.Order = 0F;
            _StripItem.ShortcutKeys = Keys.Control | Keys.N;
            _StripItem.Click += (s, e) =>
            {
                var dialog = new MeasureCaseSelectorDialog();
                var rs = dialog.ShowDialog(_StripItem.Owner.FindForm());
                if (rs == DialogResult.OK)
                {
                    var view = new MeasureView();
                    view.SetWorkModel(true);
                    ShowAtDockPanel(view);
                }
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
