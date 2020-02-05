using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util;
using NKnife.Util;

namespace NKnife.MeterKnife.Storage.Db
{
    public class EngineeringFileBuilder
    {
        private readonly HabitConfig _habitConfig;
        private readonly PathManager _pathManager;

        public EngineeringFileBuilder(HabitConfig habitConfig, PathManager pathManager)
        {
            _habitConfig = habitConfig;
            _pathManager = pathManager;
        }

        public void CreateEngineeringSqliteFile(IStorageManager storageManager, Engineering engineering)
        {
            var fileFullName = GetEngineeringSqliteFileName(engineering);
            var dir = Path.GetDirectoryName(fileFullName);
            UtilFile.CreateDirectory(dir);
            using (var command = storageManager.OpenConnection(engineering).CreateCommand())
            {
                DbUtil.CheckTable(command, storageManager.CurrentDbType, GetTablesSqlMap(storageManager.CurrentDbType, engineering));
            }
        }

        /// <summary>
        /// 获取指定工程的建表SQL语句
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="engineering">指定工程</param>
        /// <returns>SQL语句的字典，Key是表名，Value是建表的语句</returns>
        private Dictionary<string, string> GetTablesSqlMap(DatabaseType dbType, Engineering engineering)
        {
            var map = new Dictionary<string, string>();
            var dutList = new List<DUT>();
            foreach (var command in engineering.Commands)
            {
                if (!dutList.Contains(command.DUT))
                {
                    var d = command.DUT;
                    dutList.Add(d);
                    map.Add(d.Id, SqlHelper.GetCreateTableSql($"{d.Id}s", dbType, typeof(MetricalData)));
                }
            }
            map.Add(nameof(Engineering), SqlHelper.GetCreateTableSql(dbType, typeof(Engineering)));
            return map;
        }

        public string GetEngineeringSqliteFileName(Engineering engineering)
        {
            var t = engineering.CreateTime;
            var fileFullName = $"E-{t:yyMMdd-HHmmss}.mks";
            var path = _habitConfig.GetOptionValue(HabitConfig.KEY_DATA_PATH, _pathManager.UserDocumentsPath);
            if (!Directory.Exists(path))
                UtilFile.CreateDirectory(path);
            fileFullName = Path.Combine(path, $"{t:yyyyMM}{Path.DirectorySeparatorChar}", fileFullName);
            return fileFullName;
        }

    }
}
