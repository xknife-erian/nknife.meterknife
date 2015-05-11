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
            string value = Encoding.ASCII.GetString(data, 5, data.Length - 5).TrimEnd('\n');

            //DI  +1001.874E-06
            int blank = value.IndexOf(' ');
            if (blank > 0)
                value = value.Substring(blank).Trim();

            double exponent;
            if (double.TryParse(value, out exponent))
            {
                value = exponent.ToString();
            }
            careSaying.Content = value;
        }
    }
}