using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Protocol.Generic;
using NKnife.ShareResources;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Tunnels.CareOne
{
    public class CareOneProtocolUnPacker : BytesProtocolUnPacker
    {
        /// <summary>
        /// 科学计数法正则
        /// </summary>
        private static readonly Regex _ScientificNotationRegex = new Regex(RegexString.RegexStr_ScientificNotation, RegexOptions.IgnoreCase);

        public override void Execute(BytesProtocol protocol, byte[] data, byte[] command)
        {
            var careSaying = protocol as CareTalking;
            if (careSaying == null)
            {
                Debug.Assert(careSaying == null, "协议不应为Null");
            }
            Execute(careSaying, data, command);
        }

        protected virtual void Execute(CareTalking talking, byte[] data, byte[] command)
        {
            ((IProtocol<byte[]>) talking).Command = command;
            talking.GpibAddress = UtilConvert.ConvertTo<short>(data[1]);
            //_logger.Trace(string.Format("UnPack:{0}", data.ToHexString()));

            var contentBytes = new byte[data.Length - 5];
            Buffer.BlockCopy(data, 5, contentBytes, 0, data.Length - 5);

            //一般回复确认信息，无直接内容
            if (contentBytes.Length == 1)
            {
                talking.ScpiBytes = contentBytes;
                talking.Scpi = ((int)contentBytes[0]).ToString();
                return;
            }

            //+1.00355300E-01
            string value = Encoding.ASCII.GetString(contentBytes).TrimEnd('\n');

            MatchCollection mac = _ScientificNotationRegex.Matches(value);
            if (mac.Count > 0)
            {
                string firstMatch = mac[0].Groups[0].Value;
                if (double.TryParse(firstMatch, out var exponent))
                {
                    value = exponent.ToString(CultureInfo.InvariantCulture);
                }
            }
            talking.ScpiBytes = contentBytes;
            talking.Scpi = value;
        }
    }
}