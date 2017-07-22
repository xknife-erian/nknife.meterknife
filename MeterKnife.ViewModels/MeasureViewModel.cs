using System;
using System.Threading;
using GalaSoft.MvvmLight;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Utils;
using NKnife.Utility;

namespace MeterKnife.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private IExtenderProvider _ExtenderProvider;
        public SimpleLinePlot Plot { get; } = new SimpleLinePlot("");

        public void SetProvider(IExtenderProvider provider)
        {
            _ExtenderProvider = provider;
        }

        public event EventHandler PlotModelUpdated;

        private Thread _DemoThread;
        private bool _OnDemo = false;
        public void StartDemo()
        {
            var rand = new UtilityRandom();

            _DemoThread = new Thread(() =>
            {
                _OnDemo = true;
                Thread.Sleep(500);
                while (_OnDemo)
                {
                    var tail = rand.Next(0, 99999);
                    var v = $"9.99{tail}";
                    Plot.Add(float.Parse(v));
                    OnPlotModelUpdated();
                    Thread.Sleep(20);
                }
            });
            _DemoThread.Start();
        }

        public void StopDemo()
        {
            _OnDemo = false;
        }


        protected virtual void OnPlotModelUpdated()
        {
            PlotModelUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
