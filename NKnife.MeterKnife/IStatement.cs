using System;
using System.Collections.Generic;

namespace NKnife.MeterKnife
{
    /// <summary>
    ///     描述一条控制语句
    /// </summary>
    public interface IStatement : IEquatable<IStatement>, ICloneable
    {
        /// <summary>
        /// 当前控制语句
        /// </summary>
        string Body { get; set; }

        /// <summary>
        ///     当前控制语句绑定的测量设备
        /// </summary>
        IInstrument Instrument { get; set; }

        /// <summary>
        ///     当前控制语句绑定的待测单元
        /// </summary>
        IUnitUnderTest Uut { get; set; }

        /// <summary>
        /// 当前控制语句的分类
        /// </summary>
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
}