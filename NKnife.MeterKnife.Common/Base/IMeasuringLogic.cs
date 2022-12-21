using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    ///     采集数据的存储逻辑
    /// </summary>
    public interface IMeasuringLogic
    {
        /// <summary>
        ///     处理当前的被测物的测量数据
        /// </summary>
        /// <param name="dut">指定的工程与被测物</param>
        /// <param name="data">数据</param>
        Task<bool> ProcessAsync((Project, DUT) dut, MeasureData data);

        /// <summary>
        ///     根据发送源命令的关系获取被测物
        /// </summary>
        /// <param name="relation">源命令的关系</param>
        /// <returns>工程与被测物</returns>
        (Project, DUT) GetDUT(string relation);

        /// <summary>
        ///     设置命令字与指定的工程与被测物的关系
        /// </summary>
        /// <param name="dutId">源命令的关系</param>
        /// <param name="dut">指定的工程与被测物</param>
        void SetDUT(string dutId, (Project, DUT) dut);

        /// <summary>
        ///     设置命令字与被测物的关系
        /// </summary>
        void SetDUTMap(List<ScpiCommandPool> commands, Project project);

    }
}