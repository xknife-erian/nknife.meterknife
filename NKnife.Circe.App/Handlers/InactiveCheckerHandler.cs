using CommunityToolkit.Mvvm.DependencyInjection;
using LEIAO.Mercury.Modules;
using RAY.Common.Plugin.Manager;
using RAY.Windows.Common;

namespace NKnife.Circe.App.Handlers
{
    class InactiveCheckerHandler : BaseAppLifecycleHandler
    {
        /// <inheritdoc />
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager          = Ioc.Default.GetRequiredService<IPluginManager>();
            var inactiveCheckerBuilder = pluginManager.FindModuleBuilder<IInactiveChecker>();
            var inactiveChecker        = inactiveCheckerBuilder?.Build().Value;
#if RELEASE
            inactiveChecker!.Start();
#endif
            return await base.HandleStartupAsync(startupArgs);
        }
    }
}