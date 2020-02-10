using System.Drawing;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    public class SeriesStyle
    {
        public Color Color { get; set; } = LineStyleWrap.AllLineColors[0];
        public double Offset { get; set; } = 0;

        /// <summary>
        ///     数据线线径
        /// </summary>
        public double Thickness { get; set; } = 1.8;

        public LineStyleWrap SeriesLineStyle { get; set; } = LineStyleWrap.Default;
    }
}