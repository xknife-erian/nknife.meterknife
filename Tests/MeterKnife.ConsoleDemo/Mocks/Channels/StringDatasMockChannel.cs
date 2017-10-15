using MeterKnife.Keysights;
using NKnife.Utility;

namespace MeterKnife.ConsoleDemo.Mocks.Channels
{
    public class StringDatasMockChannel : KeysightChannel
    {
        private readonly UtilityRandom _Random = new UtilityRandom();

        protected override void OpenGPIBLinker()
        {
        }

        protected override string WriteAndRead(int address, string command)
        {
            /*
            job.Instruments.Add(new Instrument("Keysight", "34401", "34401", 22)); //假设测电压
            job.Instruments.Add(new Instrument("Keithley", "2000", "K2000", 23)); //假设测电流
            job.Instruments.Add(new Instrument("Keithley", "2700", "K2700", 24)); //假设2700负责20通道扫描电阻
             */
            switch (address)
            {
                case 0:
                {
                    var tail = _Random.Next(10, 99);
                    return $"25.{tail}";//温度
                }
                case 22:
                {
                    var tail = _Random.Next(100, 999);
                    return $"3.3000{tail}";
                }
                case 23:
                {
                    var tail = _Random.Next(100, 999);
                    return $"500.0{tail}";
                }
                case 24:
                {
                    var tail = _Random.Next(10000, 99999);
                    return $"10.0{tail}";
                }
            }
            return "9999.99";
        }
    }
}