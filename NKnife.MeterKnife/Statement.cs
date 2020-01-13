using System;

namespace NKnife.MeterKnife
{
    public class Statement : IStatement
    {
        #region Implementation of IStatement

        public string Body { get; set; }

        /// <summary>
        ///     当前控制语句绑定的测量设备
        /// </summary>
        public IInstrument Instrument { get; set; }
        public IUnitUnderTest Uut { get; set; }
        public StatementKind StatementKind { get; set; }
        public CommandMode CommandMode { get; set; }

        public int LoopCount { get; set; }
        public bool NeedToLoop { get; set; }

        /// <inheritdoc />
        public bool ContinueAsSoonReceiveResponse { get; set; }

        /// <inheritdoc />
        public uint Delay { get; set; }

        /// <inheritdoc />
        public uint Timeout { get; set; }

        #endregion

        #region Implementation of IEquatable<WordStatement>

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(IStatement other)
        {
            throw new NotImplementedException();
        }

        #endregion

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