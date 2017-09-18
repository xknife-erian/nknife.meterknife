using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeterKnife.ConsoleDemo
{
    class Program
    {
        static bool isRun = true;
        static void Main(string[] args)
        {
            while (isRun)
            {
                PrintHelpInfomation();
                var line = Console.ReadLine();
                switch (line)
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
                }
            }
        }

        private static void PrintHelpInfomation()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("选择Demo项目：");
            Console.WriteLine("1.CareOneSerialChannel");
            Console.WriteLine("2.KeysightChannel");
        }
    }
}
