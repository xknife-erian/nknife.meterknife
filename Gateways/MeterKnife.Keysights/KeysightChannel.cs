using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Keysights.VISAs;
using MeterKnife.Models;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;
using Timer = System.Timers.Timer;

namespace MeterKnife.Keysights
{
    public class KeysightChannel : IChannel<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KeysightChannel>();
        private readonly ushort _GPIBTarget;
        private GPIBLinker _GPIBLinker;
        private KeysightQuestionGroup _QuestionGroup = new KeysightQuestionGroup();

        public KeysightChannel(ushort gpibTarget = 0)
        {
            _GPIBTarget = gpibTarget;
            _logger.Info($"GPIBLinker GPIBTarget is {gpibTarget}.");
        }

        #region Implementation of IChannel<string>

        public bool Open()
        {
            OnOpening();
            _logger.Info($"GPIBLinker OnOpening...");
            if (_GPIBLinker == null || _GPIBTarget != _GPIBLinker.GpibSelector)
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
                }, _GPIBTarget);
            IsOpen = true;
            OnOpened();
            _logger.Info($"GPIBLinker OnOpened...");
            return true;
        }

        public bool Close()
        {
            OnCloseing();
            IsOpen = false;
            OnClosed();
            return true;
        }

        public void UpdateQuestionGroup(KeysightQuestionGroup questionGroup)
        {
            _QuestionGroup = questionGroup;
        }

        void IChannel<string>.UpdateQuestionGroup(IQuestionGroup<string> qGroup)
        {
            if (!(qGroup is KeysightQuestionGroup))
                throw new ArgumentException(nameof(qGroup), $"{nameof(qGroup)} need is {typeof(KeysightQuestionGroup).Name}");
            UpdateQuestionGroup((KeysightQuestionGroup) qGroup);
        }

        public bool IsSynchronous { get; set; } = true;
        public List<IId> Targets { get; } = new List<IId>();
        public uint TalkTotalTimeout { get; set; } = 2000;
        public bool IsOpen { get; private set; }

        #region Sync-SendReceiving

        private readonly AutoResetEvent _AutoReset = new AutoResetEvent(false);
        private readonly Timer _Timer = new Timer();
        private bool _IsLoop = true;

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

        public void StopSendReceiving()
        {
            _IsLoop = false;
            _AutoReset.Set();
            _Timer.Stop();
        }

        protected void SendReceiving(object param)
        {
            _IsLoop = true;
            _Timer.Stop();
            _Timer.Interval = TalkTotalTimeout;
            _Timer.Elapsed += (s, e) => { _AutoReset.Set(); };
            var isFirst = true;
            var w = (SyncSendReceivingParams) param;
            while (_QuestionGroup.Count > 0 && _IsLoop)
                try
                {
                    var question = _QuestionGroup.PeekOrDequeue();
                    var instrument = (Instrument) question.Instrument;
                    var exhibit = (IExhibit) question.Target;
                    w.SendAction.Invoke(question);
                    if (isFirst)
                    {
                        isFirst = false;
                        _Timer.Start();
                    }
                    var data = _GPIBLinker.WriteAndRead((ushort) instrument.Address, question.Data);
                    w.ReceivedFunc.Invoke(new KeysightAnswer(this, instrument, exhibit, data));
                    _AutoReset.WaitOne();
                }
                catch (Exception e)
                {
                    _logger.Warn($"Keysight:{e.Message}", e);
                }
            _Timer.Stop();
        }

        #endregion

        #region async, 不支持

        public void AutoSend(Action<IQuestion<string>> sendAction)
        {
            //此种Channel不设置异步方式操作
        }

        public void Break()
        {
            //此种Channel不设置异步方式操作
        }

        #endregion

        #region Event

        public event EventHandler Opening;
        public event EventHandler Opened;
        public event EventHandler Closeing;
        public event EventHandler Closed;
        public event EventHandler<ChannelModeChangedEventArgs> ChannelModeChanged;
        public event EventHandler<ChannelAnswerDataEventArgs<string>> DataArrived;

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

        #endregion

        #endregion
    }
}