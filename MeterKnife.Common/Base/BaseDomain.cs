using System.ComponentModel.DataAnnotations;

namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    /// 领域对象
    /// </summary>
    public abstract class BaseDomain : BaseRecord, IDomain
    {
        #region Implementation of IDomain

        /// <summary>
        /// 对象名，通常是显示出来的名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        #endregion
    }
}