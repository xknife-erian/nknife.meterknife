using System;
using System.Diagnostics;
using System.Text;
using Common.Logging;
using MeterKnife.Common.DataModels;
using NKnife.Converts;
using NKnife.Protocol;
using NKnife.Protocol.Generic;

namespace MeterKnife.Common.Tunnels.CareOne
{
    public class CareOneProtocolUnPacker : BytesProtocolUnPacker
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareOneProtocolUnPacker>();

        public override void Execute(BytesProtocol protocol, byte[] data, byte[] command)
        {
            var careSaying = protocol as CareSaying;
            if (careSaying == null)
            {
                Debug.Assert(careSaying == null, "协议不应为Null");
            }
            Execute(careSaying, data, command);
        }

        protected virtual void Execute(CareSaying careSaying, byte[] data, byte[] command)
        {
            ((IProtocol<byte[]>) careSaying).Command = command;
            careSaying.GpibAddress = UtilityConvert.ConvertTo<short>(data[1]);
            //_logger.Trace(string.Format("UnPack:{0}", data.ToHexString()));

            //+1.00355300E-01  100mV
            var value = Encoding.ASCII.GetString(data, 5, data.Length - 5).TrimEnd('\n');
            double exponent;
            if (double.TryParse(value, out exponent))
            {
                value = exponent.ToString();
            }
            careSaying.Content = value;
        }

    }
}