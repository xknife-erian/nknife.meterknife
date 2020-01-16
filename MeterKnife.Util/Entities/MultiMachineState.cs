namespace MeterKnife.Util.Entities
{
    /// <summary>
    /// 应用程序状态
    /// </summary>
    public enum MultiMachineState
    {
        /// <summary>
        /// 单机处理
        /// </summary>
        Single,

        /// <summary>
        /// 主从模式的主机
        /// </summary>
        Master,

        /// <summary>
        /// 主从模式的从机
        /// </summary>
        Slave
    }
}