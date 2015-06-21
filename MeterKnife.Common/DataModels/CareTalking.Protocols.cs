using System.Text;
using NKnife.Utility;
using NPOI.HPSF;

namespace MeterKnife.Common.DataModels
{
    public partial class CareTalking
    {
        private static CareTalking _temp;

        public static CareTalking BuildCareTalking(int gpib, string scpi, bool isReturn = true)
        {
            var careTalking = new CareTalking
            {
                MainCommand = isReturn ? (byte) 0xAA : (byte) 0xAB, 
                SubCommand = 0x00, 
                Scpi = scpi, 
                ScpiBytes = Encoding.ASCII.GetBytes(scpi), 
                GpibAddress = (byte) gpib
            };
            return careTalking;
        }

        // ReSharper disable once InconsistentNaming
        public static CareTalking IDN(int gpib)
        {
            return BuildCareTalking(gpib, "*IDN?");
        }

        // ReSharper disable once InconsistentNaming
        public static CareTalking READ(int gpib)
        {
            return BuildCareTalking(gpib, "READ?");
        }

        // ReSharper disable once InconsistentNaming
        public static CareTalking TEMP()
        {
            if (_temp == null)
            {
                _temp = BuildCareTalking(0, string.Empty);
                _temp.MainCommand = 0xAE;
            }
            return _temp;
        }

        public static byte[] CareGetter(byte subcommand = 0xD1)
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xA0, subcommand };
        }

        /// <summary>
        /// Care的参数设置指令
        /// </summary>
        /// <param name="subcommand">子命令</param>
        /// <param name="content">设置的参数内容</param>
        public static byte[] CareSetter(byte subcommand, params byte[] content)
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
        public static byte[] CareRestoreDefault()
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xB0, 0xD8 };
        }
    }
}