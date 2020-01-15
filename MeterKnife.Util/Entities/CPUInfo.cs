namespace NKnife.Entities
{
    public class CPUInfo
    {
        public CPUInfo()
        {
            CurrBaseBoard = new BaseBoard();
        }

        /// <summary>CPU编号
        /// </summary>
        public string ProcessorId { get; set; }
        /// <summary>当前电压
        /// </summary>
        public string CurrentVoltage { get; set; }
        /// <summary>外部频率
        /// </summary>
        public string ExtClock { get; set; }
        /// <summary>二级缓存尺寸
        /// </summary>
        public string L2CacheSize { get; set; }
        /// <summary>制造商
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>当前使用百分比
        /// </summary>
        public string LoadPercentage { get; set; }
        /// <summary>最大时钟频率
        /// </summary>
        public string MaxClockSpeed { get; set; }
        /// <summary>当前时钟频率
        /// </summary>
        public string CurrentClockSpeed { get; set; }
        /// <summary>CPU地址宽度
        /// </summary>
        public string AddressWidth { get; set; }
        /// <summary>CPU数据宽度
        /// </summary>
        public string DataWidth { get; set; }
        /// <summary>主板相关信息
        /// </summary>
        public BaseBoard CurrBaseBoard { get; set; }

        /// <summary>主板相关信息
        /// </summary>
        public class BaseBoard
        {
            /// <summary>主板制造商
            /// </summary>
            public string Manufacturer { get; set; }
            /// <summary>产品
            /// </summary>
            public string Product { get; set; }
            /// <summary>主板序列号
            /// </summary>
            public string SerialNumber { get; set; }
        }
    }
}
