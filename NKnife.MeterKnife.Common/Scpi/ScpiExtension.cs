using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Scpi
{
    public static class ScpiExtension
    {
        /// <summary>
        /// 生成协议字节数组
        /// </summary>
        /// <param name="scpi">指令</param>
        /// <param name="gpibAddress">协议体里的GPIB地址</param>
        /// <returns>协议字节数组</returns>
        public static byte[] GenerateCareProtocol(this Scpi scpi, int gpibAddress)
        {
            const byte MAIN_COMMAND = 0xAA;
            const byte SUB_COMMAND = 0x00;

            byte[] scpiBytes = Encoding.ASCII.GetBytes(scpi.Command);

            var bs = new byte[] { 0x08, (byte)gpibAddress, (byte)(scpiBytes.Length + 2), MAIN_COMMAND, SUB_COMMAND };
            var result = new byte[bs.Length + scpiBytes.Length];
            Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            Buffer.BlockCopy(scpiBytes, 0, result, bs.Length, scpiBytes.Length);
            return result;
        }


        public static byte[] GenerateCareProtocol(this CareCommand command)
        {
            var head = new byte[] { 0x08, 0x00, 0x02, command.Heads.Item1, command.Heads.Item2 };
            var newbs = UtilCollection.MergerArray(head, command.Content);
            newbs[2] = (byte)(newbs.Length - 3);
            return newbs;
        }

    }
}
