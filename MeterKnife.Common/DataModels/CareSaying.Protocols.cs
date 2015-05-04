namespace MeterKnife.Common.DataModels
{
    public partial class CareSaying
    {
        private static CareSaying _idn;
        private static CareSaying _read;
        private static CareSaying _temp;

        public static CareSaying IDN(int gpib)
        {
            return _idn ?? (_idn = BuildCareSaying(gpib, "*IDN?"));
        }

        public static CareSaying READ(int gpib)
        {
            return _read ?? (_read = BuildCareSaying(gpib, "READ?"));
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

        private static CareSaying BuildCareSaying(int gpib, string content)
        {
            var careSaying = new CareSaying();
            careSaying.MainCommand = 0xAA;
            careSaying.SubCommand = 0x00;
            careSaying.Content = content;
            careSaying.GpibAddress = (byte) gpib;
            return careSaying;
        }
    }
}