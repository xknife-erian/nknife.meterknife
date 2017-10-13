using System;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.ConsoleDemo.Mocks.Channels
{
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
}