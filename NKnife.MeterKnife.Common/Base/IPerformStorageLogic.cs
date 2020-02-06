using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    ///     采集数据的存储逻辑
    /// </summary>
    public interface IPerformStorageLogic
    {
        /// <summary>
        ///     处理当前的被测物的测量数据
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <param name="data">数据</param>
        Task<bool> ProcessAsync((Engineering, DUT) dut, MetricalData data);

        /// <summary>
        ///     根据发送源命令的关系获取被测物
        /// </summary>
        /// <param name="relation">源命令的关系</param>
        /// <returns>被测物</returns>
        (Engineering, DUT) GetDUT(string relation);

        /// <summary>
        ///     设置命令字与被测物的关系
        /// </summary>
        /// <param name="relation">源命令的关系</param>
        /// <param name="dut">被测物</param>
        void SetDUT(string relation, (Engineering, DUT) dut);
    }
}