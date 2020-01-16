using System.Collections;
using System.Collections.Generic;

namespace MeterKnife.Util.Tunnel.Common
{
    public class TunnelFilterChain : ITunnelFilterChain
    {
        private readonly LinkedList<ITunnelFilter> _Filters = new LinkedList<ITunnelFilter>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        public int Count
        {
            get { return _Filters.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<ITunnelFilter>) _Filters).IsReadOnly; }
        }

        public void RemoveFirst()
        {
            _Filters.RemoveFirst();
        }

        public void RemoveLast()
        {
            _Filters.RemoveLast();
        }

        public LinkedListNode<ITunnelFilter> Previous(LinkedListNode<ITunnelFilter> currentNode)
        {
            return currentNode.Previous;
        }

        public LinkedListNode<ITunnelFilter> Next(LinkedListNode<ITunnelFilter> currentNode)
        {
            return currentNode.Next;
        }

        public IEnumerator<ITunnelFilter> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        public void Add(ITunnelFilter item)
        {
            _Filters.AddLast(new LinkedListNode<ITunnelFilter>(item));
        }

        public bool Contains(ITunnelFilter item)
        {
            return _Filters.Contains(item);
        }

        public LinkedListNode<ITunnelFilter> Find(ITunnelFilter filter)
        {
            return _Filters.Find(filter);
        }

        public void CopyTo(ITunnelFilter[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITunnelFilter item)
        {
            return _Filters.Remove(item);
        }

        public void AddAfter(LinkedListNode<ITunnelFilter> node, ITunnelFilter newfilter)
        {
            _Filters.AddAfter(node, new LinkedListNode<ITunnelFilter>(newfilter));
        }

        public void AddBefore(LinkedListNode<ITunnelFilter> node, ITunnelFilter newfilter)
        {
            _Filters.AddBefore(node, new LinkedListNode<ITunnelFilter>(newfilter));
        }

        public void AddFirst(ITunnelFilter filter)
        {
            _Filters.AddFirst(new LinkedListNode<ITunnelFilter>(filter));
        }

        public void AddLast(ITunnelFilter filter)
        {
            _Filters.AddLast(new LinkedListNode<ITunnelFilter>(filter));
        }
    }
}