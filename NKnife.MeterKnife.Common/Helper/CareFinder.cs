namespace NKnife.MeterKnife.Common
{
    public class CareFinder
    {
        /// <summary>
        ///     在串口中寻找Care
        /// </summary>
        public virtual void SerialFinder()
        {
            //TODO:自动寻找Care暂时先搁下
//            var resetEvent = new AutoResetEvent(true);
//            StringCollection serialList = new StringCollection();//PcInterfaces.GetSerialList();
//            foreach (string serial in serialList)
//            {
//                resetEvent.Set();
//                string com = serial.ToUpper().TrimStart(new[] {'C', 'O', 'M'});
//                int port = 0;
//                if (!int.TryParse(com, out port))
//                    continue;
//                if (port <= 0)
//                    continue;
//                Slot carePort = Slot.Set(SlotType.Serial, port.ToString());
//
//                bool onFindCare = true;
//                var handler = new CareConfigHandler();
//                handler.CareSetting += (s, e) =>
//                {
//                    if (onFindCare)
//                    {
//                        if (e.CareCommand.Scpi.ToLower().StartsWith("care"))
//                        {
//                            careComm.OnSerialInitialized(carePort);
//                            careComm.Solts.Add(carePort);
//                            resetEvent.Set();
//                        }
//                    }
//                    onFindCare = false;
//                };
//
//                careComm.Bind(carePort, handler);
//                careComm.StartAsync(carePort);
//
//                _logger.Info(string.Format("串口{0}启动完成,发送寻找Care指令", port));
//                careComm.SendCommands(carePort, CommandUtil.CareGetter());
//                if (resetEvent.WaitOne(100))
//                {
//                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
//                    byte[] timebs = Encoding.ASCII.GetBytes(time);
//                    _logger.Info(string.Format("Set Care Time:{0}", time));
//                    careComm.SendCommands(carePort, CommandUtil.CareSetter(0xD9, timebs));
//                    Thread.Sleep(200);
//                }
//                careComm.Remove(carePort, handler);
//            }
        }
    }
}