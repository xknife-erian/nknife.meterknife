using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;
using System;

namespace MeterKnife.ConsoleDemo
{
    public class ChannelDatasDemo : DemoBase
    {
        #region Overrides of DemoBase

        public override void Run()
        {
            var keysightChannel = new StringDatasMockChannel();
            keysightChannel.SendReceiving(null, ReceivedFunc);
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            var measureService = DI.Get<IMeasureService>();
            measureService.AddValue("", answer.Target.Id, ToDouble(answer.Data));
            return true;
        }

        private double ToDouble(string data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ByteDatasMockChannel : ChannelBase<byte>
    {
        #region Overrides of ChannelBase<byte>

        public override bool Open()
        {
            throw new NotImplementedException();
        }

        public override bool Close()
        {
            throw new NotImplementedException();
        }

        public override void UpdateQuestionGroup(IQuestionGroup<byte> questionGroup)
        {
            throw new NotImplementedException();
        }

        public override void SendReceiving(Action<IQuestion<byte>> sendAction, Func<IAnswer<byte>, bool> receivedFunc)
        {
            throw new NotImplementedException();
        }

        public override void AutoSend(Action<IQuestion<byte>> sendAction)
        {
            throw new NotImplementedException();
        }

        public override void Break()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StringDatasMockChannel : ChannelBase<string>
    {
        #region Overrides of ChannelBase<string>

        public override bool Open()
        {
            return true;
        }

        public override bool Close()
        {
            return true;
        }

        public override void UpdateQuestionGroup(IQuestionGroup<string> questionGroup)
        {
            throw new NotImplementedException();
        }

        public override void SendReceiving(Action<IQuestion<string>> sendAction, Func<IAnswer<string>, bool> receivedFunc)
        {
            throw new NotImplementedException();
        }

        public override void AutoSend(Action<IQuestion<string>> sendAction)
        {
        }

        public override void Break()
        {
        }

        #endregion
    }
}
