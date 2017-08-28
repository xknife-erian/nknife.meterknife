using System;
using System.Collections.Generic;
using MeterKnife.Models;

namespace MeterKnife.Interfaces.Gateways
{
    /// <summary>
    /// 描述一个通道的发现器的接口
    /// </summary>
    public interface IGatewayDiscover
    {
        /// <summary>
        /// 本发现器的通道模式
        /// </summary>
        GatewayModel GatewayModel { get; set; }

        /// <summary>
        /// 本通道挂接的仪器或设备列表
        /// </summary>
        List<Instrument> Instruments { get; }

        /// <summary>
        /// 当自动发现仪器的动作执行完成
        /// </summary>
        event EventHandler Discovered;

        void BeginDiscover();
    }
}
