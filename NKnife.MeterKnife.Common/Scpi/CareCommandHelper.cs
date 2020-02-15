using System;
using NKnife.Util;

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
        public static CareCommand Temperature(ushort index, int interval = 100)
        {
            var item = new CareCommand
            {
                GpibAddress = 0,
                Interval = interval,
                Heads = (0xAE, (byte)index)
            };
            return item;
        }

        /// <summary>
        ///     查询Care的参数
        /// </summary>
        /// <param name="subCommand">子命令</param>
        public static CareCommand CareGetter(byte subCommand = 0xD1)
        {
            var item = new CareCommand
            {
                GpibAddress = 0,
                Heads = (0xA0, subCommand)
            };
            return item;
        }

        /// <summary>
        ///     设置Care的参数
        /// </summary>
        /// <param name="subCommand">子命令</param>
        /// <param name="content">设置的参数内容</param>
        public static CareCommand CareSetter(byte subCommand, params byte[] content)
        {
            var item = new CareCommand
            {
                GpibAddress = 0,
                Heads = (0xB0, subCommand),
                Content = content
            };
            return item;
        }

        /// <summary>
        ///     重新启动Care
        /// </summary>
        public static CareCommand CareReset()
        {
            var item = new CareCommand
            {
                GpibAddress = 0,
                Heads = (0xB1, 0x00),
            };
            return item;
        }

        /// <summary>
        ///     恢复Care的默认参数
        /// </summary>
        public static CareCommand CareRestoreDefault()
        {
            var item = new CareCommand
            {
                GpibAddress = 0,
                Heads = (0xB0, 0xD8),
            };
            return item;
        }
    }
}