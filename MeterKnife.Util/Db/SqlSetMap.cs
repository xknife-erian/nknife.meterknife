using System.Collections.Generic;
using System.Security.Permissions;

// ReSharper disable once CheckNamespace
namespace NKnife.Db
{
    /// <summary>
    /// 平台预书写的SQL语句集合
    /// </summary>
    public class SqlSetMap
    {
        public SqlSet Sqlite { get; set; } = new SqlSet();
        public SqlSet Mysql { get; set; } = new SqlSet();
    }

    public class SqlSet : Dictionary<string, TypeSql>
    {

    }

    public class TypeSql
    {
        public string Table { get; set; }
        public string Insert { get; set; }
        public string Update { get; set; }
    }
}
