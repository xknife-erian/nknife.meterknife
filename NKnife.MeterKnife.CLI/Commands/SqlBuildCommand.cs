using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using CliFx;
using CliFx.Attributes;
using Newtonsoft.Json;
using NKnife.Db;
using NKnife.MeterKnife.Common.Domain;
using Formatting = Newtonsoft.Json.Formatting;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("sql", Description = "实体SQL语句生成")]
    public class SqlBuildCommand : BaseCommand
    {
        #region Overrides of BaseCommand

        /// <summary>
        /// Executes command using specified implementation of <see cref="T:CliFx.Services.IConsole" />.
        /// This method is called when the command is invoked by a user through command line interface.
        /// </summary>
        public override ValueTask ExecuteAsync(IConsole console)
        {
            console.Output.WriteLine("Sql语句生成器...");
            var map = new SqlSetMap();
            var types = GetDomainList();
            foreach (var type in types)
            {
                var typeSql = CreateSqlByType(DatabaseType.SqLite, type);
                map.Sqlite.Add(type.Name, typeSql); 
                typeSql = CreateSqlByType(DatabaseType.MySql, type);
                map.Mysql.Add(type.Name, typeSql);
            }
            var json = JsonConvert.SerializeObject(map, Formatting.Indented);
            console.Output.WriteLine(json);
            console.Output.WriteLine("Sql语句生成器工作结束...");
            return new ValueTask();
        }

        #endregion

        private TypeSql CreateSqlByType(DatabaseType dbType, Type type)
        {
            var typeSql = new TypeSql();
            var sql = SqlHelper.GetCreateTableSql(dbType, type);
            typeSql.Table = sql;
            sql = SqlHelper.GetInsertSql(dbType, type);
            typeSql.Insert = sql;
            sql = SqlHelper.GetUpdateSql(dbType, type);
            typeSql.Update = sql;
            return typeSql;
        }

        /// <summary>
        ///     获取需要生成Sql语句的实体类列表
        /// </summary>
        /// <returns>需要生成Sql语句的实体类列表</returns>
        public static Type[] GetDomainList()
        {
            var list = new List<Type>();
            list.AddRange(new[]
            {
                typeof(DUT), typeof(Engineering), typeof(MeasureData), typeof(Slot)
            });
            return list.ToArray();
        }
    }
}
