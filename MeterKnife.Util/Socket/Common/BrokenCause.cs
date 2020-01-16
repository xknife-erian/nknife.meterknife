namespace NKnife.Socket.Common
{
    /// <summary>
    /// 连接中断原因
    /// </summary>
    public enum BrokenCause
    {
        /// <summary>
        /// 主动断开
        /// </summary>
        Aggressive, 
        /// <summary>
        /// 被动断开
        /// </summary>
        Passive, 
        /// <summary>
        /// 丢失心跳
        /// </summary>
        LoseHeartbeat
    }
}