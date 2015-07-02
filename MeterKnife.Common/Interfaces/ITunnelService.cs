using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using NKnife.Tunnel.Base;

namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 为应用程序提供管道服务
    /// </summary>
    public interface ITunnelService<T>
    {
        /// <summary>
        /// 绑定一个指定端口的通讯服务
        /// </summary>
        /// <param name="carePort">指定的Care端口</param>
        /// <param name="handlers">协议处理的handler</param>
        void Bind(CarePort carePort, params BaseProtocolHandler<T>[] handlers);
        /// <summary>
        /// 销毁服务
        /// </summary>
        void Destroy();
        /// <summary>
        /// 启动指定端口的串口服务
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <returns>启动是否成功</returns>
        bool Start(CarePort carePort);
        /// <summary>
        /// 停止批定端口的串口服务
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <returns>停止是否成功</returns>
        bool Stop(CarePort carePort);
        /// <summary>
        /// 向指定端口发送数据
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="data">即将发送的数据</param>
        void Send(CarePort carePort, byte[] data);
    }
}