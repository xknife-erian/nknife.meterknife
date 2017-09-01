using System;
using NKnife.Channels.Interfaces;

namespace MeterKnife.Models
{
    public class Device : IDevice
    {
        public Device(string manufacturer, string model, string name, int address = -1)
        {
            Id = Guid.NewGuid().ToString("N");
            Manufacturer = manufacturer;
            Model = model;
            Name = name;
            Address = address;
        }

        public string Id { get; set; }

        #region Implementation of IDevice

        /// <summary>
        ///     生产厂商
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        ///     型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        ///     设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     设备常用简称
        /// </summary>
        public string AbbrName { get; } = string.Empty;

        /// <summary>
        ///     设备地址
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        ///     设备说明或信息
        /// </summary>
        public string Information { get; set; } = string.Empty;

        #endregion
    }
}