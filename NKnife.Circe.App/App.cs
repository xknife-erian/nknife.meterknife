using System.Windows;
using RAY.Common;
using RAY.Common.NLogConf;
using RAY.Windows;
using RAY.Windows.Common;

namespace NKnife.Circe.App
{
    public partial class App
    {
        private readonly AppLifecycleMapper _appLifecycleMapper;

        public App()
        {
            NLogConfig.ConfigureNLog();
            DispatcherUtil.Initialize();
            LogStack.UIDispatcher = DispatcherUtil.CheckBeginInvokeOnUI;

            BaseAppLifecycleHandler.AbortStartup = Shutdown;

            _appLifecycleMapper = new AppLifecycleMapper(this);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await _appLifecycleMapper.HandleStartupAsync(e.Args);
        }

        /// <inheritdoc />
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            await _appLifecycleMapper.HandleExitAsync(e.ApplicationExitCode);
        }
    }
}