using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Common.Logging;
using NKnife.Databases.Interface;

namespace NKnife.Databases.Common
{
    public abstract class DatabaseHelper : IDataBaseHelper
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region IDataBaseHelper Members

        public abstract string ConnectionString { get; }
        public abstract IDbConnection Connection { get; }
        public abstract IDbCommand Command { get; }
        public abstract IDbDataAdapter GetAdapter(IDbCommand command);

        /// <summary>
        /// 在每次操作前是否需要关闭连接。一般针对文件型数据库(Aceess,SQLite)不需要关闭连接。
        /// </summary>
        /// <value>
        ///   <c>true</c> if [need connection close]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool NeedConnectionClose { get; }

        /// <summary>
        /// 关闭连接，但是否关闭由 NeedConnectionClose 属性决定。
        /// </summary>
        public void Close(IDbConnection conn)
        {
            try
            {
                if (NeedConnectionClose)
                    conn.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("关闭数据库连接异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 执行指定的SQL语句操作（例如查询数据库的结构或创建诸如表等的数据库对象），或执行 UPDATE、INSERT 或 DELETE 语句，
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExeNonQuery(string sql)
        {
            return ExeNonQuery(sql, null);
        }

        /// <summary>
        /// 执行指定的SQL语句操作（例如查询数据库的结构或创建诸如表等的数据库对象），或执行 UPDATE、INSERT 或 DELETE 语句，
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExeNonQuery(string sql, IList<IDbDataParameter> parameters)
        {
            return ExeNonQuery(sql, parameters, null);
        }

        /// <summary>
        /// 事务方式执行指定的SQL语句操作（例如查询数据库的结构或创建诸如表等的数据库对象），或执行 UPDATE、INSERT 或 DELETE 语句，
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExeNonQuery(string sql, IList<IDbDataParameter> parameters, IDbTransaction transaction)
        {
            try
            {
                IDbCommand cmd = Command;
                cmd.CommandText = sql;
                if (transaction != null)
                    cmd.Transaction = transaction;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                if (!(parameters == null || parameters.Count == 0))
                {
                    foreach (DbParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                }
                int i = cmd.ExecuteNonQuery();
                Close(cmd.Connection);//每次操作调用关闭连接，但是否关闭由 NeedConnectionClose 属性决定。
                return i;
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("查询异常：{0}", sql), ex);
                return -1;
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本）</param>
        /// <returns></returns>
        public object ExeScalar(string sql, CommandType commandType)
        {
            return ExeScalar(sql, commandType, null);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本）</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExeScalar(string sql, CommandType commandType, IList<IDbDataParameter> parameters)
        {
            try
            {
                IDbCommand cmd = Command;
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                Close(cmd.Connection);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("查询异常：{0}", sql), ex);
                return null;
            }
        }

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataReader,最终返回想要的结果。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="hander"></param>
        /// <returns></returns>
        public T ExeQuery<T>(string sql, IDataReaderProcess<T> hander)
        {
            try
            {
                IDbCommand cmd = Command;
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                T t = hander.Process(cmd.ExecuteReader());
                Close(cmd.Connection);
                return t;
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("查询异常：{0}", sql), ex);
                return default(T);
            }
        }

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataReader,最终返回想要的结果。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="hander"></param>
        /// <returns></returns>
        public T ExeQuery<T>(string sql, IList<IDbDataParameter> parameters, IDataReaderProcess<T> hander)
        {
            try
            {
                IDbCommand cmd = Command;
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                if (!(parameters == null || parameters.Count == 0))
                {
                    foreach (DbParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                }
                T t = hander.Process(Command.ExecuteReader());
                Close(cmd.Connection);
                return t;
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("查询异常：{0}", sql), ex);
                return default(T);
            }
        }

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataSet,最终返回想要的结果。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="hander"></param>
        /// <returns></returns>
        public T ExeQuery<T>(string sql, IDataSetProcess<T> hander)
        {
            try
            {
                var ds = new DataSet();
                IDbCommand cmd = Command;
                IDbDataAdapter adapter = GetAdapter(cmd);
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                adapter.Fill(ds);
                T t = hander.Process(ds);
                Close(cmd.Connection);
                return t;
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("查询异常：{0}", sql), ex);
                return default(T);
            }
        }

        /// <summary>
        /// 执行查询。并通过调用结果处理的接口实现处理返回的DataReader,最终返回想要的结果。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="hander"></param>
        /// <returns></returns>
        public T ExeQuery<T>(string sql, IList<IDbDataParameter> parameters, IDataSetProcess<T> hander)
        {
            try
            {
                var ds = new DataSet();
                IDbCommand cmd = Command;
                IDbDataAdapter adapter = GetAdapter(cmd);
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                if (!(parameters == null || parameters.Count == 0))
                {
                    foreach (DbParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                }
                adapter.Fill(ds);
                T t = hander.Process(ds);
                Close(cmd.Connection);
                return t;
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("查询异常：{0}", sql), ex);
                return default(T);
            }
        }

        #endregion
    }
}