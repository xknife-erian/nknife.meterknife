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

        /// <summary>
        /// 检查数据库的完整性，如不完整，将进行修正
        /// </summary>
        /// <param name="command">数据库命令</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="tableSqlMap">SQL语句的字典，Key是表名，Value是建表的语句</param>
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
                            CreateSqliteTable(command, pair);
                            break;
                        }
                    case DatabaseType.MySql:
                        {
                            var tableName = pair.Key;
                            command.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='wz_wm_devtime' AND TABLE_NAME='{tableName}s'";
                            CreateMysqlTableAndDefaultData(command, pair);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 检查数据库的完整性，如不完整，将进行修正
        /// </summary>
        /// <param name="command">数据库命令</param>
        /// <param name="pair">SQL语句的字典，Key是表名，Value是建表的语句</param>
        private static void CreateSqliteTable(IDbCommand command, KeyValuePair<string, string> pair)
        {
            var tableName = pair.Key;
            var result = command.ExecuteScalar();
            _Logger.Debug($"检查{tableName}表的状态：{result}");
            if (!int.TryParse(result.ToString(), out var count) || count <= 0)
            {
                _Logger.Info($"{tableName}表不存在，准备新建...");
                CreateSqliteTable(command, pair.Value);
            }
        }

        private static void CreateMysqlTableAndDefaultData(IDbCommand command, KeyValuePair<string, string> pair)
        {
            throw new NotImplementedException();
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
