using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using NKnife.Base;
using NKnife.Tunnel.Base;
using ScpiKnife;

namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    ///     为应用程序提供管道服务
    /// </summary>
    public interface ITunnelService<T>
    {
        /// <summary>
        ///     绑定一个指定端口的通讯服务
        /// </summary>
        /// <param name="carePort">指定的Care端口</param>
        /// <param name="handlers">协议处理的handler</param>
        void Bind(CommPort carePort, params BaseProtocolHandler<T>[] handlers);

        /// <summary>
        ///     销毁服务
        /// </summary>
        void Destroy();

        /// <summary>
        ///     启动指定端口的串口服务
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <returns>启动是否成功</returns>
        bool Start(CommPort carePort);

        /// <summary>
        ///     停止批定端口的串口服务
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <returns>停止是否成功</returns>
        bool Stop(CommPort carePort);

        /// <summary>
        ///     向指定端口发送Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="careItems">即将发送的命令组</param>
        void SendCommands(CommPort carePort, params ScpiCommandQueue.Item[] careItems);

        /// <summary>
        ///     向指定端口发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="commandArrayKey">命令组的Key</param>
        /// <param name="careItems">即将发送的命令组</param>
        void SendLoopCommands(CommPort carePort, string commandArrayKey, params ScpiCommandQueue.Item[] careItems);
    }
}