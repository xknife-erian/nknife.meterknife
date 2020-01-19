using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Services;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.Util;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("serial", Description = "连接串口，并尝试发送数据以检查连接性。")]
    public class SerialCliCommand : BaseCommand
    {
        private readonly ISerialPortHold _serialPort;

        public SerialCliCommand(ISerialPortHold serialPort)
        {
            _serialPort = serialPort;
        }

        #region Implementation of ICommand

        public override async Task ExecuteAsync(IConsole console)
        {
            _serialPort.Initialize($"COM{Port}", new SerialConfig {BaudRate = 512000});
            var bs = new List<byte>();
            for (var i = 1234567890; i < 1234567890 + 400; i++)
                bs.AddRange(BitConverter.GetBytes(i));

            _serialPort.SendReceived(bs.ToArray(), out var recv);
            if (!UtilCollection.IsNullOrEmpty(recv))
                console.Output.WriteLine(recv.ToHexString());
            console.Output.WriteLine(bs.ToHexString());
        }

        #endregion
    }
}