using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Views;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Plots;
using OxyPlot;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class RealTimeDataPlotView : DockContent
    {
        private readonly RealTimeDataPlotViewModel _viewModel;
        private readonly IDialogProvider _dialogProvider;

        public RealTimeDataPlotView(RealTimeDataPlotViewModel viewModel, IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            _viewModel = viewModel;
            InitializeComponent();
            InitializeLanguage();
            Shown += (sender, args) =>
            {
                _PlotView.Model = _viewModel.LinearPlot.GetPlotModel();
                _PlotView.BackColor = _viewModel.LinearPlot.PlotTheme.ViewBackground;
                _OriginalToolStripButton.Click += (s, e) => { _PlotView.Model.ResetAllAxes(); };
                _ZoomInToolStripButton.Click += (s, e) => { _PlotView.Model.ZoomAllAxes(1.3); };
                _ZoomOutToolStripButton.Click += (s, e) => { _PlotView.Model.ZoomAllAxes(0.7); };
                _viewModel.PlotModelUpdated += (s, e) => { _PlotView.ThreadSafeInvoke(() => _PlotView.InvalidatePlot(true)); };
            };
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_SetDataSeriesToolStripButton);
            this.Res(_OriginalToolStripButton, _ZoomInToolStripButton, _ZoomOutToolStripButton, _TimeZoomToolStripButton, _ValueRangeZoomToolStripButton);
            this.Res(_MainTabPage, tabPage2);
        }

        public void SetStyleSolution(DUTSeriesStyleSolution solution)
        {
            _viewModel.StyleSolution = solution;
        }

        private void SetDataSeriesButtonClick(object sender, EventArgs e)
        {
            var dialog = _dialogProvider.New<DataSeriesListDialog>();
            dialog.Solution = _viewModel.StyleSolution;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _viewModel.StyleSolution = dialog.Solution;
            }
        }
    }
}