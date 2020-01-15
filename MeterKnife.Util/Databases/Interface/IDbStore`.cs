using System.Collections.Generic;

namespace NKnife.Databases.Interface
{
    /// <summary>面向一种类型数据的存储器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IDbStore<T, in TId>
    {
        /// <summary>增加指定的实体记录
        /// </summary>
        bool Add(params T[] entities);

        /// <summary>删除指定ID的记录
        /// </summary>
        bool Remove(params TId[] entityIds);

        /// <summary>删除(清除)所有记录
        /// </summary>
        bool Clear();

        /// <summary>更新指定的实体记录
        /// </summary>
        bool Update(params T[] entities);

        /// <summary>查询实体数量
        /// </summary>
        long Count();

        /// <summary>查找指定的实体记录
        /// </summary>
        T Find(TId entityId);

        /// <summary>查找指定的实体记录集合
        /// </summary>
        IEnumerable<T> Find(params TId[] entityIds);
    }
}