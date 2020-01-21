using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NKnife.Db
{
    public class SqlSet
    {
        public SubSqlSet Insert { get; set; } = new SubSqlSet();
        public SubSqlSet Table { get; set; } = new SubSqlSet();
        public SubSqlSet Update { get; set; } = new SubSqlSet();

        public class SubSqlSet : Dictionary<string, string>
        {
        }
    }
}