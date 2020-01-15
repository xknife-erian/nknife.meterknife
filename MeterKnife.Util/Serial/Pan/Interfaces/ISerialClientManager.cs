using SerialKnife.Common;
using SerialKnife.Pan.Common;

namespace SerialKnife.Pan.Interfaces
{
    /// <summary>串口通讯管理器
    /// </summary>
    public interface ISerialClientManager
    {
        /// <summary>添加一个串口
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <param name="serialType"></param>
        /// <param name="enableDetialLog"></param>
        /// <returns></returns>
        bool AddPort(ushort port, SerialType serialType = SerialType.WinApi, bool enableDetialLog = false);

        /// <summary>
        /// 关闭一个串口
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <returns></returns>
        bool RemovePort(ushort port);

        /// <summary>关闭所有串口，并将所有串口从管理器中移除
        /// </summary>
        /// <returns></returns>
        bool RemoveAllPorts();

        /// <summary>向指定的串口写入数据包
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <param name="package">包含发送数据，以及相关指令及信息与事件的封装</param>
        /// <returns></returns>
        bool AddPackage(ushort port, PackageBase package);
    }
}