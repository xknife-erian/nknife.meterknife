using System;
using System.Collections.Generic;
using MeterKnife.Util.Tunnel.Generic;

namespace MeterKnife.Util.Serial.Pan.ProtocolTools
{
    public class PanFixByteHeadTailDatagramDecoder : BytesDatagramDecoder
    {
        public byte Head { get; set; }
        public byte Tail { get; set; }

        public PanFixByteHeadTailDatagramDecoder()
        {
            Head = 0xA0;
            Tail = 0xFF;
        }
        public override byte[][] Execute(byte[] data, out int finishedIndex)
        {
            var results = new List<byte[]>();
            finishedIndex = 0;
            var tempDataGram = new List<byte>();
            bool enableNewDataGram = false;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Head) //遇到头字节
                {
                    tempDataGram.Clear();
                    enableNewDataGram = true;
                }
                else if (data[i] == Tail) //遇到尾字节
                {
                    enableNewDataGram = false;
                    finishedIndex = i;
                    byte[] item;
                    if (VerifyDataGram(tempDataGram, out item))
                    {
                        results.Add(item);
                    }
                }
                else
                {
                    if (enableNewDataGram)
                    {
                        tempDataGram.Add(data[i]);
                    }
                }
            }

            return results.ToArray();
        }

        private bool VerifyDataGram(List<byte> tempDataGram, out byte[] tempData)
        {
            int len = tempDataGram.Count;
            var source = tempDataGram.ToArray();
            if (!VerifyLenAndChk(source))
            {
                tempData = new byte[] {};
                return false;
            }
            tempData = new byte[len - 2];
            Array.Copy(source,1,tempData,0,len-2); //去掉第一个字节的长度和最后一个字节的校验和
            return true;
        }

        private bool VerifyLenAndChk(byte[] source)
        {
            int len = source.Length;
            if (len < 2)
                return false;
            byte lenByte = source[0];
            byte chk = source[len - 1];

            if (lenByte != len - 2) //长度不正确
                return false;
            int sum = lenByte;
            for (int i = 0; i < len - 2; i++)
            {
                sum += source[i + 1];
            }
            if(chk != (sum % 255 % 100)) //校验和不正确
                return false;
            return true;
        }
    }
}
