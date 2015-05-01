using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Workbench;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Starter
{
    internal class MeterKnifeEnvironment : ApplicationContext
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterKnifeEnvironment>();

        public MeterKnifeEnvironment()
        {
            Initialize();
        }

        public void Initialize()
        {
            // 应用程序退出
            Application.ApplicationExit += OnApplicationExit;

            OptionInitializes();

            //开启欢迎屏幕
            Splasher.Show(typeof(SplashForm));
            Splasher.Status = "参数初始化进行中......";
            Thread.Sleep(200);
            Splasher.Status = "加载运行参数......";

            InitializeServices();

            Splasher.Status = "参数初始化完成，启动主窗体";
            Thread.Sleep(200);

            //开启UI控制窗体
            var workbench = new MainWorkbench();
            workbench.FormClosed += (s, e) => Application.Exit();
            workbench.Activated += (s, e) =>
            {
                Splasher.Close();
            };
            workbench.Show();
            workbench.Activate();
        }

        public void InitializeServices()
        {
        }

        /// <summary>
        ///     应用程序退出
        /// </summary>
        private static void OnApplicationExit(object sender, EventArgs ex)
        {
            try
            {
                //处理程序退出前要处理的东西
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        /// <summary>
        ///     初始化“选项”服务
        /// </summary>
        /// <returns></returns>
        private bool OptionInitializes()
        {
            return true;
        }
    }
}