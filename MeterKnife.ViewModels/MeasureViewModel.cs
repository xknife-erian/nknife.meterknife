using System;
using System.Collections.Generic;
using System.Threading;
using MeterKnife.Base;
using MeterKnife.Base.Viewmodels;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Plots;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.ViewModels
{
    public class MeasureViewModel : CommonViewModelBase
    {
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

        private List<ExhibitBase> _Exhibits = new List<ExhibitBase>(1);

        public List<ExhibitBase> Exhibits
        {
            get => _Exhibits;
            set { Set(() => Exhibits, ref _Exhibits, value); }
        }

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            var index = _Exhibits.IndexOf(e.Exhibit);
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