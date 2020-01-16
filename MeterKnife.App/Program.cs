using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Autofac;
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
            var sources = UtilAssembly.SearchAssemblyByDirectory(Application.StartupPath);
            var assemblies = new List<Assembly>();
            foreach (var assembly in sources)
            {
                if(assembly.FullName.Contains("MeterKnife"))
                    assemblies.Add(assembly);
            }
            builder.RegisterAssemblyModules(assemblies.ToArray());
            builder.RegisterType<Surroundings>().AsSelf().SingleInstance();
            builder.RegisterType<Workbench>().AsSelf().SingleInstance();

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
