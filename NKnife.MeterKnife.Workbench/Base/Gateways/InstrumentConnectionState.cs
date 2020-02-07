namespace NKnife.MeterKnife.Workbench.Base.Gateways
{
    /// <summary>
    /// 仪器的连接状态
    /// </summary>
    public enum InstrumentConnectionState
    {
        /// <summary>
        /// 连接成功，仪器信息匹配
        /// </summary>
        Successful,
        /// <summary>
        /// 连接失败
        /// </summary>
        Unsuccessful,
        /// <summary>
        /// 虽然连接成功，但仪器信息不匹配，可能替换成了其他仪器
        /// </summary>
        Replaced,
        /// <summary>
        /// 未知，未尝试连接
        /// </summary>
        Unknown
    }
}