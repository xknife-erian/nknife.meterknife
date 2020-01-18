using NKnife.MeterKnife.Common.DataModels;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    ///     温度获取器
    /// </summary>
    public interface ITemperatureGetter
    {
        /// <summary>
        ///     获取间隔
        /// </summary>
        int Interval { get; }

        /// <summary>
        ///     启动采集
        /// </summary>
        /// <param name="slot">采集的端口</param>
        bool StartCollect(Slot slot);

        /// <summary>
        ///     关闭采集
        /// </summary>
        /// <param name="slot">采集的端口</param>
        bool CloseCollect(Slot slot);
    }
}