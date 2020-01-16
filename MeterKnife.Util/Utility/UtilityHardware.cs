using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using MeterKnife.Util.Entities;
using Microsoft.Win32;

namespace MeterKnife.Util.Utility
{
    public class UtilityHardware
    {
        private static CPUInfo[] _Infos;
        private static string[] _MacAddressArray;

        /// <summary>获取系统串口数量
        /// </summary>
        /// <returns></returns>
        public static string[] GetSerialCommList()
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                return keyCom.GetValueNames();
            }
            return null;
        }

        /// <summary>获取指定编号的CPU信息
        /// </summary>
        public static CPUInfo GetCPUInfo(int n = 0)
        {
            if (_Infos == null)
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                ManagementObjectCollection collection = searcher.Get();
                _Infos = new CPUInfo[collection.Count];
                int i = 0;
                foreach (ManagementObject mo in collection)
                {
                    _Infos[i] = new CPUInfo();
                    try
                    {
                        object propertyValue;

                        try
                        {
                            propertyValue = mo.GetPropertyValue("ProcessorId");
                            if (propertyValue != null)
                                _Infos[i].ProcessorId = propertyValue.ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无ProcessorId属性");
                        }

                        #region 更多属性

                        try
                        {
                            propertyValue = mo.GetPropertyValue("CurrentVoltage");
                            if (propertyValue != null)
                                _Infos[i].CurrentVoltage = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无CurrentVoltage属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("ExtClock");
                            if (propertyValue != null)
                                _Infos[i].ExtClock = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无ExtClock属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("L2CacheSize");
                            if (propertyValue != null)
                                _Infos[i].L2CacheSize = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无L2CacheSize属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("Manufacturer");
                            if (propertyValue != null)
                                _Infos[i].Manufacturer = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("Name");
                            if (propertyValue != null)
                                _Infos[i].Name = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Name属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("Version");
                            if (propertyValue != null)
                                _Infos[i].Version = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Version属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("LoadPercentage");
                            if (propertyValue != null)
                                _Infos[i].LoadPercentage = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无LoadPercentage属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("MaxClockSpeed");
                            if (propertyValue != null)
                                _Infos[i].MaxClockSpeed = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无MaxClockSpeed属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("CurrentClockSpeed");
                            if (propertyValue != null)
                                _Infos[i].CurrentClockSpeed = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无CurrentClockSpeed属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("AddressWidth");
                            if (propertyValue != null)
                                _Infos[i].AddressWidth = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无AddressWidth属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("DataWidth");
                            if (propertyValue != null)
                                _Infos[i].DataWidth = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无DataWidth属性");
                        }

                        #endregion

                        mo.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("获取计算机信息异常"), e.Message);
                    }
                    i++;
                }
                if (_Infos.Length > 0)
                {
                    var mos = new ManagementObjectSearcher("Select * FROM Win32_BaseBoard");
                    foreach (ManagementObject mo in mos.Get())
                    {
                        object propertyValue;

                        try
                        {
                            propertyValue = mo.GetPropertyValue("Manufacturer");
                            if (propertyValue != null)
                            {
                                foreach (var cpuInfo in _Infos)
                                    cpuInfo.CurrBaseBoard.Manufacturer = propertyValue.ToString();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }

                        try
                        {
                            propertyValue = mo.GetPropertyValue("Product");
                            if (propertyValue != null)
                            {
                                foreach (var cpuInfo in _Infos)
                                    cpuInfo.CurrBaseBoard.Product = propertyValue.ToString();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }

                        try
                        {
                            propertyValue = mo.GetPropertyValue("SerialNumber");
                            if (propertyValue != null)
                            {
                                foreach (var cpuInfo in _Infos)
                                    cpuInfo.CurrBaseBoard.SerialNumber = propertyValue.ToString().Trim();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }
                        mo.Dispose();
                    }
                }
            }
            if (_Infos != null && n < _Infos.Length)
            {
                return _Infos[n];
            }
            return new CPUInfo();
        }

        /// <summary>获取CPU编号
        /// </summary>
        public static string GetCpuID(int n = 0)
        {
            string cpuId = GetCPUInfo(n).ProcessorId;
            return !string.IsNullOrWhiteSpace(cpuId) ? cpuId : "CPU0";
        }

        /// <summary>
        /// 获取本机的Mac地址
        /// </summary>
        public static string GetMacAddress(int n = 0)
        {
            if (_MacAddressArray == null)
            {
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                var maclist = new List<string>();
                foreach (ManagementObject mo in moc)
                {
                    bool enable = false;
                    object propertyValue;
                    try
                    {
                        propertyValue = mo.GetPropertyValue("IPEnabled");
                        if (propertyValue != null)
                            bool.TryParse(propertyValue.ToString(), out enable);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("无IPEnabled属性");
                    }
                    if(enable)
                    {
                        try
                        {
                            propertyValue = mo.GetPropertyValue("MacAddress");
                            if (propertyValue != null)
                                maclist.Add(propertyValue.ToString());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("无MacAddress属性");
                        }
                    }
                    mo.Dispose();
                }
                _MacAddressArray = maclist.ToArray();
            }
            if (_MacAddressArray != null && n < _MacAddressArray.Length)
            {
                var mac = _MacAddressArray[n];
                return !string.IsNullOrWhiteSpace(mac) ? mac : "MAC0";
            }
            return "MAC99";
        }

        /// <summary>获取第一块硬盘编号
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskID()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            ManagementObjectCollection moc = searcher.Get();
            return (from ManagementObject mo in moc select mo["SerialNumber"].ToString().Trim()).FirstOrDefault();
        }
    }
}