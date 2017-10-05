using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using MeterKnife.Base;
using MeterKnife.Base.Viewmodels;
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
        private static readonly ILog _logger = LogManager.GetLogger<MeasureViewModel>();

        public MeasureViewModel()
        {
            var themes = Habited.PlotThemes;
            var usingTheme = Habited.UsingTheme;
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

        private PlotSeriesStyleSolution _SeriesStyleSolution = new PlotSeriesStyleSolution(1);

        public PlotSeriesStyleSolution SeriesStyleSolution
        {
            get => _SeriesStyleSolution;
            set
            {
                Set(() => SeriesStyleSolution, ref _SeriesStyleSolution, value);
                Plot.SetSeries(value.ToArray());
            }
        }

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            var index = _SeriesStyleSolution.IndexOf(e.Exhibit);
            _logger.Trace($"数据Index:{index},{e.Exhibit.Id}");
            if (index >= 0)
            {
                Plot.AddValues(index, e.Value);
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