using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Keysights.VISAs;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightChannel : IChannel<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KeysightChannel>();
        private GPIBLinker _GPIBLinker;

        public KeysightChannel()
        {
            _GPIBLinker = new GPIBLinker((log) =>
            {
                switch (log.LogLevel)
                {
                    case GPIBLinker.GPIBLogLevel.Trace:
                        _logger.Trace(log.Message);
                        break;
                    case GPIBLinker.GPIBLogLevel.Warn:
                        _logger.Warn(log.Message, log.Exception);
                        break;
                    case GPIBLinker.GPIBLogLevel.Error:
                        _logger.Error(log.Message, log.Exception);
                        break;
                }
            }, 0, 0);
        }

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