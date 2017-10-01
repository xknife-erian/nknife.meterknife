using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Interfaces.Measures;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.MiscDemo
{
    class MeasureDataRandomBuilder
    {
        private readonly UtilityRandom _Rand = new UtilityRandom();
        private readonly IMeasureService _MeasureService = DI.Get<IMeasureService>();
        private Thread _DemoThread;

        private bool _OnDemo;

        public MeasureDataRandomBuilder(Form form)
        {
            form.Closing += (s, x) => { StopDemo(); };
        }

        public void StartDemo()
        {
            _Exhibits.Clear();

            _DemoThread = new Thread(() =>
            {
                _OnDemo = true;
                var head = 9;//_Rand.Next(9, 10);
                while (_OnDemo)
                {
                    for (ushort i = 0; i < 8; i++)
                    {
                        var tail = _Rand.Next(0, 99999);
                        var v = double.Parse($"{head}.99{tail}");
                        _MeasureService.AddValue(null, v);
                        Thread.Sleep(200);
                    }
                }
            });
            _DemoThread.Start();
        }

        private List<ExhibitBase> _Exhibits = new List<ExhibitBase>();

        public void StopDemo()
        {
            _OnDemo = false;
            _DemoThread?.Abort();
        }
    }
}
