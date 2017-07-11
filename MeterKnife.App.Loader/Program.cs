using System.Windows.Forms;
using Common.Logging;
using NKnife.IoC;

namespace MeterKnife.App
{
    public class Program
    {
        /// <summary>
        /// 本方法将被Starter项目通过反射加载调用。
        /// </summary>
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.Initialize();

            ILog logger = LogManager.GetLogger<Application>();
            logger.Info("IOC框架的初始化完成。");

            //开启当前程序作用域下的 ApplicationContext 实例
            Application.Run(EnvironmentKernel.Instance(args));
        }
    }
}