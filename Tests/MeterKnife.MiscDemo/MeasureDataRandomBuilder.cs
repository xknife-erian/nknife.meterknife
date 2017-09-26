using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NKnife.Utility;

namespace MeterKnife.MiscDemo
{
    class MeasureDataRandomBuilder
    {
        private readonly UtilityRandom _Rand = new UtilityRandom();
        private Thread _DemoThread;

        private bool _OnDemo;

        public void StartDemo()
        {
            _DemoThread = new Thread(() =>
            {
                _OnDemo = true;
                var top = _Rand.Next(0, 220);
                while (_OnDemo)
                {
                    Thread.Sleep(600);
                    var tail = _Rand.Next(0, 99999);
                    var v = $"{top}.99{tail}";
//                    Plot.Add(double.Parse(v));
//                    OnPlotModelUpdated();
                }
            });
            _DemoThread.Start();
        }

        public void StopDemo()
        {
            _OnDemo = false;
            _DemoThread?.Abort();
        }
    }
}
