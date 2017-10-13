using System;
using MeterKnife.ConsoleDemo.Mocks.Channels;
using MeterKnife.Electronics;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Keysights;
using MeterKnife.Models;
using MeterKnife.Scpis;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;

namespace MeterKnife.ConsoleDemo
{
    public class ChannelDatasDemo : DemoBase
    {
        private readonly StringDatasMockChannel _KeysightChannel = new StringDatasMockChannel();
        private readonly IMeasureService _MeasureService = DI.Get<IMeasureService>();

        #region Overrides of DemoBase

        public override void Run()
        {
            //模拟一次用户测量过程

            //1.新建测量事务。本事务由34401测量电阻，K2001测量电压；测量过程模拟用户暂停两次。
            var job = new MeasureJob();
            //2.在测量事务中加入一个电阻，一个电压
            job.Exhibits.Add(new Resistance());
            job.Exhibits.Add(new Voltage());
            //3.在本次测量事务中加入两台测量仪器。
            job.Instruments.Add(new Instrument("Keysight","34401","34401",22));
            job.Instruments.Add(new Instrument("Keithley","2001","K2001",23));

            //创建一次测量（单次测量是不可中断的）
            var measure = new MeasureJob.Measure(job.Id, BuildScpiSubject(), DateTime.Now);
            job.Measures.Add(measure);
            
            _KeysightChannel.UpdateQuestionGroup(_KeysightChannel.ToQuestionGroup(measure));
            _KeysightChannel.SendReceiving(SendAction, ReceivedFunc);
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            KeysightAnswer ans = answer as KeysightAnswer;
            if (ans != null)
                _MeasureService.AddValue(ans.JobNumber, ans.Target.Id, ToDouble(answer.Data));
            return true;
        }

        private void SendAction(IQuestion<string> obj)
        {
            Console.WriteLine($"Send:{obj.Data}");
        }

        private ScpiSubject BuildScpiSubject()
        {
            throw new NotImplementedException();
        }

        private double ToDouble(string data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}