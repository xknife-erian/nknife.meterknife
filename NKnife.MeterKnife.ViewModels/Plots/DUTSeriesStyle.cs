using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    /// 指定被测物与该被测物的数据折线的样式
    /// </summary>
    public class DUTSeriesStyle
    {
        public DUTSeriesStyle(string dut)
        {
            Id = Guid.NewGuid();
            DUT = dut;
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

        public Color Color { get; set; } = PlotTheme.CommonlyUsedColors[0];
        public double Offset { get; set; } = 0;

        /// <summary>
        ///     数据线线径
        /// </summary>
        public double Thickness { get; set; } = 1.8;

        public string Text { get; set; }
        public LineStyle LineStyle { get; set; } = LineStyle.Solid;
        public LinearAxis Axis { get; set; } = new LinearAxis();

        #region Overrides of Object

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return $"[{DUT}]:: {Color}|{LineStyle}|{Offset}|{Thickness}";
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

        private static DUTSeriesStyle[] _lineStyles;
        public static DUTSeriesStyle Build(LineStyle lineStyle)
        {
            switch (lineStyle)
            {
                case LineStyle.Dash:
                    return new DUTSeriesStyle() { LineStyle = LineStyle.Dash, Text = "短划线" };
                case LineStyle.LongDash:
                    return new DUTSeriesStyle() { LineStyle = LineStyle.LongDash, Text = "长划线" };
                case LineStyle.DashDot:
                case LineStyle.DashDashDot:
                case LineStyle.DashDashDotDot:
                case LineStyle.DashDotDot:
                case LineStyle.LongDashDotDot:
                case LineStyle.LongDashDot:
                    return new DUTSeriesStyle() { LineStyle = LineStyle.LongDashDot, Text = "点划线" };
                case LineStyle.Dot:
                    return new DUTSeriesStyle() { LineStyle = LineStyle.Dot, Text = "点" };
                case LineStyle.Automatic:
                case LineStyle.Solid:
                case LineStyle.None:
                default:
                    return new DUTSeriesStyle() { LineStyle = LineStyle.Solid, Text = "实线" };
            }
        }

        /// <summary>
        ///     获取常用的数据线样式集合
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<DUTSeriesStyle> GetAllLineStyles()
        {
            if (_lineStyles == null)
            {
                _lineStyles = new DUTSeriesStyle[5];
                _lineStyles[0] = Build(LineStyle.Solid);
                _lineStyles[1] = Build(LineStyle.Dot);
                _lineStyles[2] = Build(LineStyle.Dash);
                _lineStyles[3] = Build(LineStyle.LongDash);
                _lineStyles[4] = Build(LineStyle.LongDashDot);
            }
            return Array.AsReadOnly(_lineStyles);
        }
    }
}