using System;
using MeterKnife.Base;
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
        private readonly StringDatasMockChannel _keysightChannel = new StringDatasMockChannel();
        private readonly IMeasureService _measureService = DI.Get<IMeasureService>();

        public MockChannelViewModel()
        {
            _keysightChannel.TalkTotalTimeout = 80;
            _keysightChannel.Open();
        }

        public void BindingMeasure(MeasureJob.Measure measure)
        {
            _keysightChannel.Binding(measure);
        }

        public void Start()
        {
            Console.WriteLine("start...");
            _keysightChannel.SendReceiving(SendAction, ReceivedFunc);
        }

        public void Pause()
        {
            Console.WriteLine("pause___");
            _keysightChannel.StopSendReceiving();
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
                _measureService.AddValue(ans.JobNumber, ans.Target.Id, value);
                var instrument = (Instrument) ans.Instrument;
                var exhibit = (ExhibitBase) ans.Target;
                Console.WriteLine($"<:{instrument.Name}-{exhibit.Name}:{value}");
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