using System;
using System.Collections.Generic;
using System.Data;
using NKnife.Db;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// 数据库相关管理的全局服务
    /// </summary>
    public interface IDbService
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
        /// 设置获取数据库连接的方法
        /// </summary>
        /// <param name="openConnectionMethods">数据库连接的方法集合</param>
        void SetConnections(IEnumerable<Func<IDbConnection>> openConnectionMethods);
    }
}