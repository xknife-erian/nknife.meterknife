using NKnife.Channels.Interfaces;

namespace MeterKnife.Models
{
    public class Device : IDevice
    {
        public Device(string brand, string name, string abbrName, int address = -1)
        {
            Brand = brand;
            Name = name;
            AbbrName = abbrName;
            Address = address;
        }

        #region Implementation of IDevice

        /// <summary>
        ///     品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///     设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     设备常用简称
        /// </summary>
        public string AbbrName { get; }

        /// <summary>
        ///     设备地址
        /// </summary>
        public int Address { get; set; }

        #endregion
    }
}
