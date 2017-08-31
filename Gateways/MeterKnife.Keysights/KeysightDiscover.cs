﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;

namespace MeterKnife.Keysights
{
    public class KeysightDiscover : IGatewayDiscover
    {
        private static readonly ILog _logger = LogManager.GetLogger<KeysightDiscover>();

        private readonly KeysightChannel _Channel;

        public KeysightDiscover()
        {
            _Channel = DI.Get<KeysightChannel>();
            _Channel.Open();
        }

        #region Implementation of IGatewayDiscover

        /// <summary>
        /// 本发现器的通道模式
        /// </summary>
        public GatewayModel GatewayModel { get; set; } = GatewayModel.Aglient82357A;

        /// <summary>
        /// 本通道挂接的仪器或设备列表
        /// </summary>
        public List<Instrument> Instruments { get; } = new List<Instrument>();

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        public void AddInstrument()
        {
            var inst = new Instrument("NF", "1915", "NF1915", "NF1915", 5);
            Instruments.Add(inst);
            OnInstrumentAdded(new InstrumentAddedEventArgs(inst));  
        }

        public event EventHandler<InstrumentAddedEventArgs> InstrumentAdded;

        /// <summary>
        /// 当自动发现仪器的动作执行完成
        /// </summary>
        public event EventHandler Discovered;

        public void BeginDiscover()
        {
            var group = new KeysightQuestionGroup();
            _Channel.UpdateQuestionGroup(group);
            _Channel.SendReceiving(SendAction, ReceivedFunc);
            foreach (var instrument in Instruments)
            {
                UpdateInstrument(instrument);
            }
            OnDiscovered();
        }

        private void UpdateInstrument(Instrument instrument)
        {
            
        }

        private void SendAction(IQuestion<string> question)
        {
            _logger.Debug(question.Data);
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            return true;
        }

        #endregion

        protected virtual void OnDiscovered()
        {
            Discovered?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnInstrumentAdded(InstrumentAddedEventArgs e)
        {
            InstrumentAdded?.Invoke(this, e);
        }
    }
}