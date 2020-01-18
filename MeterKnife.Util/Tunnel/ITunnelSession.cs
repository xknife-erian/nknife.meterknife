namespace NKnife.MeterKnife.Util.Tunnel
{
    public interface ITunnelSession
    {
        long Id { get; }
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
