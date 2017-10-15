using System;
using MeterKnife.ConsoleDemo.Mocks.Channels;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Keysights;
using MeterKnife.Models;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;

namespace MeterKnife.ConsoleDemo.Mocks
{
    public class MockChannelViewModel
    {
        private readonly StringDatasMockChannel _KeysightChannel = new StringDatasMockChannel();
        private readonly IMeasureService _MeasureService = DI.Get<IMeasureService>();

        public MockChannelViewModel()
        {
            _KeysightChannel.Open();
        }

        public void AddMeasure(MeasureJob.Measure measure)
        {
            _KeysightChannel.UpdateQuestionGroup(_KeysightChannel.ToQuestionGroup(measure));
        }

        public void Start()
        {
            Console.WriteLine("start...");
            _KeysightChannel.SendReceiving(SendAction, ReceivedFunc);
        }

        public void Pause()
        {
            Console.WriteLine("pause___");
            _KeysightChannel.StopSendReceiving();
        }

        public void Stop()
        {
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            KeysightAnswer ans = answer as KeysightAnswer;
            if (ans != null)
            {
                var value = ToDouble(answer.Data);
                _MeasureService.AddValue(ans.JobNumber, ans.Target.Id, value);
                Console.WriteLine($"<:{value}");
            }
            return true;
        }

        private void SendAction(IQuestion<string> obj)
        {
            Console.Write($">");
        }

        private double ToDouble(string data)
        {
            return double.Parse(data);
        }
    }
}