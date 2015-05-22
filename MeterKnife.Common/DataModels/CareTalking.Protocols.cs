using System.Text;
using NKnife.Utility;
using NPOI.HPSF;

namespace MeterKnife.Common.DataModels
{
    public partial class CareTalking
    {
        private static CareTalking _temp;

        public static CareTalking BuildCareSaying(int gpib, string scpi, bool isReturn = true)
        {
            var careTalking = new CareTalking();
            if (isReturn)
                careTalking.MainCommand = 0xAA;
            else
                careTalking.MainCommand = 0xAB;
            careTalking.SubCommand = 0x00;
            careTalking.Scpi = scpi;
            careTalking.ScpiBytes = Encoding.ASCII.GetBytes(scpi);
            careTalking.GpibAddress = (byte) gpib;
            return careTalking;
        }

        public static CareTalking IDN(int gpib)
        {
            return BuildCareSaying(gpib, "*IDN?");
        }

        public static CareTalking READ(int gpib)
        {
            return BuildCareSaying(gpib, "READ?");
        }

        public static CareTalking TEMP()
        {
            if (_temp == null)
            {
                _temp = BuildCareSaying(0, string.Empty);
                _temp.MainCommand = 0xAE;
            }
            return _temp;
        }

        public static byte[] CareGetter(byte subcommand = 0xD1)
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xA0, subcommand };
        }

        public static byte[] CareSetter(byte subcommand, byte[] content)
        {
            var head = new byte[] { 0x08, 0x00, 0x02, 0xB0, subcommand};
            var newbs = UtilityCollection.MergerArray(head, content);
            newbs[2] = (byte)(newbs.Length - 3);
            return newbs;
        }

        public static byte[] CareReset()
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xB1, 0x00 };
        }
    }
}