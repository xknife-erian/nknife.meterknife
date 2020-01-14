using System;
using System.Collections.Generic;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace NKnife.Utility
{
    /// <summary>
    ///     一些简单的基于网络的小型扩展方法
    /// </summary>
    public sealed class UtilityNet
    {
        #region netapi32.dll

        [DllImport("netapi32.dll", EntryPoint = "NetMessageBufferSend", CharSet = CharSet.Unicode)]
        private static extern int NetMessageBufferSend(
            string servername,
            string msgname,
            string fromname,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 4)] byte[] buf,
            [MarshalAs(UnmanagedType.U4)] int buflen);

        #endregion

        /// <summary>
        ///     系统信使服务:发送消息
        /// </summary>
        /// <param name="fromName">发送人</param>
        /// <param name="toName">接收人(机器名或者IP)</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public static bool MessageBufferSend(string fromName, string toName, string message)
        {
            byte[] buf = Encoding.Unicode.GetBytes(message);
            return NetMessageBufferSend(null, toName, fromName, buf, buf.Length) == 0;
        }

        /// <summary>
        ///     获取本机的子网掩码。
        ///     如果无法获取，将返回Null值。
        /// </summary>
        public static IPAddress GetSubnet()
        {
            return GetNetInformation("IPSubnet");
        }

        /// <summary>
        ///     获取本机的默认网关。
        ///     如果无法获取，将返回Null值。
        /// </summary>
        public static IPAddress GetDefaultIpGateway()
        {
            return GetNetInformation("DefaultIPGateway");
        }

        /// <summary>
        ///     获取本机IP地址（V4）
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] GetLocalIpv4()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            var ipCollection = new List<IPAddress>(localIPs);
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                    ipCollection.Remove(ip);
            }
            return ipCollection.ToArray();
        }

        /// <summary>
        ///     获取本机的一些网络设置的相关信息
        /// </summary>
        private static IPAddress GetNetInformation(string ipType)
        {
            try
            {
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementBaseObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]))
                    {
                        var strings = nic[ipType] as String[];
                        if (!UtilityCollection.IsNullOrEmpty(strings))
                        {
                            string ipstr = strings[0];
                            return IPAddress.Parse(ipstr);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        ///     根据IP地址获得主机名称
        /// </summary>
        /// <param name="ip">主机的IP地址</param>
        /// <returns>主机名称</returns>
        public static string GetHostNameByIp(string ip)
        {
            ip = ip.Trim();
            if (ip == String.Empty)
                return String.Empty;
            try
            {
                // 是否 Ping 的通
                if (NetPing(ip))
                {
                    IPHostEntry host = Dns.GetHostEntry(ip);
                    return host.HostName;
                }
                return String.Empty;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        ///     根据主机名（域名）获得主机的IP地址
        /// </summary>
        /// <example>
        ///     GetIPByDomain("pc001"); GetIPByDomain("www.google.com");
        /// </example>
        /// <param name="hostName">主机名或域名</param>
        /// <returns>主机的IP地址</returns>
        public static string GetIpByHostName(string hostName)
        {
            hostName = hostName.Trim();
            if (hostName == String.Empty)
                return String.Empty;
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostName);
                return host.AddressList.GetValue(0).ToString();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        ///     是否以200毫秒的间隔时间 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public static bool NetPing(string ip)
        {
            if (String.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException("IPAddress");
            }
            IPAddress ipaddress;
            if (!IPAddress.TryParse(ip, out ipaddress))
            {
                return false;
            }
            return NetPing(ip, 200);
        }

        /// <summary>
        ///     是否 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <param name="timeout">Ping的间隔时间，单位：毫秒</param>
        /// <returns>true 通，false 不通</returns>
        public static bool NetPing(string ip, int timeout)
        {
            var ping = new Ping();
            var options = new PingOptions();
            options.DontFragment = true;
            string data = "Hello...";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingReply reply = ping.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            return false;
        }

        /// <summary>
        ///     [ 仅供Asp.net使用, WinForm无法使用 ] 获取本机的公网IP地址
        /// </summary>
        /// <returns>返回结果集合中的[0]为公网IP地址，后续的为代理地址</returns>
        public static IPAddress[] GetInternetIpAddressByLocalhost()
        {
            var iplist = new List<IPAddress>();

            string ip; // = HttpContext.Current.Request.UserHostAddress;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            iplist.Add(IPAddress.Parse(ip));

            //有代理
            string agentip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!String.IsNullOrEmpty(agentip))
            {
                if (agentip.IndexOf(".") == -1)
                    agentip = null;
                if (agentip != null)
                {
                    if (agentip.IndexOf("unknow") != -1)
                        agentip = agentip.Replace("unknow", String.Empty);

                    string[] temparyip = agentip.Replace("   ", String.Empty).Replace("'", String.Empty).Split(new[] {',', ';'});
                    //过滤代理格式中的非IP和内网IP
                    foreach (string str in temparyip)
                    {
                        if (String.IsNullOrEmpty(str))
                        {
                            if (str.Substring(0, 3) != "10."
                                && str.Substring(0, 7) != "192.168"
                                && str.Substring(0, 7) != "172.16.")
                            {
                                IPAddress i;
                                if (IPAddress.TryParse(str, out i))
                                {
                                    iplist.Add(i);
                                }
                            }
                        }
                    }
                }
            }
            return iplist.ToArray();
        }

        /// <summary>
        ///     获取一个标准的微型KeepAlive通讯包。
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateKeepAliveCommand()
        {
            const uint DUMMY = 0;
            var inOptionValues = new byte[Marshal.SizeOf(DUMMY) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)15000).CopyTo(inOptionValues, Marshal.SizeOf(DUMMY));
            BitConverter.GetBytes((uint)15000).CopyTo(inOptionValues, Marshal.SizeOf(DUMMY) * 2);
            return inOptionValues;
        }

        /// <summary>
        ///     SendArp获取MAC地址
        /// </summary>
        /// <param name="macip"></param>
        /// <returns></returns>
        public static string GetMacAddress(string macip)
        {
            var strReturn = new StringBuilder();
            try
            {
                Int32 remote = inet_addr(macip);
                var macinfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macinfo, ref length);
                string temp = Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();
                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5)
                    {
                        strReturn.Append(temp.Substring(x - 2, 2));
                    }
                    else
                    {
                        strReturn.Append(temp.Substring(x - 2, 2) + ":");
                    }
                    x -= 2;
                }
                return strReturn.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 destIp, Int32 srcIp, ref Int64 macAddr, ref Int32 phyAddrLen);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ipaddr);

    }
}