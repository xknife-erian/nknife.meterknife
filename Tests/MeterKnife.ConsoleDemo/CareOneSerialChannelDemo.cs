using System;
using System.Diagnostics;
using MeterKnife.Cares;
using MeterKnife.Models;
using Newtonsoft.Json;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.ConsoleDemo
{
    public class CareOneSerialChannelDemo: DemoBase
    {
        private CareOneSerialChannel _Channel;

        private Device _DemoDevice = new Device("HP", "34401", "HP34401");

        public SerialConfig Config { get; set; }


        public override void Run()
        {
            Debug.Assert(Config != null, "SerialConfig未设置");
            _Channel = new CareOneSerialChannel(Config);
            _Channel.Open();

            Console.Write("请输入仪器地址：");
            var address = Console.ReadLine();
            var group = GetQuestionGroup(ushort.Parse(address));
            _Channel.UpdateQuestionGroup(group);

            Console.ReadLine();
        }

        private SerialQuestionGroup GetQuestionGroup(ushort address)
        {
            _DemoDevice.Address = address;
            var group = new SerialQuestionGroup();
            group.Add(new SerialQuestion(_Channel, _DemoDevice, null, false, new byte[]{}));
            return null;
        }

        public static SerialConfig GetConfig()
        {
            Console.WriteLine("请输入串口信息。");
            Console.Write("串口号：");
            var line = Console.ReadLine();
            var config = new SerialConfig(ushort.Parse(line));
            config.BaudRate = 115200;
            Console.WriteLine(JsonConvert.SerializeObject(config));
            return config;
        }
    }
}