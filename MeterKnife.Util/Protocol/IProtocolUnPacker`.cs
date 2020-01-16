namespace MeterKnife.Util.Protocol
{
    /// <summary>
    /// 当通讯的一端接收到消息后，将消息进行处理的解析器
    /// </summary>
    /// <typeparam name="TData">内容在编程过程所使用的数据形式,可视化的读取形式</typeparam>
    public interface IProtocolUnPacker<TData>
    {
        /// <summary>开始执行协议的解析
        /// </summary>
        void Execute(IProtocol<TData> protocol, TData data, TData command);
    }
}
