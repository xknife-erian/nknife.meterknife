using System;
using System.Threading;
using MeterKnife.ConsoleDemo.Mocks;
using MeterKnife.Electronics;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using MeterKnife.Scpis;
using NKnife.IoC;

namespace MeterKnife.ConsoleDemo
{
    public class ChannelDatasDemo : DemoBase
    {
        private readonly IMeasureService _MeasureService = DI.Get<IMeasureService>();

        private readonly Instrument _Ag34401 = new Instrument("Agilent", "34401", "34401", 22);
        private readonly Instrument _Care1 = new Instrument("Care", "1", "Care1", 0);
        private readonly Instrument _K2000 = new Instrument("Keithley", "2000", "K2000", 23);
        private readonly Instrument _K2700 = new Instrument("Keithley", "2700", "K2700", 24);

        private readonly Temperature _Temperature = new Temperature() { Name = "Temperature" };
        private readonly Voltage _Voltage = new Voltage() { Name = "Voltage" };
        private readonly Current _Current = new Current() { Name = "Current" };

        private readonly Resistance _Res1 = new Resistance(){Name = "Res1"};
        private readonly Resistance _Res10 = new Resistance() { Name = "Res10" };
        private readonly Resistance _Res11 = new Resistance() { Name = "Res11" };
        private readonly Resistance _Res12 = new Resistance() { Name = "Res12" };
        private readonly Resistance _Res13 = new Resistance() { Name = "Res13" };
        private readonly Resistance _Res14 = new Resistance() { Name = "Res14" };
        private readonly Resistance _Res15 = new Resistance() { Name = "Res15" };
        private readonly Resistance _Res16 = new Resistance() { Name = "Res16" };
        private readonly Resistance _Res17 = new Resistance() { Name = "Res17" };
        private readonly Resistance _Res18 = new Resistance() { Name = "Res18" };
        private readonly Resistance _Res19 = new Resistance() { Name = "Res19" };
        private readonly Resistance _Res2 = new Resistance() { Name = "Res2" };
        private readonly Resistance _Res20 = new Resistance() { Name = "Res20" };
        private readonly Resistance _Res3 = new Resistance() { Name = "Res3" };
        private readonly Resistance _Res4 = new Resistance() { Name = "Res4" };
        private readonly Resistance _Res5 = new Resistance() { Name = "Res5" };
        private readonly Resistance _Res6 = new Resistance() { Name = "Res6" };
        private readonly Resistance _Res7 = new Resistance() { Name = "Res7" };
        private readonly Resistance _Res8 = new Resistance() { Name = "Res8" };
        private readonly Resistance _Res9 = new Resistance() { Name = "Res9" };

        private readonly AutoResetEvent _ResetFlag = new AutoResetEvent(false);

        public override void Run()
        {
            //模拟一次用户测量过程

            var viewModel = new MockChannelViewModel();

            //1.新建测量事务。
            //本事务由34401测量电流，K2001测量电压，K2700扫描测量20组电阻
            //测量过程模拟用户暂停两次。
            var job = new MeasureJob();

            //2.在测量事务中加入1个温度，1个电压，1个电流，20个电阻

            job.Exhibits.Add(_Temperature);
            job.Exhibits.Add(_Current);
            job.Exhibits.Add(_Voltage);
            job.Exhibits.Add(_Res1);
            job.Exhibits.Add(_Res2);
            job.Exhibits.Add(_Res3);
            job.Exhibits.Add(_Res4);
            job.Exhibits.Add(_Res5);
            job.Exhibits.Add(_Res6);
            job.Exhibits.Add(_Res7);
            job.Exhibits.Add(_Res8);
            job.Exhibits.Add(_Res9);
            job.Exhibits.Add(_Res10);
            job.Exhibits.Add(_Res11);
            job.Exhibits.Add(_Res12);
            job.Exhibits.Add(_Res13);
            job.Exhibits.Add(_Res14);
            job.Exhibits.Add(_Res15);
            job.Exhibits.Add(_Res16);
            job.Exhibits.Add(_Res17);
            job.Exhibits.Add(_Res18);
            job.Exhibits.Add(_Res19);
            job.Exhibits.Add(_Res20);


            //3.在本次测量事务中加入四台测量仪器。
            job.Instruments.Add(_Care1); //假设使用Care一代采集温度
            job.Instruments.Add(_Ag34401); //假设测电压
            job.Instruments.Add(_K2000); //假设测电流
            job.Instruments.Add(_K2700); //假设2700负责20通道扫描电阻

            //__1__启动一次测量
            var measure = new MeasureJob.Measure(job, BuildScpiSubject(1), DateTime.Now);
            viewModel.AddMeasure(measure);
            viewModel.Start();
            _ResetFlag.WaitOne(1000 * 3);
            viewModel.Pause();

            //__2__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(2), DateTime.Now);
            viewModel.AddMeasure(measure);
            viewModel.Start();
            _ResetFlag.WaitOne(1000 * 20);
            viewModel.Pause();

            //__3__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(3), DateTime.Now);
            viewModel.AddMeasure(measure);
            viewModel.Start();
            _ResetFlag.WaitOne(1000 * 5);
            viewModel.Pause();

            //__4__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(4), DateTime.Now);
            viewModel.AddMeasure(measure);
            viewModel.Start();
            _ResetFlag.WaitOne(1000 * 5);
            viewModel.Pause();

            //__5__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(5), DateTime.Now);
            viewModel.AddMeasure(measure);
            viewModel.Start();
            _ResetFlag.WaitOne(1000 * 5);
            viewModel.Pause();

            Console.WriteLine("channel datas demo end.");
            Console.ReadKey();
        }

        private ScpiSubject BuildScpiSubject(int flag)
        {
            var scpisubject = new ScpiSubject();

            scpisubject.Initializtion.Add(new ScpiCommand() {Instrument = _Ag34401});
            scpisubject.Initializtion.Add(new ScpiCommand() {Instrument = _K2000});
            scpisubject.Initializtion.Add(new ScpiCommand() {Instrument = _K2700});

            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _Care1, Exhibit = _Temperature});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _Ag34401, Exhibit = _Voltage});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2000, Exhibit = _Current});

            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res1});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res2});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res3});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res4});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res5});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res6});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res7});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res8});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res9});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res10});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res11});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res12});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res13});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res14});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res15});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res16});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res17});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res18});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res19});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _K2700, Exhibit = _Res20});
            return scpisubject;
        }
    }
}