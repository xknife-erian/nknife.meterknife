using System.Collections.Generic;

namespace NKnife.MeterKnife.Util.Tunnel
{
    public interface ITunnelFilterChain : ICollection<ITunnelFilter>
    {
        LinkedListNode<ITunnelFilter> Find(ITunnelFilter filter);
        void AddAfter(LinkedListNode<ITunnelFilter> node, ITunnelFilter newfilter);
        void AddBefore(LinkedListNode<ITunnelFilter> node, ITunnelFilter newfilter);
        void AddFirst(ITunnelFilter filter);
        void AddLast(ITunnelFilter filter);
        void RemoveFirst();
        void RemoveLast();
        LinkedListNode<ITunnelFilter> Previous(LinkedListNode<ITunnelFilter> currentNode);
        LinkedListNode<ITunnelFilter> Next(LinkedListNode<ITunnelFilter> currentNode);
    }
}