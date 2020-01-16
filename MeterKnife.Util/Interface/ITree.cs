using System.Collections.Generic;

namespace MeterKnife.Util.Interface
{
    public interface ITree<T>
    {
        T Parent { get; set; }
        bool HasChildren { get; }
        IList<T> Items { get; set; }
    }

}
