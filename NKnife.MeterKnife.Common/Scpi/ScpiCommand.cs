using System;
using NKnife.Interface;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    ///     面向Care的命令封装
    /// </summary>
    public class ScpiCommand : IJob
    {
        public ScpiCommand()
        {
            Interval = 10;
            GpibAddress = 0;
        }
        
        public DUT DUT { get; set; }

        public Slot Slot { get; set; }

        /// <summary>
        ///     命令指向的仪器GPIB地址
        /// </summary>
        public short GpibAddress { get; set; }

        /// <summary>
        ///     仪器的SCPI指令
        /// </summary>
        public SCPI Scpi { get; set; }

        /// <summary>
        ///     附属信息
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        ///     定时时长
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        ///     超时时长
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        ///     是否需要循环
        /// </summary>
        public bool IsLoop { get; set; }

        /// <summary>
        ///     优先完成工作，即完成循环设置的工作数量
        /// </summary>
        public bool IsPrecedenceWork { get; set; }

        /// <summary>
        ///     循环次数
        /// </summary>
        public int LoopCount { get; set; }

        /// <summary>
        ///     如设置有循环次数<see cref="LoopCount"/>，那么这里记录已完成的次数。
        /// </summary>
        public int CountOfCompleted { get; set; }

        public Func<IJob, bool> Run { get; set; }
        public Func<IJob, bool> Verify { get; set; }

        #region Implementation of IJobPoolItem

        public bool IsPool { get; } = false;

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Slot} | {Scpi}";
        }

        #endregion
    }
}