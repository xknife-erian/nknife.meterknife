using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeterKnife.Datas.Entities
{
    public class ExhibitData<T>
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public T[] Values { get; set; }
    }
}
