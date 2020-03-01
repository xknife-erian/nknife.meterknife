using System.Collections.Generic;
using System.Linq;
using NKnife.MeterKnife.Common.Scpi;
using OxyPlot;
using OxyPlot.Axes;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    /// 数据折线图中被测物与被测物数据折线样式的列表，该列表在一些情况下会被反复使用，那么这个时候可以保存为方案
    /// </summary>
    public class DUTSeriesStyleSolution : List<DUTSeriesStyle>
    {
        public string Name { get; set; }

        /// <summary>
        /// 返回指定被测物在列表中的索引
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <returns>在列表中的索引，包含则返回索引号，否则返回-1</returns>
        public int IndexOf(string dut)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].DUT.Equals(dut))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 返回列表中是否包含被测物
        /// </summary>
        /// <param name="dut">被测物</param>
        /// <returns>列表中是否包含被测物，true包含，否则反之</returns>
        public bool Contains(string dut)
        {
            return this.Any(style => style.DUT.Equals(dut));
        }

        public static DUTSeriesStyleSolution GetSolution(params ScpiCommandPool[] poolArray)
        {
            var left = 0;
            var right = 0;

            var solution = new DUTSeriesStyleSolution();
            foreach (var pool in poolArray)
            {
                for (var index = 0; index < pool.Count; index++)
                {
                    var cmd = pool[index];
                    var style = DUTSeriesStyle.Build(LineStyle.Solid); //.GetAllLineStyles()[index];
                    style.Color = PlotTheme.CommonlyUsedColors[index];
                    style.DUT = cmd.DUT.Id;

                    style.Axis = new LinearAxis();
                    style.Axis.Key = cmd.DUT.Id;
                    style.Axis.FontSize = 13d;
                    style.Axis.MajorGridlineStyle = LineStyle.Dash;
                    style.Axis.MinorGridlineStyle = LineStyle.Dot;
                    style.Axis.MaximumPadding = 0;
                    style.Axis.MinimumPadding = 0;
                    style.Axis.Angle = 0;
                    style.Axis.Maximum = 220;
                    style.Axis.Minimum = -220;
                    if (index % 2 == 0)
                    {
                        style.Axis.PositionTier = left++;
                        style.Axis.Position = AxisPosition.Left;
                    }
                    else
                    {
                        style.Axis.PositionTier = right++;
                        style.Axis.Position = AxisPosition.Right;
                    }

                    solution.Add(style);
                }
            }

            return solution;
        }
    }
}