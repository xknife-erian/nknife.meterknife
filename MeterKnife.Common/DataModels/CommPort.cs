using System;
using System.Net;
using System.Text;
using MeterKnife.Common.Tunnels;

namespace MeterKnife.Common.DataModels
{
    /// <summary>
    /// 描述一个数据端口，一般是只能打开一次的独占数据端口。比如串口，TCPIP端口等。
    /// </summary>
    public class CommPort
    {
        private readonly int[] _SerialPort = {-1, 115200};
        private string _Id;
        private IPEndPoint _IpEndPoint;
        private string[] _SerialPortInfo;

        protected CommPort()
        {
            _Id = Guid.NewGuid().ToString();
        }

        public TunnelType TunnelType { get; private set; }

        /// <summary>
        /// 获取串口信息
        /// </summary>
        /// <returns>一般由2个值构成，第1个值是串口，第2个值是该串口的波特率</returns>
        public int[] GetSerialPortInfo()
        {
            if (_SerialPort[0] == -1)
            {
                string port = _SerialPortInfo[0].ToUpper().TrimStart(new[] {'C', 'O', 'M'});
                if (!int.TryParse(port, out _SerialPort[0]))
                    _SerialPort[0] = 0;
                if (!int.TryParse(_SerialPortInfo[1], out _SerialPort[1]))
                    _SerialPort[1] = 115200;
            }
            return _SerialPort;
        }

        public IPEndPoint GetIpEndPoint()
        {
            if (_IpEndPoint == null)
            {
                IPAddress ip;
                if (!IPAddress.TryParse(_SerialPortInfo[0], out ip))
                    ip = new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00});
                int port = 0;
                if (!int.TryParse(_SerialPortInfo[1], out port))
                    port = 5025;
                _IpEndPoint = new IPEndPoint(ip, port);
            }
            return _IpEndPoint;
        }

        public static CommPort Build(TunnelType tunnelType, params string[] ports)
        {
            var carePort = new CommPort
            {
                TunnelType = tunnelType,
                _SerialPortInfo = ports
            };
            if (tunnelType == TunnelType.Serial)
            {
                if (!int.TryParse(ports[0], out carePort._SerialPort[0]))
                    carePort._SerialPort[0] = 0;
                if (ports.Length == 1 || !int.TryParse(ports[1], out carePort._SerialPort[1]))
                    carePort._SerialPort[1] = 115200;
                carePort._Id = string.Format("{0}:{1}", carePort._SerialPort[0], carePort._SerialPort[1]);
            }
            else
            {
                IPAddress ip = IPAddress.Parse(ports[0]);
                int port = int.Parse(ports[1]);
                var ipe = new IPEndPoint(ip, port);
                carePort._IpEndPoint = ipe;
                carePort._Id = ipe.ToString();
            }
            return carePort;
        }

        public override string ToString()
        {
            switch (TunnelType)
            {
                case TunnelType.Tcpip:
                    return GetIpEndPoint().ToString();
                case TunnelType.Serial:
                default:
                {
                    int[] ports = GetSerialPortInfo();
                    var sb = new StringBuilder();
                    foreach (int port in ports)
                        sb.Append(port).Append(':');
                    return sb.ToString().TrimEnd(':');
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CommPort)) return false;
            return (Equals(((CommPort) obj)));
        }

        protected bool Equals(CommPort other)
        {
            return _Id.Equals(other._Id) && TunnelType == other.TunnelType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_Id.GetHashCode()*397) ^ (int) TunnelType;
            }
        }
    }
}