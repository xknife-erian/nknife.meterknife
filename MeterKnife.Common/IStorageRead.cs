using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.Db;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// 针对存储层的查询方法封装, 并读写分离管理。
    /// </summary>
    public interface IStorageRead<T, in TId>
    {
        /// <summary>
        /// 指定ID的记录是否存在
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        /// <returns>记录是否存在，true时存在指定ID的记录，false反之。</returns>
        Task<bool> ExistAsync(TId id);

        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <returns>当前页的数据集合</returns>
        Task<IEnumerable<T>> PageAsync(int pageNumber, int pageSize, SortDirection direction = SortDirection.NONE);

        /// <summary>
        /// 查询数据记录的总数量
        /// </summary>
        /// <returns>数量</returns>
        Task<long> CountAsync();

        /// <summary>
        /// 根据指定的ID获取指定的记录并转换为对象
        /// </summary>
        /// <param name="id">指定的ID</param>
        /// <returns></returns>
        Task<T> FindOneByIdAsync(TId id);

        /// <summary>
        /// 获取所有的记录
        /// </summary>
        /// <returns>所有的记录</returns>
        Task<IEnumerable<T>> FindAllAsync();
    }
}
