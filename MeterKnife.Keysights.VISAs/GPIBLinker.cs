using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MeterKnife.Keysights.VISAs
{
    /// <summary>
    /// 对Keysight(Agilent)的GPIB控制组件的调用封装。该组件比较大的问题是仅支持.net2.0。
    /// </summary>
    public partial class GPIBLinker
    {
        /**
         * Keysight IO 程序库套件 17.2 是新一代仪器控制软件。此版本提供更出色的用户体验，以及跨越仪器平台的其他改进。
         * 利用更丰富的功能特性，连接变得空前容易。立即下载最新版本！
         * http://www.keysight.com/main/software.jspx?cc=CN&lc=chi&ckey=2175637&nid=-33330.977662&id=2175637
         */

        private Ivi.Visa.Interop.IFormattedIO488 _Gpib;

        public GPIBLinker(Action<GPIBLog> loggerAction, short gpibSelector, short address)
        {
            LoggerAction = loggerAction;
            GpibSelector = gpibSelector;
            Address = address;
        }

        public short GpibSelector { get; set; }
        public short Address { get; set; }
        public string Option { get; set; } = string.Empty;
        public int OpenTimeout { get; set; } = 2000;
        public Action<GPIBLog> LoggerAction { get; set; }

        public bool Initialize(out string idn)
        {
            string openCommand = $"GPIB{GpibSelector}::{Address}::INSTR";

            try
            {
                // create the formatted IO object
                _Gpib = new Ivi.Visa.Interop.FormattedIO488Class();

                //create the resource manager
                var resourceManager = new Ivi.Visa.Interop.ResourceManager();

                _Gpib.IO = (Ivi.Visa.Interop.IMessage) resourceManager.Open
                (
                    openCommand,
                    Ivi.Visa.Interop.AccessMode.NO_LOCK,
                    OpenTimeout,
                    Option
                );
                LoggerAction.Invoke(new GPIBLog(GPIBLogLevel.Trace, $"command: {openCommand}"));

                _Gpib.WriteString("*CLS", true);
                Thread.Sleep(500);

                _Gpib.WriteString("*IDN?", true);
                idn = _Gpib.ReadString();

                return true;
            }
            catch (Exception e)
            {
                LoggerAction.Invoke(new GPIBLog(GPIBLogLevel.Error, $"ERROR:{e.Message}"));
                idn = string.Empty;
                return false;
            }
        }

        public string Execute(string scpiCommand, uint timeOut, bool flushAndEnd = true)
        {
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
    }
}