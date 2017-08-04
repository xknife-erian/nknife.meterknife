using System;
using System.Collections.Generic;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Gateway
{
    public class VisaComm : IChannel<string>
    {
        #region Implementation of IChannel<string>

        public bool Open()
        {
            throw new NotImplementedException();
        }

        public bool Close()
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestionGroup(IQuestionGroup<string> questionGroup)
        {
            throw new NotImplementedException();
        }

        public void SendReceiving(Action<IQuestion<string>> sendAction, Func<IAnswer<string>, bool> onReceived)
        {
            throw new NotImplementedException();
        }

        public void AutoSend(Action<IQuestion<string>> sendAction)
        {
            throw new NotImplementedException();
        }

        public void Break()
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronous { get; set; }
        public List<IExhibit> Exhibits { get; } = new List<IExhibit>();
        public uint TalkTotalTimeout { get; set; }
        public bool IsOpen { get; } = false;
        public event EventHandler Opening;
        public event EventHandler Opened;
        public event EventHandler Closeing;
        public event EventHandler Closed;
        public event EventHandler<ChannelModeChangedEventArgs> ChannelModeChanged;
        public event EventHandler<ChannelAnswerDataEventArgs<string>> DataArrived;

        #endregion
    }
}