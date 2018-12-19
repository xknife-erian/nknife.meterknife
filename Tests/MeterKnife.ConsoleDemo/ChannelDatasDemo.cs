using System;
using System.Threading;
using MeterKnife.ConsoleDemo.Mocks;
using MeterKnife.Electronics;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using MeterKnife.Scpis;
using NKnife.IoC;

namespace MeterKnife.ConsoleDemo
{
    public class ChannelDatasDemo : DemoBase
    {
        private readonly Instrument _ag34401 = new Instrument("Agilent", "34401", "34401", 22);
        private readonly Instrument _care1 = new Instrument("Care", "1", "Care1", 0);
        private readonly Instrument _k2000 = new Instrument("Keithley", "2000", "K2000", 23);
        private readonly Instrument _k2700 = new Instrument("Keithley", "2700", "K2700", 24);

        private readonly Temperature _temperature = new Temperature() { Name = "Temperature" };
        private readonly Voltage _voltage = new Voltage() { Name = "Voltage" };
        private readonly Current _current = new Current() { Name = "Current" };

        private readonly Resistance _res1 = new Resistance(){Name = "Res1"};
        private readonly Resistance _res10 = new Resistance() { Name = "Res10" };
        private readonly Resistance _res11 = new Resistance() { Name = "Res11" };
        private readonly Resistance _res12 = new Resistance() { Name = "Res12" };
        private readonly Resistance _res13 = new Resistance() { Name = "Res13" };
        private readonly Resistance _res14 = new Resistance() { Name = "Res14" };
        private readonly Resistance _res15 = new Resistance() { Name = "Res15" };
        private readonly Resistance _res16 = new Resistance() { Name = "Res16" };
        private readonly Resistance _res17 = new Resistance() { Name = "Res17" };
        private readonly Resistance _res18 = new Resistance() { Name = "Res18" };
        private readonly Resistance _res19 = new Resistance() { Name = "Res19" };
        private readonly Resistance _res2 = new Resistance() { Name = "Res2" };
        private readonly Resistance _res20 = new Resistance() { Name = "Res20" };
        private readonly Resistance _res3 = new Resistance() { Name = "Res3" };
        private readonly Resistance _res4 = new Resistance() { Name = "Res4" };
        private readonly Resistance _res5 = new Resistance() { Name = "Res5" };
        private readonly Resistance _res6 = new Resistance() { Name = "Res6" };
        private readonly Resistance _res7 = new Resistance() { Name = "Res7" };
        private readonly Resistance _res8 = new Resistance() { Name = "Res8" };
        private readonly Resistance _res9 = new Resistance() { Name = "Res9" };

        private readonly AutoResetEvent _resetFlag = new AutoResetEvent(false);

        public override void Run()
        {
            var measureService = DI.Get<IMeasureService>();
            measureService.StartService();
            var datasService = DI.Get<IDatasService>();
            datasService.StartService();
            //模拟一次用户测量过程

            var viewModel = new MockChannelViewModel();

            //1.新建测量事务。
            //本事务由34401测量电流，K2001测量电压，K2700扫描测量20组电阻
            //测量过程模拟用户暂停两次。
            var job = measureService.CreateMeasureJob();

            //2.在测量事务中加入1个温度，1个电压，1个电流，20个电阻

            job.Exhibits.Add(_temperature);
            job.Exhibits.Add(_current);
            job.Exhibits.Add(_voltage);
            job.Exhibits.Add(_res1);
            job.Exhibits.Add(_res2);
            job.Exhibits.Add(_res3);
            job.Exhibits.Add(_res4);
            job.Exhibits.Add(_res5);
            job.Exhibits.Add(_res6);
            job.Exhibits.Add(_res7);
            job.Exhibits.Add(_res8);
            job.Exhibits.Add(_res9);
            job.Exhibits.Add(_res10);
            job.Exhibits.Add(_res11);
            job.Exhibits.Add(_res12);
            job.Exhibits.Add(_res13);
            job.Exhibits.Add(_res14);
            job.Exhibits.Add(_res15);
            job.Exhibits.Add(_res16);
            job.Exhibits.Add(_res17);
            job.Exhibits.Add(_res18);
            job.Exhibits.Add(_res19);
            job.Exhibits.Add(_res20);


            //3.在本次测量事务中加入四台测量仪器。
            job.Instruments.Add(_care1); //假设使用Care一代采集温度
            job.Instruments.Add(_ag34401); //假设测电压
            job.Instruments.Add(_k2000); //假设测电流
            job.Instruments.Add(_k2700); //假设2700负责20通道扫描电阻

            //__1__启动一次测量
            var measure = new MeasureJob.Measure(job, BuildScpiSubject(1), DateTime.Now);
            viewModel.BindingMeasure(measure);
            viewModel.Start();
            _resetFlag.WaitOne(1000 * 3);
            viewModel.Pause();

            //__2__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(2), DateTime.Now);
            viewModel.BindingMeasure(measure);
            viewModel.Start();
            _resetFlag.WaitOne(1000 * 20);
            viewModel.Pause();

            //__3__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(3), DateTime.Now);
            viewModel.BindingMeasure(measure);
            viewModel.Start();
            _resetFlag.WaitOne(1000 * 5);
            viewModel.Pause();

            //__4__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(4), DateTime.Now);
            viewModel.BindingMeasure(measure);
            viewModel.Start();
            _resetFlag.WaitOne(1000 * 5);
            viewModel.Pause();

            //__5__启动一次测量
            measure = new MeasureJob.Measure(job, BuildScpiSubject(5), DateTime.Now);
            viewModel.BindingMeasure(measure);
            viewModel.Start();
            _resetFlag.WaitOne(1000 * 5);
            viewModel.Pause();

            Console.WriteLine("channel datas demo end.");
            Console.ReadKey();
        }

        private ScpiSubject BuildScpiSubject(int flag)
        {
            var scpisubject = new ScpiSubject();

            scpisubject.Initializtion.Add(new ScpiCommand() {Instrument = _ag34401});
            scpisubject.Initializtion.Add(new ScpiCommand() {Instrument = _k2000});
            scpisubject.Initializtion.Add(new ScpiCommand() {Instrument = _k2700});

            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _care1, Exhibit = _temperature});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _ag34401, Exhibit = _voltage});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2000, Exhibit = _current});

            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res1});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res2});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res3});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res4});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res5});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res6});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res7});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res8});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res9});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res10});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res11});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res12});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res13});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res14});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res15});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res16});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res17});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res18});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res19});
            scpisubject.Measure.Add(new ScpiCommand() {Instrument = _k2700, Exhibit = _res20});
            return scpisubject;
        }
    }
}