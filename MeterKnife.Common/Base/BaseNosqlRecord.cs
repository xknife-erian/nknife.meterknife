using System.ComponentModel.DataAnnotations.Schema;

namespace NKnife.MeterKnife.Common.Base
{
    public class BaseNosqlRecord : BaseRecord
    {
        /// <summary>
        /// 文档格式版本
        /// </summary>
        [Column(nameof(Version))]
        public int Version { get; set; } = 1;

        #region Overrides of BaseRecord

        /// <inheritdoc />
        public override void AddLog(string user, string note = "")
        {
        }

        #endregion
    }
}
