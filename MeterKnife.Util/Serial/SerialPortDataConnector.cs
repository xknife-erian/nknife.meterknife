using System;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Util.Tunnel.Common;
using NKnife.MeterKnife.Util.Tunnel.Events;

namespace NKnife.MeterKnife.Util.Serial
{
    public class SerialPortDataConnector : ISerialConnector
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ISerialPortHold _serial;

        public SerialPortDataConnector(ISerialPortHold serial)
        {
            _serial = serial;//非单例注入
            IsInitialized = false;
        }

        #region IKnifeSerialConnector

        public bool IsInitialized { get; set; }

        #region event

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;
        public event EventHandler<SessionEventArgs> DataSent;

        protected virtual void OnSessionBuilt(SessionEventArgs e)
        {
            var handler = SessionBuilt;
            handler?.Invoke(this, e);
        }

        protected virtual void OnSessionBuilt()
        {
            OnSessionBuilt(new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber
            }));
        }

        protected virtual void OnSessionBroken(SessionEventArgs e)
        {
            var handler = SessionBroken;
            handler?.Invoke(this, e);
        }

        protected virtual void OnSessionBroken()
        {
            OnSessionBroken(new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber
            }));
        }

        protected virtual void OnDataReceived(SessionEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }

        protected virtual void OnDataReceived(string relation, byte[] source, byte[] data)
        {
            var e = new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber,
                Source = source,
                Data = data,
                Relation = relation
            });
            OnDataReceived(e);
        }

        protected virtual void OnDataSent(SessionEventArgs e)
        {
            DataSent?.Invoke(this, e);
        }

        protected virtual void OnDataSent(byte[] data)
        {
            var e = new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber,
                Data = data
            });
            OnDataSent(e);
        }

        #endregion

        public int PortNumber { get; set; }

        public SerialConfig SerialConfig { get; set; }

        public void Send(long id, byte[] data, string relation)
        {
            if (_serial == null)
                return;
            _serial.SendReceived(data, out var received);
            OnDataSent(data);
            if (received != null)
                OnDataReceived(relation, data, received);
        }

        public void SendAll(byte[] data, string relation)
        {
            if (_serial == null)
                return;
            _serial.SendReceived(data, out var received);
            OnDataSent(data); //激发发送协议完成事件
            if (received != null)
                OnDataReceived(relation, data, received); //激发接收到数据的事件
        }

        public void KillSession(long id)
        {
            if (_serial.IsOpen) _serial.Close();
        }

        public bool Stop()
        {
            if (!_serial.IsOpen) 
                return true;
            var result = _serial.Close();
            if (result) OnSessionBroken();
            IsInitialized = false;
            return result;
        }

        public bool Start()
        {
            if (_serial.IsOpen) 
                return true;
            var port = $"COM{PortNumber}";
            if (SerialConfig == null)
                SerialConfig = new SerialConfig();
            var result = _serial.Initialize(port, SerialConfig);
            if (result)
            {
                _Logger.Info($"串口{port}初始化完成：{true}");
                OnSessionBuilt();
            }
            else
            {
                _Logger.Warn($"串口{port}初始化完成：{false}");
            }

            IsInitialized = true;
            return result;
        }

        #endregion
    }
}