using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using MeterKnife.Keysights.VISAs;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightChannel : IChannel<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KeysightChannel>();
        private GPIBLinker _GPIBLinker;
        private KeysightQuestionGroup _QuestionGroup = new KeysightQuestionGroup();

        protected virtual void OnOpening()
        {
            Opening?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOpened()
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCloseing()
        {
            Closeing?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChannelModeChanged(ChannelModeChangedEventArgs e)
        {
            ChannelModeChanged?.Invoke(this, e);
        }

        protected virtual void OnDataArrived(ChannelAnswerDataEventArgs<string> e)
        {
            DataArrived?.Invoke(this, e);
        }

        #region Implementation of IChannel<string>

        public bool Open()
        {
            OnOpening();
            _GPIBLinker = new GPIBLinker(log =>
            {
                switch (log.LogLevel)
                {
                    case GPIBLogLevel.Trace:
                        _logger.Trace(log.Message);
                        break;
                    case GPIBLogLevel.Warn:
                        _logger.Warn(log.Message, log.Exception);
                        break;
                    case GPIBLogLevel.Error:
                        _logger.Error(log.Message, log.Exception);
                        break;
                }
            }, 0);
            OnOpened();
            return true;
        }

        public bool Close()
        {
            OnCloseing();
            OnClosed();
            return true;
        }

        public void UpdateQuestionGroup(KeysightQuestionGroup questionGroup)
        {
            _QuestionGroup = questionGroup;
        }

        void IChannel<string>.UpdateQuestionGroup(IQuestionGroup<string> questionGroup)
        {
            if (!(questionGroup is KeysightQuestionGroup))
                throw new ArgumentException(nameof(questionGroup), $"{nameof(questionGroup)} need is {typeof(KeysightQuestionGroup).Name}");
            UpdateQuestionGroup((KeysightQuestionGroup) questionGroup);
        }

        #region Sync-SendReceiving

        protected class SyncSendReceivingParams
        {
            public SyncSendReceivingParams(Action<IQuestion<string>> sendAction, Func<AnswerBase<string>, bool> receivedFunc)
            {
                SendAction = sendAction;
                ReceivedFunc = receivedFunc;
            }

            public Action<IQuestion<string>> SendAction { get; set; }
            public Func<AnswerBase<string>, bool> ReceivedFunc { get; set; }
        }

        /// <summary>
        ///     发送数据并同步等待数据返回
        /// </summary>
        /// <param name="sendAction">当发送完成时</param>
        /// <param name="receivedFunc">当采集到数据(返回的数据)的处理方法。当返回true时，表示接收数据是完整的；返回flase时，表示接收数据不完整，还需要继续接收。</param>
        /// <returns>是否采集到数据</returns>
        public void SendReceiving(Action<IQuestion<string>> sendAction, Func<IAnswer<string>, bool> receivedFunc)
        {
            ThreadPool.QueueUserWorkItem(SendReceiving, new SyncSendReceivingParams(sendAction, receivedFunc));
#if DEBUG
            int a, b = 0;
            ThreadPool.GetAvailableThreads(out a, out b);
            _logger.Trace($"WorkerThreads: {a}, CompletionPortThreads: {b}");
#endif
        }

        protected void SendReceiving(object param)
        {
            var w = (SyncSendReceivingParams) param;
            while (_QuestionGroup.Count > 0)
            {
                try
                {
                    var question = _QuestionGroup.PeekOrDequeue();
                    w.SendAction.Invoke(question);
                    var data = _GPIBLinker.Execute((ushort) question.Device.Address, question.Data, TalkTotalTimeout);
                    w.ReceivedFunc.Invoke(new KeysightAnswer(this, question.Device, question.Exhibit, data));
                }
                catch (Exception e)
                {
                    _logger.Warn($"Keysight:{e.Message}", e);
                }
            }
        }

        #endregion


        public void AutoSend(Action<IQuestion<string>> sendAction)
        {
            //此种Channel不设置异步方式操作
        }

        public void Break()
        {
            //此种Channel不设置异步方式操作
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