using System;
using System.ComponentModel.DataAnnotations;
using NKnife.Db.Base;
using NKnife.Interface;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 被测物
    /// </summary>
    public class DUT : IId
    {
        public DUT()
        {
            Id = $"DUT{SequentialGuid.Create().ToString("N").ToUpper()}";
            CreateTime = DateTime.Now;
        }

        [Key] [Index] [Required] 
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string ImagePath { get; set; }
        public string ReportPath { get; set; }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Id}/{Name}";
        }

        #endregion
    }
}
