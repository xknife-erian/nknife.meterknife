using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Interfaces;

namespace MeterKnife.Util.Serial.Wrappers
{
    /// <summary>
    ///     通过.net实现的串口操作类
    /// </summary>
    public class SerialPortWrapperDotNet : ISerialPortWrapper
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        protected readonly AutoResetEvent _reset = new AutoResetEvent(false);

        protected int _currReadLength;
        protected bool _onReceive;

        protected string _portName;
        protected SerialConfig _serialConfig;

        /// <summary>
        ///     串口操作类（通过.net 类库）
        /// </summary>
        protected SerialPort _serialPort;

        protected byte[] _syncBuffer = new byte[512]; //当同步读取时的Buffer
        protected byte _tail = 0xFF;

        protected int _timeOut = 150;

        #region ISerialPortWrapper Members

        public byte Tail
        {
            get { return _tail; }
            set { _tail = value; }
        }

        /// <summary>
        ///     串口是否打开标记
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        ///     初始化操作器通讯串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool Initialize(string portName, SerialConfig config)
        {
            _portName = portName;
            _serialConfig = config;
            _serialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = config.BaudRate, //9600,
                DataBits = config.DataBits, //8,
                ReadTimeout = config.ReadTimeout, //150,
                ReceivedBytesThreshold = config.ReceivedBytesThreshold, //1,
                ReadBufferSize = config.ReadBufferSize, //32,
                DtrEnable = config.DtrEnable,
                Parity = config.Parity,
                RtsEnable = config.RtsEnable
            };

            _serialPort.DataReceived += SerialPortDataReceived;
            _serialPort.ErrorReceived += SerialPortErrorReceived;

            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                    _serialPort.Open();
                }
                else
                {
                    _serialPort.Open();
                }
                IsOpen = _serialPort.IsOpen;
                if (IsOpen)
                {
                    _Logger.Info($"通讯:成功打开串口:{portName}。{_serialPort.BaudRate},{_serialPort.ReceivedBytesThreshold},{_serialPort.ReadTimeout}");
                }
                return _serialPort.IsOpen;
            }
            catch (Exception e)
            {
                _Logger.Warn($"无法打开串口:{portName}::{e}");
                IsOpen = false;
                return false;
            }
        }

        /// <summary>
        ///     关闭串口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                    _Logger.Info(string.Format("通讯:成功关闭串口:{0}。", _serialPort.PortName));
                }
                IsOpen = false;
                return true;
            }
            catch (Exception e)
            {
                _Logger.Warn("关闭串口异常:" + _serialPort.PortName, e);
                return false;
            }
        }

        /// <summary>
        ///     设置串口读取超时
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeOut(int timeout)
        {
            if (!IsOpen)
                return;
            _serialPort.ReadTimeout = timeout;
            _timeOut = timeout;
        }

        /// <summary>
        ///     发送数据
        /// </summary>
        /// <param name="cmd">待发送的数据</param>
        /// <param name="recv">回复的数据</param>
        /// <returns>回复的数据的长度</returns>
        public virtual int SendReceived(byte[] cmd, out byte[] recv)
        {
            try
            {
                _currReadLength = 0;
                _onReceive = true;
                _serialPort.Write(cmd, 0, cmd.Length);
                if (_reset.WaitOne(_timeOut))
                {
                    //收到返回
                    recv = new byte[_syncBuffer.Length];
                    if (recv.Length > 0)
                        Buffer.BlockCopy(_syncBuffer, 0, recv, 0, _syncBuffer.Length);
                    _syncBuffer = new byte[0];
                    return recv.Length;
                }
                //未收到返回，超时
                recv = new byte[0];
                return 0;
            }
            catch (Exception e)
            {
                _Logger.Warn($"串口发送与接收时底层异常:{e}");
                recv = new byte[0];
                return 0;
            }
        }

        #endregion

        /// <summary>
        ///     接收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_onReceive)
            {
                _serialPort.DiscardInBuffer();
                return;
            }
            try
            {
                var readedBuffer = new byte[_serialConfig.ReadBufferSize];
                int recvCount = _serialPort.Read(readedBuffer, 0, readedBuffer.Length);
                _syncBuffer = new byte[recvCount];
                Buffer.BlockCopy(readedBuffer, 0, _syncBuffer, 0, recvCount);
                _onReceive = false;
                _reset.Set();
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("读取到时,非异常。{0}", ex.Message);
            }
            catch (IOException ex)
            {
                _Logger.Warn(string.Format("串口读取异常：{0}", ex.Message), ex);
            }
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _serialPort.DiscardInBuffer();
        }
    }
}