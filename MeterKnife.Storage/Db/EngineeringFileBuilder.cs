using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                DbUtil.CheckTable(command, storageManager.CurrentDbType, GetTablesSqlMap(engineering));
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

        public string GetEngineeringSqliteFileName(Engineering engineering)
        {
            var t = engineering.CreateTime;
            var fileFullName = $"E-{t:yyMMdd-HHmmss}.mks";
            var path = _habitConfig.GetOptionValue(HabitConfig.KEY_DATA_PATH, _pathManager.UserDocumentsPath);
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path = $"{path}{Path.DirectorySeparatorChar}";
            fileFullName = Path.Combine(path, $"{t:yyyyMM}{Path.DirectorySeparatorChar}", fileFullName);
            return fileFullName;
        }

    }
}
