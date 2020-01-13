using System.Collections.Generic;

namespace NKnife.MeterKnife
{
    /// <summary>
    ///     描述一条控制语句
    /// </summary>
    public interface IStatement : IQueueNode
    {
        string WordBody { get; set; }
        byte[] HexBody { get; set; }
        string UutId { get; set; }
        StatementKind StatementKind { get; set; }
        CommandMode CommandMode { get; set; }

        #region 循环指令的特征

        int LoopCount { get; set; }
        bool NeedToLoop { get; set; }

        #endregion

        #region 执行指令的时长相关

        /// <summary>
        ///     如果为True时，当<see cref="StatementKind" />为<seealso cref="StatementKind.Ask" />时，如果接收到远端回应立即执行下一条语句，
        ///     而不等待(忽略)<see cref="Delay" />所配置的时间。
        /// </summary>
        bool ContinueAsSoonReceiveResponse { get; set; }

        /// <summary>
        ///     当<see cref="StatementKind" />为<see cref="StatementKind.Ask" />时，需要等待远端回应的时间（毫秒）。
        ///     该属性视<see cref="ContinueAsSoonReceiveResponse" />的配置可能被忽略。
        /// </summary>
        uint Delay { get; set; }

        /// <summary>
        ///     在<see cref="Delay" />配置的时长内，如果还没有收到数据，继续等待的时间（毫秒）。
        /// </summary>
        uint Timeout { get; set; }

        #endregion
    }

    public enum CommandMode
    {
        Hex,
        Word
    }

    /// <summary>
    ///     语句的分类
    /// </summary>
    public enum StatementKind : byte
    {
        Ask = 0,
        Setup = 1,
        Test = 2
    }


    public interface IStatementQueue : IQueue<IStatement>
    {
        /// <summary>
        ///     是否对时序要求严格。当为True时，仅<see cref="IStatement.Delay" />属性有效，其他相关时间属性设置将被忽略。
        /// </summary>
        bool IsTimeCritical { get; set; }
    }

    public interface IQueue<T> where T : IQueueNode
    {
    }

    public interface IQueueNode
    {
        IQueueNode Last { get; set; }
        IQueueNode Next { get; set; }
    }

    public class BaseQueue : Queue<IStatement>, IQueue<IStatement>
    {
    }

    public class BaseStatementQueue : BaseQueue, IStatementQueue
    {
        #region Implementation of IStatementQueue

        public void AddJobsLeader(IStatement prepare, IStatement sustainable, IStatement maintain)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Implementation of IStatementQueue

        /// <summary>
        ///     是否对时序要求严格。当为True时，仅<see cref="IStatement.Delay" />属性有效，其他相关时间属性设置将被忽略。
        /// </summary>
        public bool IsTimeCritical { get; set; }

        #endregion
    }
}