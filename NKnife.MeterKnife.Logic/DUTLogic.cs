using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Logic
{
    public class DUTLogic : IDUTLogic
    {
        private readonly IStoragePlatform<DUT> _storage;

        public DUTLogic(IStoragePlatform<DUT> storage)
        {
            _storage = storage;
        }

        #region Implementation of IDUTLogic

        /// <summary>
        /// 创建一个被测物
        /// </summary>
        /// <param name="dut">被测物</param>
        public async Task<bool> BuildAsync(DUT dut)
        {
            return await _storage.InsertAsync(dut);
        }

        /// <summary>
        /// 删除一个被测物
        /// </summary>
        /// <param name="dut"></param>
        public async Task<bool> DeleteAsync(DUT dut)
        {
            return await _storage.RemoveAsync(dut.Id);
        }

        /// <summary>
        /// 获取所有的被测物
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DUT>> GetAllDUTAsync()
        {
            return await _storage.FindAllAsync();
        }

        #endregion
    }
}
