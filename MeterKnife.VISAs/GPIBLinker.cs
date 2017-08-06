using System;
using System.Threading;
using Ivi.Visa.Interop;

namespace MeterKnife.VISAs
{
    public class GPIBLinker
    {
        private IFormattedIO488 _Gpib;

        public GPIBLinker(Action<string> loggerAction, short gpibSelector, short address)
        {
            LoggerAction = loggerAction;
            GpibSelector = gpibSelector;
            Address = address;
        }

        public short GpibSelector { get; set; }
        public short Address { get; set; }
        public string Option { get; set; } = string.Empty;
        public int OpenTimeout { get; set; } = 2000;
        public Action<string> LoggerAction { get; set; }

        public bool Initialize(out string idn)
        {
            string openCommand = $"GPIB{GpibSelector}::{Address}::INSTR";

            try
            {
                // create the formatted IO object
                _Gpib = new FormattedIO488Class();

                //create the resource manager
                var mgr = new ResourceManager();

                _Gpib.IO = (IMessage)mgr.Open(openCommand, AccessMode.NO_LOCK, OpenTimeout, Option);
                LoggerAction.Invoke($"VISA.SET: {openCommand}");

                _Gpib.WriteString("*CLS", true);
                Thread.Sleep(500);

                _Gpib.WriteString("*IDN?", true);
                idn = _Gpib.ReadString();

                return true;
            }
            catch (Exception e)
            {
                LoggerAction.Invoke($"ERROR:{e.Message}");
                idn = string.Empty;
                return false;
            }
        }

        public string Execute(string scpiCommand, uint timeOut, bool flushAndEnd = true)
        {
            _Gpib.WriteString(scpiCommand, flushAndEnd);
            Thread.Sleep((int) timeOut);
            return _Gpib.ReadString();
        }
    }
}