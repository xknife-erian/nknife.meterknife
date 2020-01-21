using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using NKnife.Db;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Base;
using NLog;

namespace NKnife.MeterKnife.Storage.Base
{
    public abstract class BaseStorageWrite<T> : IStorageWrite<T, string>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        protected readonly IStorageManager _storageManager;
        protected readonly SqlSet _sqlSet;

        protected BaseStorageWrite(IStorageManager storageManager)
        {
            _storageManager = storageManager;
            _sqlSet = storageManager.SqlSetMap[storageManager.CurrentDbType];
            TableName = BuildTableName(typeof(T).Name);
        }

        /// <summary>
        ///     当前实现面向的数据表表名
        /// </summary>
        public string TableName { get; }

        protected static string BuildTableName(string typeName)
        {
            return $"{typeName}s";
        }

        /// <summary>
        ///     将指定的对象插入数据库中
        /// </summary>
        /// <param name="domain">指定的对象</param>
        public async Task<bool> InsertAsync(T domain)
        {
            if (domain == null)
                return false;
            var conn = _storageManager.OpenWriteConnection();
            var sql = _sqlSet.Insert[GetSqlKey()];
            int i = 0;
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
            var conn = _storageManager.OpenWriteConnection();
            var sql = _sqlSet.Insert[GetSqlKey()];
            var i = await conn.ExecuteAsync(sql, domains);
            return i == domains.Count();
        }

        /// <summary>
        ///     更新指定的对象
        /// </summary>
        /// <param name="domain">指定的对象</param>
        public async Task<bool> UpdateAsync(T domain)
        {
            var conn = _storageManager.OpenWriteConnection();
            var sql = _sqlSet.Update[GetSqlKey()];
            try
            {
                IRecord<T> d = (IRecord<T>) domain;
                sql = $"{sql} Where Id='{d.Id}'";//TODO:有巨大的问题，此处需要再好好构思一下。
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
            var conn = _storageManager.OpenWriteConnection();
            var sql = $"DELETE FROM {TableName} WHERE {nameof(IRecord<T>.Id)}='{id}'";
            var i = await conn.ExecuteAsync(sql);
            return i == 1;
        }

        protected string GetSqlKey()
        {
            return TableName.Substring(0, TableName.Length - 1);
        }
    }
}