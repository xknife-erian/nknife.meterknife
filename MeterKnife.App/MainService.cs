using System;
using System.IO;
using System.Reflection;
using NKnife.ControlKnife;

namespace MeterKnife.App
{
    class MainService
    {
        private static string Loader => $"{typeof(MainService).Namespace}.EnvironmentDynamicLoader";
        private static string LoaderMethod => "InvokeMethod";
        private static string UnLoaderMethod => "Unload";

        private static string AppDomainMainAssembly => $"{typeof(MainService).Namespace}.Loader.dll";
        private static string AppDomainFirst => $"{typeof(MainService).Namespace}.Program";
        private static string AppDomainFirstMethod => "Main";

        private object _Service;

        public void Load(string[] args)
        {
            // 通过反射加载主程序启动点
            string assemblyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomainMainAssembly);
            if (!File.Exists(assemblyFile))
                return;
            Splasher.Status = "准备加载服务工作域.";
            Assembly assembly = Assembly.LoadFile(assemblyFile);
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
