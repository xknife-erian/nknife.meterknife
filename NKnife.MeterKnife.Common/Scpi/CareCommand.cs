namespace NKnife.MeterKnife.Common.Scpi
{
    public class CareCommand
    {
        public CareCommand((byte, byte) heads, byte[] content = null)
        {
            Heads = heads;
            Content = content;
        }

        /// <summary>
        ///     当是Care的专属协议时的主命令字与子命令字
        /// </summary>
        public (byte, byte) Heads { get; set; }

        /// <summary>
        ///     当是Care的专属协议时的协议主体内容
        /// </summary>
        public byte[] Content { get; set; }
    }
}