using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Util.Tunnel.Generic;

namespace MeterKnife.Util.Serial.Pan.ProtocolTools
{
    public class PanFixByteHeadTailDatagramEncoder : BytesDatagramEncoder
    {

        public byte Head { get; set; }
        public byte Tail { get; set; }

        public PanFixByteHeadTailDatagramEncoder()
        {
            Head = 0xA0;
            Tail = 0xFF;
        }

        /// <summary>
        /// 固定头，尾，长度，校验
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override byte[] Execute(byte[] data)
        {
            var len = (byte)(data.Length%255);
            var chk = GetChk(len,data);
            if (data.Length > 0)
            {
                var result = new byte[data.Length + 4];
                result[0] = Head;
                result[1] = len;
                result[data.Length + 2] = chk;
                result[data.Length + 3] = Tail;
                Array.Copy(data, 0, result, 2, data.Length);
                return result;
            }
            return new byte[] {Head, 0x00, 0x00, Tail};
        }

        private static byte GetChk(byte len, IEnumerable<byte> data)
        {
            int total = len + data.Aggregate(0, (current, t) => current + t);
            return (byte) (total%255%100);
        }
    }
}
