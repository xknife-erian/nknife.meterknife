using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightQuestionGroup : List<KeysightQuestion>, IQuestionGroup<string>
    {
        /// <summary>
        /// 获取当前Group中需要处理的Question所在的索引
        /// </summary>
        public int CurrentIndex { get; set; } = 0;

        /// <summary>
        /// 获取当前Group中需要处理的Question
        /// </summary>
        public KeysightQuestion Current
        {
            get
            {
                if (Count > CurrentIndex)
                    return this[CurrentIndex];
                return null;
            }
        }

        /// <summary>
        /// 取出一条Question。当该Question需要Loop时，保留Question在Group中；当该Question不需要Loop时，弹出并从Group中移除。
        /// </summary>
        public KeysightQuestion PeekOrDequeue()
        {
            var question = Current;
            if (question == null)
            {
                return null;
            }
            if (!question.IsLoop)
            {
                Remove(question);
                if (CurrentIndex == Count)
                    SetCurrent();
            }
            else
            {
                SetCurrent();
            }
            return question;
        }

        private void SetCurrent()
        {
            if (CurrentIndex < Count - 1)
                CurrentIndex++;
            else
                CurrentIndex = 0;
        }

        public void Add(params KeysightQuestion[] questions)
        {
            AddRange(questions);
        }

        #region Implementation of IEnumerable<out IQuestion>

        /// <summary>
        ///     返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        ///     可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1" />。
        /// </returns>
        IEnumerator<IQuestion<string>> IEnumerable<IQuestion<string>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<IQuestion>

        /// <summary>
        ///     将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 中。
        /// </summary>
        /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 的对象。</param>
        /// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。</exception>
        void ICollection<IQuestion<string>>.Add(IQuestion<string> item)
        {
            Add((KeysightQuestion)item);
        }

        /// <summary>
        ///     确定 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否包含特定值。
        /// </summary>
        /// <returns>
        ///     如果在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中找到 <paramref name="item" />，则为 true；否则为 false。
        /// </returns>
        /// <param name="item">要在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中定位的对象。</param>
        bool ICollection<IQuestion<string>>.Contains(IQuestion<string> item)
        {
            return Contains((KeysightQuestion)item);
        }

        /// <summary>
        ///     从特定的 <see cref="T:System.Array" /> 索引处开始，将 <see cref="T:System.Collections.Generic.ICollection`1" /> 的元素复制到一个
        ///     <see cref="T:System.Array" /> 中。
        /// </summary>
        /// <param name="array">
        ///     作为从 <see cref="T:System.Collections.Generic.ICollection`1" /> 复制的元素的目标位置的一维
        ///     <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。
        /// </param>
        /// <param name="arrayIndex"><paramref name="array" /> 中从零开始的索引，将在此处开始复制。</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> 为 null。</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> 小于 0。</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="array" /> 是多维数组。- 或 -源
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> 中的元素数大于从 <paramref name="arrayIndex" /> 到目标
        ///     <paramref name="array" /> 结尾处之间的可用空间。- 或 -无法自动将类型 <paramref name="T" /> 强制转换为目标 <paramref name="array" /> 的类型。
        /// </exception>
        void ICollection<IQuestion<string>>.CopyTo(IQuestion<string>[] array, int arrayIndex)
        {
            ((ICollection<IQuestion<string>>)this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <returns>
        ///     如果已从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中成功移除 <paramref name="item" />，则为 true；否则为
        ///     false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1" /> 中没有找到 <paramref name="item" />，该方法也会返回 false。
        /// </returns>
        /// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除的对象。</param>
        /// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。</exception>
        bool ICollection<IQuestion<string>>.Remove(IQuestion<string> item)
        {
            return Remove((KeysightQuestion)item);
        }

        /// <summary>
        ///     获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否为只读。
        /// </summary>
        /// <returns>
        ///     如果 <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读，则为 true；否则为 false。
        /// </returns>
        bool ICollection<IQuestion<string>>.IsReadOnly => ((ICollection<IQuestion<string>>)this).IsReadOnly;

        #endregion
    }
}
