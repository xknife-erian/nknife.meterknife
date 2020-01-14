using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using NKnife.Wrapper;

namespace MeterKnife.Kernel.Services
{
    public class CareService
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareService>();

        /// <summary>
        ///     在串口中寻找Care
        /// </summary>
        public virtual void SerialFinder(BaseCareCommunicationService careComm)
        {
            var resetEvent = new AutoResetEvent(true);
            StringCollection serialList = PcInterfaces.GetSerialList();
            foreach (string serial in serialList)
            {
                resetEvent.Set();
                string com = serial.ToUpper().TrimStart(new[] {'C', 'O', 'M'});
                int port = 0;
                if (!int.TryParse(com, out port))
                    continue;
                if (port <= 0)
                    continue;
                CommPort carePort = CommPort.Build(TunnelType.Serial, port.ToString());

                bool onFindCare = true;
                var handler = new CareConfigHandler();
                handler.CareConfigging += (s, e) =>
                {
                    if (onFindCare)
                    {
                        if (e.Item.Scpi.ToLower().StartsWith("care"))
                        {
                            careComm.OnSerialInitialized(carePort);
                            careComm.Cares.Add(carePort);
                            resetEvent.Set();
                        }
                    }
                    onFindCare = false;
                };

                careComm.Bind(carePort, handler);
                careComm.Start(carePort);

                _logger.Info(string.Format("串口{0}启动完成,发送寻找Care指令", port));
                careComm.SendCommands(carePort, CommandUtil.CareGetter());
                if (resetEvent.WaitOne(100))
                {
                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    byte[] timebs = Encoding.ASCII.GetBytes(time);
                    _logger.Info(string.Format("Set Care Time:{0}", time));
                    careComm.SendCommands(carePort, CommandUtil.CareSetter(0xD9, timebs));
                    Thread.Sleep(200);
                }
                careComm.Remove(carePort, handler);
            }
        }
    }
}