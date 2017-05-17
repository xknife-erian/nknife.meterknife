using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MeterKnife.Datas.Entities
{
    public class ExhibitData<T>
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public T[] Values { get; set; }

        #region Overrides of Object

        /// <summary>返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。</summary>
        /// <returns>
        /// <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。</returns>
        public override string ToString()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }

        #endregion
    }
}
