using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util;
using NKnife.Util;
using NLog;

namespace NKnife.MeterKnife.Storage.Db
{
    /// <summary>
    ///     系统的数据库管理器
    /// </summary>
    public sealed class StorageManager : IStorageManager
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 工程的数据库连接字典。Key是工程编号，Value是该工程的数据库连接。
        /// </summary>
        private readonly Dictionary<string, IDbConnection> _engineeringSqliteConnMap = new Dictionary<string, IDbConnection>();
        private readonly IDbConnection _dutMysqlConn = null;
        private readonly IDbConnection _platformConn = null;
        private readonly EngineeringFileBuilder _engineeringFileBuilder;
        private readonly StorageOption _option;

        public StorageManager(IOptions<StorageOption> options, EngineeringFileBuilder engineeringFileBuilder)
        {
            _engineeringFileBuilder = engineeringFileBuilder;
            _option = options.Value;
            CurrentDbType = _option.CurrentDbType;
            SqlSetMap = _option.SqlSetMap;
        }

        /// <summary>
        ///     当前数据库类型
        /// </summary>
        public DatabaseType CurrentDbType { get; }

        /// <summary>
        ///     预书写的Sql语句集合
        /// </summary>
        public SqlSetMap SqlSetMap { get; }

        /// <summary>
        ///     打开指定的工程数据库连接，并返回该连接
        /// </summary>
        /// <param name="engineering">指定的工程</param>
        /// <returns>数据库连接</returns>
        public IDbConnection OpenConnection(Engineering engineering)
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
                    if (!_engineeringSqliteConnMap.TryGetValue(engineering.Number, out conn))
                    {
                        conn = new SQLiteConnection(BuildEngineeringSqliteConnectionString(engineering));
                        _engineeringSqliteConnMap.Add(engineering.Number, conn);
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

        /// <summary>
        ///     关闭指定的工程数据库连接
        /// </summary>
        /// <param name="engineering">指定的工程</param>
        public void CloseConnection(Engineering engineering)
        {
            if (_engineeringSqliteConnMap.TryGetValue(engineering.Number, out var conn))
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

        /// <summary>
        ///     创建工程存储
        /// </summary>
        /// <param name="engineering">用来建立工程存储的名称</param>
        public void CreateEngineering(Engineering engineering)
        {
            switch (CurrentDbType)
            {
                case DatabaseType.MySql:
                    //TODO:每个工程是否独立建库呢？这一块怎么整还没有想明白。
                    break;
                case DatabaseType.SqLite:
                    _engineeringFileBuilder.CreateEngineeringSqliteFile(this, engineering);
                    break;
            }
        }

        private string BuildEngineeringSqliteConnectionString(Engineering engineering)
        {
            var fileName = _engineeringFileBuilder.GetEngineeringSqliteFileName(engineering);
            return string.Format(_option.SqliteEngineeringConnection, fileName);
        }
    }
}