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
                IDomain d = (IDomain) domain;
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
        ///     根据记录ID，逻辑删除指定记录，即打上“已删除标记”，但并不从数据库中删除记录
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        public async Task<bool> LogicDeleteAsync(string id)
        {
            var conn = _storageManager.OpenWriteConnection();
            var sql = $"UPDATE {TableName} SET {nameof(IDomain.State)}={(short) RecordState.Deleted} WHERE {nameof(IDomain.Id)}='{id}'";
            var i = await conn.ExecuteAsync(sql);
            return i == 1;
        }

        /// <summary>
        /// 根据记录ID集合，逻辑删除指定记录，即打上“已删除标记”，但并不从数据库中删除记录
        /// </summary>
        /// <param name="ids">指定的记录ID集合</param>
        public async Task<bool> LogicDeleteMultiAsync(IEnumerable<string> ids)
        {
            if (ids == null)
                return await Task.Run((() => true));
            var array = ids.ToArray();
            if(array.Length<=0)
                return await Task.Run((() => true));

            var conn = _storageManager.OpenWriteConnection();
            var sb = new StringBuilder($"UPDATE {TableName} SET {nameof(IDomain.State)}={(short) RecordState.Deleted} WHERE");
            for (int j = 0; j < array.Count(); j++)
            {
                if (j > 0)
                    sb.Append(" OR ");
                else
                    sb.Append(' ');
                sb.Append($"{nameof(IDomain.Id)}='{array[j]}'");
            }
            var i = await conn.ExecuteAsync(sb.ToString());
            return i == 1;
        }

        /// <summary>
        ///     根据记录ID，从数据库中移除该记录，该记录被移除后，不可恢复
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        public async Task<bool> RemoveAsync(string id)
        {
            var conn = _storageManager.OpenWriteConnection();
            var sql = $"DELETE FROM {TableName} WHERE {nameof(IDomain.Id)}='{id}'";
            var i = await conn.ExecuteAsync(sql);
            return i == 1;
        }

        protected string GetSqlKey()
        {
            return TableName.Substring(0, TableName.Length - 1);
        }
    }
}