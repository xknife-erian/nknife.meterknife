using System;
using System.ComponentModel.DataAnnotations;
using NKnife.Db.Base;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 测量数据
    /// </summary>
    public class MetricalData
    {
        public MetricalData()
        {

        }

        /// <summary>
        /// 测量时间
        /// </summary>
        [Key]
        [Index]
        [Required]
        public DateTime Time { get; set; }

        /// <summary>
        /// 测量数据值
        /// </summary>
        public double Data { get; set; }

        /// <summary>
        /// 对该值的标记，如：异常，超差等
        /// </summary>
        public short Flag { get; set; }

        /// <summary>
        /// 当前测量时间的运算值
        /// </summary>
        public double Ufunc { get; set; }

        /// <summary>
        /// 一些其他记录
        /// </summary>
        public string Note { get; set; }


    }
}