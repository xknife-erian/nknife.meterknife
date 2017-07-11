using System;
using System.Threading;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Common.Logging;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.App
{
    internal class Program
    {
        public static AutoResetEvent AutoResetEvent { get; private set; }
        public static MainService MainService { get; private set; }

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AutoUpdater.OpenDownloadPage = true;
            AutoUpdater.LetUserSelectRemindLater = true;
            AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            AutoUpdater.RemindLaterAt = 2;
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.Start("http://127.0.0.1");//("http://rbsoft.org/updates/AutoUpdaterTest.xml");

            AutoResetEvent = new AutoResetEvent(false);

            //开启欢迎屏幕
            Splasher.Show(typeof(SplashForm));

            FileCleaner.Run();

            var listenServiceThread = new Thread(RunListener) {IsBackground = true};
            listenServiceThread.Start(args);

            var mainServiceThread = new Thread(RunMainService) {IsBackground = true};
            mainServiceThread.Start(args);
            AutoResetEvent.WaitOne();
        }

        private static void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var dr = MessageBox.Show($"有新版本{args.CurrentVersion}发布。正在使用的版本：{args.InstalledVersion}。\r\n是否下载新版本?",
                        @"有可用更新", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"没有可用的更新，请稍候重试。", @"没有可用更新",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(@"连接到远程更新服务器时发生异常，请检查互联网连接是否通畅。", @"检查更新失败",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RunListener(object obj)
        {
            var listener = new Listener();
            listener.Initialize();
        }

        private static void RunMainService(object obj)
        {
            MainService = new MainService();
            MainService.Load((string[]) obj);
        }
    }
}