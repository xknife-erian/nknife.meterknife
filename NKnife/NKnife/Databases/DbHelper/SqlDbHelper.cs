using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NKnife.Databases.DbHelper
{
    /// <summary>SqlHelper是一个微软完全开放源码的数据库操作类, 用于简化你重复的去写那些数据库连接（DbConnection）,DbCommand,DbDataReader等等。
    /// SqlHelper 封装过后通常是只需要给方法传入一些参数如数据库连接字符串，SQL参数等，就可以访问数据库了，很方便。
    /// SqlHelper 类用于通过一组静态方法来封装数据访问功能。
    /// 该类不能被继承或实例化，因此将其声明为包含专用构造函数的不可继承类。
    /// 在 SqlHelper 类中实现的每种方法都提供了一组一致的重载。
    /// 这提供了一种很好的使用 SqlHelper 类来执行命令的模式，同时为开发人员选择访问数据的方式提供了必要的灵活性。
    /// 每种方法的重载都支持不同的方法参数，因此开发人员可以确定传递连接、事务和参数信息的方式。
    /// </summary>
    public class SqlDbHelper
    {
        private readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        /// <summary>
        /// ExecuteNonQuery操作，对数据库进行 增、删、改 操作(（1）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <returns> </returns>
        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ExecuteNonQuery操作，对数据库进行 增、删、改 操作（2）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <returns> </returns>
        public int ExecuteNonQuery(string sql, CommandType commandType)
        {
            return ExecuteNonQuery(sql, commandType, null);
        }

        /// <summary>
        /// ExecuteNonQuery操作，对数据库进行 增、删、改 操作（3）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <param name="parameters">参数数组 </param>
        /// <returns> </returns>
        public int ExecuteNonQuery(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            int count;
            using (var connection = new SqlConnection(_ConnectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    count = command.ExecuteNonQuery();
                }
            }
            return count;
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询，并返回一个DataSet类型结果（1）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <returns> </returns>
        public DataSet ExecuteDataSet(string sql)
        {
            return ExecuteDataSet(sql, CommandType.Text, null);
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询，并返回一个DataSet类型结果（2）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <returns> </returns>
        public DataSet ExecuteDataSet(string sql, CommandType commandType)
        {
            return ExecuteDataSet(sql, commandType, null);
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询，并返回一个DataSet类型结果（3）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <param name="parameters">参数数组 </param>
        /// <returns> </returns>
        public DataSet ExecuteDataSet(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            var ds = new DataSet();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }
            }
            return ds;
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询，并返回一个DataTable类型结果（1）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <returns> </returns>
        public DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataTable(sql, CommandType.Text, null);
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询，并返回一个DataTable类型结果（2）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <returns> </returns>
        public DataTable ExecuteDataTable(string sql, CommandType commandType)
        {
            return ExecuteDataTable(sql, commandType, null);
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询，并返回一个DataTable类型结果（3）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <param name="parameters">参数数组 </param>
        /// <returns> </returns>
        public DataTable ExecuteDataTable(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            var data = new DataTable();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            return data;
        }

        /// <summary>
        /// ExecuteReader执行一查询，返回一SqlDataReader对象实例（1）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <returns> </returns>
        public SqlDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ExecuteReader执行一查询，返回一SqlDataReader对象实例（2）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <returns> </returns>
        public SqlDataReader ExecuteReader(string sql, CommandType commandType)
        {
            return ExecuteReader(sql, commandType, null);
        }

        /// <summary>
        /// ExecuteReader执行一查询，返回一SqlDataReader对象实例（3）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <param name="parameters">参数数组 </param>
        /// <returns> </returns>
        public SqlDataReader ExecuteReader(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            var connection = new SqlConnection(_ConnectionString);
            var command = new SqlCommand(sql, connection) {CommandType = commandType};
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// ExecuteScalar执行一查询，返回查询结果的第一行第一列（1）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <returns> </returns>
        public Object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ExecuteScalar执行一查询，返回查询结果的第一行第一列（2）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <returns> </returns>
        public Object ExecuteScalar(string sql, CommandType commandType)
        {
            return ExecuteScalar(sql, commandType, null);
        }

        /// <summary>
        /// ExecuteScalar执行一查询，返回查询结果的第一行第一列（3）
        /// </summary>
        /// <param name="sql">要执行的SQL语句 </param>
        /// <param name="commandType">要执行的查询类型（存储过程、SQL文本） </param>
        /// <param name="parameters"></param>
        /// <returns> </returns>
        public Object ExecuteScalar(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            object result;
            using (var connection = new SqlConnection(_ConnectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    result = command.ExecuteScalar();
                }
            }
            return result;
        }

        /// <summary>
        /// 返回当前连接的数据库中所有由用户创建的数据库
        /// </summary>
        /// <returns> </returns>
        public DataTable GetTables()
        {
            DataTable data;
            using (var connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                data = connection.GetSchema("Tables");
            }
            return data;
        }
    }
}