using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Logic
{
    /// <summary>
    ///     描述一次测量执行事件的数据逻辑
    ///     2020年1月18日新增
    /// </summary>
    public class PerformStorageLogic : IPerformStorageLogic
    {
        /// <summary>
        ///     采集编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     关于本次采集的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     被测单元
        /// </summary>
        public DUT Dut { get; set; }

        /// <summary>
        ///     处理当前的温度数据
        /// </summary>
        /// <param name="temp">温度数据</param>
        public void ProcessCurrentTemperature(MetricalData temp)
        {
        }
    }
}
