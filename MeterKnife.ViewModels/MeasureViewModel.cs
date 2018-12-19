using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using MeterKnife.Base;
using MeterKnife.Base.Viewmodels;
using MeterKnife.Events;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using MeterKnife.Plots;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.ViewModels
{
    public class MeasureViewModel : CommonViewModelBase
    {
        private static readonly ILog Logger = LogManager.GetLogger<MeasureViewModel>();

        public MeasureViewModel()
        {
            var themes = Habit.PlotThemes;
            var usingTheme = Habit.UsingTheme;
            foreach (var plotTheme in themes)
            {
                if (plotTheme.Name == usingTheme)
                {
                    Plot = new PlainPolyLinePlot(plotTheme);
                }
            }
            var measureService = DI.Get<IMeasureService>();
            measureService.Measured += OnMeasured;
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

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            var index = _seriesStyleSolution.IndexOf(e.ExhibitId);
            Logger.Trace($"数据Index:{index},{e.ExhibitId}");
            if (index >= 0)
            {
                var style = _seriesStyleSolution.Styles[index].SeriesStyle;
                Plot.AddValues(index, e.Value + style.Offset);
                OnPlotModelUpdated();
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