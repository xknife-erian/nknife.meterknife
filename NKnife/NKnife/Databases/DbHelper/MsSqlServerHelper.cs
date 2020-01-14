using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace NKnife.Databases.DbHelper
{
    public class MsSqlServerHelper
    {
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <returns></returns>
        /// 
        public static bool TestConnection(string connStr)
        {
            try
            {
                using(var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //返回DataSet
        public static DataSet ExecuteDataSet(string cmdText,string connStr)
        {
            using (var cn = new SqlConnection(connStr))
            {
                cn.Open();

                //创建一个SqlCommand对象，并对其进行初始化
                var cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //创建SqlDataAdapter对象以及DataSet
                var da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                //返回ds
                return ds;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText, string connStr)
        {
            using (var cn = new SqlConnection(connStr))
            {
                cn.Open();

                //创建一个SqlCommand对象，并对其进行初始化
                var cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //创建SqlDataAdapter对象以及DataSet
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                //返回ds
                return dt;
            }
        }

        //返回DataReader
        public static IDataReader ExecuteDataReader(string cmdText, string connStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(connStr);
            conn.Open();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                return null;
            }
        }

        /// <summary>
        /// 执行SQL指令，无返回查询结果
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //创建一个SqlCommand对象，并对其进行初始化
                SqlCommand cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //执行
                int var = cmd.ExecuteNonQuery();

                //返回var
                return var;
            }
        }


        /// <summary>
        /// 执行SQL指令返回单个查询结果
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //执行
                object val = cmd.ExecuteScalar();

                //if (Convert.IsDBNull(val))
                //{
                //    return 0;
                //}
                //else
                //{
                    //返回var
                    return val;
                //}
            }
        }

        public static IEnumerable<string> GetCommands(string script)
        {
            Regex regex = new Regex(@"\r{0,1}\nGO\r{0,1}\n");
            string[] commands = regex.Split(script);
            return commands.Where(s => s.Trim().Length > 0);
        }
    }
}
