using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using NKnife.Db;
using NKnife.Interface;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.Util;
using NLog;

namespace NKnife.MeterKnife.Storage.Base
{
    public class StoragePlatform<T> : IStoragePlatform<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        protected readonly SqlSet _sqlSet;

        protected readonly IStorageManager _storageManager;

        public StoragePlatform(IStorageManager storageManager)
        {
            _storageManager = storageManager;
            switch (storageManager.CurrentDbType)
            {
                case DatabaseType.SqLite:
                    _sqlSet = storageManager.SqlSetMap.Sqlite;
                    break;
                case DatabaseType.MySql:
                    _sqlSet = storageManager.SqlSetMap.Mysql;
                    break;
            }
            TableName = BuildTableName(typeof(T).Name);
        }

        protected static string BuildTableName(string typeName)
        {
            return $"{typeName}s";
        }

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
        /// <returns>当前页的数据集合</returns>
        public virtual async Task<IEnumerable<T>> PageAsync(int pageNumber, int pageSize, SortDirection direction)
        {
            var conn = _storageManager.OpenPlatformConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT * FROM {TableName} LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        ///     根据指定的ID获取指定的记录并转换为对象
        /// </summary>
        /// <param name="id">指定的ID</param>
        /// <returns></returns>
        public virtual async Task<T> FindOneByIdAsync(string id)
        {
            var conn = _storageManager.OpenPlatformConnection();
            return await conn.QueryFirstAsync<T>($"SELECT * FROM {TableName} WHERE {nameof(IId.Id)}='{id}'");
        }

        /// <summary>
        ///     指定ID的记录是否存在
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        /// <returns>记录是否存在，true时存在指定ID的记录，false反之。</returns>
        public virtual async Task<bool> ExistAsync(string id)
        {
            var conn = _storageManager.OpenPlatformConnection();
            var sql = $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(IId.Id)}='{id}'";
            return await conn.ExecuteAsync(sql) > 0;
        }

        /// <summary>
        ///     查询记录的数据记录统计数量
        /// </summary>
        /// <returns>数量</returns>
        public virtual async Task<long> CountAsync()
        {
            var conn = _storageManager.OpenPlatformConnection();
            var sql = $"SELECT COUNT(*) FROM {TableName}";
            var count = await conn.ExecuteScalarAsync<long>(sql);
            return count;
        }

        /// <summary>
        ///     获取所有记录
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            var conn = _storageManager.OpenPlatformConnection();
            var sql = $"SELECT * FROM '{TableName}'";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        ///     将指定的对象插入数据库中
        /// </summary>
        /// <param name="domain">指定的对象</param>
        public async Task<bool> InsertAsync(T domain)
        {
            if (domain == null)
                return false;
            var conn = _storageManager.OpenPlatformConnection();
            var sql = _sqlSet[GetSqlKey()].Insert;
            var i = 0;
            try
            {
                i = await conn.ExecuteAsync(sql, domain);
            }
            catch (Exception e)
            {
                _Logger.Error($"数据库新增数据异常。\r\nExceptionMessage: {e.Message}\r\nSQL: {sql}\r\nDomain: {JsonConvert.SerializeObject(domain)}");
            }

            return i == 1;
        }

        /// <summary>
        ///     将指定的对象批量插入数据库中
        /// </summary>
        /// <param name="domains">指定的对象</param>
        public async Task<bool> InsertManyAsync(IEnumerable<T> domains)
        {
            var conn = _storageManager.OpenPlatformConnection();
            var sql = _sqlSet[GetSqlKey()].Insert;
            var i = await conn.ExecuteAsync(sql, domains);
            return i == domains.Count();
        }

        /// <summary>
        ///     更新指定的对象
        /// </summary>
        /// <param name="domain">指定的对象</param>
        public async Task<bool> UpdateAsync(T domain)
        {
            var conn = _storageManager.OpenPlatformConnection();
            var sql = _sqlSet[GetSqlKey()].Update;
            try
            {
                var d = (IId) domain;
                sql = $"{sql} Where Id='{d.Id}'"; //TODO:有巨大的问题，此处需要再好好构思一下。
                var i = await conn.ExecuteAsync(sql, domain);
                return i == 1;
            }
            catch (Exception e)
            {
                _Logger.Warn(e, $"数据库更新实体时写库异常：{e.Message}\r\n{sql}");
                return false;
            }
        }

        /// <summary>
        ///     根据记录ID，从数据库中移除该记录，该记录被移除后，不可恢复
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        public async Task<bool> RemoveAsync(string id)
        {
            var conn = _storageManager.OpenPlatformConnection();
            var sql = $"DELETE FROM {TableName} WHERE {nameof(IId.Id)}='{id}'";
            var i = await conn.ExecuteAsync(sql);
            return i == 1;
        }

        protected string GetSqlKey()
        {
            return TableName.Substring(0, TableName.Length - 1);
        }
    }
}