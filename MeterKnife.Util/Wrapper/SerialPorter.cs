using System;
using System.IO.Ports;
using System.Text;
using Common.Logging;

namespace MeterKnife.Util.Wrapper
{
    public class SerialPorter
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialPorter>();
        private readonly SerialPort _SerialPort = new SerialPort();

        public bool IsOpen
        {
            get { return (_SerialPort != null && _SerialPort.IsOpen); }
        }

        public bool Initialize(ushort port)
        {
            _SerialPort.PortName = string.Format("COM{0}", port);
            _SerialPort.BaudRate = 9600;
            _SerialPort.DataBits = 8;
            _SerialPort.ReadTimeout = 1000;
            _SerialPort.WriteBufferSize = 2048;
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
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("串口初始化异常.{0}", _SerialPort.PortName), e);
                return false;
            }
        }

        public bool ClosePort()
        {
            try
            {
                if (_SerialPort != null && _SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     发送十六进制ACSII码
        /// </summary>
        /// <param name="hexSrc">(源数据)字符串的Hex表示</param>
        public int WriteToSerial(string hexSrc)
        {
            return WriteToSerial(HexConvertToByte(hexSrc));
        }

        /// <summary>
        ///     发送十六进制ACSII码的字节表示方式
        /// </summary>
        /// <param name="byteArraySrc">(源数据)字符串的Hex表示</param>
        public int WriteToSerial(byte[] byteArraySrc)
        {
            try
            {
                _SerialPort.Write(byteArraySrc, 0, byteArraySrc.Length);
                return 0;
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("向串口写数据异常。{0}", Encoding.Default.GetString(byteArraySrc)), e);
                return 1;
            }
        }

        /// <summary>
        ///     从串口读取数据到指定的字节数组中
        /// </summary>
        /// <param name="outcmd">指定的字节数组引用</param>
        /// <returns>为>=0时表示读取成功，且有数据，数据字节数被返回；为小于0时表示读取失败，或无数据</returns>
        public int Read(ref byte[] outcmd)
        {
            try
            {
                var result = _SerialPort.Read(outcmd, 0, outcmd.Length);
                return result;
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("从串口读数据异常。"), e);
                return -1;
            }
        }

        /// <summary>
        ///     将字符串的Hex表示转换成字节数组
        /// </summary>
        /// <param name="hexSrc">(源数据)字符串的Hex表示</param>
        /// <returns></returns>
        public static byte[] HexConvertToByte(string hexSrc)
        {
            var hex = hexSrc.Replace(" ", "");
            var byteArray = new byte[hex.Length/2];
            for (int i = 0, j = 0; i < hex.Length; i = i + 2, j++)
                byteArray[j] = Convert.ToByte(hex.Substring(i, 2), 16);
            return byteArray;
        }

        /// <summary>
        ///     将字符串转换成Hex的字符串表示
        /// </summary>
        /// <param name="sourceStr">(源数据)字符串</param>
        /// <returns></returns>
        public static string StringConvertToHex(string sourceStr)
        {
            var b = Encoding.Default.GetBytes(sourceStr.ToCharArray());
            var sb = new StringBuilder();
            foreach (var cb in b)
            {
                sb.Append(Convert.ToString(cb, 16)).Append(" ");
            }
            return sb.ToString().TrimEnd(' ');
        }

    }
}