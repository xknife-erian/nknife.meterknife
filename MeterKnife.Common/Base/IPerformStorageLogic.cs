using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    /// 采集数据的存储逻辑
    /// </summary>
    public interface IPerformStorageLogic
    {
        /// <summary>
        ///     处理当前的被测物的测量数据
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <param name="data">数据</param>
        Task<bool> ProcessAsync(DUT dut, MetricalData data);

        /// <summary>
        /// 根据协议的命令字获取被测物（通常是Care自带的数据采集）
        /// </summary>
        /// <param name="mainCommand">主命令字</param>
        /// <param name="subCommand">子命令字</param>
        /// <returns>被测物</returns>
        DUT GetDUT(byte mainCommand, byte subCommand);

        /// <summary>
        /// 根据发送协议获取被测物
        /// </summary>
        /// <param name="sourceCommand">源命令</param>
        /// <returns>被测物</returns>
        DUT GetDUT(byte[] sourceCommand);
    }
}