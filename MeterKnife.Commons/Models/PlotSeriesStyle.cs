using System.Drawing;

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
    }
}