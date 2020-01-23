namespace NKnife.MeterKnife.Util.Tunnel
{
    /// <summary>
    ///     通道内的数据包装
    /// </summary>
    public interface ITunnelSession
    {
        long Id { get; }

        /// <summary>
        /// 这次数据产生的发生源，即发送的数据。问什么，答什么……
        /// </summary>
        byte[] Source { get; set; }

        /// <summary>
        /// 原始数据。内容在原始状态时所使用的数据形式。
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// 如Protocol来包装数据，可以使用该Tag来存放个性化的数据Wrapper
        /// </summary>
        object Tag { get; }
    }
}
