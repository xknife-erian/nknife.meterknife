using System;
using System.Threading;

namespace MeterKnife.Keysights.VISAs
{
    /// <summary>
    /// 对Keysight(Agilent)的GPIB控制组件的调用封装。该组件比较大的问题是仅支持.net2.0。
    /// </summary>
    public class GPIBLinker
    {
        /**
         * Keysight IO 程序库套件 17.2 是新一代仪器控制软件。此版本提供更出色的用户体验，以及跨越仪器平台的其他改进。
         * 利用更丰富的功能特性，连接变得空前容易。立即下载最新版本！
         * http://www.keysight.com/main/software.jspx?cc=CN&lc=chi&ckey=2175637&nid=-33330.977662&id=2175637
         */

        private readonly Ivi.Visa.Interop.IFormattedIO488 _Gpib;
        private readonly Ivi.Visa.Interop.ResourceManager _MessageController;

        /// <summary>
        /// 目标设备的GPIB总线地址
        /// </summary>
        private ushort _TargetInstrument = 0;

        public GPIBLinker(Action<GPIBLog> loggerAction, ushort gpibSelector)
        {
            LoggerAction = loggerAction;
            GpibSelector = gpibSelector;

            _Gpib = new Ivi.Visa.Interop.FormattedIO488Class();
            _MessageController = new Ivi.Visa.Interop.ResourceManager();
        }

        public ushort GpibSelector { get; set; }
        public string Option { get; set; } = string.Empty;
        public int OpenTimeout { get; set; } = 2000;
        public Action<GPIBLog> LoggerAction { get; set; }

        public string IDN(ushort address)
        {
            Execute(address, "*CLS");
            return Execute(address, "*IDN?", 500);
        }

        public void Execute(ushort address, string scpiCommand, bool flushAndEnd = true)
        {
            try
            {
                _Gpib.WriteString(scpiCommand, flushAndEnd);
            }
            catch (Exception e)
            {
                LoggerAction.Invoke(new GPIBLog(GPIBLogLevel.Error, $"ERROR:{e.Message}"));
            }
        }

        public string Execute(ushort address, string scpiCommand, uint timeOut, bool flushAndEnd = true)
        {
            if (address != _TargetInstrument)
            {
                OpenTargetInstrument(address);
                _TargetInstrument = address;
            }
            try
            {
                _Gpib.WriteString(scpiCommand, flushAndEnd);
                Thread.Sleep((int)timeOut);
                return _Gpib.ReadString();
            }
            catch (Exception e)
            {
                LoggerAction.Invoke(new GPIBLog(GPIBLogLevel.Error, $"ERROR:{e.Message}"));
                return "";
            }
        }

        private void OpenTargetInstrument(ushort address)
        {
            var command = GetOpenCommand(address);
            _Gpib.IO = (Ivi.Visa.Interop.IMessage)_MessageController.Open
            (
                command,
                Ivi.Visa.Interop.AccessMode.NO_LOCK,
                OpenTimeout,
                Option
            );
            LoggerAction.Invoke(new GPIBLog(GPIBLogLevel.Trace, $"command: {command}"));
        }

        private string GetOpenCommand(ushort address)
        {
            return $"GPIB{GpibSelector}::{address}::INSTR";
        }
    }
}