using System;
using System.Collections;
using System.Collections.Specialized;
using System.Management;
using System.Text;

namespace NKnife.Wrapper
{
    /// <summary>
    ///     获取系统信息
    /// </summary>
    /// <example>
    /// </example>
    public sealed class WMI
    {
        private readonly ArrayList _Mocs;
        private readonly StringDictionary _Names; // 用来存储属性名，便于忽略大小写查询正确名称。

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="path"></param>
        public WMI(string path)
        {
            _Names = new StringDictionary();
            _Mocs = new ArrayList();

            try
            {
                var cimobject = new ManagementClass(path);
                ManagementObjectCollection moc = cimobject.GetInstances();

                bool ok = false;
                foreach (ManagementObject mo in moc)
                {
                    var o = new Hashtable();
                    _Mocs.Add(o);

                    foreach (PropertyData p in mo.Properties)
                    {
                        o.Add(p.Name, p.Value);
                        if (!ok) _Names.Add(p.Name, p.Name);
                    }

                    ok = true;
                    mo.Dispose();
                }
                moc.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="path"></param>
        public WMI(WMIPath path)
            : this(path.ToString())
        {
        }

        /// <summary>
        ///     信息集合数量
        /// </summary>
        public int Count
        {
            get { return _Mocs.Count; }
        }

        /// <summary>
        ///     获取指定属性值，注意某些结果可能是数组。
        /// </summary>
        public object this[int index, string propertyName]
        {
            get
            {
                try
                {
                    string trueName = _Names[propertyName.Trim()]; // 以此可不区分大小写获得正确的属性名称。
                    var h = (Hashtable) _Mocs[index];
                    return h[trueName];
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     返回所有属性名称。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string[] PropertyNames(int index)
        {
            try
            {
                var h = (Hashtable) _Mocs[index];
                var result = new string[h.Keys.Count];

                h.Keys.CopyTo(result, 0);

                Array.Sort(result);
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     返回测试信息。
        /// </summary>
        /// <returns></returns>
        public string Test()
        {
            try
            {
                var result = new StringBuilder(1000);

                for (int i = 0; i < Count; i++)
                {
                    int j = 0;
                    foreach (string s in PropertyNames(i))
                    {
                        result.Append(string.Format("{0}:{1}={2}\n", ++j, s, this[i, s]));

                        if (this[i, s] is Array)
                        {
                            var v1 = this[i, s] as Array;
                            for (int x = 0; x < v1.Length; x++)
                            {
                                result.Append("\t" + v1.GetValue(x) + "\n");
                            }
                        }
                    }
                    result.Append("======WMI=======\n");
                }
                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    #region WMIPath

    public enum WMIPath
    {
        // 硬件
        Win32_Processor, // CPU 处理器
        Win32_PhysicalMemory, // 物理内存条
        Win32_Keyboard, // 键盘
        Win32_PointingDevice, // 点输入设备，包括鼠标。
        Win32_FloppyDrive, // 软盘驱动器
        Win32_DiskDrive, // 硬盘驱动器
        Win32_CDROMDrive, // 光盘驱动器
        Win32_BaseBoard, // 主板
        Win32_BIOS, // BIOS 芯片
        Win32_ParallelPort, // 并口
        Win32_SerialPort, // 串口
        Win32_SerialPortConfiguration, // 串口配置
        Win32_SoundDevice, // 多媒体设置，一般指声卡。
        Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
        Win32_USBController, // USB 控制器
        Win32_NetworkAdapter, // 网络适配器
        Win32_NetworkAdapterConfiguration, // 网络适配器设置
        Win32_Printer, // 打印机
        Win32_PrinterConfiguration, // 打印机设置
        Win32_PrintJob, // 打印机任务
        Win32_TCPIPPrinterPort, // 打印机端口
        Win32_POTSModem, // MODEM
        Win32_POTSModemToSerialPort, // MODEM 端口
        Win32_DesktopMonitor, // 显示器
        Win32_DisplayConfiguration, // 显卡
        Win32_DisplayControllerConfiguration, // 显卡设置
        Win32_VideoController, // 显卡细节。
        Win32_VideoSettings, // 显卡支持的显示模式。

        // 操作系统
        Win32_TimeZone, // 时区
        Win32_SystemDriver, // 驱动程序
        Win32_DiskPartition, // 磁盘分区
        Win32_LogicalDisk, // 逻辑磁盘
        Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
        Win32_LogicalMemoryConfiguration, // 逻辑内存配置
        Win32_PageFile, // 系统页文件信息
        Win32_PageFileSetting, // 页文件设置
        Win32_BootConfiguration, // 系统启动配置
        Win32_ComputerSystem, // 计算机信息简要
        Win32_OperatingSystem, // 操作系统信息
        Win32_StartupCommand, // 系统自动启动程序
        Win32_Service, // 系统安装的服务
        Win32_Group, // 系统管理组
        Win32_GroupUser, // 系统组帐号
        Win32_UserAccount, // 用户帐号
        Win32_Process, // 系统进程
        Win32_Thread, // 系统线程
        Win32_Share, // 共享
        Win32_NetworkClient, // 已安装的网络客户端
        Win32_NetworkProtocol, // 已安装的网络协议
    }

    #endregion

}

// WMI w = new WMI(WMIPath.Win32_NetworkAdapterConfiguration);
// for (int i = 0; i < w.Count; i ++)
// {
//    if ((bool)w[i, "IPEnabled"])
//    {
//      Console.WriteLine("Caption:{0}", w[i, "Caption"]);
//      Console.WriteLine("MAC Address:{0}", w[i, "MACAddress"]);
//    }
// }