using System;

namespace MeterKnife.ConsoleDemo
{
    public class Program
    {
        private static bool _isRun = true;

        public static void Main(string[] args)
        {
            while (_isRun)
            {
                PrintHelpInfomation();
                var line = Console.ReadLine();
                if (line == null)
                    continue;
                switch (line.ToUpper())
                {
                    case "1":
                        var config = CareOneSerialChannelDemo.GetConfig();
                        var demo = new CareOneSerialChannelDemo();
                        demo.Config = config;
                        demo.Run();
                        break;
                    case "2":
                        new KeysightChannelDemo().Run();
                        break;
                    case "X":
                        _isRun = false;
                        break;
                }
            }
        }

        private static void PrintHelpInfomation()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("选择Demo项目：");
            Console.WriteLine("1. CareOneSerialChannel");
            Console.WriteLine("2. KeysightChannel");
            Console.WriteLine("X. Exit Demo.");
        }
    }
}