using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Instrument> Instruments { get; }

        /// <summary>
        /// 建立仪器信息
        /// </summary>
        void CreateInstrument();

        /// <summary>
        /// 删除仪器信息
        /// </summary>
        /// <param name="instrument">指定的仪器</param>
        void DeleteInstrument(Instrument instrument);

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
