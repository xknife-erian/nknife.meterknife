using System;

namespace NKnife.Events
{
    public class EventArgs<T> : EventArgs
    {
        public T Item { get; private set; }
        public EventArgs(T item)
        {
            Item = item;
        }
    }

    public class EventArgs<T1, T2> : EventArgs
    {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }

        public EventArgs(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }

    public class EventArgs<T1, T2, T3> : EventArgs
    {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }
        public T3 Item3 { get; private set; }

        public EventArgs(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }

}
