using System;
using System.Collections.ObjectModel;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;

namespace MeterKnife.Base
{
    public abstract class GatewayDiscoverBase : IGatewayDiscover
    {
        /// <summary>
        /// 本发现器的测量途径模式
        /// </summary>
        public abstract GatewayModel GatewayModel { get; set; }

        /// <summary>
        /// 本测量途径挂接的仪器或设备列表
        /// </summary>
        public ObservableCollection<Instrument> Instruments { get; } = new ObservableCollection<Instrument>();

        /// <summary>
        /// 建立仪器信息
        /// </summary>
        public abstract void CreateInstrument();

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
        /// 开始搜索该测量途径下的所有仪器(一般来讲这是一个异步操作)
        /// </summary>
        public abstract void BeginDiscover();

        protected virtual void OnDiscovered()
        {
            Discovered?.Invoke(this, EventArgs.Empty);
        }
    }
}
