using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;

namespace MeterKnife.MiscDemo.Mocks
{
    class AbcExhibit : ExhibitBase
    {
        #region Overrides of Object

        /// <summary>返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。</summary>
        /// <returns>
        /// <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。</returns>
        public override string ToString()
        {
            return Id.Substring(0, 5);
        }

        #endregion
    }
}
