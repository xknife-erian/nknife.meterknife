using System;
using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NKnife.Db;
using NKnife.MeterKnife.Common;
using NLog;

namespace NKnife.MeterKnife.Storage.Db
{
    /// <summary>
    ///     系统的数据库管理器
    /// </summary>
    public sealed class StorageManager : IStorageManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IDbService _dbService;
        private readonly StoragesOption _option;

        private IDbConnection _farReadConnection;
        private IDbConnection _farWriteConnection;

        public StorageManager(IOptions<StoragesOption> options, IDbService dbService)
        {
            _option = options.Value;
            _dbService = dbService;
            CurrentDbType = dbService.CurrentDbType;
            SqlSetMap = dbService.SqlSetMap;
            dbService.SetConnections(new Func<IDbConnection>[] {OpenReadConnection, OpenWriteConnection});
            ConnectionParamChanged += (s, e) =>
            {
                _logger.Info("Connection Param Changed.");
                _farReadConnection = null;
                _farWriteConnection = null;
            };
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
        ///     当数据库连接参数发生变化时发生。一般在外界配置文件发生改变时变化。
        /// </summary>
        public event EventHandler<EventArgs> ConnectionParamChanged;

        /// <summary>
        ///     打开“写”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public IDbConnection OpenWriteConnection()
        {
            if (_farWriteConnection != null
                && _farWriteConnection.State != ConnectionState.Broken
                && _farWriteConnection.State != ConnectionState.Closed)
                return _farWriteConnection;

            if (_farWriteConnection != null && _farWriteConnection.State == ConnectionState.Broken)
                _farWriteConnection.Close();

            if (_farWriteConnection != null && _farWriteConnection.State == ConnectionState.Closed)
            {
                _farWriteConnection.Open();
                return _farWriteConnection;
            }

            if (_farWriteConnection == null)
            {
                switch (_dbService.CurrentDbType)
                {
                    case DatabaseType.MySql:
                        _farWriteConnection = new MySqlConnection(_option.MysqlWriteString);
                        break;
                    default:
                        _farWriteConnection = new SQLiteConnection(_option.SqliteReadString);
                        break;
                }

                _farWriteConnection.Open();
            }

            return _farWriteConnection;
        }

        /// <summary>
        ///     关闭“写”数据库连接
        /// </summary>
        public void CloseWriteConnection()
        {
            if (_farWriteConnection == null)
                return;
            _farWriteConnection.Close();
            _farWriteConnection.Dispose();
            _farWriteConnection = null;
        }

        /// <summary>
        ///     打开“读”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public IDbConnection OpenReadConnection()
        {
            if (_farReadConnection != null
                && _farReadConnection.State != ConnectionState.Broken
                && _farReadConnection.State != ConnectionState.Closed)
                return _farReadConnection;

            if (_farReadConnection != null && _farReadConnection.State == ConnectionState.Broken)
            {
                _farReadConnection.Close();
            }

            if (_farReadConnection != null && _farReadConnection.State == ConnectionState.Closed)
            {
                _farReadConnection.Open();
                return _farReadConnection;
            }

            if (_farReadConnection == null)
            {
                switch (_dbService.CurrentDbType)
                {
                    case DatabaseType.MySql:
                        _farReadConnection = new MySqlConnection(_option.MysqlReadString);
                        break;
                    default:
                        _farReadConnection = new SQLiteConnection(_option.SqliteWriteString);
                        break;
                }

                _farReadConnection.Open();
            }

            return _farReadConnection;
        }

        /// <summary>
        ///     关闭“读”数据库连接
        /// </summary>
        public void CloseReadConnection()
        {
            if (_farReadConnection == null)
                return;
            _farReadConnection.Close();
            _farReadConnection.Dispose();
            _farReadConnection = null;
        }

        private void OnConnectionParamChanged()
        {
            ConnectionParamChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}