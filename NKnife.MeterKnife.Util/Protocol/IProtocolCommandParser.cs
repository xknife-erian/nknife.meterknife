namespace NKnife.MeterKnife.Util.Protocol
{
    /// <summary>
    /// 从原生消息体中获取命令字
    /// </summary>
    public interface IProtocolCommandParser<TData>
    {
        /// <summary>从原生消息体中获取命令字
        /// </summary>
        /// <param name="datagram">The datagram.</param>
        /// <returns></returns>
        TData GetCommand(TData datagram);
    }
}
