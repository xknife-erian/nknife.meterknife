using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IProjectLogic
    {
        /// <summary>
        ///     新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        Task<bool> CreateProjectAsync(Project project);

        /// <summary>
        ///     修改一个测量工程
        /// </summary>
        Task UpdateProjectAsync(Project eng);

        /// <summary>
        ///     获取指定被测物的测量数据
        /// </summary>
        /// <param name="dut">指定被测物</param>
        Task<IEnumerable<MeasureData>> GetDUTDataAsync((Project, DUT) dut);

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        Task RemoveProjectAsync(Project eng);

    }
}