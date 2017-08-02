using System;
using System.Threading;
using GalaSoft.MvvmLight;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Models;
using MeterKnife.Plots;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private IExtenderProvider _ExtenderProvider;
        public PlainPolyLinePlot Plot { get; }

        public MeasureViewModel()
        {
            var hd = DI.Get<IHabitedDatas>();
            var themes = hd.PlotThemes;
            var usingTheme = hd.UsingTheme;
            foreach (var plotTheme in themes)
            {
                if (plotTheme.Name == usingTheme)
                    Plot = new PlainPolyLinePlot(plotTheme);
            }

        }

        public void SetProvider(IExtenderProvider provider)
        {
            _ExtenderProvider = provider;
        }

        public event EventHandler PlotModelUpdated;

        protected virtual void OnPlotModelUpdated()
        {
            PlotModelUpdated?.Invoke(this, EventArgs.Empty);
        }

        #region Demo数据生成

        private Thread _DemoThread;

        private bool _OnDemo = false;

        public void StartDemo()
        {
            var rand = new UtilityRandom();
            _DemoThread = new Thread(() =>
            {
                _OnDemo = true;
                while (_OnDemo)
                {
                    Thread.Sleep(600);
                    var tail = rand.Next(0, 99999);
                    var v = $"9.99{tail}";
                    Plot.Add(double.Parse(v));
                    OnPlotModelUpdated();
                }
            });
            _DemoThread.Start();
        }

        public void StopDemo()
        {
            _OnDemo = false;
            _DemoThread?.Abort();
        }

        #endregion
    }
}
