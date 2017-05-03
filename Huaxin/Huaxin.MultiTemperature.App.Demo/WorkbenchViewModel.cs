using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using GalaSoft.MvvmLight;
using Huaxin.MultiTemperature.Common;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Collections;

namespace Huaxin.MultiTemperature.App.Demo
{
    public class WorkbenchViewModel : ViewModelBase
    {
        private static readonly ILog _logger = LogManager.GetLogger<WorkbenchViewModel>();

        private readonly SyncQueue<byte> _AnswerQueue = new SyncQueue<byte>();
        private Thread _DataArrivedThread;
        private readonly DataParser _DataParser = new DataParser();

        private SerialChannel _SerialChannel;

        public ObservableCollection<ushort> Paths { get; set; } = new ObservableCollection<ushort>();
        public ushort CurrentSerial { get; set; }
        public bool IsOpen { get; set; }

        public WorkbenchViewModel()
        {
            _DataParser.Completed += DataParser_Completed;
        }

        private void DataParser_Completed(object sender, DataParser.HxBagEventArgs e)
        {
            var value = e.Bag.GetValue().Reverse().ToArray();
            float f = BitConverter.ToSingle(value, 0);
            _logger.Info($">>>> {f}");
        }

        public void OpenSerial()
        {
            var config = new SerialConfig(CurrentSerial);
            _SerialChannel = new SerialChannel(config);
            _SerialChannel.DataArrived += SerialChannel_DataArrived;

            StartDataArrivedThread();

            IsOpen = _SerialChannel.Open();
        }

        private void StartDataArrivedThread()
        {
            _DataParser.DataArrivedThreadStarted = true;
            _DataArrivedThread = new Thread(_DataParser.Parser);
            _DataArrivedThread.IsBackground = true;
            _DataArrivedThread.Start(_AnswerQueue);
        }

        private void SerialChannel_DataArrived(object sender, ChannelAnswerDataEventArgs<byte[]> e)
        {
            var answer = (SerialAnswer) e.Answer;
            _logger.Trace(answer.Data.ToHexString());
            foreach (var b in answer.Data)
                _AnswerQueue.Enqueue(b);
        }

        public void CloseSerial()
        {
            _DataParser.DataArrivedThreadStarted = false;
            _SerialChannel?.Close();
            _DataArrivedThread.Abort();
            IsOpen = false;
        }

        public void Start(string text, decimal value)
        {
            if (Paths.Count <= 0)
                return;
            var questionGroup = new SerialQuestionGroup();
            foreach (var path in Paths)
            {
                var p = path.ToString().PadLeft(2, '0');
                var cmdstring = text.Replace("XX", p).Trim();
                var cmd = cmdstring.ToBytes();
                var question = new SerialQuestion(_SerialChannel, null, null, true, cmd);
                questionGroup.Add(question);
            }
            _SerialChannel.TalkTotalTimeout = (uint) value;
            _SerialChannel.UpdateQuestionGroup(questionGroup);
            _SerialChannel.AutoSend(OnSend);
        }

        private void OnSend(IQuestion<byte[]> obj)
        {
            _logger.Trace($"SEND>>>{obj.Data.ToHexString()}");
        }

        public void Stop()
        {
            _SerialChannel.Break();
        }

        public void BuildPdfPrint()
        {
        }
    }
}