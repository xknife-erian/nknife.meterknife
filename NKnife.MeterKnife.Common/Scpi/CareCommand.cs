using System.Text;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Scpi
{
    public class CareCommand : ScpiCommand
    {
        #region MeterCare专用属性

        /// <summary>
        ///     是否是Care的专属协议
        /// </summary>
        public bool IsCare { get; set; } = true;

        /// <summary>
        ///     当是Care的专属协议时的主命令字与子命令字
        /// </summary>
        public (byte, byte) Heads { get; set; }

        /// <summary>
        ///     当是Care的专属协议时的协议主体内容
        /// </summary>
        public byte[] Content { get; set; }

        #endregion
    }
}