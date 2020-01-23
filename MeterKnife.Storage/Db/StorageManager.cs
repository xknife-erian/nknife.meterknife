using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NKnife.Db;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NLog;

namespace NKnife.MeterKnife.Storage.Db
{
    /// <summary>
    ///     系统的数据库管理器
    /// </summary>
    public sealed class StorageManager : IStorageManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly StorageOption _option;

        private IDbConnection _platformConn = null;
        private IDbConnection _dutMysqlConn = null;
        private readonly Dictionary<string,IDbConnection> _dutSqliteConnMap = new Dictionary<string, IDbConnection>();

        public StorageManager(IOptions<StorageOption> options)
        {
            _option = options.Value;
            CurrentDbType = _option.CurrentDbType;
            SqlSetMap = _option.SqlSetMap;
        }

        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public DatabaseType CurrentDbType { get; }

        /// <summary>
        /// 预书写的Sql语句集合
        /// </summary>
        public SqlSetMap SqlSetMap { get; }

        /// <summary>
        ///     打开指定的被测物数据库连接，并返回该连接
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <returns>数据库连接</returns>
        public IDbConnection OpenConnection(DUT dut)
        {
            IDbConnection conn;
            switch (CurrentDbType)
            {
                case DatabaseType.MySql:
                {
                    conn = _dutMysqlConn;
                    break;
                }
                default:
                {
                    if (!_dutSqliteConnMap.TryGetValue(dut.Id, out conn))
                    {
                        conn = new SQLiteConnection(BuildSqliteDutConnection(dut));
                        _dutSqliteConnMap.Add(dut.Id, conn);
                    }
                    break;
                }
            }

            if (conn != null && conn.State != ConnectionState.Broken && conn.State != ConnectionState.Closed)
                return conn;
            if (conn != null && conn.State == ConnectionState.Broken)
                conn.Close();
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
                return conn;
            }
            return conn;
        }

        private string BuildSqliteDutConnection(DUT dut)
        {
            return _option.SqliteDUTConnection;
        }

        /// <summary>
        ///     关闭指定的被测物数据库连接
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        public void CloseConnection(DUT dut)
        {
            if (_dutSqliteConnMap.TryGetValue(dut.Id, out var conn))
                conn?.Close();
        }

        /// <summary>
        ///     打开本软件管理信息数据库连接，并返回该连接
        /// </summary>
        public IDbConnection OpenPlatformConnection()
        {
            var conn = _platformConn;
            if (conn != null && conn.State != ConnectionState.Broken && conn.State != ConnectionState.Closed)
                return conn;
            if (conn != null && conn.State == ConnectionState.Broken)
                conn.Close();
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
                return conn;
            }
            if (conn == null)
            {
                switch (CurrentDbType)
                {
                    case DatabaseType.MySql:
                        conn = new MySqlConnection(_option.MysqlPlatformConnection);
                        break;
                    default:
                        conn = new SQLiteConnection(_option.SqlitePlatformConnection);
                        break;
                }
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        ///     关闭管理信息数据库连接
        /// </summary>
        public void ClosePlatformConnection()
        {
            _platformConn?.Close();
        }
    }
}