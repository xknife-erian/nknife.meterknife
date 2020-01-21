using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using NKnife.Db;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Base;

namespace NKnife.MeterKnife.Storage.Base
{
    public abstract class BaseStorageRead<T> : IStorageRead<T, string>, IWmStorageRead<T, string>
    {
        protected readonly IStorageManager _storageManager;

        protected BaseStorageRead(IStorageManager storageManager)
        {
            _storageManager = storageManager;
            TableName = BuildTableName(typeof(T).Name);
        }

        protected static string BuildTableName(string typeName)
        {
            return $"{typeName}s";
        }

        #region Implementation of IStorageRead<T,in string>

        /// <summary>
        ///     当前实现面向的数据表表名
        /// </summary>
        public string TableName { get; }

        /// <summary>
        ///     分页查询方法
        /// </summary>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>当前页的数据集合</returns>
        public virtual async Task<IEnumerable<T>> PageAsync(int pageNumber, int pageSize, SortDirection direction, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT * FROM {TableName} LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            if (!skipDeleteTag)
                sql = $"SELECT * FROM {TableName} WHERE {nameof(IDomain.State)}='{(short) RecordState.Normal}' LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        /// 分页查询指定ID的组织关联的实体
        /// </summary>
        /// <param name="organizationId">指定组织的ID</param>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>该组织管理（关联）的实体</returns>
        public virtual async Task<IEnumerable<T>> PageByOrganizationAsync(string organizationId, int pageSize, int pageNumber, SortDirection direction = SortDirection.NONE, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT * FROM {TableName} WHERE OrganizationId='{organizationId}' LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            if (!skipDeleteTag)
                sql = $"SELECT * FROM {TableName} WHERE OrganizationId='{organizationId}' AND {nameof(IDomain.State)}='{(short)RecordState.Normal}' LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        /// 分页查询指定ID的社区关联的实体
        /// </summary>
        /// <param name="communityId">指定社区的ID</param>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>该社区管理（关联）的实体</returns>
        public virtual async Task<IEnumerable<T>> PageByCommunityAsync(string communityId, int pageSize, int pageNumber, SortDirection direction = SortDirection.NONE, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT * FROM {TableName} WHERE CommunityId='{communityId}' LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            if (!skipDeleteTag)
                sql = $"SELECT * FROM {TableName} WHERE CommunityId='{communityId}' AND {nameof(IDomain.State)}='{(short)RecordState.Normal}' LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        /// 查询指定社区ID关联的记录统计数量
        /// </summary>
        /// <param name="communityId">社区ID</param>
        /// <param name="skipDeleteTag"></param>
        public virtual async Task<long> CountByCommunityAsync(string communityId, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT COUNT(*) FROM {TableName} WHERE CommunityId='{communityId}'";
            if (!skipDeleteTag)
                sql = $"SELECT COUNT(*) FROM {TableName} WHERE CommunityId='{communityId}' AND {nameof(IDomain.State)}='{(short)RecordState.Normal}'";
            var result = await conn.ExecuteScalarAsync<long>(sql);
            return result;
        }

        /// <summary>
        /// 查询指定组织ID关联的记录统计数量
        /// </summary>
        /// <param name="organizationId">组织ID</param>
        /// <param name="skipDeleteTag"></param>
        public virtual async Task<long> CountByOrganizationAsync(string organizationId, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT COUNT(*) FROM {TableName} WHERE OrganizationId='{organizationId}'";
            if (!skipDeleteTag)
                sql = $"SELECT COUNT(*) FROM {TableName} WHERE OrganizationId='{organizationId}' AND {nameof(IDomain.State)}='{(short)RecordState.Normal}'";
            var result = await conn.ExecuteScalarAsync<long>(sql);
            return result;
        }

        /// <summary>
        ///     根据指定的ID获取指定的记录并转换为对象
        /// </summary>
        /// <param name="id">指定的ID</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns></returns>
        public virtual async Task<T> FindOneByIdAsync(string id, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            if (skipDeleteTag)
                return await conn.QueryFirstAsync<T>($"SELECT * FROM {TableName} WHERE {nameof(IDomain.Id)}='{id}'");
            var sql = $"SELECT * FROM {TableName} WHERE {nameof(IDomain.Id)}='{id}' AND {nameof(IDomain.State)}='{(short) RecordState.Normal}'";
            return await conn.QueryFirstAsync<T>(sql);
        }

        /// <summary>
        ///     指定ID的记录是否存在
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>记录是否存在，true时存在指定ID的记录，false反之。</returns>
        public virtual async Task<bool> ExistAsync(string id, bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            var sql = skipDeleteTag
                ? $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(IDomain.Id)}='{id}'"
                : $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(IDomain.Id)}='{id}' AND {nameof(IDomain.State)}={(short) RecordState.Normal}";
            return await conn.ExecuteAsync(sql) > 0;
        }

        /// <summary>
        ///     查询记录的数据记录统计数量
        /// </summary>
        /// <param name="skipDeleteTag">是否跳过打上删除标记的记录，默认(当false时)有删除标记的记录不包含在结果中</param>
        /// <returns>数量</returns>
        public virtual async Task<long> CountAsync(bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            var sql = skipDeleteTag
                ? $"SELECT COUNT(*) FROM {TableName}"
                : $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(IDomain.State)}={(short) RecordState.Normal}";
            var count = await conn.ExecuteScalarAsync<long>(sql);
            return count;
        }

        /// <summary>
        ///     获取所有的组织
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAllAsync(bool skipDeleteTag = false)
        {
            var conn = _storageManager.OpenReadConnection();
            if (skipDeleteTag)
                return await conn.QueryAsync<T>($"SELECT * FROM {TableName}");
            var sql = $"SELECT * FROM {TableName} WHERE State='{(short) RecordState.Normal}'";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        /// 用指定的SQL语句获取胖实体集合。
        /// </summary>
        protected async Task<IEnumerable<TK>> FindFatAsync<TK>(string sql)
        {
            var conn = _storageManager.OpenReadConnection();
            var result = await conn.QueryAsync<TK>(sql);
            return result;
        }

        #endregion

    }
}