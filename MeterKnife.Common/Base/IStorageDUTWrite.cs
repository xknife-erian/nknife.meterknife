using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    /// 针对存储层的增、删、改的方法封装, 并读写分离管理。
    /// </summary>
    public interface IStorageDUTWrite<in T>
    {
        /// <summary>
        /// 将指定的工程实体插入数据库中
        /// </summary>
        /// <param name="engineering">指定的被测试物</param>
        Task<bool> InsertAsync(Engineering engineering);

        /// <summary>
        /// 将指定的对象插入数据库中
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="domain">指定的对象</param>
        Task<bool> InsertAsync((Engineering, DUT) dut, T domain);

        /// <summary>
        /// 将指定的对象批量插入数据库中
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="domains">指定的对象</param>
        Task<bool> InsertManyAsync((Engineering, DUT) dut, IEnumerable<T> domains);

        /// <summary>
        /// 更新指定的对象
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="domain">指定的对象</param>
        Task<bool> UpdateAsync((Engineering, DUT) dut, T domain);

        /// <summary>
        /// 更新指定的工程实体
        /// </summary>
        /// <param name="engineering">指定的工程实体</param>
        Task<bool> UpdateAsync(Engineering engineering);

        /// <summary>
        /// 根据记录ID，从数据库中移除该记录，该记录被移除后，不可恢复
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="id">指定的记录ID</param>
        Task<bool> RemoveAsync((Engineering, DUT) dut, DateTime id);
    }
}