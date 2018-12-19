using System;
using System.Threading;
using Common.Logging;
using MeterKnife.Base;
using MeterKnife.Base.Channels;
//using MeterKnife.Keysights.VISAs;
using MeterKnife.Models;
using MeterKnife.Scpis;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Interfaces.Channels;
using Timer = System.Timers.Timer;

namespace MeterKnife.Keysights
{
    public class KeysightChannel : MeasureChannelBase<string>
    {
        private static readonly ILog Logger = LogManager.GetLogger<KeysightChannel>();
        private readonly ushort _gpibTarget;
        //private GPIBLinker _GPIBLinker;
        private KeysightQuestionGroup _questionGroup = new KeysightQuestionGroup();

        public KeysightChannel(ushort gpibTarget = 0)
        {
            _gpibTarget = gpibTarget;
            Logger.Info($"GPIBLinker GPIBTarget is {gpibTarget}.");

            IsSynchronous = true;
            TalkTotalTimeout = 2000;
        }

        #region Implementation of IChannel<string>

        public override bool Open()
        {
            OnOpening();
            Logger.Info($"GPIBLinker OnOpening...");
            OpenGpibLinker();
            IsOpen = true;
            OnOpened();
            Logger.Info($"GPIBLinker OnOpened...");
            return true;
        }

        protected virtual void OpenGpibLinker()
        {
            /*
            if (_GPIBLinker == null || _GPIBTarget != _GPIBLinker.GpibSelector)
            {
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
            }
            */
        }

        public override bool Close()
        {
            OnCloseing();
            IsOpen = false;
            OnClosed();
            return true;
        }

        public override void UpdateQuestionGroup(IQuestionGroup<string> qGroup)
        {
            if (!(qGroup is KeysightQuestionGroup))
                throw new ArgumentException(nameof(qGroup), $"{nameof(qGroup)} need is {typeof(KeysightQuestionGroup).Name}");
            UpdateQuestionGroup((KeysightQuestionGroup) qGroup);
        }

        public void UpdateQuestionGroup(KeysightQuestionGroup questionGroup)
        {
            _questionGroup = questionGroup;
        }

        #region Sync-SendReceiving

        private readonly AutoResetEvent _autoReset = new AutoResetEvent(false);
        private readonly Timer _timer = new Timer();
        private bool _isLoop = true;

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
        public override void SendReceiving(Action<IQuestion<string>> sendAction, Func<IAnswer<string>, bool> receivedFunc)
        {
            ThreadPool.QueueUserWorkItem(SendReceiving, new SyncSendReceivingParams(sendAction, receivedFunc));
#if DEBUG
            int a, b = 0;
            ThreadPool.GetAvailableThreads(out a, out b);
            Logger.Trace($"WorkerThreads: {a}, CompletionPortThreads: {b}");
#endif
        }

        public void StopSendReceiving()
        {
            _isLoop = false;
            _autoReset.Set();
            _timer.Stop();
        }

        protected void SendReceiving(object param)
        {
            _isLoop = true;
            _timer.Stop();
            _timer.Interval = TalkTotalTimeout;
            _timer.Elapsed += (s, e) => { _autoReset.Set(); };
            var isFirst = true;
            var w = (SyncSendReceivingParams) param;
            while (_questionGroup.Count > 0 && _isLoop)
            {
                var q = _questionGroup.PeekOrDequeue();
                var instrument = (Instrument)q.Instrument;
                var exhibit = (ExhibitBase)q.Target;
                try
                {
                    w.SendAction.Invoke(q);
                    if (isFirst)
                    {
                        isFirst = false;
                        _timer.Start();
                    }
                    var data = WriteAndRead(instrument.Address, q.Data);
                    var answer = new KeysightAnswer(_questionGroup.JobNumber, this, instrument, exhibit, data);
                    w.ReceivedFunc.Invoke(answer);
                    _autoReset.WaitOne();
                }
                catch (Exception e)
                {
                    Logger.Warn($"Keysight:{e.Message}", e);
                }
            }
            _timer.Stop();
        }

        protected virtual string WriteAndRead(int address, string command)
        {
            return "";// _GPIBLinker.WriteAndRead((ushort) address, command);
        }

        #endregion

        #region async, 不支持

        public override void AutoSend(Action<IQuestion<string>> sendAction)
        {
            //此种Channel不设置异步方式操作
        }

        public override void Break()
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

        protected virtual void OnChannelModeChanged(ChannelModeChangedEventArgs e)
        {
            ChannelModeChanged?.Invoke(this, e);
        }

        protected virtual void OnDataArrived(ChannelAnswerDataEventArgs<string> e)
        {
            DataArrived?.Invoke(this, e);
        }

        #endregion

        #endregion

        #region Overrides of MeasureChannelBase<string>

        public override void Binding(MeasureJob.Measure measure)
        {
            var questionGroup = new KeysightQuestionGroup();
            var scpis = measure.ScpiSubject;
            foreach (ScpiCommand command in scpis.Initializtion)
            {
                var keysightQuestion = new KeysightQuestion(this, command.Instrument, command.Exhibit, false, command.Command);
                questionGroup.Add(keysightQuestion);
            }
            foreach (ScpiCommand command in scpis.Measure)
            {
                var keysightQuestion = new KeysightQuestion(this, command.Instrument, command.Exhibit, true, command.Command);
                //keysightQuestion.LoopInterval = command.Interval;
                questionGroup.Add(keysightQuestion);
            }
            UpdateQuestionGroup(questionGroup);
        }

        #endregion
    }
}