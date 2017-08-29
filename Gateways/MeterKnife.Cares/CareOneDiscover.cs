using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;

namespace MeterKnife.Cares
{
    public class CareOneDiscover : IGatewayDiscover
    {
        #region Implementation of IGatewayDiscover

        /// <summary>
        /// 本发现器的测量途径模式
        /// </summary>
        public GatewayModel GatewayModel { get; set; } = GatewayModel.CareOne;

        /// <summary>
        /// 本测量途径挂接的仪器或设备列表
        /// </summary>
        public List<Instrument> Instruments { get; } = new List<Instrument>();

        private int _DemoCount = 1;

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        public void AddInstrument()
        {
            var inst = new Instrument("HP", "34401", "HP34401", "HP34401", 22);
            inst.DatasCount = _DemoCount++;
            Instruments.Add(inst);
            OnInstrumentAdded(new InstrumentAddedEventArgs(inst));
        }

        public event EventHandler<InstrumentAddedEventArgs> InstrumentAdded;

        public event EventHandler Discovered;

        /// <summary>
        /// 开始搜索该测量途径下的所有仪器
        /// </summary>
        public void BeginDiscover()
        {
            throw new NotImplementedException();
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