using System;
using Newtonsoft.Json;

namespace MeterKnife.Models.Datas
{
    /// <summary>
    /// 被测物在单一时刻的测量值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExhibitData<T>
    {
        /// <summary>
        /// 被测物的ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 被测物的单一时刻
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 测量值
        /// </summary>
        public T Value { get; set; }

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
