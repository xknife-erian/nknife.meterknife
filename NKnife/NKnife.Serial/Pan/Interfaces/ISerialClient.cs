using SerialKnife.Interfaces;

namespace SerialKnife.Pan.Interfaces
{
    /// <summary>描述一个串口操作的接口
    /// </summary>
    internal interface ISerialClient
    {
        /// <summary>该串口是否激活
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        bool Active { get; }

        /// <summary>向串口即将发送的指令包的集合
        /// </summary>
        ISerialDataPool DataPool { get; }

        /// <summary>打开端口
        /// </summary>
        /// <param name="port">用序号表达一个串口</param>
        /// <returns></returns>
        bool OpenPort(ushort port);

        /// <summary>关闭端口
        /// </summary>
        /// <returns></returns>
        bool ClosePort();
    }
}