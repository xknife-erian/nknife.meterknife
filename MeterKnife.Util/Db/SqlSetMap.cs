using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NKnife.Db
{
    /// <summary>
    /// 平台预书写的SQL语句集合，Key是数据库类型
    /// </summary>
    public class SqlSetMap : Dictionary<DatabaseType, SqlSet>
    {
    }
}
