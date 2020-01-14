using System;
using System.Windows.Forms;
using Common.Logging;
using NKnife.IoC;

namespace NKnife.Domains
{
    public class AppDomainFirster
    {
        public Func<string[], ApplicationContext> Context { get; set; }

        /// <summary>
        /// 本方法将被Starter项目通过反射加载调用。
        /// </summary>
        public virtual void RunMainMethod(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.Initialize();

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("IoC框架的初始化完成。");

            //开启当前程序作用域下的 ApplicationContext 实例
            Application.Run(Context.Invoke(args));
        }
    }
}