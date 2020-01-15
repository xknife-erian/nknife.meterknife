using System;
using System.IO;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.NLog;
using Ninject.Modules;
using NKnife.NLog3.Controls;
using NKnife.NLog3.Controls.WPF;
using NKnife.NLog3.Properties;

namespace NKnife.NLog3.IoC
{
    public class NLogModules : NinjectModule
    {
        public enum AppStyle
        {
            WinForm,
            WPF
        }

        private const string CONFIG_FILE_NAME = "nlog.config";

        static NLogModules()
        {
            Style = AppStyle.WinForm;
        }

        public static AppStyle Style { get; set; }

        public override void Load()
        {
            //当发现程序目录中无NLog的配置文件时，根据程序的模式（WinForm或者WPF）自动释放不同NLog的配置文件
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);
            if (!File.Exists(file))
            {
                string configContent;
                switch (Style)
                {
                    case AppStyle.WPF:
                        configContent = OwnResources.nlog_wpf_config;
                        break;
                    default:
                        configContent = OwnResources.nlog_winform_config;
                        break;
                }
                using (StreamWriter write = File.CreateText(file))
                {
                    write.Write(configContent);
                    write.Flush();
                    write.Dispose();
                }
            }

            //配置Common.Logging适配器
            var properties = new NameValueCollection();
            properties["configType"] = "FILE";
            properties["configFile"] = "~/NLog.config";//string.Format("~/{0}", CONFIG_FILE_NAME);
            LogManager.Adapter = new NLogLoggerFactoryAdapter(properties);

            Bind<LoggerInfoDetailForm>().To<LoggerInfoDetailForm>().InSingletonScope();
            Bind<LogMessageFilter>().ToSelf().InSingletonScope();
        }
    }
}