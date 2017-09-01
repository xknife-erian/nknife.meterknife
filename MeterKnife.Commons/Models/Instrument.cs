using System;
using System.Drawing;

namespace MeterKnife.Models
{
    /// <summary>
    ///     仪器
    /// </summary>
    public class Instrument : Device
    {
        public Instrument(string manufacturer, string model, string name, int address = -1)
            : base(manufacturer, model, name, address)
        {
        }

        /// <summary>
        ///     仪器的图片
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        ///     使用该议器采集的数据数量
        /// </summary>
        public int DatasCount { get; set; }

        /// <summary>
        ///     最后一次使用该仪器的时间
        /// </summary>
        public DateTime LastUsingTime { get; set; } = DateTime.Now;

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