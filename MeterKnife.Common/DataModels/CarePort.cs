using System;
using System.Net;
using System.Text;
using MeterKnife.Common.Tunnels;

namespace MeterKnife.Common.DataModels
{
    public class CarePort
    {
        private readonly int[] _SerialPort = {-1, 115200};
        private string _Id;
        private IPEndPoint _IpEndPoint;
        private string[] _Ports;

        protected CarePort()
        {
            _Id = Guid.NewGuid().ToString();
        }

        public TunnelType TunnelType { get; private set; }

        public int[] GetSerialPort()
        {
            if (_SerialPort[0] == -1)
            {
                string port = _Ports[0].ToUpper().TrimStart(new[] {'C', 'O', 'M'});
                if (!int.TryParse(port, out _SerialPort[0]))
                    _SerialPort[0] = 0;
                if (!int.TryParse(_Ports[1], out _SerialPort[1]))
                    _SerialPort[1] = 115200;
            }
            return _SerialPort;
        }

        public IPEndPoint GetIpEndPoint()
        {
            if (_IpEndPoint == null)
            {
                IPAddress ip;
                if (!IPAddress.TryParse(_Ports[0], out ip))
                    ip = new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00});
                int port = 0;
                if (!int.TryParse(_Ports[1], out port))
                    port = 5025;
                _IpEndPoint = new IPEndPoint(ip, port);
            }
            return _IpEndPoint;
        }

        public static CarePort Build(TunnelType tunnelType, params string[] ports)
        {
            var carePort = new CarePort
            {
                TunnelType = tunnelType,
                _Ports = ports
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
                    int[] ports = GetSerialPort();
                    var sb = new StringBuilder();
                    foreach (int port in ports)
                        sb.Append(port).Append(':');
                    return sb.ToString();
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CarePort)) return false;
            return (Equals(((CarePort) obj)));
        }

        protected bool Equals(CarePort other)
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