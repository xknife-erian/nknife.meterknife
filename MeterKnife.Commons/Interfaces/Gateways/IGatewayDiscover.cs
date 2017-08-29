using System;
using System.Collections.Generic;
using MeterKnife.Models;

namespace MeterKnife.Interfaces.Gateways
{
    /// <summary>
    /// 描述一个测量途径的发现器的接口
    /// </summary>
    public interface IGatewayDiscover
    {
        /// <summary>
        /// 本发现器的测量途径模式
        /// </summary>
        GatewayModel GatewayModel { get; set; }

        /// <summary>
        /// 本测量途径挂接的仪器或设备列表
        /// </summary>
        List<Instrument> Instruments { get; }

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        void AddInstrument();

        /// <summary>
        /// 当有仪器添加后
        /// </summary>
        event EventHandler<InstrumentAddedEventArgs> InstrumentAdded;

        /// <summary>
        /// 当自动发现仪器的动作执行完成
        /// </summary>
        event EventHandler Discovered;

        /// <summary>
        /// 开始搜索该测量途径下的所有仪器
        /// </summary>
        void BeginDiscover();
    }
}
