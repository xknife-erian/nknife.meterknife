using System;
using Newtonsoft.Json;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    /// 指定被测物与该被测物的数据折线的样式
    /// </summary>
    public class DUTSeriesStyle
    {
        public DUTSeriesStyle(string dut, SeriesStyle seriesStyle)
        {
            Id = Guid.NewGuid();
            DUT = dut;
            SeriesStyle = seriesStyle;
        }

        public DUTSeriesStyle()
        {
        }

        [JsonIgnore]
        public Guid Id { get; }

        /// <summary>
        /// 指定被测物
        /// </summary>
        public string DUT { get; set; }

        /// <summary>
        /// 该被测物的数据折线
        /// </summary>
        public SeriesStyle SeriesStyle { get; set; }

        #region Overrides of Object

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return $"[{DUT}]:: {SeriesStyle}";
        }

        /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
        /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        public override bool Equals(object obj)
        {
            return obj is DUTSeriesStyle style && Equals(style);
        }

        #region Equality members

        protected bool Equals(DUTSeriesStyle other)
        {
            return Id.Equals(other.Id);
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #endregion
    }
}