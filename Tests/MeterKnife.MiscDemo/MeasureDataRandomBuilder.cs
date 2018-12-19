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
        private readonly IMeasureService _measureService = DI.Get<IMeasureService>();
        private readonly UtilityRandom _rand = new UtilityRandom();
        private Thread _demoThread;

        private readonly MeasureView _measureViewForm;

        private bool _onDemo;

        public MeasureDataRandomBuilder(MeasureView form)
        {
            _measureViewForm = form;
            form.Closing += (s, x) => { StopDemo(); };
        }

        public void StartDemo()
        {
            var solution = _measureViewForm.ViewModel.SeriesStyleSolution;
            var index = solution.Styles.Count;

            if (index >= 0)
            {
                _demoThread = new Thread(() =>
                {
                    _onDemo = true;
                    var head = 9; //_Rand.Next(9, 10);
                    while (_onDemo)
                    {
                        for (ushort i = 0; i < index; i++)
                        {
                            var tail = _rand.Next(0, 99999);
                            var v = double.Parse($"{head}.99{tail}");
                            _measureService.AddValue("", solution.Styles[i].Exhibit.Id, v);
                            Thread.Sleep(100);
                        }
                    }
                });
                _demoThread.Start();
            }
            else
            {
                MessageBox.Show("无被采集物，Demo结束。");
            }
        }


        public void StopDemo()
        {
            _onDemo = false;
            _demoThread?.Abort();
        }
    }
}