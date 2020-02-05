using System;

namespace NKnife.MeterKnife.Common.Tunnels.Handlers
{
    public class CareConfigHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public CareConfigHandler()
        {
            Commands.Add(new byte[] { 0xA0, 0xD0 });
            Commands.Add(new byte[] { 0xA0, 0xD1 });
            Commands.Add(new byte[] { 0xA0, 0xD2 });
            Commands.Add(new byte[] { 0xA0, 0xD3 });
            Commands.Add(new byte[] { 0xA0, 0xD4 });
            Commands.Add(new byte[] { 0xA0, 0xD5 });
            Commands.Add(new byte[] { 0xA0, 0xD6 });
            Commands.Add(new byte[] { 0xA0, 0xD7 });
            Commands.Add(new byte[] { 0xA0, 0xD8 });
            Commands.Add(new byte[] { 0xA0, 0xD9 });
            Commands.Add(new byte[] { 0xA0, 0xDA });
            Commands.Add(new byte[] { 0xA0, 0xDB });
            Commands.Add(new byte[] { 0xA0, 0xDC });
            Commands.Add(new byte[] { 0xA0, 0xDD });
            Commands.Add(new byte[] { 0xA0, 0xDE });
            Commands.Add(new byte[] { 0xA0, 0xDF });
        }

        public override void Received(CareTalking protocol)
        {
            _Logger.Trace(message: $"{protocol.MainCommand.ToHexString()}|{protocol.SubCommand.ToHexString()}:{protocol.Scpi}");
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                
            }
        }

    }
}
