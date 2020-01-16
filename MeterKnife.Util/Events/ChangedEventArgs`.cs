using System;

namespace MeterKnife.Util.Events
{
    public class ChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 改变前的值
        /// </summary>
        public virtual T OldItem { get; private set; }

        /// <summary>
        /// 改变后的值
        /// </summary>
        public virtual T NewItem { get; private set; }

        public ChangedEventArgs(T oldItem, T newItem)
        {
            OldItem = oldItem;
            NewItem = newItem;
        }
    }
}
