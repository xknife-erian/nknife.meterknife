using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.Db;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// 面向本项目（水域表具管理）的一些数据层扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IWmStorageRead<T, in TId>
    {
        /// <summary>
        /// 分页查询指定ID的组织关联的实体
        /// </summary>
        /// <param name="organizationId">指定组织的ID</param>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>该组织管理（关联）的实体</returns>
        Task<IEnumerable<T>> PageByOrganizationAsync(string organizationId, int pageSize, int pageNumber, SortDirection direction = SortDirection.NONE, bool skipDeleteTag = false);

        /// <summary>
        /// 分页查询指定ID的社区关联的实体
        /// </summary>
        /// <param name="communityId">指定社区的ID</param>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>该社区管理（关联）的实体</returns>
        Task<IEnumerable<T>> PageByCommunityAsync(string communityId, int pageSize, int pageNumber, SortDirection direction = SortDirection.NONE, bool skipDeleteTag = false);

        /// <summary>
        /// 查询指定社区ID关联的记录统计数量
        /// </summary>
        /// <param name="communityId">社区ID</param>
        /// <param name="skipDeleteTag"></param>
        Task<long> CountByCommunityAsync(string communityId, bool skipDeleteTag = false);

        /// <summary>
        /// 查询指定组织ID关联的记录统计数量
        /// </summary>
        /// <param name="organizationId">组织ID</param>
        /// <param name="skipDeleteTag"></param>
        Task<long> CountByOrganizationAsync(string organizationId, bool skipDeleteTag = false);
    }
}