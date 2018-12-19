using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Keysights
{
    public class KeysightDiscover : GatewayDiscoverBase
    {
        private static readonly ILog Logger = LogManager.GetLogger<KeysightDiscover>();

        private readonly KeysightChannel _channel;

        public KeysightDiscover()
        {
            _channel = DI.Get<KeysightChannel>();
            _channel.Open();
        }

        #region Implementation of IGatewayDiscover

        /// <summary>
        ///     本发现器的通道模式30393
        /// </summary>
        public override GatewayModel GatewayModel { get; set; } = GatewayModel.Aglient82357A;

        private readonly UtilityRandom _random = new UtilityRandom();

        /// <summary>
        ///     手动添加仪器
        /// </summary>
        public override void CreateInstrument()
        {
            var model = $"20{_random.Next(10, 99)}";
            var inst = new Instrument("Keithley", model, $"Keithley{model}", _random.Next(1, 36));
            Instruments.Add(inst);
        }

        public override void BeginDiscover()
        {
            var group = new KeysightQuestionGroup();
            _channel.UpdateQuestionGroup(group);
            _channel.SendReceiving(SendAction, ReceivedFunc);
            foreach (var instrument in Instruments)
                UpdateInstrument(instrument);
            OnDiscovered();
        }

        /// <summary>
        ///     刷新本测量途径挂接的仪器或设备列表
        /// </summary>
        public override List<InstrumentConnectionState> Refresh()
        {
            throw new NotImplementedException();
        }

        private void UpdateInstrument(Instrument instrument)
        {
        }

        private void SendAction(IQuestion<string> question)
        {
            Logger.Debug(question.Data);
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            return true;
        }

        #endregion
    }
}