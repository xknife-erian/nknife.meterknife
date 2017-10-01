using System;
using System.Collections.Generic;
using System.Threading;
using MeterKnife.Base.Viewmodels;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Plots;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.ViewModels
{
    public class MeasureViewModel : CommonViewModelBase
    {
        public MeasureViewModel()
        {
            var hd = DI.Get<IHabited>();
            var themes = hd.PlotThemes;
            var usingTheme = hd.UsingTheme;
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

        public List<int> SeriesNumbers { get; set; } = new List<int>();

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            if (SeriesNumbers.Contains(e.Number))
            {
                Plot.AddValues(e.Number, e.Value);
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