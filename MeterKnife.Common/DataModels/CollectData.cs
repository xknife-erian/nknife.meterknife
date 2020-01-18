using System;

namespace NKnife.MeterKnife.Common.DataModels
{
    /// <summary>
    /// 采集数据
    /// </summary>
    public class CollectData
    {
        public const short VOLTAGE = 1;
        public const short RESISTANCE = 2;
        public const short CURRENT = 3;
        public static CollectData Build(DateTime dateTime, double data, double temperature)
        {
            return new CollectData(dateTime, data, temperature);
        }

        public CollectData()
        {

        }
        public CollectData(DateTime dateTime, double data, double temperature)
        {
            DateTime = dateTime;
            Data = data;
            Temperature = temperature;
        }

        /// <summary>
        /// 数据类型（指采集的是电压，电阻等的分类）
        /// </summary>
        public short DataType { get; set; }

        /// <summary>
        /// 采集数据的时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 采集到的数据
        /// </summary>
        public double Data { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public double Temperature { get; set; }
    }
}