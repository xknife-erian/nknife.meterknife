namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    ///     Care组装SCPI指令的帮助方法
    /// </summary>
    public static class CareCommandHelper
    {
        /// <summary>
        ///     查询温度
        /// </summary>
        public static ScpiCommand Temperature(ushort index, int interval = 100)
        {
            var item = new ScpiCommand
            {
                GpibAddress = 0,
                Interval = interval,
                Tag = new CareCommand((0xAE, (byte) index))
            };
            return item;
        }

        /// <summary>
        ///     查询Care的参数
        /// </summary>
        /// <param name="subCommand">子命令</param>
        public static ScpiCommand CareGetter(byte subCommand = 0xD1)
        {
            var item = new ScpiCommand
            {
                GpibAddress = 0,
                Tag = new CareCommand((0xA0, subCommand))
            };
            return item;
        }

        /// <summary>
        ///     设置Care的参数
        /// </summary>
        /// <param name="subCommand">子命令</param>
        /// <param name="content">设置的参数内容</param>
        public static ScpiCommand CareSetter(byte subCommand, params byte[] content)
        {
            var item = new ScpiCommand
            {
                GpibAddress = 0,
                Tag = new CareCommand((0xB0, subCommand), content)
            };
            return item;
        }

        /// <summary>
        ///     重新启动Care
        /// </summary>
        public static ScpiCommand CareReset()
        {
            var item = new ScpiCommand
            {
                GpibAddress = 0,
                Tag = new CareCommand((0xB1, 0x00))
            };
            return item;
        }

        /// <summary>
        ///     恢复Care的默认参数
        /// </summary>
        public static ScpiCommand CareRestoreDefault()
        {
            var item = new ScpiCommand
            {
                GpibAddress = 0,
                Tag = new CareCommand((0xB0, 0xD8))
            };
            return item;
        }
    }
}