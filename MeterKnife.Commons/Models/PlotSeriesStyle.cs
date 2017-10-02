using System.Drawing;
using OxyPlot;

namespace MeterKnife.Models
{
    public class PlotSeriesStyle
    {
        public Color Color { get; set; } = Color.Yellow;
        public Color MarkerFillColor { get; set; } = Color.Red;
        public Color MarkerStrokeColor { get; set; } = Color.Red;

        /// <summary>
        ///     数据线线径
        /// </summary>
        public double Thickness { get; set; } = 1.8;

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

        public static LineStyleWrap[] GetAllLineStyles()
        {
            var lineStyleWraps = new LineStyleWrap[5];
            lineStyleWraps[0] = Build(LineStyle.Solid);
            lineStyleWraps[1] = Build(LineStyle.Dot);
            lineStyleWraps[2] = Build(LineStyle.Dash);
            lineStyleWraps[3] = Build(LineStyle.LongDash);
            lineStyleWraps[4] = Build(LineStyle.LongDashDot);
            return lineStyleWraps;
        }

        public struct LineStyleWrap
        {
            public LineStyleWrap(LineStyle lineStyle, string text)
            {
                Text = text;
                LineStyle = lineStyle;
            }
            public string Text { get; set; }
            public LineStyle LineStyle { get; set; }

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