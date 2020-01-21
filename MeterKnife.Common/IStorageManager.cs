using System;
using System.Data;
using NKnife.Db;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// 系统的数据库管理器
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// 当前数据库类型
        /// </summary>
        DatabaseType CurrentDbType { get; }

        /// <summary>
        /// 预书写的Sql语句集合
        /// </summary>
        SqlSetMap SqlSetMap { get; }

        /// <summary>
        /// 当数据库连接参数发生变化时发生。一般在外界配置文件发生改变时变化。
        /// </summary>
        event EventHandler<EventArgs> ConnectionParamChanged;

        /// <summary>
        ///     打开“写”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        IDbConnection OpenWriteConnection();

        /// <summary>
        ///     关闭“写”数据库连接
        /// </summary>
        void CloseWriteConnection();

        /// <summary>
        ///     打开“读”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        IDbConnection OpenReadConnection();

        /// <summary>
        ///     关闭“读”数据库连接
        /// </summary>
        void CloseReadConnection();
    }
}