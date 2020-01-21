using System.Collections.Generic;
using System.Threading.Tasks;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// 针对存储层的增、删、改的方法封装, 并读写分离管理。
    /// </summary>
    public interface IStorageWrite<in T, in TId>
    {   
        /// <summary>
        /// 将指定的对象插入数据库中
        /// </summary>
        /// <param name="domain">指定的对象</param>
        Task<bool> InsertAsync(T domain);

        /// <summary>
        /// 将指定的对象批量插入数据库中
        /// </summary>
        /// <param name="domains">指定的对象</param>
        Task<bool> InsertManyAsync(IEnumerable<T> domains);

        /// <summary>
        /// 更新指定的对象
        /// </summary>
        /// <param name="domain">指定的对象</param>
        Task<bool> UpdateAsync(T domain);

        /// <summary>
        /// 根据记录ID，逻辑删除指定记录，即打上“已删除标记”，但并不从数据库中删除记录
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        Task<bool> LogicDeleteAsync(TId id);

        /// <summary>
        /// 根据记录ID集合，逻辑删除指定记录，即打上“已删除标记”，但并不从数据库中删除记录
        /// </summary>
        /// <param name="ids">指定的记录ID集合</param>
        Task<bool> LogicDeleteMultiAsync(IEnumerable<TId> ids);

        /// <summary>
        /// 根据记录ID，从数据库中移除该记录，该记录被移除后，不可恢复
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        Task<bool> RemoveAsync(TId id);
    }
}