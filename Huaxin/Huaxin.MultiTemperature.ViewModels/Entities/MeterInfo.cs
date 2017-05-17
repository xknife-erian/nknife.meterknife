using System.Collections.Generic;

namespace Huaxin.MultiTemperature.ViewModels.Entities
{
    /// <summary>
    /// 仪器信息
    /// </summary>
    public class MeterInfo
    {
        /// <summary>
        /// 数据库的ID。自动生成。
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 出厂编号。
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 型号规格。
        /// </summary>
        public string ModelNumber { get; set; }
        /// <summary>
        /// 生产厂商。
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 仪器名称。
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 校准或标定的流水号列表
        /// </summary>
        public List<string> CalibrationNumber { get; set; }
    }
}