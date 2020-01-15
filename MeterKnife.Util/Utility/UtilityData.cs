using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NKnife.Attributes;

namespace NKnife.Utility
{
    public static class UtilityData
    {
        /// <summary>通过<see cref="EntityColumnAttribute"/>定制特性返回该对象持久化为<see cref="DataTable"/>时的表结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable PickUpObjectPropertiesToTableSchema<T>() where T : class, new()
        {
            var table = new DataTable(String.Format("{0}Table", typeof(T).Name));
            var type = typeof(T);
            var propertieInfos = type.GetProperties();
            foreach (var propertyInfo in propertieInfos) //获取所有的属性
            {
                object[] pattrs = propertyInfo.GetCustomAttributes(false); //获取所有的定制特性
                if (pattrs.OfType<EntityColumnAttribute>().Any()) //如果该属性的定制特性包含指定的特性
                {
                    DataColumn column;
                    var pa = pattrs.OfType<EntityColumnAttribute>().FirstOrDefault();
                    var columnType = propertyInfo.PropertyType == typeof(DateTime?) ? typeof(DateTime) : propertyInfo.PropertyType;
                    if (pa != null && !String.IsNullOrWhiteSpace(pa.ColumnName))
                        column = new DataColumn(pa.ColumnName, columnType);
                    else
                        column = new DataColumn(propertyInfo.Name, columnType);
                    if (!table.Columns.Contains(column.ColumnName))
                        table.Columns.Add(column);
                }
            }
            return table;
        }
    }
}
