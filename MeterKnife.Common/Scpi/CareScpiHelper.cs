using System;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    ///     Care组装SCPI指令的帮助方法
    /// </summary>
    public static class CareScpiHelper
    {
        public static Scpi BuildScpiCommand(string scpi, bool isReturn = true)
        {
            var careTalking = new Scpi
            {
                IsReturn = isReturn,
                Command = scpi,
            };
            return careTalking;
        }

        // ReSharper disable once InconsistentNaming
        public static Scpi IDN(int gpib)
        {
            return BuildScpiCommand("*IDN?");
        }

        // ReSharper disable once InconsistentNaming
        public static Scpi READ(int gpib)
        {
            return BuildScpiCommand("READ?");
        }

        // ReSharper disable once InconsistentNaming
        public static Scpi FETC(int gpib)
        {
            return BuildScpiCommand("FETC?");
        }

        public static byte[] GenerateProtocol(CareCommand careCommand)
        {
            var head = new byte[] { 0x08, 0x00, 0x02, careCommand.Heads.Item1, careCommand.Heads.Item2 };
            var newbs = UtilCollection.MergerArray(head, careCommand.Content);
            newbs[2] = (byte)(newbs.Length - 3);
            return newbs;
        }

        /// <summary>
        ///     查询温度
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static CareCommand TEMP(ushort index, int interval = 100)
        {
            var item = new CareCommand
            {
                IsCare = true,
                GpibAddress = 0,
                Interval = interval,
                Heads = new Tuple<byte, byte>(0xAE, (byte)index)
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
                IsCare = true,
                GpibAddress = 0,
                Heads = new Tuple<byte, byte>(0xA0, subCommand)
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
                IsCare = true,
                GpibAddress = 0,
                Heads = new Tuple<byte, byte>(0xB0, subCommand),
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
                IsCare = true,
                GpibAddress = 0,
                Heads = new Tuple<byte, byte>(0xB1, 0x00),
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
                IsCare = true,
                GpibAddress = 0,
                Heads = new Tuple<byte, byte>(0xB0, 0xD8),
            };
            return item;
        }
    }
}