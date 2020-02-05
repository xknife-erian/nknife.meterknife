using System;
using System.ComponentModel.DataAnnotations;
using NKnife.Db.Base;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 被测物
    /// </summary>
    public class DUT
    {
        public DUT()
        {
            Id = $"DUT{SequentialGuid.Create().ToString("N").ToUpper()}";
            CreateTime = DateTime.Now;
        }

        [Key] [Index] [Required] public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string ImagePath { get; set; }
        public string ReportPath { get; set; }
    }
}
