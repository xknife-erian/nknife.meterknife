using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Plots;
using NKnife.MeterKnife.Plots.Util;
using NLog;

namespace NKnife.MeterKnife.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        public MeasureViewModel(IHabitManager habit)
        {
            var themes = habit.GetHabitValue("PlotThemes", new List<PlotTheme>());
            var usingTheme = habit.GetHabitValue<string>("UsingTheme", null);
            foreach (var plotTheme in themes)
            {
                if (plotTheme.Name == usingTheme)
                {
                    Plot = new PlainPolyLinePlot(plotTheme);
                }
            }
        }

        private PlotSeriesStyleSolution _seriesStyleSolution = new PlotSeriesStyleSolution();

        public PlotSeriesStyleSolution SeriesStyleSolution
        {
            get => _seriesStyleSolution;
            set
            {
                Set(() => SeriesStyleSolution, ref _seriesStyleSolution, value);
                Plot.SetSeries(value.Styles.ToArray());
            }
        }

        public PlainPolyLinePlot Plot { get; }

        public event EventHandler PlotModelUpdated;

        protected virtual void OnPlotModelUpdated()
        {
            PlotModelUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}