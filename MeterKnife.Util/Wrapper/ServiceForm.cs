using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Common.Logging;
using NKnife.Interface;

namespace NKnife.Wrapper
{
    /// <summary>
    /// Windows服务因没有UI主线程，无法用一般的办法显示窗体。本类的静态方法呼叫系统的功能，来实现打开窗体。
    /// </summary>
    public class ServiceForm
    {
        private static readonly ILog _logger = LogManager.GetLogger<ServiceForm>();

        /// <summary>
        ///     开启窗体 使用后用 System.Windows.Forms.Application.Run(_Form);来启动窗体
        /// </summary>
        /// <param name="evnetLog">日志</param>
        public static void Open(EventLog evnetLog)
        {
            try
            {
                GetDesktopWindow();
                IntPtr windowsStation = GetProcessWindowStation();
                IntPtr threadId = GetCurrentThreadId();
                IntPtr deskTop = GetThreadDesktop(threadId);
                IntPtr hwinstaUser = OpenWindowStation("WinSta0", false, 33554432);
                if (hwinstaUser == IntPtr.Zero)
                {
                    RpcRevertToSelf();
                    return;
                }
                SetProcessWindowStation(hwinstaUser);
                IntPtr hdeskUser = OpenDesktop("Default", 0, false, 33554432);
                RpcRevertToSelf();
                if (hdeskUser == IntPtr.Zero)
                {
                    SetProcessWindowStation(windowsStation);
                    CloseWindowStation(hwinstaUser);
                    return;
                }
                SetThreadDesktop(hdeskUser);
                SetThreadDesktop(deskTop);
                SetProcessWindowStation(windowsStation);
                CloseDesktop(hdeskUser);
                CloseWindowStation(hwinstaUser);
            }
            catch (Exception e)
            {
                _logger.Error("弹出服务窗体异常。", e);
                evnetLog.WriteEntry(e.ToString());
            }
        }

        #region User32.DLL

        [DllImport("user32.dll")]
        private static extern int GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetProcessWindowStation();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern IntPtr GetThreadDesktop(IntPtr dwThread);

        [DllImport("user32.dll")]
        private static extern IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, int dwDesiredAccess);

        [DllImport("User32.dll")]
        private static extern IntPtr OpenDesktop(string lpsxDesktop, uint dwFlags, bool fInherit, uint dwDesiredAccess);

        [DllImport("user32.dll")]
        private static extern IntPtr CloseDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        private static extern IntPtr SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        private static extern IntPtr SetProcessWindowStation(IntPtr hWinSta);

        [DllImport("user32.dll")]
        private static extern IntPtr CloseWindowStation(IntPtr hWinSta);

        #endregion

        #region Rpcrt4.dll

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern IntPtr RpcImpersonatClient(int rpc);

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern IntPtr RpcRevertToSelf();

        #endregion

        #region 调用

        /**
        protected override void OnStart(string[] args)
        {
            Thread _TestForm = new Thread(new ThreadStart(StartListen));
            _TestForm.StartBeatingTimer();
        }
        public void StartListen()
        {
            ServiceForm.Open(EventLog);
            System.Windows.Forms.Form _MyForm = new System.Windows.Forms.Form();
            _MyForm.Text = "打开窗体 Application";
            System.Windows.Forms.Application.Run(_MyForm);
            while (true)
            {
                Thread.Sleep(100000);
            }
        }
        **/

        #endregion
    }
}