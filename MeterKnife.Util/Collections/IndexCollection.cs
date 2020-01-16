using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MeterKnife.Util.Collections
{
    /// <summary>
    /// 集合基础类（泛型）
    /// 实现可通过键或索引来访问为关联的 System.String 键和 System.Object 值的集合。
    /// </summary>
    /// <typeparam name="T">集合收集对象的类型</typeparam>
    public class IndexCollection<T> : NameObjectCollectionBase
    {
        private bool _UniqueKey;

        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexCollection() : this(false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uniqueKey">键是否限制为唯一</param>
        public IndexCollection(bool uniqueKey)
        {
            _UniqueKey = uniqueKey;
        }

        /// <summary>
        /// 键是否限制为唯一
        /// </summary>
        public virtual bool UniqueKey
        {
            get { return _UniqueKey; }
            protected set { _UniqueKey = value; }
        }

        /// <summary>
        /// 按索引的方式获取项
        /// </summary>
        /// <param name="index">索引</param>
        public virtual T this[int index]
        {
            get { return (T) BaseGet(index); }
        }

        /// <summary>
        /// 按键值方式获取项
        /// </summary>
        /// <param name="key">键值</param>
        public virtual T this[string key]
        {
            get { return (T) base.BaseGet(key); }
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        public virtual T[] Values
        {
            get { return base.BaseGetAllValues(typeof (T)) as T[]; }
        }

        /// <summary>
        /// 添加一项到集合中
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">项</param>
        public virtual void Add(string key, T @value)
        {
            if (UniqueKey && Contains(key))
            {
                throw new Exception("已存在键值：" + key);
            }
            BaseAdd(key, @value);
        }

        /// <summary>
        /// 添加/替换一项到集合中
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">项</param>
        public virtual void Set(string key, T @value)
        {
            BaseSet(key, @value);
        }

        /// <summary>
        /// 集合中是否包含某项
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>是/否</returns>
        public virtual bool Contains(string key)
        {
            return (BaseGet(key) != null);
        }

        /// <summary>
        /// 移除某项
        /// </summary>
        /// <param name="key">键值</param>
        public virtual void Remove(string key)
        {
            BaseRemove(key);
        }

        /// <summary>
        /// 在指定处移除某项
        /// </summary>
        /// <param name="index">索引</param>
        public virtual void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// 清空集合中所有元素
        /// </summary>
        public virtual void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// 复制到数组中
        /// </summary>
        /// <returns>数组</returns>
        public virtual T[] CopyToArray()
        {
            return base.BaseGetAllValues(typeof (T)) as T[];
        }

        /// <summary>
        /// 获取具有相同Key的项
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>具有相同Key的项</returns>
        public virtual T[] GetItem(string key)
        {
            var list = new List<T>();
            for (int i = 0; i < Count; i++)
            {
                if (string.Compare(Keys[i], key, true) == 0)
                {
                    list.Add(this[i]);
                }
            }
            return list.ToArray();
        }
    }
}