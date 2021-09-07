using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Util;
using NKnife.Util;

namespace NKnife.MeterKnife.Storage.Db
{
    public class ProjectFileBuilder
    {
        private readonly IHabitManager _habitManager;
        private readonly IPathManager _pathManager;

        public ProjectFileBuilder(IHabitManager habitManager, IPathManager pathManager)
        {
            _habitManager = habitManager;
            _pathManager = pathManager;
        }

        public void CreateEngineeringSqliteFile(IStorageManager storageManager, Project project)
        {
            var fileFullName = GetEngineeringSqliteFileName(project);
            var dir = Path.GetDirectoryName(fileFullName);
            UtilFile.CreateDirectory(dir);
            using (var command = storageManager.OpenConnection(project).CreateCommand())
            {
                DbUtil.CheckTable(command, storageManager.CurrentDbType, GetTablesSqlMap(storageManager.CurrentDbType, project));
            }
        }

        /// <summary>
        /// 获取指定工程的建表SQL语句
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="project">指定工程</param>
        /// <returns>SQL语句的字典，Key是表名，Value是建表的语句</returns>
        private Dictionary<string, string> GetTablesSqlMap(DatabaseType dbType, Project project)
        {
            var map = new Dictionary<string, string>();
            var dutList = new List<DUT>();
            foreach (var pool in project.CommandPools)
            {
                foreach (ScpiCommand command in pool)
                {
                    if (command.DUT != null && !dutList.Contains(command.DUT))
                    {
                        var d = command.DUT;
                        dutList.Add(d);
                        map.Add(d.Id, SqlHelper.GetCreateTableSql($"{d.Id}s", dbType, typeof(MeasureData)));
                    }
                }
            }
            map.Add(nameof(Project), SqlHelper.GetCreateTableSql(dbType, typeof(Project)));
            return map;
        }

        private string GetEngineeringSqliteFileName(Project project)
        {
            var t = project.CreateTime;
            var fileFullName = $"E-{t:yyMMdd-HHmmss}.mks";
            var path = _habitManager.GetOptionValue(HabitKey.Data_MetricalData_Path, _pathManager.UserDocumentsPath);
            if (!Directory.Exists(path))
                UtilFile.CreateDirectory(path);
            fileFullName = Path.Combine(path, $"{t:yyyyMM}{Path.DirectorySeparatorChar}", fileFullName);
            project.Path = fileFullName;
            return fileFullName;
        }

    }
}
