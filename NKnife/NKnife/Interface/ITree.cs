using System.Collections.Generic;

namespace NKnife.Interface
{
    public interface ITree<T>
    {
        T Parent { get; set; }
        bool HasChildren { get; }
        IList<T> Items { get; set; }
    }

}
