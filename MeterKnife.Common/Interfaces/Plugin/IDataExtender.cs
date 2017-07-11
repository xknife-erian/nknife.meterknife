namespace MeterKnife.Interfaces.Plugin
{
    /// <summary>
    /// IQRunTime的基接口，描述取号程序的数据提供器
    /// </summary>
    public interface IDataExtender
    {
        /// <summary>获取窗口某业务的等候人数
        /// </summary>
        /// <returns>等候人数</returns>
        int GetWaitCountByCounter(int counterId);

        /// <summary>返回指定分段标记的等候人数
        /// </summary>
        /// <param name="callHead">分段标记</param>
        /// <returns>等候人数</returns>
        int GetWaitCountByCallHead(string callHead);

        /// <summary>
        /// 找到和该客户等级具有相同分段标记的所有客户等级等候人数之和
        /// </summary>
        /// <param name="vipId">客户等级</param>
        /// <returns>等候人数</returns>
        int GetWaitCountByVipId(string vipId);

        /// <summary>返回转移到窗口的等候号码
        /// </summary>
        /// <param name="counterIndex"></param>
        /// <returns></returns>
        string GetWinQNumberList(int counterIndex);

        /// <summary>
        /// 返回等候号码集合
        /// </summary>
        /// <returns></returns>
        string GetQNumberList();

        /// <summary>
        /// 删除指定的等候号码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        bool DeleteNumber(string number);
    }
}