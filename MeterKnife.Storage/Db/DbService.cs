using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using NKnife.Db;
using NKnife.MeterKnife.Common;
using NLog;

namespace NKnife.MeterKnife.Storage.Db
{
    public class DbService : IDbService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private IEnumerable<Func<IDbConnection>> _openConnectionMethods;
        private SqlSetMap _sqlSetMap = new SqlSetMap();

        #region Implementation of IDbService

        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public DatabaseType CurrentDbType { get; } = DatabaseType.MySql;

        /// <summary>
        /// 预书写的Sql语句集合
        /// </summary>
        public SqlSetMap SqlSetMap
        {
            get
            {
                if (_sqlSetMap == null || _sqlSetMap.Count <= 0)
                {
                    _sqlSetMap = new SqlSetMap();
                    FillSqlMap(_sqlSetMap);
                }
                return _sqlSetMap;
            }
        }

        private bool _isStart = false;

        /// <summary>
        /// 启动数据库相关管理的全局服务。包括初始化数据库，包括库中的表是否存在，如不存在创建。
        /// </summary>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_isStart)
                return Task.Delay(50, cancellationToken);
            _logger.Info("IHostedService-StartAsync:启动数据库相关管理的全局服务。……");
            if (_openConnectionMethods == null || !_openConnectionMethods.Any())
                throw new InvalidOperationException();
            foreach (var connection in _openConnectionMethods)
            {
                using (var command = connection.Invoke().CreateCommand())
                {
                    // ReSharper disable once PossibleNullReferenceException
                    foreach (var dbType in SqlSetMap.Keys)
                    {
                        if (CurrentDbType.Equals(dbType))
                            CheckTable(command, dbType);
                    }
                }
            }
            _logger.Info("IHostedService-StartAsync:启动数据库相关管理的全局服务。启动完成。");
            _isStart = true;
            return Task.Delay(500, cancellationToken);
        }

        /// <summary>
        /// 停止数据库相关管理的全局服务。
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Delay(50, cancellationToken);
        }

        private static void FillSqlMap(SqlSetMap sqlSetMap)
        {
            var dirName = Path.Combine(GetRootDirName(), "Sqls");
            var xmlFile = Path.Combine(dirName, SqlHelper.SQL_FILE_NAME);
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException($"{xmlFile} not found.");
            var document = new XmlDocument();
            document.Load(xmlFile);
            if (document.DocumentElement == null)
                throw new XmlException($"没有根节点");
            foreach (var node in document.DocumentElement.ChildNodes)
            {
                if (!(node is XmlElement ele))
                    continue;
                var tableName = ele.GetAttribute("name");
                foreach (var sqlNode in ele.ChildNodes)
                {
                    if (!(sqlNode is XmlElement sqlElement))
                        continue;
                    var dbTypeName = sqlElement.GetAttribute(nameof(DatabaseType));
                    var dbType = DatabaseType.SqLite;
                    if (DatabaseType.MySql.ToString().Equals(dbTypeName))
                        dbType = DatabaseType.MySql;
                    if (!sqlSetMap.ContainsKey(dbType))
                        sqlSetMap.Add(dbType, new SqlSet());
                    var text = sqlElement.InnerText;

                    var sqlstate = sqlElement.GetAttribute(nameof(SqlString));
                    switch (sqlstate)
                    {
                        case nameof(SqlString.Insert):
                            sqlSetMap[dbType].Insert[tableName] = text;
                            break;
                        case nameof(SqlString.Table):
                            sqlSetMap[dbType].Table[tableName] = text;
                            break;
                        case nameof(SqlString.Update):
                            sqlSetMap[dbType].Update[tableName] = text;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置获取数据库连接的方法
        /// </summary>
        /// <param name="openConnectionMethods">数据库连接的方法集合</param>
        public void SetConnections(IEnumerable<Func<IDbConnection>> openConnectionMethods)
        {
            _openConnectionMethods = openConnectionMethods;
        }

        #endregion

        /// <summary>
        /// SQL语句的XML所在的目录
        /// </summary>
        private static string GetRootDirName()
        {
            var location = Assembly.GetEntryAssembly()?.Location;
            var dir = new DirectoryInfo(location ?? throw new InvalidOperationException());
            var dirName = Directory.GetDirectoryRoot(location);
            if (dir.Parent != null)
                dirName = dir.Parent.FullName;
            return dirName;
        }

        private void CheckTable(IDbCommand command, DatabaseType dbType)
        {
            var sqlMap = SqlSetMap[dbType].Table;
            foreach (var pair in sqlMap)
            {
                switch (dbType)
                {
                    case DatabaseType.SqLite:
                    {
                        var tableName = pair.Key;
                        command.CommandText = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{tableName}s'";
                        CreateTableAndDefaultData(command, tableName, pair);
                        break;
                    }
                    case DatabaseType.MySql:
                    {
                        var tableName = pair.Key;
                        command.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='wz_wm_devtime' AND TABLE_NAME='{tableName}s'";
                        CreateTableAndDefaultData(command, tableName, pair);
                        break;
                    }
                }
            }
        }

        private void CreateTableAndDefaultData(IDbCommand command, string tableName, KeyValuePair<string, string> pair)
        {
            var result = command.ExecuteScalar();
            _logger.Debug($"检查{tableName}表的状态：{result}");
            if (!int.TryParse(result.ToString(), out var count) || count <= 0)
            {
                _logger.Info($"{tableName}表不存在，准备新建...");
                CreateSqliteTable(command, pair.Value);
                switch (tableName)
                {
                    // case nameof(User):
                    //     CreateAdminUser(command);
                    //     break;
                    // case nameof(UserSnack):
                    //     CreateAdminUserSnack(command);
                    //     break;
                    // case nameof(Role):
                    //     CreateDefaultRole(command);
                    //     break;
                    // case nameof(Organization):
                    //     break;
                }
            }
        }

        /// <summary>
        /// 创建Sqlite数据表
        /// </summary>
        private void CreateSqliteTable(IDbCommand command, string sql)
        {
            command.CommandText = sql;
            var result = command.ExecuteNonQuery();
            _logger.Info($"新建表执行结果：{result}");
        }

        /*

        private void CreateDefaultRole(IDbCommand command)
        {
            var adminRole = new Role();
            adminRole.Id = Auths.AdminUser.ORG_ADMIN_ROLE_ID;
            adminRole.Name = Auths.AdminUser.ORG_ADMIN_ROLE_NAME;
            adminRole.Auths = JsonConvert.SerializeObject(Auths.Instance.Keys);
            var sql = $@"INSERT INTO {nameof(Role)}s (
                                    {nameof(Role.Id)},
                                    {nameof(Role.Name)}, {nameof(Role.Auths)}, 
                                    {nameof(IDomain.State)}, {nameof(IDomain.Other)}, {nameof(IDomain.Logs)}
                                ) VALUES (
                                '{adminRole.Id}',
                                '{adminRole.Name}','{adminRole.Auths}',
                                0,'-','{adminRole.Logs}')";
            command.CommandText = sql;
            var count = command.ExecuteNonQuery();
            _logger.Info($"创建{Auths.AdminUser.ORG_ADMIN_ROLE_NAME}角色：{count}");

            adminRole.Id = Auths.AdminUser.COM_ADMIN_ROLE_ID;
            adminRole.Name = Auths.AdminUser.COM_ADMIN_ROLE_NAME;
            adminRole.Auths = JsonConvert.SerializeObject(Auths.Instance.Keys);
            sql = $@"INSERT INTO {nameof(Role)}s (
                                    {nameof(Role.Id)},
                                    {nameof(Role.Name)}, {nameof(Role.Auths)}, 
                                    {nameof(IDomain.State)}, {nameof(IDomain.Other)}, {nameof(IDomain.Logs)}
                                ) VALUES (
                                '{adminRole.Id}',
                                '{adminRole.Name}','{adminRole.Auths}',
                                0,'-','{adminRole.Logs}')";
            command.CommandText = sql;
            count = command.ExecuteNonQuery();
            _logger.Info($"创建{Auths.AdminUser.COM_ADMIN_ROLE_NAME}角色：{count}");

            adminRole.Id = Auths.AdminUser.MET_ADMIN_ROLE_ID;
            adminRole.Name = Auths.AdminUser.MET_ADMIN_ROLE_NAME;
            adminRole.Auths = JsonConvert.SerializeObject(Auths.Instance.Keys);
            sql = $@"INSERT INTO {nameof(Role)}s (
                                    {nameof(Role.Id)},
                                    {nameof(Role.Name)}, {nameof(Role.Auths)}, 
                                    {nameof(IDomain.State)}, {nameof(IDomain.Other)}, {nameof(IDomain.Logs)}
                                ) VALUES (
                                '{adminRole.Id}',
                                '{adminRole.Name}','{adminRole.Auths}',
                                0,'-','{adminRole.Logs}')";
            command.CommandText = sql;
            count = command.ExecuteNonQuery();
            _logger.Info($"创建{Auths.AdminUser.MET_ADMIN_ROLE_NAME}角色：{count}");

            adminRole.Id = Auths.AdminUser.FIN_ADMIN_ROLE_ID;
            adminRole.Name = Auths.AdminUser.FIN_ADMIN_ROLE_NAME;
            adminRole.Auths = JsonConvert.SerializeObject(Auths.Instance.Keys);
            sql = $@"INSERT INTO {nameof(Role)}s (
                                    {nameof(Role.Id)},
                                    {nameof(Role.Name)}, {nameof(Role.Auths)}, 
                                    {nameof(IDomain.State)}, {nameof(IDomain.Other)}, {nameof(IDomain.Logs)}
                                ) VALUES (
                                '{adminRole.Id}',
                                '{adminRole.Name}','{adminRole.Auths}',
                                0,'-','{adminRole.Logs}')";
            command.CommandText = sql;
            count = command.ExecuteNonQuery();
            _logger.Info($"创建{Auths.AdminUser.FIN_ADMIN_ROLE_NAME}角色：{count}");
        }

        private void CreateAdminUser(IDbCommand command)
        {
            var admin = new User()
            {
                Id = Auths.AdminUser.SUPER_ADMIN_USER_ID,
                Name = "石斧",
                Nickname = Auths.AdminUser.USER_NAME,
                Mobile = "13901230088",
                MobileConfirmed = true,
                Email = "administrator@waterzone.cn",
                EmailConfirmed = true,
                LockoutEnabled = false,
                LockoutEnd = new DateTime(2019, 1, 1),
                OrganizationId = Auths.AdminUser.SUPER_ADMIN_USER_ID,
                Roles = "[]",
                Address = "-",
            };
            //var count = command.Connection.Insert(admin);
            var email = "administrator@waterzone.cn";
            var mobile = "18618360830";
            var nickname = Auths.AdminUser.USER_NAME;
            var sql = $@"INSERT INTO {nameof(User)}s (
                                    {nameof(User.Id)},
                                    {nameof(User.Name)}, {nameof(User.Nickname)}, {nameof(User.Mobile)},
                                    {nameof(User.Email)}, {nameof(User.EmailConfirmed)}, {nameof(User.MobileConfirmed)},
                                    {nameof(User.LockoutEnabled)}, {nameof(User.LockoutEnd)},
                                    {nameof(User.OrganizationId)}, {nameof(User.Roles)}, {nameof(User.Address)},
                                    {nameof(User.State)}, {nameof(User.AccessFailedCount)}, {nameof(User.Sex)},
                                    {nameof(User.Birthday)}, {nameof(User.Other)}, {nameof(User.Logs)}
                                ) VALUES (
                                '{Auths.AdminUser.SUPER_ADMIN_USER_ID}', '石斧', '{nickname}', '{mobile}',
                                '{email}',1,1,0,'{new DateTime(2019, 1, 1):yyyy-MM-dd HH:mm:ss}',
                                '{Auths.AdminUser.SUPER_ADMIN_USER_ID}','[]','-',0,0,0,'{DateTime.Now:yyyy-MM-dd HH:mm:ss}','-','-')";
            command.CommandText = sql;
            var count = command.ExecuteNonQuery();
            _logger.Info($"创建超级管理员账户：{count}");
        }

        private void CreateAdminUserSnack(IDbCommand command)
        {
            var email = "administrator@waterone.online";
            var mobile = "18618360830";
            var nickname = Auths.AdminUser.USER_NAME;
            var pepper = Guid.NewGuid().ToString("N");

            var sql = $@"INSERT INTO {nameof(UserSnack)}s (
                                    {nameof(UserSnack.Id)},
                                    {nameof(UserSnack.Pepper)}, {nameof(UserSnack.Value)}, 
                                    {nameof(UserSnack.NormalizedNickname)}, {nameof(UserSnack.Mobile)}, {nameof(UserSnack.NormalizedEmail)},
                                    {nameof(UserSnack.State)}, {nameof(UserSnack.Other)}, {nameof(UserSnack.Logs)}
                                ) VALUES (
                                '{Auths.AdminUser.SUPER_ADMIN_USER_ID}', 
                                '{pepper}', '{PasswordHelper.CreateHash(FastMd5.Create(Auths.AdminUser.INITIAL_PASSWORD), pepper)}', 
                                '{nickname.ToUpper()}','{mobile}','{email.ToUpper()}',
                                0,'-','-')";
            command.CommandText = sql;
            var count = command.ExecuteNonQuery();
            _logger.Info($"创建超级管理员账户附属信息：{count}");
        }

        */
    }
}
