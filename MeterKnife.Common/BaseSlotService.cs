using System.Linq;
using MeterKnife.Util.Scpi;
using MeterKnife.Util.Tunnel.Base;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Tunnels.CareOne;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// Ant版提供的通讯服务
    /// </summary>
    public abstract class BaseSlotService : ITunnelService<byte[]>
    {
        void ITunnelService<byte[]>.Bind(Slot slot, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(slot, handlers.Cast<CareOneProtocolHandler>().ToArray());
        }

        /// <summary>
        ///     销毁服务
        /// </summary>
        public abstract void Destroy();

        /// <summary>
        ///     向指定端口发送Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="careItems">即将发送的命令组</param>
        public abstract void SendCommands(Slot carePort, params ScpiCommandQueue.Item[] careItems);

        /// <summary>
        ///     向指定端口发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="commandArrayKey">命令组的Key</param>
        /// <param name="careItems">即将发送的命令组</param>
        public abstract void SendLoopCommands(Slot carePort, string commandArrayKey, params ScpiCommandQueue.Item[] careItems);

        /// <summary>
        ///     启动指定端口的串口服务
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <returns>启动是否成功</returns>
        public abstract bool Start(Slot carePort);

        /// <summary>
        ///     停止批定端口的串口服务
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <returns>停止是否成功</returns>
        public abstract bool Stop(Slot carePort);

        /// <summary>
        ///     绑定一个指定端口的通讯服务
        /// </summary>
        /// <param name="slot">指定的端口</param>
        /// <param name="handlers">协议处理的handler</param>
        public abstract void Bind(Slot slot, params CareOneProtocolHandler[] handlers);

        /// <summary>
        ///     移除一个指定端口的通讯服务
        /// </summary>
        /// <param name="slot">指定的端口</param>
        /// <param name="handler">协议处理的handler</param>
        public abstract void Remove(Slot slot, CareOneProtocolHandler handler);
    }
}