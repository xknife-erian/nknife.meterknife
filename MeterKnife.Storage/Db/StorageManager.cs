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

        private readonly Dictionary<string, IDbConnection> _dutSqliteConnMap = new Dictionary<string, IDbConnection>();
        private readonly StorageOption _option;
        private readonly IDbConnection _dutMysqlConn = null;
        private readonly IDbConnection _platformConn = null;
        private readonly HabitConfig _habitConfig;
        private readonly PathManager _pathManager;

        public StorageManager(IOptions<StorageOption> options, HabitConfig habitConfig, PathManager pathManager)
        {
            _habitConfig = habitConfig;
            _pathManager = pathManager;
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
                    if (!_dutSqliteConnMap.TryGetValue(engineering.Number, out conn))
                    {
                        conn = new SQLiteConnection(BuildSqliteEngineeringConnection(engineering));
                        _dutSqliteConnMap.Add(engineering.Number, conn);
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
            if (_dutSqliteConnMap.TryGetValue(engineering.Number, out var conn))
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
                case DatabaseType.SqLite:
                    CreateEngineeringSqliteFile(engineering);
                    break;
            }
        }

        private void CreateEngineeringSqliteFile(Engineering engineering)
        {
            var fileFullName = GetEngineeringSqliteFileName(engineering);
            var dir = Path.GetDirectoryName(fileFullName);
            UtilFile.CreateDirectory(dir);
            using (var command = OpenConnection(engineering).CreateCommand())
            {
                DbUtil.CheckTable(command, CurrentDbType, GetTablesSqlMap(engineering));
            }
        }

        private Dictionary<string, string> GetTablesSqlMap(Engineering engineering)
        {
            var dutList = new List<DUT>();
            foreach (var command in engineering.Commands)
            {
                if (!dutList.Contains(command.DUT))
                    dutList.Add(command.DUT);
            }

            return null;
        }

        private string GetEngineeringSqliteFileName(Engineering engineering)
        {
            var t = engineering.CreateTime;
            var fileFullName = $"E-{t:yyMMdd-HHmmss}.mks";
            var path = _habitConfig.GetOptionValue(HabitConfig.KEY_DATA_PATH, _pathManager.UserDocumentsPath);
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path = $"{path}{Path.DirectorySeparatorChar}";
            fileFullName = Path.Combine(path, $"{t:yyyyMM}{Path.DirectorySeparatorChar}", fileFullName);
            return fileFullName;
        }

        private string BuildSqliteEngineeringConnection(Engineering engineering)
        {
            var fileName = GetEngineeringSqliteFileName(engineering);
            return string.Format(_option.SqliteEngineeringConnection, fileName);
        }
    }
}