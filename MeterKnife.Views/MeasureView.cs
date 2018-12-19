using System;
using System.Windows.Forms;
using MeterKnife.ViewModels;
using MeterKnife.Views.Measures;
using MeterKnife.Views.Measures.Series;
using OxyPlot;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views
{
    public partial class MeasureView : DockContent
    {
        private readonly MeasureViewModel _viewModel = new MeasureViewModel();

        public MeasureView()
        {
            InitializeComponent();
            _PlotView.Model = _viewModel.Plot.GetPlotModel();
            _PlotView.BackColor = _viewModel.Plot.PlotTheme.ViewBackground;
            _viewModel.PlotModelUpdated += (s, e) => { _PlotView.ThreadSafeInvoke(() => _PlotView.InvalidatePlot(true)); };
            _OriginalToolStripButton.Click += (s, e) => { _PlotView.Model.ResetAllAxes(); };
            _ZoomInToolStripButton.Click += (s, e) => { _PlotView.Model.ZoomAllAxes(1.3); };
            _ZoomOutToolStripButton.Click += (s, e) => { _PlotView.Model.ZoomAllAxes(0.7); };
        }

        public MeasureViewModel ViewModel => _viewModel;

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
            }
            else
            {
            }
        }

        public void AddDataToolStripItem(ToolStripMenuItem item)
        {
            _DataToolStrip.Items.Add(item);
        }

        private void SetDataSeriesButtonClick(object sender, EventArgs e)
        {
            var dialog = new DataSeriesListDialog();
            dialog.Solution = _viewModel.SeriesStyleSolution;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _viewModel.SeriesStyleSolution = dialog.Solution;
            }
        }
    }
}