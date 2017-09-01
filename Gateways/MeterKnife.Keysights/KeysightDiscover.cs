using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Instrument> Instruments { get; } = new ObservableCollection<Instrument>();

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        public void CreateInstrument()
        {
            var inst = new Instrument("NF", "1915", "NF1915", 5);
            Instruments.Add(inst);
        }

        /// <summary>
        /// 删除仪器信息
        /// </summary>
        /// <param name="instrument">指定的仪器</param>
        public void DeleteInstrument(Instrument instrument)
        {
            Instrument t = null;
            foreach (var inst in Instruments)
            {
                if (instrument.Equals(inst))
                {
                    t = inst;
                    break;
                }
            }
            if (t != null)
                Instruments.Remove(t);
        }


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
    }
}
