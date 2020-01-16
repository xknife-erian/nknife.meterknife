using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MeterKnife.Util.Wrapper
{
    /// <summary>
    /// 有关计算机的一些操作方法
    /// </summary>
    public class Computer
    {
        /// <summary>重启应用程序。
        /// 调用方法：Application.ExitThread(); Gean.Computer.Restart();
        /// </summary>
        public static void RestartApplication()
        {
            var thread = new Thread(RestartApplicationMethod);
            object appName = Application.ExecutablePath;
            Thread.Sleep(200);
            thread.Start(appName);
        }

        /// <summary>重启应用程序的委托方法。
        /// </summary>
        /// <param name="obj">The obj.</param>
        private static void RestartApplicationMethod(object obj)
        {
            var process = new Process();
            process.StartInfo.FileName = obj.ToString();
            process.Start();
        }

        /// <summary>系统启动相关，关机，重启等
        /// </summary>
        public class POWER
        {
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            internal struct TokPriv1Luid
            {
                public int Count;
                public long Luid;
                public int Attr;
            }

            [DllImport("kernel32.dll", ExactSpelling = true)]
            internal static extern IntPtr GetCurrentProcess();

            [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
            internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

            [DllImport("advapi32.dll", SetLastError = true)]
            internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

            [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
            internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
                                                              ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

            [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
            internal static extern bool ExitWindowsEx(int doFlag, int rea);

            internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
            internal const int TOKEN_QUERY = 0x00000008;
            internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
            internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            /// <summary>注销
            /// </summary>
            internal const int EWX_LOGOFF = 0x00000000;

            /// <summary>先保存再关机
            /// </summary>
            internal const int EWX_SHUTDOWN = 0x00000001;

            /// <summary>重启
            /// </summary>
            internal const int EWX_REBOOT = 0x00000002;

            /// <summary>不保存
            /// </summary>
            internal const int EWX_FORCE = 0x00000004;

            /// <summary>强制关机
            /// </summary>
            internal const int EWX_POWEROFF = 0x00000008;

            /// <summary>不保存就关机(WIN2K以上版本)
            /// </summary>
            internal const int EWX_FORCEIFHUNG = 0x00000010;

            private static void DoExitWin(int doFlag)
            {
                bool ok;
                TokPriv1Luid tp;
                IntPtr hproc = GetCurrentProcess();
                IntPtr htok = IntPtr.Zero;
                ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
                tp.Count = 1;
                tp.Luid = 0;
                tp.Attr = SE_PRIVILEGE_ENABLED;
                ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
                ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
                ok = ExitWindowsEx(doFlag, 0);
            }

            /// <summary>重启计算机
            /// </summary>
            public static void REBOOT()
            {
                DoExitWin(EWX_FORCE | EWX_REBOOT);
            }

            /// <summary>关闭计算机
            /// </summary>
            public static void POWEROFF()
            {
                DoExitWin(EWX_FORCE | EWX_POWEROFF);
            }

            /// <summary>注销计算机
            /// </summary>
            public static void LOGOFF()
            {
                DoExitWin(EWX_FORCE | EWX_LOGOFF);
            }
        }

        /// <summary>设置一个应用程序的自启动
        /// </summary>
        public class AutoStart
        {
            /// <summary>检查一个自启动键是否存在.
            /// </summary>
            /// <param name="keyValue"></param>
            /// <returns></returns>
            private static bool IsValueExist(string keyValue)
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey run = hklm.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

                string ValueStr1 = null;

                try
                {
                    ValueStr1 = (string) run.GetValue(keyValue);
                    hklm.Close();
                }
                catch (Exception Er1)
                {
                    MessageBox.Show(Er1.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (ValueStr1 == null)
                    return false;
                else
                    return true;
            }

            /// <summary>添加注册表自启动键
            /// </summary>
            /// <param name="keyValue">路径和文件名</param>
            public static void SetAutoValue(string keyValue)
            {
                // 检查键值是否已经存在,不会重复注册
                var rt = IsValueExist(keyValue);
                if (rt)
                {
                    return;
                }

                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey run = hklm.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

                try
                {
                    if (run != null)
                        run.SetValue(keyValue, Application.ExecutablePath); // 获得路径和文件名
                    hklm.Close();
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            /// <summary>删除指定注册表自启动键
            /// </summary>
            /// <param name="keyValue"></param>
            public static void DelAutoValue(string keyValue)
            {
                // 检查键值是否已经存在,如果已不存在则不操作了
                bool rt = IsValueExist(keyValue);
                if (!rt)
                {
                    return;
                }

                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey run = hklm.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

                try
                {
                    if (run != null)
                        run.DeleteValue(keyValue);
                    hklm.Close();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        /// <summary>屏幕分辩率相关
        /// </summary>
        public class DisplaySettings
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct DEVMODE
            {
                public const int DM_DISPLAYFREQUENCY = 0x400000;
                public const int DM_PELSWIDTH = 0x80000;
                public const int DM_PELSHEIGHT = 0x100000;
                private const int CCHDEVICENAME = 32;
                private const int CCHFORMNAME = 32;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)] 
                public string dmDeviceName;
                public short dmSpecVersion;
                public short dmDriverVersion;
                public short dmSize;
                public short dmDriverExtra;
                public int dmFields;

                public int dmPositionX;
                public int dmPositionY;
                public DMDO dmDisplayOrientation;
                public int dmDisplayFixedOutput;

                public short dmColor;
                public short dmDuplex;
                public short dmYResolution;
                public short dmTTOption;
                public short dmCollate;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)] 
                public string dmFormName;
                public short dmLogPixels;
                public int dmBitsPerPel;
                public int dmPelsWidth;
                public int dmPelsHeight;
                public int dmDisplayFlags;
                public int dmDisplayFrequency;
                public int dmICMMethod;
                public int dmICMIntent;
                public int dmType;
                public int dmDitherType;
                public int dmReserved1;
                public int dmReserved2;
                public int dmPanningWidth;
                public int dmPanningHeight;
            }

            private enum DMDO
            {
                DEFAULT = 0,
                D90 = 1,
                D180 = 2,
                D270 = 3
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern int ChangeDisplaySettings([In] ref DEVMODE lpDevMode, int dwFlags);

            /// <summary>设置屏幕分辩率相关
            /// </summary>
            /// <param name="width">分辩率的宽度，默认1024.</param>
            /// <param name="height">分辩率的高度度，默认768.</param>
            /// <param name="frequency">屏幕刷新率，默认85.</param>
            public static void SetDisplaySettings(int width = 1024, int height = 768, int frequency = 85)
            {
                DEVMODE dm = new DEVMODE();
                dm.dmSize = (short) Marshal.SizeOf(typeof (DEVMODE));
                dm.dmPelsWidth = width;
                dm.dmPelsHeight = height;
                dm.dmDisplayFrequency = frequency;
                dm.dmFields = DEVMODE.DM_PELSWIDTH | DEVMODE.DM_PELSHEIGHT | DEVMODE.DM_DISPLAYFREQUENCY;
                ChangeDisplaySettings(ref dm, 0);
            }
        }
    }
}