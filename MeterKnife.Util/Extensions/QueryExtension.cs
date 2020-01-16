using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class QueryExtension
    {
        public static IEnumerable<T> AsEnumerable<T>(this DataTable table, bool dateTimeToString) where T : class, new()
        {
            return table.ToList<T>(dateTimeToString).AsEnumerable();
        }

        public static IEnumerable<T> AsEnumerable<T>(this DataTable table) where T : class, new()
        {
            return table.ToList<T>().AsEnumerable();
        }

        /// <summary>
        ///     DataRow扩展方法：将DataRow类型转化为指定类型的实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dr) where T : class, new()
        {
            return ToModel<T>(dr, true);
        }

        /// <summary>
        ///     DataRow扩展方法：将DataRow类型转化为指定类型的实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dateTimeToString">是否需要将日期转换为字符串，默认为转换,值为true</param>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dr, bool dateTimeToString) where T : class, new()
        {
            if (dr != null)
                return ToList<T>(dr.Table, dateTimeToString).First();//TODO:此处感觉有BUG
            return null;
        }

        /// <summary>
        ///     DataTable扩展方法：将DataTable类型转化为指定类型的实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            return ToList<T>(dt, true);
        }

        /// <summary>
        ///     DataTable扩展方法：将DataTable类型转化为指定类型的实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dt"></param>
        /// <param name="dateTimeToString">是否需要将日期转换为字符串，默认为转换,值为true</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt, bool dateTimeToString) where T : class, new()
        {
            var list = new List<T>();

            if (dt != null)
            {
                var infos = new List<PropertyInfo>();
                Array.ForEach(typeof (T).GetProperties(), p =>
                {
                    if (dt.Columns.Contains(p.Name))
                    {
                        infos.Add(p);
                    }
                });
                SetList(list, infos, dt, dateTimeToString);
            }
            return list;
        }

        #region 私有方法

        private static void SetList<T>(ICollection<T> list, List<PropertyInfo> infos, DataTable dt, bool dateTimeToString)
            where T : class, new()
        {
            foreach (DataRow dr in dt.Rows)
            {
                var model = new T();
                infos.ForEach(p =>
                {
                    if (dr[p.Name] != DBNull.Value)
                    {
                        object tempValue = dr[p.Name];
                        if (dr[p.Name] is DateTime && dateTimeToString)
                        {
                            tempValue = dr[p.Name].ToString();
                        }
                        try
                        {
                            p.SetValue(model, tempValue, null);
                        }
                        catch (Exception e)
                        {
                            Debug.Fail(e.Message);
                        }
                    }
                });
                list.Add(model);
            }
        }

        #endregion
    }
}