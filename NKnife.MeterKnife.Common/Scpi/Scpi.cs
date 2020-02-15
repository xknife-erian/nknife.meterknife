using System;
using System.Text;
using System.Xml;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    /// 针对SCPI标准命令的封装。
    /// SCPI，可编程仪器标准命令，是一种建立在现有标准IEEE488.1 和 IEEE 488.2 基础上，
    /// 并遵循了IEEE754 标准中浮点运算规则、ISO646 信息交换7位编码符号（相当于Ascii编
    /// 程）等多种标准的标准化仪器编程语言。
    /// </summary>
    public class Scpi
    {
        public Scpi()
        {
            IsHex = false;
        }

        /// <summary>
        /// 命令主体
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 命令主体是用原生字符串表达,还是16进制字符串表达
        /// </summary>
        public bool IsHex { get; set; }

        /// <summary>
        /// 命令的解释
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 生成协议字节数组
        /// </summary>
        /// <param name="gpibAddress">协议体里的GPIB地址</param>
        /// <returns>协议字节数组</returns>
        public virtual byte[] GenerateCareProtocol(int gpibAddress)
        {
            const byte MAIN_COMMAND = 0xAA;
            const byte SUB_COMMAND = 0x00;

            byte[] scpiBytes = IsHex ? UtilByte.ConvertToBytes(Command) : Encoding.ASCII.GetBytes(Command);

            var bs = new byte[] {0x08, (byte) gpibAddress, (byte) (scpiBytes.Length + 2), MAIN_COMMAND, SUB_COMMAND};
            var result = new byte[bs.Length + scpiBytes.Length];
            Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            Buffer.BlockCopy(scpiBytes, 0, result, bs.Length, scpiBytes.Length);
            return result;
        }

        public override string ToString()
        {
            return $"{Command}/{Description}";
        }

    }
}