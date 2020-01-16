using System;
using System.Collections.Concurrent;
using Common.Logging;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Pan.Common;
using MeterKnife.Util.Serial.Pan.Interfaces;

namespace MeterKnife.Util.Serial.Pan
{
    /// <summary>
    ///     串口通讯管理器
    /// </summary>
    public sealed class SerialClients : ISerialClientManager, IDisposable
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialClients>();

        /// <summary>
        ///     串口管理器字典，以串口号作为键值
        /// </summary>
        private readonly ConcurrentDictionary<ushort, ISerialClient> _SerialMap;

        public SerialClients()
        {
            _SerialMap = new ConcurrentDictionary<ushort, ISerialClient>();
        }

        #region Implementation of IDisposable

        /// <summary>
        ///     执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        void IDisposable.Dispose()
        {
            RemoveAllPorts();
        }

        #endregion

        #region ISerialClientManager Members

        /// <summary>
        ///     添加一个串口
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <param name="serialType"></param>
        /// <param name="enableDetialLog"></param>
        /// <returns></returns>
        public bool AddPort(ushort port, SerialType serialType = SerialType.WinApi, bool enableDetialLog = false)
        {
            if (_SerialMap.ContainsKey(port))
            {
                return _SerialMap[port].Active;
            }
            ISerialClient serial = new SerialClient(serialType, enableDetialLog);
            serial.OpenPort(port);
            _SerialMap.TryAdd(port, serial);
            return true;
        }

        /// <summary>
        ///     关闭一个串口
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <returns></returns>
        public bool RemovePort(ushort port)
        {
            ISerialClient serial;
            if (_SerialMap.TryRemove(port, out serial))
            {
                serial.ClosePort();
            }
            return true;
        }

        /// <summary>
        ///     关闭所有串口，并将所有串口从管理器中移除
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllPorts()
        {
            try
            {
                foreach (SerialClient serialuti in _SerialMap.Values)
                {
                    serialuti.ClosePort();
                }
                _SerialMap.Clear();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Warn("SerialCommunicationManager类UnInitialize异常", ex);
                return false;
            }
        }

        /// <summary>
        ///     向指定的串口写入一个数据包
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <param name="package">包含发送数据，以及相关指令及信息与事件的封装</param>
        public bool AddPackage(ushort port, PackageBase package)
        {
            ISerialClient serialClient;
            if (!_SerialMap.TryGetValue(port, out serialClient)) return false;
            if (!serialClient.Active) return false;
            serialClient.DataPool.AddPackage(package);
            return true;
        }

        #endregion
    }
}