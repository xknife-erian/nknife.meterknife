using MeterKnife.Common.Tunnels;
using NKnife.Base;
using NKnife.Utility;
using ScpiKnife;

namespace MeterKnife.Common.Util
{
    /// <summary>
    ///     面向Command的帮助方法
    /// </summary>
    public static class CommandUtil
    {
        public static ScpiCommand BuildScpiCommand(string scpi, bool isReturn = true)
        {
            var careTalking = new ScpiCommand
            {
                IsReturn = isReturn,
                Command = scpi,
            };
            return careTalking;
        }

        // ReSharper disable once InconsistentNaming
        public static ScpiCommand IDN(int gpib)
        {
            return BuildScpiCommand("*IDN?");
        }

        // ReSharper disable once InconsistentNaming
        public static ScpiCommand READ(int gpib)
        {
            return BuildScpiCommand("READ?");
        }

        // ReSharper disable once InconsistentNaming
        public static ScpiCommand FETC(int gpib)
        {
            return BuildScpiCommand("FETC?");
        }

        public static byte[] GenerateProtocol(CommandQueue.CareItem item)
        {
            var head = new byte[] { 0x08, 0x00, 0x02, item.Heads.First, item.Heads.Second };
            var newbs = UtilityCollection.MergerArray(head, item.Content);
            newbs[2] = (byte)(newbs.Length - 3);
            return newbs;
        }

        /// <summary>
        ///     查询温度
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static CommandQueue.CareItem TEMP()
        {
            var item = new CommandQueue.CareItem
            {
                IsCare = true,
                GpibAddress = 0,
                Heads = Pair<byte, byte>.Build(0xAE, 0x00)
            };
            return item;
        }

        /// <summary>
        ///     查询Care的参数
        /// </summary>
        /// <param name="subcommand">子命令</param>
        public static CommandQueue.CareItem CareGetter(byte subcommand = 0xD1)
        {
            var item = new CommandQueue.CareItem
            {
                IsCare = true,
                GpibAddress = 0,
                Heads = Pair<byte, byte>.Build(0xA0, subcommand)
            };
            return item;
        }

        /// <summary>
        ///     设置Care的参数
        /// </summary>
        /// <param name="subcommand">子命令</param>
        /// <param name="content">设置的参数内容</param>
        public static CommandQueue.CareItem CareSetter(byte subcommand, params byte[] content)
        {
            var item = new CommandQueue.CareItem
            {
                IsCare = true,
                GpibAddress = 0,
                Heads = Pair<byte, byte>.Build(0xB0, subcommand),
                Content = content
            };
            return item;
        }

        /// <summary>
        ///     重新启动Care
        /// </summary>
        public static CommandQueue.CareItem CareReset()
        {
            var item = new CommandQueue.CareItem
            {
                IsCare = true,
                GpibAddress = 0,
                Heads = Pair<byte, byte>.Build(0xB1, 0x00),
            };
            return item;
        }

        /// <summary>
        ///     恢复Care的默认参数
        /// </summary>
        public static CommandQueue.CareItem CareRestoreDefault()
        {
            var item = new CommandQueue.CareItem
            {
                IsCare = true,
                GpibAddress = 0,
                Heads = Pair<byte, byte>.Build(0xB0, 0xD8),
            };
            return item;
        }
    }
}