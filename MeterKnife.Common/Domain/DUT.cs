using System;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 被测物
    /// </summary>
    public class DUT
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string ImagePath { get; set; }
        public string ReportPath { get; set; }
    }
}
