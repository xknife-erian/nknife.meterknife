using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IDUTLogic
    {
        /// <summary>
        /// 新建一个被测物
        /// </summary>
        /// <param name="dut">被测物</param>
        Task<bool> BuildAsync(DUT dut);

        /// <summary>
        /// 删除一个被测物
        /// </summary>
        /// <param name="dut"></param>
        Task<bool> DeleteAsync(DUT dut);

        /// <summary>
        /// 获取所有的被测物
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DUT>> GetAllDUTAsync();
    }
}