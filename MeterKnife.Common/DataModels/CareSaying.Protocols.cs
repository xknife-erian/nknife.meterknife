using NKnife.Utility;
using NPOI.HPSF;

namespace MeterKnife.Common.DataModels
{
    public partial class CareSaying
    {
        private static CareSaying _temp;

        public static CareSaying IDN(int gpib)
        {
            return BuildCareSaying(gpib, "*IDN?");
        }

        public static CareSaying READ(int gpib)
        {
            return BuildCareSaying(gpib, "READ?");
        }

        public static CareSaying TEMP()
        {
            if (_temp == null)
            {
                _temp = BuildCareSaying(0, string.Empty);
                _temp.MainCommand = 0xAE;
            }
            return _temp;
        }

        public static CareSaying BuildCareSaying(int gpib, string content, bool isReturn = true)
        {
            var careSaying = new CareSaying();
            if (isReturn)
                careSaying.MainCommand = 0xAA;
            else
                careSaying.MainCommand = 0xAB;
            careSaying.SubCommand = 0x00;
            careSaying.Content = content;
            careSaying.GpibAddress = (byte) gpib;
            return careSaying;
        }

        public static byte[] CareGetter(byte subcommand = 0xD1)
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xA0, subcommand };
        }

        public static byte[] CareSetter(byte subcommand, byte[] content)
        {
            var head = new byte[] { 0x08, 0x00, 0x02, 0xB0, subcommand};
            return UtilityCollection.MergerArray(head, content);
        }

        public static byte[] CareReset()
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xB1, 0x00 };
        }
    }
}