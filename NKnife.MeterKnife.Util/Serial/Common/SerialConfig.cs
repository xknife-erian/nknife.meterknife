using System.IO.Ports;
using System.Text;

namespace NKnife.MeterKnife.Util.Serial.Common
{
    public class SerialConfig
    {
        public SerialConfig()
        {
            BaudRate = 9600;
            DataBits = 8;
            ReadTimeout = 1000;
            SyncModelWaitTimeout = 100;
            ReceivedBytesThreshold = 1;
            ReadBufferSize = 64;
            DtrEnable = false;
            Parity = Parity.None;
            RtsEnable = false;
        }

        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public int ReadTimeout { get; set; }
        /// <summary>
        /// 同步模式下等待回复的时长
        /// </summary>
        public int SyncModelWaitTimeout { get; set; }
        public int ReceivedBytesThreshold { get; set; }
        public int ReadBufferSize { get; set; }
        public bool DtrEnable { get; set; }
        public Parity Parity { get; set; }
        public bool RtsEnable { get; set; }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SerialConfig:");
            sb.Append("\tBaudRate").AppendLine($"{BaudRate}");
            sb.Append("\tDataBits").AppendLine($"{DataBits}");
            sb.Append("\tReadTimeout").AppendLine($"{ReadTimeout}");
            sb.Append("\tReadBufferSize").AppendLine($"{ReadBufferSize}");
            sb.Append("\tDtrEnable").AppendLine($"{DtrEnable}");
            sb.Append("\tParity").AppendLine($"{Parity}");
            sb.Append("\tRtsEnable").AppendLine($"{RtsEnable}");
            sb.Append("\tSyncModelWaitTimeout").AppendLine($"{SyncModelWaitTimeout}");
            sb.Append("\tReceivedBytesThreshold").AppendLine($"{ReceivedBytesThreshold}");
            return base.ToString();
        }

        #endregion
    }
}