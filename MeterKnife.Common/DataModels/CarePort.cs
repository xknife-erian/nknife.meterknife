using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MeterKnife.Common.Tunnels;
using NKnife.Utility;

namespace MeterKnife.Common.DataModels
{
    public class CarePort
    {
        public TunnelType TunnelType { get; set; }

        public string Port { get; set; }

        private int _SerialPort = -1;

        public int GetSerialPort()
        {
            if (_SerialPort == -1)
            {
                if (!int.TryParse(Port, out _SerialPort))
                {
                    _SerialPort = 0;
                }
            }
            return _SerialPort;
        }

        private IPEndPoint _IpEndPoint = null;

        public IPEndPoint GetIpEndPoint()
        {
            if (_IpEndPoint == null)
            {
                var ss = Port.Split(new char[] {':'});
                if (ss.Length != 2)
                {
                    _IpEndPoint = new IPEndPoint(new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00}), 5025);
                    return _IpEndPoint;
                }
                IPAddress ip;
                if (!IPAddress.TryParse(ss[0], out ip))
                {
                    ip = new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00});
                }
                _IpEndPoint = new IPEndPoint(ip, int.Parse(ss[1]));
            }
            return _IpEndPoint;
        }

        public static CarePort Build(TunnelType tunnelType, string port)
        {
            var carePort = new CarePort
            {
                TunnelType = tunnelType, 
                Port = port
            };
            return carePort;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CarePort)) return false;
            return Equals((CarePort) obj);
        }

        protected bool Equals(CarePort other)
        {
            return TunnelType == other.TunnelType && string.Equals(Port, other.Port);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) TunnelType*397) ^ (Port != null ? Port.GetHashCode() : 0);
            }
        }
    }
}
