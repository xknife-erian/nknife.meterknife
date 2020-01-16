namespace MeterKnife.Util.Protocol
{
    /// <summary>
    /// 描述一个将协议内容按指定的格式组装成一个指定类型(一般是字符串，但也可以是任何，如文件)
    /// </summary>
    /// <typeparam name="TData">内容在编程过程所使用的数据形式,可视化的读取形式</typeparam>
    public interface IProtocolPacker<TData>
    {
        TData Combine(IProtocol<TData> content);
    }
}
