using System;
using System.Drawing;
using Newtonsoft.Json;
using NKnife.Interface;

namespace MeterKnife.Models
{
    /// <summary>
    ///     仪器
    /// </summary>
    public class Instrument : IId
    {
        public Instrument(string manufacturer, string model, string name, int address = -1)
        {
            Id = Guid.NewGuid().ToString("N");
            Manufacturer = manufacturer;
            Model = model;
            Name = name;
            Address = address;
        }

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

        /// <summary>
        ///     仪器的图片
        /// </summary>
        [JsonIgnore]
        public Image Image { get; set; }

        /// <summary>
        ///     使用该议器采集的数据数量
        /// </summary>
        public int DatasCount { get; set; }

        /// <summary>
        ///     最后一次使用该仪器的时间
        /// </summary>
        public DateTime LastUsingTime { get; set; } = DateTime.Now;

        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            var bo = obj as Instrument;
            if (bo == null)
                return false;
            return Equals(bo);
        }

        #region Equality members

        protected bool Equals(Instrument other)
        {
            return string.Equals(Id, other.Id)
                   && string.Equals(Manufacturer, other.Manufacturer)
                   && string.Equals(Model, other.Model);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^
                       ((Manufacturer != null ? Manufacturer.GetHashCode() : 0) * 397) ^
                       (Model != null ? Model.GetHashCode() : 0);
            }
        }

        #endregion
    }
}