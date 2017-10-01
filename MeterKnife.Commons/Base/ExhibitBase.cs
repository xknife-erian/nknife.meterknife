using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Base
{
    public abstract class ExhibitBase : IExhibit
    {
        protected ExhibitBase()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
        }

        #region Implementation of IExhibit

        public string Id { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedTime { get; set; }

        #endregion

        #region Overrides of Object

        /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
        /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        public override bool Equals(object obj)
        {
            var eb = obj as ExhibitBase;
            if (eb == null) return false;
            return Equals(eb);
        }

        #region Equality members

        protected bool Equals(ExhibitBase other)
        {
            return string.Equals(Id, other.Id);
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        #endregion

        #endregion
    }
}