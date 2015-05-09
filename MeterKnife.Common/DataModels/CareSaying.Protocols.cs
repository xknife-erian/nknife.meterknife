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
    }
}