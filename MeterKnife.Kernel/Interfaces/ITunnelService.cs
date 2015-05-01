using MeterKnife.Kernel.Tunnels;

namespace MeterKnife.Kernel.Interfaces
{
    /// <summary>
    /// 为应用程序提供管理服务
    /// </summary>
    public interface ITunnelService
    {
        /// <summary>
        /// 创建一个指定端口的串口服务
        /// </summary>
        /// <param name="port">指定的端口</param>
        /// <param name="handlers">协议处理的handler</param>
        void Build(int port, params CareOneProtocolHandler[] handlers);
        /// <summary>
        /// 销毁指定端口的串口服务
        /// </summary>
        /// <param name="port"></param>
        void Destroy(int port);
        /// <summary>
        /// 启动指定端口的串口服务
        /// </summary>
        /// <param name="port">指定端口</param>
        /// <returns>启动是否成功</returns>
        bool Start(int port);
        /// <summary>
        /// 停止批定端口的串口服务
        /// </summary>
        /// <param name="port">指定端口</param>
        /// <returns>停止是否成功</returns>
        bool Stop(int port);
        /// <summary>
        /// 向指定端口发送数据
        /// </summary>
        /// <param name="port">指定端口</param>
        /// <param name="data">即将发送的数据</param>
        void Send(int port, byte[] data);
    }
}