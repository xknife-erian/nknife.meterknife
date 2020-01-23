using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common
{
    public interface IPerformStorageLogic
    {
        /// <summary>
        ///     处理当前的温度数据
        /// </summary>
        /// <param name="temp">温度数据</param>
        void ProcessCurrentTemperature(MetricalData temp);
    }
}