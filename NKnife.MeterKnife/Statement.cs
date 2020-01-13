using System;

namespace NKnife.MeterKnife
{
    public class Statement : IStatement, IEquatable<Statement>, ICloneable
    {
        protected readonly string _statementId;

        public Statement()
        {
            _statementId = Guid.NewGuid().ToString("N").ToUpper();
        }

        #region Implementation of IQueueNode

        public IQueueNode Last { get; set; }
        public IQueueNode Next { get; set; }

        #endregion

        #region Implementation of IStatement

        public string Header { get; set; }
        public string WordBody { get; set; }
        public byte[] HexBody { get; set; }
        public string Footer { get; set; }
        public string UutId { get; set; }
        public IStatementQueue Parent { get; set; }
        public StatementKind StatementKind { get; set; }
        public CommandMode CommandMode { get; set; }
        public int LoopCount { get; set; }
        public bool NeedToLoop { get; set; }

        /// <summary>
        ///     是否对时序要求严格。当为True时，仅<see cref="IStatement.Delay" />属性有效，其他相关时间属性设置将被忽略。
        /// </summary>
        public bool IsTimeCritical { get; set; }

        /// <summary>
        ///     如果为True时，当<see cref="IStatement.StatementKind" />为<seealso cref="IStatement.StatementKind.Ask" />
        ///     时，如果接收到远端回应立即执行下一条语句，
        ///     而不等待(忽略)<see cref="IStatement.Delay" />所配置的时间。
        /// </summary>
        public bool ContinueAsSoonReceiveResponse { get; set; }

        /// <summary>
        ///     当<see cref="IStatement.StatementKind" />为<seealso cref="IStatement.StatementKind.Ask" />时，需要等待远端回应的时间（毫秒）。
        ///     该属性视<see cref="IStatement.ContinueAsSoonReceiveResponse" />的配置可能被忽略。
        /// </summary>
        public uint Delay { get; set; }

        /// <summary>
        ///     在<see cref="IStatement.Delay" />配置的时长内，如果还没有收到数据，继续等待的时间（毫秒）。
        /// </summary>
        public uint Timeout { get; set; }

        public IStatement Add(IStatement statement)
        {
            this.Next = statement;
            return statement;
        }

        #endregion

        #region Equality members

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(Statement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _statementId == other._statementId;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Statement) obj);
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return (_statementId != null ? _statementId.GetHashCode() : 0);
        }

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}