using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Autofac;
using NKnife.IoC;
using NKnife.Util;

namespace MeterKnife.App
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.Properties.Settings.Default.CultureInfoName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var builder = new ContainerBuilder();
            var assemblies = UtilAssembly.SearchAssemblyByDirectory(Application.StartupPath);
            builder.RegisterAssemblyModules(assemblies);

            using (var container = builder.Build())
            {
                var surroundings = container.Resolve<Surroundings>();
                Surroundings.Workbench = container.Resolve<Workbench>();
                //开启当前程序作用域下的 ApplicationContext 实例
                Application.Run(surroundings);
            }
        }
    }
}
