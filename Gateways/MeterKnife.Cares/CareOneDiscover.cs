using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Instrument> Instruments { get; } = new ObservableCollection<Instrument>();

        private int _DemoCount = 1;

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        public void CreateInstrument()
        {
            var inst = new Instrument("HP", "34401", "HP34401", 22);
            inst.DatasCount = _DemoCount++;
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
    }
}