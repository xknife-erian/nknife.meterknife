using System;
using System.Data;
using System.Data.OleDb;

namespace NKnife.Databases.DbHelper
{
    public class AccessHelper
    {
        //private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        public static string _ConnString;

        #region "ExecuteSQL"

        public static int ExecuteSql(string strSql)
        {
            var conn = new OleDbConnection(_ConnString);
            var cmd = new OleDbCommand(strSql, conn);
            try
            {
                conn.Open(); //打开数据库链接
                cmd.ExecuteNonQuery(); //执行无返回值的数据库操作
                return 0;
            }
            finally
            {
                cmd.Dispose(); //释放该组件占用的资源
                conn.Close(); //每次操作完毕都要关闭链接
            }
        }

        #endregion

        #region "ExecuteSQLDS"

        public static DataSet ExecuteSqlDataSet(string strSql)
        {
            var conn = new OleDbConnection(_ConnString);

            try
            {
                conn.Open();
                var da = new OleDbDataAdapter(strSql, conn);
                var ds = new DataSet("ds"); //调用OleDbDataAdapter的Fill方法，为DataSet填充数据
                da.Fill(ds);
                return ds; //返回得到的DataSet对象，它保存了从数据库查询到都的数据
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close(); //关闭连接
            }
        }

        #endregion

        #region "ExecuteSQLDR"

        public static OleDbDataReader ExecuteSQLDataReader(string strSQL)
        {
            var conn = new OleDbConnection(_ConnString);
            var cmd = new OleDbCommand(strSQL, conn);
            try
            {
                conn.Open();
                OleDbDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch (OleDbException ex)
            {
                //_logger.Warn("ExecuteSQLDataReader异常." + ex.Message, ex);
                cmd.Dispose();
                conn.Close();
                return null;
            }
            finally
            {
                //cmd.Dispose();
                //conn.Close();
            }
        }

        #endregion

        #region "ExecuteSQLValue"

        public static string ExecuteSQLValue(string strSQL)
        {
            var conn = new OleDbConnection(_ConnString);
            var cmd = new OleDbCommand(strSQL, conn);
            try
            {
                conn.Open();
                object r = cmd.ExecuteScalar();
                if (Equals(r, null))
                {
                    //throw new Exception("Value Unavailable!");
                    return string.Empty;
                }
                else
                {
                    return Convert.ToString(r);
                }
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public static int ExecuteSQLIntValue(string strSQL)
        {
            var conn = new OleDbConnection(_ConnString);
            var cmd = new OleDbCommand(strSQL, conn);
            try
            {
                conn.Open();
                object r = cmd.ExecuteScalar();
                if (Equals(r, null))
                {
                    //throw new Exception("Value Unavailable!");
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(r);
                }
            }
            catch (OleDbException ex)
            {
                //throw new Exception(ex.Message);
                //_logger.Warn("ExecuteSQLIntValue出现异常." + ex.Message, ex);
                return 0;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        #endregion

        static AccessHelper()
        {
            const string baseconn = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = {0}";
            const string path = @"Data\AccessData\PanChinaQRecord.mdb";
            _ConnString = string.Format(baseconn, path);
        }
    }
}