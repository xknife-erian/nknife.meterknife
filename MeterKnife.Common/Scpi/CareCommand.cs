using System;
using System.Text;
using NKnife.Interface;
using NKnife.MeterKnife.Common.Domain;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    ///     面向Care的命令封装
    /// </summary>
    public class CareCommand : IJob
    {
        public CareCommand()
        {
            IsCare = false;
            Interval = 10;
            GpibAddress = 0;
        }

        public DUT DUT { get; set; }

        public Slot Slot { get; set; }

        /// <summary>
        ///     是否是Care的专属协议
        /// </summary>
        public bool IsCare { get; set; }

        /// <summary>
        ///     仪器的SCPI指令
        /// </summary>
        public Scpi Scpi { get; set; }

        /// <summary>
        ///     命令指向的仪器GPIB地址
        /// </summary>
        public short GpibAddress { get; set; }

        /// <summary>
        ///     当是Care的专属协议时的主命令字与子命令字
        /// </summary>
        public (byte, byte) Heads { get; set; }

        /// <summary>
        ///     当是Care的专属协议时的协议主体内容
        /// </summary>
        public byte[] Content { get; set; }

        public int Interval { get; set; }
        public int Timeout { get; set; }

        public bool IsLoop { get; set; }
        public int LoopCount { get; set; }
        public int CountOfCompleted { get; set; }

        public Func<IJob, bool> Run { get; set; }
        public Func<IJob, bool> Verify { get; set; }

        #region Implementation of IJobPoolItem

        public bool IsPool { get; } = false;

        #endregion

        public CareCommand Clone()
        {
            var item = new CareCommand
            {
                Content = Content,
                GpibAddress = GpibAddress,
                Heads = Heads,
                Interval = Interval,
                IsCare = IsCare,
                Scpi = Scpi
            };
            return item;
        }

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