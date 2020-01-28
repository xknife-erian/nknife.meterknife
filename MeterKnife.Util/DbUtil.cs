using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NKnife.Db;
using NLog;

namespace NKnife.MeterKnife.Util
{
    public class DbUtil
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        public static void CheckTable(IDbCommand command, DatabaseType dbType, Dictionary<string, string> tableSqlMap)
        {
            foreach (var pair in tableSqlMap)
            {
                switch (dbType)
                {
                    case DatabaseType.SqLite:
                        {
                            var tableName = pair.Key;
                            command.CommandText = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{tableName}s'";
                            CreateTableAndDefaultData(command, pair);
                            break;
                        }
                    case DatabaseType.MySql:
                        {
                            var tableName = pair.Key;
                            command.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='wz_wm_devtime' AND TABLE_NAME='{tableName}s'";
                            CreateTableAndDefaultData(command, pair);
                            break;
                        }
                }
            }
        }

        private static void CreateTableAndDefaultData(IDbCommand command, KeyValuePair<string, string> pair)
        {
            var tableName = pair.Key;
            var result = command.ExecuteScalar();
            _Logger.Debug($"检查{tableName}表的状态：{result}");
            if (!int.TryParse(result.ToString(), out var count) || count <= 0)
            {
                _Logger.Info($"{tableName}表不存在，准备新建...");
                CreateSqliteTable(command, pair.Value);
                // switch (tableName)
                // {
                //     case nameof(User):
                //         CreateAdminUser(command);
                //         break;
                //     case nameof(UserSnack):
                //         CreateAdminUserSnack(command);
                //         break;
                //     case nameof(Role):
                //         CreateDefaultRole(command);
                //         break;
                //     case nameof(Organization):
                //         break;
                // }
            }
        }

        /// <summary>
        /// 创建Sqlite数据表
        /// </summary>
        private static void CreateSqliteTable(IDbCommand command, string sql)
        {
            command.CommandText = sql;
            var result = command.ExecuteNonQuery();
            _Logger.Info($"新建表执行结果：{result}");
        }
    }
}
