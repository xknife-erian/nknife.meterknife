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
            AutoUpdater.Start("http://rbsoft.org/updates/AutoUpdaterTest.xml");

            AutoResetEvent = new AutoResetEvent(false);

            var logger = LogManager.GetLogger<SplashForm>();

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
                    var dialogResult =
                        MessageBox.Show(
                            $"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. Do you want to update the application now?",
                            @"Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            //You can use Download Update dialog used by AutoUpdater.NET to download the update.
                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"There is no update available please try again later.", @"No update available",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(@"There is a problem reaching update server please check your internet connection and try again later.", @"Update check failed",
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