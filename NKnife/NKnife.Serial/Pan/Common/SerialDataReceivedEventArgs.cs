using System;

namespace SerialKnife.Pan.Common
{
    public class SerialDataReceivedEventArgs : EventArgs
    {
        public SerialDataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; set; }
    }
}