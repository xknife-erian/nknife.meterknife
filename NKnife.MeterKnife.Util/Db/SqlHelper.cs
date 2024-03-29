﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using NKnife.Db.Base;

// ReSharper disable once CheckNamespace
namespace NKnife.Db
{
    public static class SqlHelper
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> _TypePropertiesMap = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _TypeTableNameMap = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        public static string GetCreateTableSql(DatabaseType databaseType, Type type)
        {
            var tableName = GetTableName(type);
            return GetCreateTableSql(tableName, databaseType, type);
        }

        public static string GetCreateTableSql(string tableName, DatabaseType databaseType, Type type)
        {
            var allProperties = TypePropertiesCache(type);

            var sbTableSql = new StringBuilder();
            var sbIndexSql = new StringBuilder();
            for (var i = 0; i < allProperties.Count; i++)
            {
                var ps = allProperties[i];
                sbTableSql.Append(ps.Name);
                SetDbType(databaseType, sbTableSql, ps);

                if (HasKeyAttribute(ps) || IsId(ps))
                {
                    sbTableSql.Append(" PRIMARY KEY");
                }

                if (HasIndexAttribute(ps))
                {
                    sbIndexSql.Append($" CREATE INDEX {tableName}_{ps.Name} ON {tableName}({ps.Name});");
                }

                if (IsNotNull(ps))
                {
                    sbTableSql.Append(" NOT NULL");
                }

                if (i < allProperties.Count - 1)
                {
                    sbTableSql.Append(", ");
                }
            }
            var sql = $"CREATE TABLE {tableName} ( {sbTableSql} );";
            if (sbIndexSql.Length > 0)
                sql = $"{sql}{sbIndexSql}";
            return sql;
        }

        /// <summary>
        /// 根据类属性的类型设置数据库字段类型
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="sb">sql语句</param>
        /// <param name="p">属性</param>
        private static void SetDbType(DatabaseType databaseType, StringBuilder sb, PropertyInfo p)
        {
            var pt = p.PropertyType;
            if (pt == typeof(int) || pt == typeof(uint) || pt == typeof(short) || pt == typeof(ushort))
            {
                sb.Append(" INT");
            }
            else if (pt == typeof(long) || pt == typeof(ulong))
            {
                sb.Append(" BIGINT");
            }
            else if (pt == typeof(float) || pt == typeof(double) || pt == typeof(decimal))
            {
                sb.Append(" DOUBLE");
            }
            else if (pt == typeof(string))
            {
                Attribute attr = Attribute.GetCustomAttribute(p, typeof(MaxLengthAttribute));
                if (attr != null && attr is MaxLengthAttribute attribute && attribute.Length < 250)
                {
                    var length = attribute.Length;
                    sb.Append(length <= 0 ? " CHAR" : $" CHAR({length})");
                }
                else
                {
                    sb.Append(" TEXT");
                }
            }
            else if (pt == typeof(DateTime))
            {
                sb.Append(" DATETIME");
            }
            else if (pt.IsEnum || pt == typeof(bool))
            {
                switch (databaseType)
                {
                    case DatabaseType.MySql:
                        sb.Append(" SMALLINT");
                        break;
                    default:
                        sb.Append(" TINYINT");
                        break;
                }
            }
            else if (pt == typeof(byte[]))
            {
                sb.Append(" BLOB");
            }
            else
            {
                sb.Append(" TEXT");
            }
        }

        private static bool IsNotNull(PropertyInfo propertyInfo)
        {
            var attr = Attribute.GetCustomAttribute(propertyInfo, typeof(RequiredAttribute));
            return attr != null;
        }

        private static bool IsId(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name == "Id";
        }

        private static bool HasKeyAttribute(PropertyInfo propertyInfo)
        {
            var attr = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute));
            return attr != null;
        }

        private static bool HasIndexAttribute(PropertyInfo ps)
        {
            var attr = Attribute.GetCustomAttribute(ps, typeof(IndexAttribute));
            return attr != null;
        }

        public static string GetInsertSql(DatabaseType dbType, Type type)
        {
            var name = GetTableName(type);
            var allProperties = TypePropertiesCache(type);

            var sbColumnList = new StringBuilder(null);
            var sbParameterList = new StringBuilder(null);
            for (var i = 0; i < allProperties.Count; i++)
            {
                var property = allProperties[i];
                sbColumnList.Append(property.Name);
                sbParameterList.AppendFormat("@{0}", property.Name);
                if (i < allProperties.Count - 1)
                {
                    sbColumnList.Append(", ");
                    sbParameterList.Append(", ");
                }
            }

            return $"INSERT INTO {name} ( {sbColumnList} ) Values ( {sbParameterList} );";
        }

        public static string GetUpdateSql(DatabaseType dbType, Type type)
        {
            var name = GetTableName(type);
            var allProperties = TypePropertiesCache(type);

            var sbColumnList = new StringBuilder(null);
            for (var i = 0; i < allProperties.Count; i++)
            {
                var property = allProperties[i];
                sbColumnList.Append($"{property.Name}=@{property.Name}");
                if (i < allProperties.Count - 1)
                {
                    sbColumnList.Append(", ");
                }
            }

            return $"UPDATE {name} SET {sbColumnList};";
        }

        /// <summary>
        /// 根据Class名字得出数据库中数据表名
        /// </summary>
        public static string GetTableName(Type type)
        {
            if (_TypeTableNameMap.TryGetValue(type.TypeHandle, out var name))
            {
                return name;
            }

            var attributes = type.GetCustomAttributes(false);
            foreach (var attribute in attributes)
            {
                if (attribute is TableAttribute ta)
                {
                    name = ta.Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                name = $"{type.Name}s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }

            _TypeTableNameMap[type.TypeHandle] = name;
            return name;
        }

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (_TypePropertiesMap.TryGetValue(type.TypeHandle, out var pis))
                return pis.ToList();

            var properties = type.GetProperties().ToArray();
            _TypePropertiesMap[type.TypeHandle] = properties;
            return properties.ToList();
        }
    }
}