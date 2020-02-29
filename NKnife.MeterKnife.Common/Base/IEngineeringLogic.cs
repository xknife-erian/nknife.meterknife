using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IEngineeringLogic
    {
        /// <summary>
        ///     新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        Task<bool> CreateEngineeringAsync(Engineering engineering);

        /// <summary>
        ///     获取指定被测物的测量数据
        /// </summary>
        /// <param name="dut">指定被测物</param>
        Task<IEnumerable<MeasureData>> GetDUTDataAsync((Engineering, DUT) dut);

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        Task RemoveEnginneringAsync(Engineering eng);
    }
}