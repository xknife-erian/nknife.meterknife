using System.Data;
using NKnife.Db;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    ///     系统的数据库管理器
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        ///     当前数据库类型
        /// </summary>
        DatabaseType CurrentDbType { get; }

        /// <summary>
        ///     预书写的Sql语句集合
        /// </summary>
        SqlSetMap SqlSetMap { get; }

        /// <summary>
        ///     打开指定的被测物数据库连接，并返回该连接
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <returns>数据库连接</returns>
        IDbConnection OpenConnection(DUT dut);

        /// <summary>
        ///     关闭指定的被测物数据库连接
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        void CloseConnection(DUT dut);

        /// <summary>
        ///     打开本软件管理信息数据库连接，并返回该连接
        /// </summary>
        IDbConnection OpenPlatformConnection();

        /// <summary>
        ///     关闭管理信息数据库连接
        /// </summary>
        void ClosePlatformConnection();
    }
}