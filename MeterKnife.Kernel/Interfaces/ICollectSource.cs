using System;
using MeterKnife.Kernel.EventParameters;

namespace MeterKnife.Kernel.Interfaces
{
    /// <summary>
    /// 一个描述采集数据源的接口
    /// </summary>
    public interface ICollectSource
    {
        int Port { get; set; }
        int GpibAddress { get; set; }
        /// <summary>
        /// 当收到仪器采集数据时
        /// </summary>
        event EventHandler<CollectEventArgs> ReceviedCollectData;

        /// <summary>
        /// 当收到仪器的采集温度数据时
        /// </summary>
        event EventHandler<CollectEventArgs> ReceviedTemperatureData;
    }
}
