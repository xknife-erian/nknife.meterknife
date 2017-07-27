using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OxyPlot;

namespace MeterKnife.Utils
{
    public class PlotTheme
    {
        /// <summary>
        /// 图表区背景色
        /// </summary>
        public OxyColor AreaColor { get; set; }

        /// <summary>
        /// 设置图表区背景色
        /// </summary>
        public void SetAreaColor(Color color)
        {
            AreaColor = OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// 数据线颜色
        /// </summary>
        public OxyColor SeriesColor { get; set; }

        /// <summary>
        /// 设置图表区背景色
        /// </summary>
        public void SetSeriesColor(Color color)
        {
            SeriesColor = OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// 数据线线径
        /// </summary>
        public double Thickness { get; set; } = 2;
    }
}
