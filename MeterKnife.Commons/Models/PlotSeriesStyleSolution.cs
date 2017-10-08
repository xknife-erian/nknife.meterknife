using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;
using Newtonsoft.Json;
using NKnife.Base;

namespace MeterKnife.Models
{
    /// <summary>
    /// 数据折线图中被测物与被测物数据折线样式的列表，该列表在一些情况下会被反复使用，那么这个时候可以保存为方案
    /// </summary>
    public class PlotSeriesStyleSolution
    {
        public PlotSeriesStyleSolution()
        {
        }

        public List<PlotSeriesStyleSolution.ExhibitSeriesStyle> Styles { get; set; } = new List<ExhibitSeriesStyle>(1);

        public string SolutionName { get; set; }

        /// <summary>
        /// 返回指定被测物在列表中的索引
        /// </summary>
        /// <param name="exhibit">指定的被测物</param>
        /// <returns>在列表中的索引，包含则返回索引号，否则返回-1</returns>
        public int IndexOf(ExhibitBase exhibit)
        {
            for (int i = 0; i < Styles.Count; i++)
            {
                if (Styles[i].Exhibit.Equals(exhibit))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 返回列表中是否包含被测物
        /// </summary>
        /// <param name="exhibit">被测物</param>
        /// <returns>列表中是否包含被测物，true包含，否则反之</returns>
        public bool Contains(ExhibitBase exhibit)
        {
            return Styles.Any(style => style.Exhibit.Equals(exhibit));
        }

        /// <summary>
        /// 指定被测物与该被测物的数据折线的样式
        /// </summary>
        public class ExhibitSeriesStyle
        {
            public ExhibitSeriesStyle(ExhibitBase exhibit, PlotSeriesStyle seriesStyle)
            {
                Id = Guid.NewGuid();
                Exhibit = exhibit;
                SeriesStyle = seriesStyle;
            }

            public ExhibitSeriesStyle()
            {
            }

            [JsonIgnore]
            public Guid Id { get; }

            /// <summary>
            /// 指定被测物
            /// </summary>
            public ExhibitBase Exhibit { get; set; }

            /// <summary>
            /// 该被测物的数据折线
            /// </summary>
            public PlotSeriesStyle SeriesStyle { get; set; }

            #region Overrides of Object

            /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
            /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
            /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
            public override bool Equals(object obj)
            {
                var style = obj as ExhibitSeriesStyle;
                return style != null && Equals(style);
            }

            #region Equality members

            protected bool Equals(ExhibitSeriesStyle other)
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
}