using System.Collections.Generic;
using System.Linq;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    /// 数据折线图中被测物与被测物数据折线样式的列表，该列表在一些情况下会被反复使用，那么这个时候可以保存为方案
    /// </summary>
    public class PlotSeriesStyleSolution
    {
        public PlotSeriesStyleSolution()
        {
        }

        public List<DUTSeriesStyle> Styles { get; set; } = new List<DUTSeriesStyle>(1);

        public string SolutionName { get; set; }

        /// <summary>
        /// 返回指定被测物在列表中的索引
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <returns>在列表中的索引，包含则返回索引号，否则返回-1</returns>
        public int IndexOf(string dut)
        {
            for (int i = 0; i < Styles.Count; i++)
            {
                if (Styles[i].DUT.Equals(dut))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 返回列表中是否包含被测物
        /// </summary>
        /// <param name="exhibit">被测物</param>
        /// <returns>列表中是否包含被测物，true包含，否则反之</returns>
        public bool Contains(string exhibit)
        {
            return Styles.Any(style => style.DUT.Equals(exhibit));
        }

    }
}