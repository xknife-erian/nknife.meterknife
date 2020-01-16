using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace MeterKnife.Util.Collections
{
    /// <summary>一个同步安全的队列类型，内部包含一个AutoResetEvent，可通过该AutoResetEvent处理本队列的监听。
    /// </summary>
    public class SyncQueue<T> : ICollection
    {
        private readonly object _Lock = new object();
        private readonly Queue<T> _Q = new Queue<T>();

        public SyncQueue()
        {
            AutoResetEvent = new AutoResetEvent(false);
        }

        public SyncQueue(IEnumerable<T> collection)
            : this()
        {
            foreach (T t in collection)
            {
                Enqueue(t);
            }
        }

        public SyncQueue(int capacity)
            : this()
        {
            _Q = new Queue<T>(capacity);
        }

        public AutoResetEvent AutoResetEvent { get; protected set; }

        public void Clear()
        {
            while (_Q.Count > 0)
            {
                Dequeue();
            }
        }

        public T Dequeue()
        {
            T r = default(T);
            if (_Q.Count > 0)
            {
                lock (_Lock)
                {
                    if (_Q.Count > 0) //锁内还需有判断，因为有可能进入锁的时候Queue已经没数据了
                    {
                        r = _Q.Dequeue();
                    }
                }
            }
            return r;
        }

        /// <summary>向队列中压入一条指定类型的数据
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            lock (_Lock)
            {
                _Q.Enqueue(item);
            }
            AutoResetEvent.Set();
        }

        public T Peek()
        {
            T t = default(T);
            if (_Q.Count > 0)
            {
                lock (_Lock)
                {
                    if (_Q.Count > 0) //锁内还需有判断，因为有可能进入锁的时候Queue已经没数据了
                    {
                        t = _Q.Peek();
                    }
                }
            }
            return t;
        }

        public T[] ToArray()
        {
            lock (_Lock)
            {
                return _Q.ToArray();
            }
        }

        public void TrimExcess()
        {
            lock (_Lock)
            {
                _Q.TrimExcess();
            }
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator"/> 对象。
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            lock (_Lock)
            {
                return _Q.GetEnumerator();
            }
        }

        #endregion

        #region Implementation of ICollection

        /// <summary>
        /// 从特定的 <see cref="T:System.Array"/> 索引处开始，将 <see cref="T:System.Collections.ICollection"/> 的元素复制到一个 <see cref="T:System.Array"/> 中。
        /// </summary>
        /// <param name="array">作为从 <see cref="T:System.Collections.ICollection"/> 复制的元素的目标位置的一维 <see cref="T:System.Array"/>。<see cref="T:System.Array"/> 必须具有从零开始的索引。</param>
        /// <param name="index"><paramref name="array"/> 中从零开始的索引，将在此处开始复制。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array"/> 为 null。</exception>
        ///   
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index"/> 小于零。</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="array"/> 是多维的。- 或 -源 <see cref="T:System.Collections.ICollection"/> 中的元素数目大于从 <paramref name="index"/> 到目标 <paramref name="array"/> 末尾之间的可用空间。</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">源 <see cref="T:System.Collections.ICollection"/> 的类型无法自动转换为目标 <paramref name="array"/> 的类型。</exception>
        void ICollection.CopyTo(Array array, int index)
        {
            if (!(array is T[]))
                throw new ArgumentException("传入数据不是指定的类型，应传入T[]。");
            lock (_Lock)
            {
                _Q.CopyTo((T[]) array, index);
            }
        }

        /// <summary>
        /// 获取 <see cref="T:System.Collections.ICollection"/> 中包含的元素数。
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Collections.ICollection"/> 中包含的元素数。</returns>
        public int Count
        {
            get
            {
                lock (_Lock)
                {
                    return _Q.Count;
                }
            }
        }

        /// <summary>
        /// 获取一个可用于同步对 <see cref="T:System.Collections.ICollection"/> 的访问的对象。
        /// </summary>
        /// <returns>可用于同步对 <see cref="T:System.Collections.ICollection"/> 的访问的对象。</returns>
        object ICollection.SyncRoot
        {
            get { return ((ICollection)_Q).SyncRoot; }
        }

        /// <summary>
        /// 获取一个值，该值指示是否同步对 <see cref="T:System.Collections.ICollection"/> 的访问（线程安全）。
        /// </summary>
        /// <returns>如果对 <see cref="T:System.Collections.ICollection"/> 的访问是同步的（线程安全），则为 true；否则为 false。</returns>
        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)_Q).IsSynchronized; }
        }

        #endregion
    }
}