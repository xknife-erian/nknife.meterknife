using System;
using System.Drawing;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    /// 图表主题，保存图表的颜色等信息。
    /// </summary>
    public class PlotTheme
    {
        public string Id { get; set; }
        public string Name { get; set; } = "默认主题";

        public PlotTheme()
        {
            Id = Guid.NewGuid().ToString("D");
        }

        /// <summary>
        ///     图表画框区背景色
        /// </summary>
        public Color ViewBackground { get; set; } = Color.MidnightBlue;

        /// <summary>
        ///     图表区背景色
        /// </summary>
        public Color AreaBackground { get; set; } = Color.DimGray;

        /// <summary>
        ///     左侧数轴网格线颜色，第一个值是突出显示的颜色，第二个值是一般显示的颜色
        /// </summary>
        public GridLineColors LeftAxisGridLineColors { get; set; } = new GridLineColors();

        /// <summary>
        ///     底部数轴网格线颜色，第一个值是突出显示的颜色，第二个值是一般显示的颜色
        /// </summary>
        public GridLineColors BottomAxisGridLineColors { get; set; } = new GridLineColors();
        
        /// <summary>
        ///     顶部侧数轴网格线颜色，第一个值是突出显示的颜色，第二个值是一般显示的颜色
        /// </summary>
        public GridLineColors TopAxisGridLineColors { get; set; } = new GridLineColors();

        /// <summary>
        ///     右侧数轴网格线颜色，第一个值是突出显示的颜色，第二个值是一般显示的颜色
        /// </summary>
        public GridLineColors RightAxisGridLineColors { get; set; } = new GridLineColors();

        /// <summary>返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。</summary>
        /// <returns>
        /// <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 一对颜色，用在比如数轴网格线的着色上
        /// </summary>
        public class GridLineColors
        {
            /// <summary>
            /// 突出显示的颜色
            /// </summary>
            public Color Major { get; set; } = ColorTranslator.FromHtml("#40000000");
            /// <summary>
            /// 一般显示的颜色
            /// </summary>
            public Color Minor { get; set; } = ColorTranslator.FromHtml("#20000000");
        }
    }
}