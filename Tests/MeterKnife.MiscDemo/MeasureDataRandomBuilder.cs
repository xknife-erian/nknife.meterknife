using System.Threading;
using System.Windows.Forms;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Views;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.MiscDemo
{
    internal class MeasureDataRandomBuilder
    {
        private readonly IMeasureService _MeasureService = DI.Get<IMeasureService>();
        private readonly UtilityRandom _Rand = new UtilityRandom();
        private Thread _DemoThread;

        private readonly MeasureView _MeasureViewForm;

        private bool _OnDemo;

        public MeasureDataRandomBuilder(MeasureView form)
        {
            _MeasureViewForm = form;
            form.Closing += (s, x) => { StopDemo(); };
        }

        public void StartDemo()
        {
            var exhibits = _MeasureViewForm.ViewModel.Exhibits;
            var index = exhibits.Count - 1;

            if (index >= 0)
            {
                _DemoThread = new Thread(() =>
                {
                    _OnDemo = true;
                    var head = 9; //_Rand.Next(9, 10);
                    while (_OnDemo)
                        for (ushort i = 0; i < index; i++)
                        {
                            var tail = _Rand.Next(0, 99999);
                            var v = double.Parse($"{head}.99{tail}");
                            _MeasureService.AddValue(exhibits[i], v);
                            Thread.Sleep(200);
                        }
                });
                _DemoThread.Start();
            }
            else
            {
                MessageBox.Show("无被采集物，Demo结束。");
            }
        }


        public void StopDemo()
        {
            _OnDemo = false;
            _DemoThread?.Abort();
        }
    }
}