namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    /// 该接口描述了一些非业务逻辑的功能。如：重启应用程序等等。
    /// </summary>
    public interface IFuctionExtender : IExtender
    {
        /// <summary>清除运行时数据-如当前等候队列，当前出号序号等
        /// </summary>
        void ClearRunTimeData();

        /// <summary>启动排队服务
        /// </summary>
        void StartQService();

        /// <summary>停止排队服务
        /// </summary>
        void StopQService();

        /// <summary>排队服务启动完成
        /// </summary>
        void QServiceLoadComplate();

        /// <summary>重启应用程序
        /// </summary>
        void RestartApplication();
    }
}