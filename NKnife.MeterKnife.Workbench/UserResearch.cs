using System;
using System.Management;

namespace NKnife.MeterKnife.Workbench
{
    public class UserResearch
    {
        public UserResearch()
        {
            //获取系统启动后经过的毫秒数 
            var iTC = Environment.TickCount / 60000;
            //系统标识符和版本号 
            var strSystem = Environment.OSVersion;
            //获取映射到进程上下文的物理内存量 
            var strRem = Environment.WorkingSet.ToString();
            //系统目录的完全限定路径 
            var strSD = Environment.SystemDirectory;
            //获取此本地计算机的 NetBIOS 名称 
            var strMN = Environment.MachineName;
            //获取与当前用户关联的网络域名 
            var strUDN = Environment.UserDomainName;
            CpuId = GetCpuID();
            MacAddress = GetMacAddress();
            DiskId = GetDiskID();
            IpAddress = GetIPAddress();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }

        /// <summary>
        ///     系统名称
        /// </summary>
        public string ComputerName { get; }

        /// <summary>
        ///     CPU序列号
        /// </summary>
        public string CpuId { get; }

        /// <summary>
        ///     硬盘ID
        /// </summary>
        public string DiskId { get; }

        /// <summary>
        ///     IP地址
        /// </summary>
        public string IpAddress { get; }

        /// <summary>
        ///     网卡/Mac地址
        /// </summary>
        public string MacAddress { get; }

        /// <summary>
        ///     系统型号
        /// </summary>
        public string SystemType { get; }

        /// <summary>
        ///     物理内存（单位b）
        /// </summary>
        public string TotalPhysicalMemory { get; }

        private string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码
                var cpuInfo = ""; //cpu序列号
                var mc = new ManagementClass("Win32_Processor");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }

                return cpuInfo;
            }
            catch
            {
                return "unKnow";
            }
        }

        private string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址
                var mac = "";
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    if ((bool) mo["IPEnabled"])
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }

                return mac;
            }
            catch
            {
                return "unKnow";
            }
        }

        private string GetIPAddress()
        {
            try
            {
                //获取IP地址
                var st = "";
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    if ((bool) mo["IPEnabled"])
                    {
                        //st=mo["IpAddress"].ToString();
                        Array ar;
                        ar = (Array) mo.Properties["IpAddress"].Value;
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }

                return st;
            }
            catch
            {
                return "unKnow";
            }
        }

        private string GetDiskID()
        {
            try
            {
                //获取硬盘ID
                var HDid = "";
                var mc = new ManagementClass("Win32_DiskDrive");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    HDid = (string) mo.Properties["Model"].Value;
                }

                return HDid;
            }
            catch
            {
                return "unKnow";
            }
        }

        /// <summary>
        ///     PC类型
        /// </summary>
        /// <returns></returns>
        private string GetSystemType()
        {
            try
            {
                var st = "";
                var mc = new ManagementClass("Win32_ComputerSystem");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    st = mo["SystemType"].ToString();
                }

                return st;
            }
            catch
            {
                return "unKnow";
            }
        }

        /// <summary>
        ///     物理内存
        /// </summary>
        /// <returns></returns>
        private string GetTotalPhysicalMemory()
        {
            try
            {
                var st = "";
                var mc = new ManagementClass("Win32_ComputerSystem");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    st = mo["TotalPhysicalMemory"].ToString();
                }

                return st;
            }
            catch
            {
                return "unKnow";
            }
        }

        /// <summary>
        ///     系统名称
        /// </summary>
        /// <returns></returns>
        private string GetComputerName()
        {
            try
            {
                return Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unKnow";
            }
        }
    }

    /// <summary>
    ///     计算机硬件处理类
    /// </summary>
    public class HardwareHandler

    {
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

            Win32_NetworkProtocol // 已安装的网络协议
        }

        /// <summary>
        ///     Cpu信息
        /// </summary>
        /// <returns></returns>
        public void CpuInfo()

        {
            try

            {
                var mc = new ManagementClass(WMIPath.Win32_Processor.ToString());

                var moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)

                {
                    Console.WriteLine("CPU编号：" + mo.Properties["ProcessorId"].Value);

                    Console.WriteLine("CPU型号：" + mo.Properties["Name"].Value);

                    Console.WriteLine("CPU状态：" + mo.Properties["Status"].Value);

                    Console.WriteLine("主机名称：" + mo.Properties["SystemName"].Value);
                }
            }

            catch

            {
                Console.WriteLine("Erroe");
            }
        }

        /// <summary>
        ///     主板信息
        /// </summary>
        public void MainBoardInfo()

        {
            try

            {
                var mc = new ManagementClass(WMIPath.Win32_BaseBoard.ToString());

                var moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)

                {
                    Console.WriteLine("主板ID：" + mo.Properties["SerialNumber"].Value);

                    Console.WriteLine("制造商：" + mo.Properties["Manufacturer"].Value);

                    Console.WriteLine("型号：" + mo.Properties["Product"].Value);

                    Console.WriteLine("版本：" + mo.Properties["Version"].Value);
                }
            }

            catch

            {
                Console.WriteLine("Erroe");
            }
        }

        /// <summary>
        ///     硬盘信息
        /// </summary>
        public void DiskDriveInfo()

        {
            try

            {
                var mc = new ManagementClass(WMIPath.Win32_DiskDrive.ToString());

                var moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    Console.WriteLine("硬盘SN：" + mo.Properties["SerialNumber"].Value);
                    Console.WriteLine("型号：" + mo.Properties["Model"].Value);
                    Console.WriteLine("大小：" + Convert.ToDouble(mo.Properties["Size"].Value) / (1024 * 1024 * 1024));
                }
            }
            catch
            {
                Console.WriteLine("Erroe");
            }
        }

        /// <summary>
        ///     网络连接信息
        /// </summary>
        public void NetworkInfo()
        {
            try
            {
                var mc = new ManagementClass(WMIPath.Win32_NetworkAdapterConfiguration.ToString());
                var moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    Console.WriteLine("MAC地址：" + mo.Properties["MACAddress"].Value);
                    Console.WriteLine("IP地址：" + mo.Properties["IPAddress"].Value);
                }
            }
            catch
            {
                Console.WriteLine("Erroe");
            }
        }

        /// <summary>
        ///     操作系统信息
        /// </summary>
        public void OsInfo()
        {
            try
            {
                var mc = new ManagementClass(WMIPath.Win32_OperatingSystem.ToString());
                var moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    Console.WriteLine("操作系统：" + mo.Properties["Name"].Value);
                    Console.WriteLine("版本：" + mo.Properties["Version"].Value);
                    Console.WriteLine("系统目录：" + mo.Properties["SystemDirectory"].Value);
                }
            }
            catch
            {
                Console.WriteLine("Erroe");
            }
        }
    }
}