using System.Windows.Forms;
using MeterKnife.Models;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls.Instruments
{
    public class CellClickEventArgs : MouseEventArgs
    {
        /// <summary>初始化 <see cref="T:System.Windows.Forms.MouseEventArgs" /> 类的新实例。</summary>
        /// <param name="instrument">仪器</param>
        /// <param name="button">
        ///     <see cref="T:System.Windows.Forms.MouseButtons" /> 值之一，它指示曾按下的是哪个鼠标按钮。
        /// </param>
        /// <param name="clicks">鼠标按钮曾被按下的次数。</param>
        /// <param name="x">鼠标单击的 x 坐标（以像素为单位）。</param>
        /// <param name="y">鼠标单击的 y 坐标（以像素为单位）。</param>
        /// <param name="delta">鼠标轮已转动的制动器数的有符号计数。</param>
        public CellClickEventArgs(Instrument instrument, MouseButtons button, int clicks, int x, int y, int delta)
            : base(button, clicks, x, y, delta)
        {
            Instrument = instrument;
        }

        public Instrument Instrument { get; set; }
    }
}