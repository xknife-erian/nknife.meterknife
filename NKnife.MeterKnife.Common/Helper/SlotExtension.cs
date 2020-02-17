using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Util.Serial.Common;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Common.Domain
{
    public static class SlotExtension
    {
        /// <summary>
        ///     获取串口信息
        /// </summary>
        public static (short, SerialConfig) GetSerialPortInfo(this Slot slot)
        {
            if (slot.TunnelType != TunnelType.Serial)
                return (-1, null);
            return JsonConvert.DeserializeObject<(short, SerialConfig)>(slot.Config);
        }

        /// <summary>
        ///     获取TCPIP相关信息
        /// </summary>
        public static IPEndPoint GetIpEndPoint(this Slot slot)
        {
            if (slot.TunnelType != TunnelType.Tcpip)
                return new IPEndPoint(new IPAddress(new byte[] { 0x0, 0x0, 0x0, 0x0 }), 0);
            return JsonConvert.DeserializeObject<IPEndPoint>(slot.Config);
        }

        /// <summary>
        /// 设置<see cref="Slot"/>的相关属性
        /// </summary>
        /// <param name="slot"><see cref="Slot"/></param>
        /// <param name="tunnelType"><see cref="Slot"/>的类型</param>
        /// <param name="tunnelConfig"><see cref="Slot"/>的相关配置</param>
        public static void Set(this Slot slot, TunnelType tunnelType, object tunnelConfig)
        {
            slot.TunnelType = tunnelType;
            slot.Config = JsonConvert.SerializeObject(tunnelConfig);
        }
    }
}
