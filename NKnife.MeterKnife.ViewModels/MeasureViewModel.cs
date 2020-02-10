﻿using System;
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

        public MeasureViewModel(IHabitManager habit)
        {
            var dvList = new List<PlotTheme>();
            var dv = new PlotTheme();
            dvList.Add(dv);
            var themes = habit.GetHabitValue("PlotThemes", dvList);
            var usingTheme = habit.GetHabitValue("UsingTheme", dv.Name);
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

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            var index = _seriesStyleSolution.IndexOf(e.DUT.Item2.Id);
            _Logger.Trace($"数据Index:{index},{e.DUT}");
            if (index >= 0)
            {
                var style = _seriesStyleSolution.Styles[index].SeriesStyle;
                Plot.AddValues(index, e.Data.Data + style.Offset);
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