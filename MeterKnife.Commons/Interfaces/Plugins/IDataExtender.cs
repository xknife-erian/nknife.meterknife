namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    /// IQRunTime的基接口，描述取号程序的数据提供器
    /// </summary>
    public interface IDataExtender : IExtender
    {
        /// <summary>获取窗口某业务的等候人数
        /// </summary>
        /// <returns>等候人数</returns>
        int GetWaitCountByCounter(int counterId);
    }
}