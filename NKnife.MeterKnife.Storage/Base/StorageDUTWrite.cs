﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NLog;

namespace NKnife.MeterKnife.Storage.Base
{
    public class StorageDUTWrite<T> : IStorageDUTWrite<T>
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        protected readonly IStorageManager _storageManager;
        protected readonly SqlSet _sqlSet;

        public StorageDUTWrite(IStorageManager storageManager)
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

        /// <summary>
        ///     当前实现面向的数据表表名
        /// </summary>
        public string TableName { get; }

        protected static string BuildTableName(string typeName)
        {
            return $"{typeName}s";
        }

        /// <summary>
        /// 将指定的工程实体插入数据库中
        /// </summary>
        /// <param name="project">指定的被测试物</param>
        public async Task<bool> InsertAsync(Project project)
        {
            if (project == null)
                return false;
            var conn = _storageManager.OpenConnection(project);
            var key = GetSqlKey();
            var sql = _sqlSet[key].Insert;
            int i = 0;
            try
            {
                i = await conn.ExecuteAsync(sql, project);
            }
            catch (Exception e)
            {
                _Logger.Error($"数据库新增数据异常。\r\nExceptionMessage: {e.Message}\r\nSQL: {sql}\r\nDomain: {JsonConvert.SerializeObject(project)}");
            }
            return i == 1;
        }

        /// <summary>
        ///     将指定的对象插入数据库中
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="domain">指定的对象</param>
        public async Task<bool> InsertAsync((Project, DUT) dut, T domain)
        {
            if (domain == null)
                return false;
            //根据不同的工程获取不同的数据库连接
            var conn = _storageManager.OpenConnection(dut.Item1);
            var key = GetSqlKey();
            if (!_sqlSet.ContainsKey(key))
            {
                _Logger.Debug($"Sql预置语句不包含“{key}”的语句。{JsonConvert.SerializeObject(_sqlSet.Keys)}");
                return false;
            }
            var sql = _sqlSet[key].Insert;
            //数据表的表名不再是实体名，而是被测物ID
            sql = sql.Replace($"{typeof(T).Name}", $"{dut.Item2.Id}");
            int i = 0;
            try
            {
                //_Logger.Info($"{sql}\r\n---- {JsonConvert.SerializeObject(domain)}");
                i = await conn.ExecuteAsync(sql, domain);
            }
            catch (Exception e)
            {
                _Logger.Error($"数据库新增数据异常。\r\nMessage: {e.Message}\r\nSQL: {sql}\r\nDomain: {JsonConvert.SerializeObject(domain)}");
            }
            return i == 1;
        }

        /// <summary>
        ///     将指定的对象批量插入数据库中
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="domains">指定的对象</param>
        public async Task<bool> InsertManyAsync((Project, DUT) dut, IEnumerable<T> domains)
        {
            var conn = _storageManager.OpenConnection(dut.Item1);
            var key = GetSqlKey();
            var sql = _sqlSet[key].Insert;
            var i = await conn.ExecuteAsync(sql, domains);
            return i == domains.Count();
        }

        /// <summary>
        ///     更新指定的对象
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="domain">指定的对象</param>
        public async Task<bool> UpdateAsync((Project, DUT) dut, T domain)
        {
            var conn = _storageManager.OpenConnection(dut.Item1);
            var key = GetSqlKey();
            var sql = _sqlSet[key].Update;
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
        /// 更新指定的工程实体
        /// </summary>
        /// <param name="project">指定的工程实体</param>
        public Task<bool> UpdateAsync(Project project)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     根据记录ID，从数据库中移除该记录，该记录被移除后，不可恢复
        /// </summary>
        /// <param name="dut">指定的被测试物</param>
        /// <param name="id">指定的记录ID</param>
        public async Task<bool> RemoveAsync((Project, DUT) dut, DateTime id)
        {
            var conn = _storageManager.OpenConnection(dut.Item1);
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