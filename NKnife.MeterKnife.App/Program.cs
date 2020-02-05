using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NKnife.MeterKnife.Storage.Db;
using NKnife.Win.Forms.Forms;
using NLog;

namespace NKnife.MeterKnife.App
{
    public class Program
    {
        internal static IContainer Container { get; private set; }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace(":-)");

            //开启欢迎屏幕
            Splasher.Show(typeof(SplashForm));
            FileCleaner.Run();

            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions().Configure<StorageSetting>(conf.GetSection(nameof(StorageSetting)));

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);

            builder.RegisterAssemblyModules(typeof(Logic.Global).Assembly);
            builder.RegisterAssemblyModules(typeof(Workbench.Workbench).Assembly);
            builder.RegisterType<Startup>().AsSelf();

            using (Container = builder.Build())
            {
                var startup = Container.Resolve<Startup>();
                //开启当前程序作用域下的 ApplicationContext 实例
                Application.Run(startup);
            }
        }
    }
}