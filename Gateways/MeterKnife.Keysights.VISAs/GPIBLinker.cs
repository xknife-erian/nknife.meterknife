using System;
using System.Threading;

namespace MeterKnife.Keysights.VISAs
{
    /// <summary>
    /// 对Keysight(Agilent)的GPIB控制组件的调用封装。该组件比较大的问题是仅支持.net2.0。
    /// </summary>
    public class GpibLinker
    {
        
        /**
         * Keysight IO 程序库套件 17.2 是新一代仪器控制软件。此版本提供更出色的用户体验，以及跨越仪器平台的其他改进。
         * 利用更丰富的功能特性，连接变得空前容易。立即下载最新版本！
         * http://www.keysight.com/main/software.jspx?cc=CN&lc=chi&ckey=2175637&nid=-33330.977662&id=2175637
         */
        /*
        private readonly Ivi.Visa.Interop.IFormattedIO488 _gpib;
        private readonly Ivi.Visa.Interop.ResourceManager _messageController;

        /// <summary>
        /// 目标设备的GPIB总线地址
        /// </summary>
        private ushort _targetInstrument = 0;

        public GpibLinker(Action<GpibLog> loggerAction, ushort gpibSelector)
        {
            LoggerAction = loggerAction;
            GpibSelector = gpibSelector;

            _gpib = new Ivi.Visa.Interop.FormattedIO488Class();
            _messageController = new Ivi.Visa.Interop.ResourceManager();
        }
        */
        public ushort GpibSelector { get; set; }
        public string Option { get; set; } = string.Empty;
        public int OpenTimeout { get; set; } = 2000;
        public Action<GpibLog> LoggerAction { get; set; }

        public string Idn(ushort address)
        {
            Write(address, "*CLS");
            return WriteAndRead(address, "*IDN?");
        }

        public void Write(ushort address, string scpiCommand, bool flushAndEnd = true)
        {
            try
            {
                //_gpib.WriteString(scpiCommand, flushAndEnd);
            }
            catch (Exception e)
            {
                LoggerAction.Invoke(new GpibLog(GpibLogLevel.Error, $"ERROR:{e.Message}"));
            }
        }

        public string WriteAndRead(ushort address, string scpiCommand, bool flushAndEnd = true)
        {
            /*
            if (address != _targetInstrument)
            {
                OpenTargetInstrument(address);
                _targetInstrument = address;
            }
            try
            {
                _gpib.WriteString(scpiCommand, flushAndEnd);
            }
            catch (Exception e)
            {
                LoggerAction.Invoke(new GpibLog(GpibLogLevel.Error, $"WRITE-ERROR:{e.Message}"));
                return "";
            }
            try
            {
                return _gpib.ReadString();
            }
            catch (Exception e)
            {
                LoggerAction.Invoke(new GpibLog(GpibLogLevel.Error, $"READ-ERROR:{e.Message}"));
                return "";
            }
            */
            return "";
        }

        private void OpenTargetInstrument(ushort address)
        {
            /*
            var command = GetOpenCommand(address);
            _gpib.IO = (Ivi.Visa.Interop.IMessage)_messageController.Open
            (
                command,
                Ivi.Visa.Interop.AccessMode.NO_LOCK,
                OpenTimeout,
                Option
            );
            LoggerAction.Invoke(new GpibLog(GpibLogLevel.Trace, $"command: {command}"));
            */
        }

        private string GetOpenCommand(ushort address)
        {
            return $"GPIB{GpibSelector}::{address}::INSTR";
        }
    }
}