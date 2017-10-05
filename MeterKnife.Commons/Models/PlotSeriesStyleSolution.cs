using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;
using NKnife.Base;

namespace MeterKnife.Models
{
    /// <summary>
    /// 数据折线图中被测物与被测物数据折线样式的列表，该列表在一些情况下会被反复使用，那么这个时候可以保存为方案
    /// </summary>
    public class PlotSeriesStyleSolution : List<PlotSeriesStyleSolution.ExhibitSeriesStyle>
    {
        public PlotSeriesStyleSolution()
        {
        }

        public PlotSeriesStyleSolution(int capacty)
            : base(capacty)
        {
        }

        public string Name { get; set; }

        /// <summary>
        /// 返回指定被测物在列表中的索引
        /// </summary>
        /// <param name="exhibit">指定的被测物</param>
        /// <returns>在列表中的索引，包含则返回索引号，否则返回-1</returns>
        public int IndexOf(ExhibitBase exhibit)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Exhibit.Equals(exhibit))
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
            return this.Any(style => style.Exhibit.Equals(exhibit));
        }

        /// <summary>
        /// 指定被测物与该被测物的数据折线的样式
        /// </summary>
        public class ExhibitSeriesStyle
        {
            public ExhibitSeriesStyle(ExhibitBase exhibit, PlotSeriesStyle seriesStyle)
            {
                Exhibit = exhibit;
                SeriesStyle = seriesStyle;
            }

            /// <summary>
            /// 指定被测物
            /// </summary>
            public ExhibitBase Exhibit { get; set; }
            /// <summary>
            /// 该被测物的数据折线
            /// </summary>
            public PlotSeriesStyle SeriesStyle { get; set; }
        }
    }
}