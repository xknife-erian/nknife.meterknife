using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OxyPlot;

namespace MeterKnife.Models
{
    public class PlotSeriesStyle
    {
        public Color Color { get; set; } = AllSeriesColors[0];
        public Color MarkerFillColor { get; set; } = Color.Red;
        public Color MarkerStrokeColor { get; set; } = Color.Red;

        /// <summary>
        ///     数据线线径
        /// </summary>
        public double Thickness { get; set; } = 1.8;

        public LineStyleWrap SeriesLineStyle { get; set; } = LineStyleWrap.Default;

        private static ReadOnlyCollection<Color> _allColors;

        /// <summary>
        /// 获取预置的折线图数据线的颜色，共13种易识别颜色
        /// </summary>
        public static ReadOnlyCollection<Color> AllSeriesColors
        {
            get
            {
                if (_allColors == null)
                {
                    var colors = new Color[13];
                    colors[0] = Color.FromArgb(255, 255, 0);
                    colors[1] = Color.FromArgb(0, 255, 255);
                    colors[2] = Color.FromArgb(255, 0, 255);

                    colors[3] = Color.FromArgb(0, 255, 0);
                    colors[4] = Color.FromArgb(0, 0, 255);
                    colors[5] = Color.FromArgb(255, 0, 0);

                    colors[6] = Color.FromArgb(128, 255, 128);
                    colors[7] = Color.FromArgb(128, 128, 255);
                    colors[8] = Color.FromArgb(255, 128, 128);

                    colors[9] = Color.FromArgb(255, 128, 0);
                    colors[10] = Color.FromArgb(0, 128, 255);
                    colors[11] = Color.FromArgb(255, 0, 128);
                    colors[12] = Color.FromArgb(128, 255, 0);
                    _allColors = Array.AsReadOnly(colors);
                }
                return _allColors;
            }
        }

        public static LineStyleWrap Build(LineStyle lineStyle)
        {
            switch (lineStyle)
            {
                case LineStyle.Dash:
                    return new LineStyleWrap(LineStyle.Dash, "短划线");
                case LineStyle.LongDash:
                    return new LineStyleWrap(LineStyle.LongDash, "长划线");
                case LineStyle.DashDot:
                case LineStyle.DashDashDot:
                case LineStyle.DashDashDotDot:
                case LineStyle.DashDotDot:
                case LineStyle.LongDashDotDot:
                case LineStyle.LongDashDot:
                    return new LineStyleWrap(LineStyle.LongDashDot, "点划线");
                case LineStyle.Dot:
                    return new LineStyleWrap(LineStyle.Dot, "点");
                case LineStyle.Automatic:
                case LineStyle.Solid:
                case LineStyle.None:
                default:
                    return new LineStyleWrap(LineStyle.Solid, "实线");
            }
        }

        /// <summary>
        /// 获取常用的数据线样式集合
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<LineStyleWrap> GetAllLineStyles()
        {
            var lineStyleWraps = new LineStyleWrap[5];
            lineStyleWraps[0] = Build(LineStyle.Solid);
            lineStyleWraps[1] = Build(LineStyle.Dot);
            lineStyleWraps[2] = Build(LineStyle.Dash);
            lineStyleWraps[3] = Build(LineStyle.LongDash);
            lineStyleWraps[4] = Build(LineStyle.LongDashDot);
            return Array.AsReadOnly(lineStyleWraps);
        }

        /// <summary>
        /// 将数据线样式，和对样式的中文描述封装类(结构体)
        /// </summary>
        public struct LineStyleWrap
        {
            public LineStyleWrap(LineStyle lineStyle, string text)
            {
                Text = text;
                LineStyle = lineStyle;
            }

            public string Text { get; set; }
            public LineStyle LineStyle { get; set; }

            public static LineStyleWrap Default => new LineStyleWrap(LineStyle.Solid, "实线");

            #region Overrides of ValueType

            /// <summary>返回该实例的完全限定类型名。</summary>
            /// <returns>包含完全限定类型名的 <see cref="T:System.String" />。</returns>
            public override string ToString()
            {
                return Text;
            }

            #endregion
        }
    }
}