using System;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Y轴区间，图表上下方留白空间的级别，1-10级，1级留白最大，10级留白最小。默认5级。
        /// </summary>
        public short YSpaceLevel { get; set; } = 5;

        public PlotTheme()
        {
            Id = Guid.NewGuid().ToString("D");
        }

        /// <summary>
        ///     图表画框区背景色
        /// </summary>
        public Color ViewBackground { get; set; } = Color.White;

        /// <summary>
        ///     图表区背景色
        /// </summary>
        public Color AreaBackground { get; set; } = Color.FromArgb(205, 230, 205);

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
            /// 突出(关键值)显示的颜色
            /// </summary>
            public Color Major { get; set; } = Color.White;
            /// <summary>
            /// 淡化(普通值)显示的颜色
            /// </summary>
            public Color Minor { get; set; } = Color.GhostWhite;
        }


        private static ReadOnlyCollection<Color> _allColors;

        /// <summary>
        ///     获取预置的折线图数据线的颜色，常用颜色。
        /// </summary>
        public static ReadOnlyCollection<Color> CommonlyUsedColors
        {
            get
            {
                if (_allColors == null)
                {
                    var colors = new Color[8];
                    colors[0] = Color.FromArgb(68, 114, 196);
                    colors[1] = Color.FromArgb(237, 125, 49);

                    colors[2] = Color.FromArgb(112, 173, 71);
                    colors[3] = Color.FromArgb(255, 192,0);

                    colors[4] = Color.FromArgb(158,72,14);
                    colors[5] = Color.FromArgb(165,165,165);

                    colors[6] = Color.FromArgb(67,104,43);
                    colors[7] = Color.FromArgb(83,155,213);

                    _allColors = Array.AsReadOnly(colors);
                }
                return _allColors;
            }
        }
    }
}