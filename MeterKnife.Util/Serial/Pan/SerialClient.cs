using System;
using System.Threading;
using Common.Logging;
using MeterKnife.Util.IoC;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Interfaces;
using MeterKnife.Util.Serial.Pan.Common;
using MeterKnife.Util.Serial.Pan.Interfaces;

namespace MeterKnife.Util.Serial.Pan
{
    /// <summary>
    ///     串口通讯器。每个实例绑定一个端口。
    /// </summary>
    internal class SerialClient : ISerialClient
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialClient>();
        private readonly bool _EnableDetialLogger;
        private readonly ManualResetEventSlim _QSendWaitEvent = new ManualResetEventSlim(false);
        private readonly ISerialPortWrapper _SerialComm;
        private ISerialDataPool _DataPool;
        private string _PortName;

        public SerialClient(SerialType serialType = SerialType.WinApi, bool enableDetialLog = false)
        {
            _EnableDetialLogger = enableDetialLog;
            _SerialComm = DI.Get<ISerialPortWrapper>(serialType.ToString());
        }

        /// <summary>
        ///     指令发送的循环线程
        /// </summary>
        private void QuerySendLoop()
        {
            while (Active)
            {
                SendProcess();
                Thread.Sleep(1);
            }
        }

        /// <summary>
        ///     串口数据发送函数
        ///     从数据池中检索数据，没有数据包则直接返回，
        ///     有数据包则先按照数据包中的发送准备时长进行延时等候（PreSleepBeforeSendData），
        ///     然以将数据从串口发出，待数据接收超时后，激发数据包发送完成事件
        /// </summary>
        private void SendProcess()
        {
            try
            {
                PackageBase package;
                int packageType; //packageType 1=单向，2=双向,3=轮询
                if (!_DataPool.TryGetPackage(out package, out packageType))
                {
                    return;
                }

                PreSleepBeforeSendData(package.SendInterval.PreSendInterval);
                SetReadTimeOutAccordingToDevice(packageType == 1
                    ? TimeSpan.FromMilliseconds(10)
                    : package.SendInterval.ReadTimeoutInterval);

                byte[] received;
                var recvCount = _SerialComm.SendReceived(package.DataToSend, out received);

                SendLogger(package);

                SleepAfterReadData(package.SendInterval.AfterReadInterval);

                package.OnPackageSent(PackageSentEventArgs(received, package, recvCount));
            }
            catch (Exception e)
            {
                _logger.Warn("SendProcess异常", e);
            }
        }

        /// <summary>
        ///     打印日志，根据不同的情况
        /// </summary>
        /// <param name="package"></param>
        private void SendLogger(PackageBase package)
        {
            if (_EnableDetialLogger)
            {
                _logger.Trace(string.Format("串发:{0}", package.DataToSend.ToHexString()));
            }
            else if (package.GetType() == typeof (OneWayPackage))
            {
                _logger.Debug(string.Format("串发:{0}, {1},{2}",
                    package.DataToSend.ToHexString(),
                    package.GetType().Name,
                    package.SendInterval));
            }
            else if (package.GetType() == typeof (TwoWayPackage))
            {
                var twowayPackage = (TwoWayPackage) package;
                _logger.Debug(string.Format("串发:{0}, {1},{2},{3}",
                    package.DataToSend.ToHexString(),
                    package.GetType().Name,
                    package.SendInterval,
                    twowayPackage.PackageId));
            }
        }

        /// <summary>
        ///     数据发送前延时
        /// </summary>
        /// <param name="preSendInterval"></param>
        private void PreSleepBeforeSendData(TimeSpan preSendInterval)
        {
            var duration = (int) preSendInterval.TotalMilliseconds;

            if (duration > 0)
            {
                _QSendWaitEvent.Reset();
                //阻塞等待消息
                _QSendWaitEvent.Wait(duration);
            }
        }

        private void SleepAfterReadData(TimeSpan afterReadInterval)
        {
            var duration = (int) afterReadInterval.TotalMilliseconds;

            if (duration > 0)
            {
                _QSendWaitEvent.Reset();
                //阻塞等待消息
                _QSendWaitEvent.Wait(duration);
            }
        }

        private void SetReadTimeOutAccordingToDevice(TimeSpan afterSendInterval)
        {
            var duration = (int) afterSendInterval.TotalMilliseconds;
            _SerialComm.SetTimeOut(duration);
        }

        private static PackageSentEventArgs PackageSentEventArgs(byte[] received, PackageBase package, int recvCount)
        {
            var pk = package as TwoWayPackage;
            var id = pk == null ? 0 : pk.PackageId;
            if (recvCount > 0)
            {
                //if (package.GetType() == typeof (TwoWayPackage))
                //    id = ((TwoWayPackage) package).PackageId;
                return new PackageSentEventArgs(package.Port, true, received, id);
            }
            return new PackageSentEventArgs(package.Port, false, null, id);
        }

        #region ISerialClient

        /// <summary>
        ///     打开端口
        /// </summary>
        /// <param name="port">用序号表达一个串口</param>
        /// <returns></returns>
        public bool OpenPort(ushort port)
        {
            _PortName = string.Format("COM{0}", port);
            if (_SerialComm.Initialize(_PortName, new SerialConfig()))
            {
                Active = true;
                _DataPool = new SerialDataPool();
                _logger.Info(string.Format("通讯串口{0}打开成功", _PortName));

                //一个端口一个线程，专门处理该端口的数据收发
                var queryThread = new Thread(QuerySendLoop) {IsBackground = true};
                queryThread.Start();
                return true;
            }
            Active = false;
            _logger.Info("通讯串口" + _PortName + "打开失败");
            return false;
        }

        /// <summary>
        ///     关闭端口
        /// </summary>
        /// <returns></returns>
        public bool ClosePort()
        {
            try
            {
                if (_SerialComm.IsOpen)
                    _SerialComm.Close();
                Active = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     该串口是否激活
        /// </summary>
        /// <value>
        ///     <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; private set; }

        /// <summary>
        ///     向串口即将发送的指令包的集合
        /// </summary>
        public ISerialDataPool DataPool
        {
            get { return _DataPool ?? (_DataPool = new SerialDataPool()); }
        }

        #endregion
    }
}