using System;
using System.Net;
using System.Text;
using NKnife.MeterKnife.Common.Tunnels;

namespace NKnife.MeterKnife.Common.DataModels
{
    /// <summary>
    ///     描述一个数据端口，一般是只能打开一次的独占数据端口。比如串口，TCPIP端口等。
    /// </summary>
    public sealed class Slot
    {
        private readonly string _id;
        private readonly int[] _serialPort = {-1, 115200};
        private IPEndPoint _ipEndPoint;
        private string[] _serialPortInfo;

        private Slot()
        {
            _id = Guid.NewGuid().ToString();
        }

        public TunnelType TunnelType { get; set; }

        /// <summary>
        ///     获取串口信息
        /// </summary>
        /// <returns>一般由2个值构成，第1个值是串口，第2个值是该串口的波特率</returns>
        public int[] GetSerialPortInfo()
        {
            if (_serialPort[0] == -1)
            {
                var port = _serialPortInfo[0].ToUpper().TrimStart('C', 'O', 'M');
                if (!int.TryParse(port, out _serialPort[0]))
                    _serialPort[0] = 0;
                if (!int.TryParse(_serialPortInfo[1], out _serialPort[1]))
                    _serialPort[1] = 115200;
            }

            return _serialPort;
        }

        public IPEndPoint GetIpEndPoint()
        {
            if (_ipEndPoint == null)
            {
                if (!IPAddress.TryParse(_serialPortInfo[0], out var ip))
                    ip = new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00});
                if (!int.TryParse(_serialPortInfo[1], out var port))
                    port = 5025;
                _ipEndPoint = new IPEndPoint(ip, port);
            }

            return _ipEndPoint;
        }

        public static Slot Build(TunnelType tunnelType, params string[] ports)
        {
            var carePort = new Slot
            {
                TunnelType = tunnelType,
                _serialPortInfo = ports
            };
            if (tunnelType == TunnelType.Serial)
            {
                if (!int.TryParse(ports[0], out carePort._serialPort[0]))
                    carePort._serialPort[0] = 0;
                if (ports.Length == 1 || !int.TryParse(ports[1], out carePort._serialPort[1]))
                    carePort._serialPort[1] = 115200;
            }
            else
            {
                var ip = IPAddress.Parse(ports[0]);
                var port = int.Parse(ports[1]);
                var ipe = new IPEndPoint(ip, port);
                carePort._ipEndPoint = ipe;
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
                    var ports = GetSerialPortInfo();
                    var sb = new StringBuilder();
                    foreach (var port in ports)
                        sb.Append(port).Append(':');
                    return sb.ToString().TrimEnd(':');
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Slot)) return false;
            return Equals((Slot) obj);
        }

        private bool Equals(Slot other)
        {
            return _id.Equals(other._id) && TunnelType == other.TunnelType;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode() * 397;
        }
    }
}