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
        /// 本次通讯的关联
        /// </summary>
        string Relation { get; }
    }
}
