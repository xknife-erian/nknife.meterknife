using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels.Care;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.Util.Tunnel.Base;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    ///     为应用程序提供插槽的管理服务
    /// </summary>
    public interface ISlotService
    {
        /// <summary>
        ///     绑定一个指定插槽的通讯服务
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        /// <param name="processor">指定的插槽连接器</param>
        /// <param name="handlers">协议处理的handler</param>
        void Bind(Slot slot, SlotProcessor processor, params CareProtocolHandler[] handlers);

        /// <summary>
        ///     移除指定插槽的一个处理器
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        /// <param name="handler">处理器</param>
        void Remove(Slot slot, CareProtocolHandler handler);

        /// <summary>
        ///     销毁服务
        /// </summary>
        void Destroy();

        /// <summary>
        ///     启动指定插槽的服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>启动是否成功</returns>
        bool Start(Slot slot);

        /// <summary>
        ///     停止指定插槽的服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>停止是否成功</returns>
        bool Stop(Slot slot);

        /// <summary>
        ///     向指定插槽发送Scpi命令组
        /// </summary>
        /// <param name="slot">指定插槽</param>
        /// <param name="cmdArray">即将发送的命令组</param>
        void SendCommands(Slot slot, params CareCommand[] cmdArray);

        /// <summary>
        ///     向指定插槽发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        /// <param name="cmdArray">即将发送的命令组</param>
        void SendLoopCommands(Slot slot, params CareCommand[] cmdArray);
    }
}