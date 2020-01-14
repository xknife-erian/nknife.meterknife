using System;

namespace NKnife.Attributes
{
    /// <summary>描述当前实体对象的属性在存储到数据表时所在列相关的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class EntityColumnAttribute : Attribute
    {
        /// <summary>描述当前对象的属性在存储到数据表时所在列相关的属性
        /// </summary>
        /// <param name="columnName">列的别名</param>
        public EntityColumnAttribute(string columnName)
            :this()
        {
            ColumnName = columnName;
        }

        /// <summary>描述当前对象的属性在存储到数据表时所在列相关的属性
        /// </summary>
        public EntityColumnAttribute()
        {
        }

        /// <summary>列的别名
        /// </summary>
        public string ColumnName { get; set; }
    }
}
