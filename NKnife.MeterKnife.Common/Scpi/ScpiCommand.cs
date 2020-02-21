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

        public int Interval { get; set; }
        public int Timeout { get; set; }

        public bool IsLoop { get; set; }
        /// <summary>
        /// 优先完成工作，即完成循环设置的工作数量
        /// </summary>
        public bool IsPrecedenceWork { get; set; }
        public int LoopCount { get; set; }
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