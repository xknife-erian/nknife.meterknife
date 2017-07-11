using System;
using System.Reflection;
using Common.Logging;

namespace MeterKnife.App
{
    public class EnvironmentDynamicLoader
    {
        private static readonly ILog _logger = LogManager.GetLogger<EnvironmentDynamicLoader>();

        /// <summary>
        ///     服务的域名
        /// </summary>
        public static readonly string domain_name = $"{typeof(EnvironmentDynamicLoader).Namespace}.AppDomain";

        private readonly AppDomainProxy _RemoteLoader;

        private AppDomain _AppDomain;

        public EnvironmentDynamicLoader()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
            var proxyTypeName = typeof(AppDomainProxy).FullName ?? string.Empty;
            var setup = new AppDomainSetup
            {
                ApplicationName = domain_name,
                ApplicationBase = baseDir,
                PrivateBinPath = baseDir,
                ShadowCopyFiles = "true"
            };
            try
            {
                _AppDomain = AppDomain.CreateDomain(domain_name, null, setup);

                //关键!! 通过 CreateInstanceAndUnwrap 方法启动新的 Domain 中的类似Main函数的类型及方法。
                _RemoteLoader = (AppDomainProxy) _AppDomain.CreateInstanceAndUnwrap(assemblyName, proxyTypeName);
            }
            catch (Exception e)
            {
                _logger.Fatal("呼叫主服务域异常", e);
            }
        }

        public void InvokeMethod(string assemblyFullName, string className, string methodName, string[] args)
        {
            _RemoteLoader.InvokeMethod(assemblyFullName, className, methodName, args);
        }

        public void Unload()
        {
            try
            {
                AppDomain.Unload(_AppDomain);
                _AppDomain = null;
            }
            catch (CannotUnloadAppDomainException ex)
            {
                _logger.Error("Cannot Unload AppDomain Exception!", ex);
            }
        }
    }
}