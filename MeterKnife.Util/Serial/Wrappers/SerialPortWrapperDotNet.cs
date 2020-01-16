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
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        protected readonly AutoResetEvent _Reset = new AutoResetEvent(false);

        protected int _CurrReadLength;
        protected bool _OnReceive;

        protected string _PortName;
        protected SerialConfig _SerialConfig;

        /// <summary>
        ///     串口操作类（通过.net 类库）
        /// </summary>
        protected SerialPort _SerialPort;

        protected byte[] _SyncBuffer = new byte[512]; //当同步读取时的Buffer
        protected byte _Tail = 0xFF;

        protected int _TimeOut = 150;

        #region ISerialPortWrapper Members

        public byte Tail
        {
            get { return _Tail; }
            set { _Tail = value; }
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
            _PortName = portName;
            _SerialConfig = config;
            _SerialPort = new SerialPort
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

            _SerialPort.DataReceived += SerialPortDataReceived;
            _SerialPort.ErrorReceived += SerialPortErrorReceived;

            try
            {
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                    _SerialPort.Open();
                }
                else
                {
                    _SerialPort.Open();
                }
                IsOpen = _SerialPort.IsOpen;
                if (IsOpen)
                {
                    _logger.Info(string.Format("通讯:成功打开串口:{0}。{1},{2},{3}", portName, _SerialPort.BaudRate,
                        _SerialPort.ReceivedBytesThreshold, _SerialPort.ReadTimeout));
                }
                return _SerialPort.IsOpen;
            }
            catch (Exception e)
            {
                _logger.Warn("无法打开串口:" + portName, e);
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
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                    _logger.Info(string.Format("通讯:成功关闭串口:{0}。", _SerialPort.PortName));
                }
                IsOpen = false;
                return true;
            }
            catch (Exception e)
            {
                _logger.Warn("关闭串口异常:" + _SerialPort.PortName, e);
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
            _SerialPort.ReadTimeout = timeout;
            _TimeOut = timeout;
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
                _CurrReadLength = 0;
                _OnReceive = true;
                _SerialPort.Write(cmd, 0, cmd.Length);
                if (_Reset.WaitOne(_TimeOut))
                {
                    //收到返回
                    recv = new byte[_SyncBuffer.Length];
                    if (recv.Length > 0)
                        Buffer.BlockCopy(_SyncBuffer, 0, recv, 0, _SyncBuffer.Length);
                    _SyncBuffer = new byte[0];
                    return recv.Length;
                }
                //未收到返回，超时
                recv = new byte[0];
                return 0;
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("串口发送与接收时底层异常:{0}", e));
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
            if (!_OnReceive)
            {
                _SerialPort.DiscardInBuffer();
                return;
            }
            try
            {
                var readedBuffer = new byte[_SerialConfig.ReadBufferSize];
                int recvCount = _SerialPort.Read(readedBuffer, 0, readedBuffer.Length);
                _SyncBuffer = new byte[recvCount];
                Buffer.BlockCopy(readedBuffer, 0, _SyncBuffer, 0, recvCount);
                _OnReceive = false;
                _Reset.Set();
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("读取到时,非异常。{0}", ex.Message);
            }
            catch (IOException ex)
            {
                _logger.Warn(string.Format("串口读取异常：{0}", ex.Message), ex);
            }
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _SerialPort.DiscardInBuffer();
        }
    }
}