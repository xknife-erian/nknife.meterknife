using System.Collections.Generic;

namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    /// 一个描述分页时，一个页面里的对象集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
    {
        public Page()
        {
            
        }
        public Page(long total, int pageNumber, int pageSize, IEnumerable<T> array)
        {
            Total = total;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Array = array;
        }

        public long Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Array { get; set; }
    }
}
