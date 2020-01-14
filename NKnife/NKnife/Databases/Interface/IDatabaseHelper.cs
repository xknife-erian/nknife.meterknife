using System.Collections.Generic;
using System.Data;

namespace NKnife.Databases.Interface
{
    /// <summary>针对数据库的操作帮助类。来自nknife的设计。
    /// </summary>
    public interface IDataBaseHelper
    {
        /// <summary>连接字符串
        /// </summary>
        string ConnectionString { get; }

        IDbConnection Connection { get; }
        IDbCommand Command { get; }
        IDbDataAdapter GetAdapter(IDbCommand command);

        /// <summary>在每次操作前是否需要关闭连接。一般针对文件型数据库(Aceess,SQLite)不需要关闭连接。
        /// </summary>
        /// <value>
        ///   <c>true</c> if [need connection close]; otherwise, <c>false</c>.
        /// </value>
        bool NeedConnectionClose { get; }

        /// <summary>
        /// 关闭连接，但是否关闭由 NeedConnectionClose 属性决定。
        /// </summary>
        void Close(IDbConnection conn);

        /// <summary>
        /// 执行指定的SQL语句操作（例如查询数据库的结构或创建诸如表等的数据库对象），或执行 UPDATE、INSERT 或 DELETE 语句，
        /// </summary>
        int ExeNonQuery(string sql);

        /// <summary>
        /// 执行指定的SQL语句操作（例如查询数据库的结构或创建诸如表等的数据库对象），或执行 UPDATE、INSERT 或 DELETE 语句，
        /// </summary>
        int ExeNonQuery(string sql, IList<IDbDataParameter> parameters);

        /// <summary>
        /// 事务方式执行指定的SQL语句操作（例如查询数据库的结构或创建诸如表等的数据库对象），或执行 UPDATE、INSERT 或 DELETE 语句，
        /// </summary>
        int ExeNonQuery(string sql, IList<IDbDataParameter> parameters, IDbTransaction transaction);

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <returns> </returns>
        object ExeScalar(string sql, CommandType commandType);

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <param name="parameters"></param>
        /// <returns> </returns>
        object ExeScalar(string sql, CommandType commandType, IList<IDbDataParameter> parameters);

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataReader,最终返回想要的结果。
        /// </summary>
        T ExeQuery<T>(string sql, IDataReaderProcess<T> hander);

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataSet,最终返回想要的结果。
        /// </summary>
        T ExeQuery<T>(string sql, IDataSetProcess<T> hander);

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataReader,最终返回想要的结果。
        /// </summary>
        T ExeQuery<T>(string sql, IList<IDbDataParameter> parameters, IDataReaderProcess<T> hander);

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataReader,最终返回想要的结果。
        /// </summary>
        T ExeQuery<T>(string sql, IList<IDbDataParameter> parameters, IDataSetProcess<T> hander);
    }
}