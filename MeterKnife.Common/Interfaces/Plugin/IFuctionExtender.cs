namespace MeterKnife.Interfaces.Plugin
{
    /// <summary>取号程序的功能的提供器，实现一些类似重启应用程序的功能
    /// </summary>
    public interface IFuctionExtender
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

        /// <summary>显示触摸屏界面
        /// </summary>
        void ShowTouchScreen();
    }
}