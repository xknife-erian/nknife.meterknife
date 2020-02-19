using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
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
        private readonly StorageSetting _setting;
        private readonly IHabitManager _habitManager;
        private readonly IPathManager _pathManager;
        /// <summary>
        /// 软件第一次访问数据库时，检查数据库的完整性
        /// </summary>
        private bool _isFirst = true;

        public StorageManager(IOptions<StorageSetting> setting, EngineeringFileBuilder engineeringFileBuilder, IHabitManager habitManager, IPathManager pathManager)
        {
            _engineeringFileBuilder = engineeringFileBuilder;
            _habitManager = habitManager;
            _pathManager = pathManager;
            _setting = setting.Value;
            CurrentDbType = _setting.CurrentDbType;
            SqlSetMap = _setting.SqlSetMap;
            SqlMapper.AddTypeHandler(typeof(ScpiCommandPool), new ObjectToJsonTypeHandler());
            SqlMapper.AddTypeHandler(typeof(List<ScpiCommandPool>), new ObjectToJsonTypeHandler());
            SqlMapper.AddTypeHandler(typeof(List<ScpiCommandPool>), new ObjectToJsonTypeHandler());
            SqlMapper.AddTypeHandler(typeof(MetrologyValue[]), new ObjectToJsonTypeHandler());
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
                    if (!_engineeringSqliteConnMap.TryGetValue(engineering.Id, out conn))
                    {
                        conn = new SQLiteConnection(BuildEngineeringSqliteConnectionString(engineering));
                        _engineeringSqliteConnMap.Add(engineering.Id, conn);
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
            if (_engineeringSqliteConnMap.TryGetValue(engineering.Id, out var conn))
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
                        conn = new MySqlConnection(_setting.MysqlPlatformConnection);
                        break;
                    default:
                        var path = _habitManager.GetOptionValue(HabitKey.Data_MetricalData_Path, _pathManager.UserDocumentsPath);
                        if (!Directory.Exists(path))
                            UtilFile.CreateDirectory(path);
                        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                            path = $"{path}{Path.DirectorySeparatorChar}";
                        var pf = string.Format(_setting.SqlitePlatformConnection, path);
                        conn = new SQLiteConnection(pf);
                        break;
                }
                conn.Open();
                if (_isFirst)
                {
                    DbUtil.CheckTable(conn.CreateCommand(), CurrentDbType, GetPlatformTableSqlMap());
                    _isFirst = false;
                }
            }

            return conn;
        }

        private Dictionary<string, string> GetPlatformTableSqlMap()
        {
            var map = new Dictionary<string, string>();
            map.Add(nameof(DUT), SqlHelper.GetCreateTableSql(CurrentDbType, typeof(DUT)));
            map.Add(nameof(Engineering), SqlHelper.GetCreateTableSql(CurrentDbType, typeof(Engineering)));
            map.Add(nameof(Slot), SqlHelper.GetCreateTableSql(CurrentDbType, typeof(Slot)));
            return map;
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
            return string.Format(_setting.SqliteEngineeringConnection, engineering.Path);
        }
    }
}