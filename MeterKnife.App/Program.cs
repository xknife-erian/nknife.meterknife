using System;
using System.Threading;
using System.Windows.Forms;
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

            DI.Initialize();

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