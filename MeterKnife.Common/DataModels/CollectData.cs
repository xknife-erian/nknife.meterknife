using System;

namespace MeterKnife.Common.DataModels
{
    /// <summary>
    /// 采集数据
    /// </summary>
    public class CollectData
    {
        public const short VOLTAGE = 1;
        public const short RESISTANCE = 2;
        public const short CURRENT = 3;
        public static CollectData Build(DateTime dateTime, double data)
        {
            return new CollectData(dateTime, data);
        }

        public CollectData()
        {

        }
        public CollectData(DateTime dateTime, double data)
        {
            DateTime = dateTime;
            Data = data;
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
    }
}