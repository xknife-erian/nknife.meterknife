using NKnife.Db;

namespace NKnife.MeterKnife.Storage.Db
{
    public class StorageSetting
    {
        public string MysqlPlatformConnection { get; set; }
        public string MysqlDUTConnection { get; set; }
        public string SqlitePlatformConnection { get; set; }
        public string SqliteEngineeringConnection { get; set; }
        public DatabaseType CurrentDbType { get; set; }
        public SqlSetMap SqlSetMap { get; set; }
    }
}
