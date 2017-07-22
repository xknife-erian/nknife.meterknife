using System;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.ViewModels;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views.Measures
{
    public partial class MeasureView : DockContent
    {
        private readonly MeasureViewModel _ViewModel = new MeasureViewModel();

        public MeasureView()
        {
            InitializeComponent();
            _PlotView.Model = _ViewModel.Plot.PlotModel;
            _ViewModel.PlotModelUpdated += (s, e) =>
            {
                _PlotView.ThreadSafeInvoke(() => _PlotView.InvalidatePlot(true));
            };
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Shown" /> 事件。</summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _ViewModel.StartDemo();
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.FormClosing" /> 事件。</summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.Windows.Forms.FormClosingEventArgs" />。</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _ViewModel.StopDemo();
        }

        #endregion

        #endregion

        public void SetProvider(IExtenderProvider extenderProvider)
        {
            _ViewModel.SetProvider(extenderProvider);
        }
    }
}
