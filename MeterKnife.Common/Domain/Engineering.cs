using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Jobs;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 一个测量工程
    /// </summary>
    public class Engineering : ICloneable
    {
        public Engineering()
        {
            Number = Guid.NewGuid().ToString("n");
        }

        /// <summary>
        /// 工程编号
        /// </summary>
        public string Number { get; set; }

        public List<CareCommand> Commands { get; set; } = new List<CareCommand>();

        #region Implementation of ICloneable

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
