using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.ViewModels.Plots;
using NLog;

namespace NKnife.MeterKnife.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private DUTSeriesStyleSolution _solution = new DUTSeriesStyleSolution();

        public MeasureViewModel(IHabitManager habit, IMeasureService measureService)
        {
            var dvList = new List<PlotTheme>();
            var dv = new PlotTheme();
            dvList.Add(dv);
            var themes = habit.GetHabitValue("PlotThemes", dvList);
            var usingTheme = habit.GetHabitValue("UsingTheme", dv.Name);
            foreach (var plotTheme in themes)
            {
                if (plotTheme.Name == usingTheme)
                    LinearPlot = new DUTLinearPlot(plotTheme);
            }

            measureService.Measured += OnMeasured;
        }

        public DUTSeriesStyleSolution StyleSolution
        {
            get => _solution;
            set
            {
                Set(() => StyleSolution, ref _solution, value);
                LinearPlot.SetSeries(value.ToArray());
            }
        }

        public DUTLinearPlot LinearPlot { get; }

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            var index = _solution.IndexOf(e.DUT.Item2.Id);
            _Logger.Trace($"数据Index:{index},{e.Measurements.Data},{e.Time},{e.DUT},{e.Group}");
            if (index >= 0)
            {
                LinearPlot.AddValues(index, e.Time, e.Measurements.Data + _solution[index].Offset);
                OnPlotModelUpdated();
            }
        }

        public event EventHandler PlotModelUpdated;

        protected virtual void OnPlotModelUpdated()
        {
            PlotModelUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}