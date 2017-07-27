using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.ViewModels;
using MeterKnife.Views.Measures.Dialogs;
using OxyPlot;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views.Measures
{
    public partial class MeasureView : DockContent
    {
        private readonly MeasureViewModel _ViewModel = new MeasureViewModel();

        public MeasureView()
        {
            InitializeComponent();
            _PlotView.Model = _ViewModel.Plot.GetPlotModel();
            _PlotView.BackColor = _ViewModel.Plot.PlotTheme.ViewBackground;
            _ViewModel.PlotModelUpdated += (s, e) =>
            {
                _PlotView.ThreadSafeInvoke(() => _PlotView.InvalidatePlot(true));
            };
            _OriginalToolStripButton.Click += (s, e) => {_PlotView.Model.ResetAllAxes(); };
            _ZoomInToolStripButton.Click += (s, e) => { _PlotView.Model.ZoomAllAxes(1.3); };
            _ZoomOutToolStripButton.Click += (s, e) => { _PlotView.Model.ZoomAllAxes(0.7); };
        }

        public PlotModel GetMainPlotModel()
        {
            return _PlotView.Model;
        }

        /// <summary>
        /// 设置窗体的工作模式，是新建的测量，还是打开已有的测量
        /// </summary>
        /// <param name="isNewMeasure">true时是新建测量，false是打开已有的测量</param>
        public void SetWorkModel(bool isNewMeasure)
        {
            if (isNewMeasure)
            {
                var dialog = new MeasureSettingDialog();
                dialog.ShowDialog(this);
            }
            else
            {
            }
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
