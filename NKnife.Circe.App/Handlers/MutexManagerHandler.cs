using NLog;
using RAY.Windows;
using RAY.Windows.Common;

namespace NKnife.Circe.App.Handlers
{
    class MutexManagerHandler : BaseAppLifecycleHandler
    {
        private const string MUTEX_NAME = "LEIAO.Mercury.App.Mutex";
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();
        private Mutex? _mutex;

        public MutexManagerHandler()
        {
            
        }

        public override Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            _mutex = new Mutex(true, MUTEX_NAME, out var createdNew);
            if (!createdNew)
            {
                SimpleNamedPipeClient.SendStartArgsThroughPipe(startupArgs);
                AbortStartup?.Invoke();
            }
            return base.HandleStartupAsync(startupArgs);
        }
    }
}
