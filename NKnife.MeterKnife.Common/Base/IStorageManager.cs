using System.Data;
using NKnife.Db;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
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
        ///     打开指定的工程数据库连接，并返回该连接
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <returns>数据库连接</returns>
        IDbConnection OpenConnection(Project project);

        /// <summary>
        ///     关闭指定的工程数据库连接
        /// </summary>
        /// <param name="project">指定的工程</param>
        void CloseConnection(Project project);

        /// <summary>
        ///     打开本软件管理信息数据库连接，并返回该连接
        /// </summary>
        IDbConnection OpenPlatformConnection();

        /// <summary>
        ///     关闭管理信息数据库连接
        /// </summary>
        void ClosePlatformConnection();

        /// <summary>
        ///     创建工程存储
        /// </summary>
        /// <param name="projectName">用来建立工程存储的名称</param>
        void CreateEngineering(Project projectName);
    }
}