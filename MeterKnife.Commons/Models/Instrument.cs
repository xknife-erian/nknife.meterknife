using System;
using System.Drawing;

namespace MeterKnife.Models
{
    /// <summary>
    /// 仪器
    /// </summary>
    public class Instrument : Device
    {
        public Instrument(string manufacturer, string model, string name, int address = -1) 
            : base(manufacturer, model, name, address)
        {
        }

        /// <summary>
        /// 仪器的图片
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// 仪器的连接字符串
        /// </summary>
        public string ConnectString { get; set; } = string.Empty;

        /// <summary>
        /// 使用该议器采集的数据数量
        /// </summary>
        public int DatasCount { get; set; }

        /// <summary>
        /// 最后一次使用该仪器的时间
        /// </summary>
        public DateTime LastUsingTime { get; set; } = DateTime.Now;
    }
}