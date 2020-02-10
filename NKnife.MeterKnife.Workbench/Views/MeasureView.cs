using System;
using System.Windows.Forms;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Plots;
using OxyPlot;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class MeasureView : DockContent
    {
        private readonly MeasureViewModel _viewModel;
        private readonly IDialogService _dialogService;

        public MeasureView(MeasureViewModel viewModel, IDialogService dialogService)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _dialogService = dialogService;
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

        private void SetDataSeriesButtonClick(object sender, EventArgs e)
        {
            var dialog = _dialogService.New<DataSeriesListDialog>();
            dialog.Solution = _viewModel.SeriesStyleSolution;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _viewModel.SeriesStyleSolution = dialog.Solution;
            }
        }
    }
}