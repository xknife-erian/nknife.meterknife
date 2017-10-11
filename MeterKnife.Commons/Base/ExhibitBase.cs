using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces;

namespace MeterKnife.Base
{
    public abstract class ExhibitBase : IExhibit
    {
        public string Name { get; set; }

        #region Implementation of IExhibit

        /// <summary>
        /// 被测物的ID，如不设置，将使用自动生成的ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
        public string Detail { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        #endregion

        #region Overrides of Object

        /// <summary>返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。</summary>
        /// <returns>
        /// <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。</returns>
        public override string ToString()
        {
            return Name;
        }

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

        protected virtual bool Equals(ExhibitBase other)
        {
            return string.Equals(Id, other.Id);
        }

        private readonly int _Hash = (Guid.NewGuid().GetHashCode() >> 28) * 31;

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return _Hash;
        }

        #endregion

        #endregion
    }
}