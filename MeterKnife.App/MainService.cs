using System;
using System.IO;
using System.Reflection;
using NKnife.ControlKnife;

namespace MeterKnife.App
{
    class MainService
    {
        private static string Loader => "Pansoft.CQMS.Queue.Environments.EnvironmentDynamicLoader";

        private static string LoaderMethod => "InvokeMethod";

        private static string UnLoaderMethod => "Unload";

        private static string AppDomainFirst => "Pansoft.CQMS.Queue.Environments.Firster";

        private static string AppDomainFirstMethod => "RunMainMethod";

        private static string AppDomainMainAssembly => "Pansoft.CQMS.Queue.Environments.exe";

        private object _Service;

        public void Load(string[] args)
        {
            string assemblyname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomainMainAssembly);
            Splasher.Status = "准备加载服务工作域.";
            Assembly assembly = Assembly.LoadFile(assemblyname);
            _Service = assembly.CreateInstance(Loader);
            Splasher.Status = "域服务加载.";

            if (_Service == null)
                return;
            MethodInfo method = _Service.GetType().GetMethod(LoaderMethod);
            Splasher.Status = $"找到主域载入方法:{method.Name}";
            method.Invoke(_Service, new object[]
                                       {
                                           AppDomainMainAssembly,
                                           AppDomainFirst,
                                           AppDomainFirstMethod,
                                           args
                                       });
        }

        public void UnLoad()
        {
            MethodInfo method = _Service.GetType().GetMethod(UnLoaderMethod);
            method.Invoke(_Service, null);
        }
    }
}
