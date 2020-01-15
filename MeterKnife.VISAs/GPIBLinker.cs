using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Ivi.Visa;
using Ivi.Visa.FormattedIO;
using Ivi.Visa.Interop; 

namespace MeterKnife.VISAs
{
    public class GPIBLinker
    {
        private IFormattedIO488 _Gpib;

        public short GpibSelector { get; set; }
        public short Address { get; set; }
        public ResourceOpenStatus Option;
        public int OpenTimeout { get; set; }


        public string Initialize()
        {
            string fullAddress = $"GPIB{GpibSelector}::{Address}::INSTR";

            try
            {
                // create the formatted IO object
                _Gpib = new MessageBasedFormattedIO();

                //create the resource manager

                _Gpib.IO = (IMessage)GlobalResourceManager.Open(fullAddress, AccessModes.ExclusiveLock, OpenTimeout, out Option);

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