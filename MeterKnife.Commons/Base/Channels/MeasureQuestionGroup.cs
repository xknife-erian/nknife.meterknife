using System;
using System.Collections.Generic;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Base.Channels
{
    public abstract class MeasureQuestionGroup<T> : List<MeasureQuestion<T>>, IQuestionGroup<T>
    {
        #region Implementation of IEnumerable<out IQuestion<T>>

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1" />。</returns>
        public IEnumerator<IQuestion<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ICollection<IQuestion<T>>

        /// <summary>将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 中。</summary>
        /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 的对象。</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。
        /// </exception>
        public void Add(IQuestion<T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>确定 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否包含特定值。</summary>
        /// <returns>如果在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中找到 <paramref name="item" />，则为 true；否则为 false。</returns>
        /// <param name="item">要在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中定位的对象。</param>
        public bool Contains(IQuestion<T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     从特定的 <see cref="T:System.Array" /> 索引处开始，将 <see cref="T:System.Collections.Generic.ICollection`1" /> 的元素复制到一个
        ///     <see cref="T:System.Array" /> 中。
        /// </summary>
        /// <param name="array">
        ///     作为从 <see cref="T:System.Collections.Generic.ICollection`1" /> 复制的元素的目标位置的一维
        ///     <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。
        /// </param>
        /// <param name="arrayIndex">
        ///     <paramref name="array" /> 中从零开始的索引，将在此处开始复制。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="array" /> 为 null。
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="arrayIndex" /> 小于 0。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="array" /> 是多维数组。- 或 -源 <see cref="T:System.Collections.Generic.ICollection`1" /> 中的元素数大于从
        ///     <paramref name="arrayIndex" /> 到目标 <paramref name="array" /> 结尾处之间的可用空间。- 或 -无法自动将类型 <paramref name="T" /> 强制转换为目标
        ///     <paramref name="array" /> 的类型。
        /// </exception>
        public void CopyTo(IQuestion<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除特定对象的第一个匹配项。</summary>
        /// <returns>
        ///     如果已从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中成功移除 <paramref name="item" />，则为 true；否则为
        ///     false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1" /> 中没有找到 <paramref name="item" />，该方法也会返回 false。
        /// </returns>
        /// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除的对象。</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。
        /// </exception>
        public bool Remove(IQuestion<T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否为只读。</summary>
        /// <returns>如果 <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读，则为 true；否则为 false。</returns>
        public bool IsReadOnly { get; }

        #endregion
    }
}