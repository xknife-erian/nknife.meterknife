using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Views;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Plots;
using OxyPlot;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class StaticDataPlotView : DockContent
    {
        private readonly StaticDataPlotViewModel _viewModel;
        private readonly IDialogProvider _dialogProvider;

        public StaticDataPlotView(StaticDataPlotViewModel viewModel, IDialogProvider dialogProvider)
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
                Task.Factory.StartNew(async () => { await _viewModel.LoadDataAsync(); });
            };
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_SetDataSeriesToolStripButton);
            this.Res(_OriginalToolStripButton, _ZoomInToolStripButton, _ZoomOutToolStripButton, _TimeZoomToolStripButton, _ValueRangeZoomToolStripButton);
            this.Res(_MainTabPage, tabPage2);
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

        public void SetEngineering(Engineering eng)
        {
            _viewModel.SetEngineering(eng);
            Text = eng.Name;
        }
    }
}