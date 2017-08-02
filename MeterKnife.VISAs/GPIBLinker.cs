using System;
using System.Threading;
using VisaComLib;

namespace MeterKnife.VISAs
{
    public class GPIBLinker
    {
        private IFormattedIO488 _Gpib;

        public short GpibSelector { get; set; }
        public short Address { get; set; }
        public string Option { get; set; }
        public int OpenTimeout { get; set; }

        public string Initialize()
        {
            string fullAddress = $"GPIB{GpibSelector}::{Address}::INSTR";

            try
            {
                // create the formatted IO object
                _Gpib = new FormattedIO488Class();

                //create the resource manager
                var mgr = new ResourceManager();

                _Gpib.IO = (IMessage)mgr.Open(fullAddress, AccessMode.NO_LOCK, OpenTimeout, Option);

                _Gpib.WriteString("*CLS", true);
                Thread.Sleep(500);

                _Gpib.WriteString("*IDN?", true);
                var value = _Gpib.ReadString();

                return value;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}