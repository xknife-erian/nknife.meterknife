using MeterKnife.Util.Tunnel.Common;

namespace MeterKnife.Util.Socket.Interfaces
{
    public interface IHeartbeatFilter
    {
        /// <summary>
        /// 心跳协议
        /// </summary>
        Heartbeat Heartbeat { get; set; }

        /// <summary>
        /// 心跳间隔
        /// </summary>
        double Interval { get; set; }

        /// <summary>
        /// 严格模式开关
        /// </summary>
        /// <returns>
        /// true  心跳返回内容一定要和HeartBeat类中定义的ReplayOfClient一致才算有心跳响应
        /// false 心跳返回任何内容均算有心跳相应
        /// </returns>
        bool EnableStrictMode { get; set; }

        /// <summary>
        /// 主动模式
        /// </summary>
        bool EnableAggressiveMode { get; set; }
    }
}