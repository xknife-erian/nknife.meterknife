using System;
using MeterKnife.Common.EventParameters;

namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述采集数据源的接口
    /// </summary>
    public interface ICollectSource
    {
        IMeter Meter { get; set; }

        /// <summary>
        /// 当收到仪器采集数据时
        /// </summary>
        event EventHandler<CollectEventArgs> ReceviedCollectData;
    }
}
