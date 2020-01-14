using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public class KnifeSocketClientFilterChain : ITunnelFilterChain<EndPoint, Socket>
    {
        private readonly LinkedList<KnifeSocketClientFilter> _Filters = new LinkedList<KnifeSocketClientFilter>();

        IEnumerator<ITunnelFilter<EndPoint, Socket>> IEnumerable<ITunnelFilter<EndPoint, Socket>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ITunnelFilter<EndPoint, Socket>>.Add(ITunnelFilter<EndPoint, Socket> item)
        {
            Add((KnifeSocketClientFilter)item);
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        bool ICollection<ITunnelFilter<EndPoint, Socket>>.Contains(ITunnelFilter<EndPoint, Socket> item)
        {
            return Contains((KnifeSocketClientFilter)item);
        }

        void ICollection<ITunnelFilter<EndPoint, Socket>>.CopyTo(ITunnelFilter<EndPoint, Socket>[] array, int arrayIndex)
        {
            CopyTo((KnifeSocketClientFilter[])array, arrayIndex);
        }

        bool ICollection<ITunnelFilter<EndPoint, Socket>>.Remove(ITunnelFilter<EndPoint, Socket> item)
        {
            return Remove((KnifeSocketClientFilter)item);
        }

        public int Count
        {
            get { return _Filters.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KnifeSocketClientFilter>)_Filters).IsReadOnly; }
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddAfter(ITunnelFilter<EndPoint, Socket> filter, ITunnelFilter<EndPoint, Socket> newfilter)
        {
            AddAfter((KnifeSocketClientFilter)filter, (KnifeSocketClientFilter)newfilter);
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddBefore(ITunnelFilter<EndPoint, Socket> filter, ITunnelFilter<EndPoint, Socket> newfilter)
        {
            AddBefore((KnifeSocketClientFilter)filter, (KnifeSocketClientFilter)newfilter);
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddFirst(ITunnelFilter<EndPoint, Socket> filter)
        {
            AddFirst((KnifeSocketClientFilter)filter);
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddLast(ITunnelFilter<EndPoint, Socket> filter)
        {
            AddLast((KnifeSocketClientFilter)filter);
        }

        public void RemoveFirst()
        {
            _Filters.RemoveFirst();
        }

        public void RemoveLast()
        {
            _Filters.RemoveLast();
        }

        public IEnumerator<KnifeSocketClientFilter> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        public void Add(KnifeSocketClientFilter item)
        {
            _Filters.AddLast(item);
        }

        public bool Contains(KnifeSocketClientFilter item)
        {
            return _Filters.Contains(item);
        }

        public void CopyTo(KnifeSocketClientFilter[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(KnifeSocketClientFilter item)
        {
            return _Filters.Remove(item);
        }

        public void AddAfter(KnifeSocketClientFilter filter, KnifeSocketClientFilter newfilter)
        {
            _Filters.AddAfter(new LinkedListNode<KnifeSocketClientFilter>(filter), new LinkedListNode<KnifeSocketClientFilter>(newfilter));
        }

        public void AddBefore(KnifeSocketClientFilter filter, KnifeSocketClientFilter newfilter)
        {
            _Filters.AddBefore(new LinkedListNode<KnifeSocketClientFilter>(filter), new LinkedListNode<KnifeSocketClientFilter>(newfilter));
        }

        public void AddFirst(KnifeSocketClientFilter filter)
        {
            _Filters.AddFirst(new LinkedListNode<KnifeSocketClientFilter>(filter));
        }

        public void AddLast(KnifeSocketClientFilter filter)
        {
            _Filters.AddLast(new LinkedListNode<KnifeSocketClientFilter>(filter));
        }
    }
}
