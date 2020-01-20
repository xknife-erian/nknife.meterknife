using System;
using System.Threading;
using NKnife.Collections;
using NKnife.Interface;

namespace NKnife.MeterKnife.Util.Scpi
{
    /// <summary>
    ///     面向Care的命令封装
    /// </summary>
    public class CareCommand : IJobPoolItem
    {
        public CareCommand()
        {
            IsCare = false;
            Interval = 10;
            GpibAddress = 0;
        }

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
        public Tuple<byte, byte> Heads { get; set; }

        /// <summary>
        ///     当是Care的专属协议时的协议主体内容
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        ///     当是Care的专属协议时的等候周期
        /// </summary>
        public int Interval { get; set; }

        #region Implementation of IJobPoolItem

        public bool IsPool { get; } = false;

        #endregion

        public static CareCommand NullCommand()
        {
            var ci = new CareCommand {GpibAddress = -1};
            return ci;
        }

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
    }
}