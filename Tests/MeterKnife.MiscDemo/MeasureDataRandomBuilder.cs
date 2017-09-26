using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MeterKnife.Interfaces.Measures;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.MiscDemo
{
    class MeasureDataRandomBuilder
    {
        private readonly UtilityRandom _Rand = new UtilityRandom();
        private IMeasureService _MeasureService = DI.Get<IMeasureService>();
        private Thread _DemoThread;

        private bool _OnDemo;

        public void StartDemo()
        {
            _DemoThread = new Thread(() =>
            {
                _OnDemo = true;
                var top = 9;//_Rand.Next(9, 10);
                while (_OnDemo)
                {
                    for (ushort i = 0; i < 8; i++)
                    {
                        var tail = _Rand.Next(0, 99999);
                        var v = double.Parse($"{top}.99{tail}");
                        _MeasureService.AddValue(i, null, v);
                        Thread.Sleep(200);
                    }
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
