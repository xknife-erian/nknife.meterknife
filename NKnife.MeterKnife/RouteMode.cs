using System;

namespace NKnife.MeterKnife.Core
{
    /// <summary>
    /// 连接器的多路采集的技术模式。
    /// </summary>
    public enum RouteMode
    {
        /// <summary>
        /// 通过连接器实现多路
        /// </summary>
        Connection,
        /// <summary>
        /// 通过某台设备实现多路
        /// </summary>
        Instrument
    }
}
