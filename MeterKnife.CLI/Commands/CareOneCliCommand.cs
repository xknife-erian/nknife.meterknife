using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;
using MeterKnife.Util.Serial;
using MeterKnife.Util.Serial.Common;
using NKnife.Util;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("care", Description = "连接MeterCare，尝试发送数据")]
    public class CareOneCliCommand : ICommand
    {
        private readonly ISerialPortHold _serial;

        public CareOneCliCommand(ISerialPortHold serial)
        {
            _serial = serial;
        }

        [CommandOption("port", 'p', IsRequired = true)]
        public ushort Port { get; set; }

        #region Implementation of ICommand

        public async Task ExecuteAsync(IConsole console)
        {
            var b = (int) SerialHelper.BaudRates[SerialHelper.BaudRates.Length - 2];
            _serial.Initialize($"COM{Port}", new SerialConfig {BaudRate = b});
            var bs = new List<byte>();
            for (var i = 0; i < 1000; i++) 
                bs.AddRange(new byte[] {0xAA, 0x55});

            _serial.SendReceived(bs.ToArray(), out var recv);

            if (!UtilCollection.IsNullOrEmpty(recv))
                console.Output.WriteLine(recv.ToHexString());
        }

        #endregion
    }
}