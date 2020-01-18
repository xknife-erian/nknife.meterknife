using System.IO.Ports;

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
        /// ��������ģ��Ϊͬ��ģʽʱ(һ��һ��)���ȴ��ش�ĳ�ʱʱ�䡣
        /// </summary>
        public int SyncModelWaitTimeout { get; set; }
        public int ReceivedBytesThreshold { get; set; }
        public int ReadBufferSize { get; set; }
        public bool DtrEnable { get; set; }
        public Parity Parity { get; set; }
        public bool RtsEnable { get; set; }
    }
}