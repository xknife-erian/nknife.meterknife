using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Kernel;
using NKnife.IoC;
using NKnife.Interface;
using NKnife.Util;
using NKnife.Win.Forms.Forms;

namespace MeterKnife.App
{
    internal class Surroundings : ApplicationContext
    {
        private static readonly ILog _Logger = LogManager.GetLogger<Surroundings>();

        private static BaseCareCommunicationService _careComm;

        /// <summary>所有启动项的集合，将在 Initialize() 函数中被初始化
        /// </summary>
        private static readonly List<IEnvironmentItem> _StarterItems = new List<IEnvironmentItem>();

        public static Form Workbench { get; set; }

        public Surroundings()
        {
            Initialize();
        }

        private void Initialize()
        {
            // 应用程序退出
            Application.ApplicationExit += OnApplicationExit;

            OptionInitializes();

            //开启欢迎屏幕
            Splasher.Show(typeof (SplashForm));
            Splasher.Status = "参数初始化进行中......";
            Thread.Sleep(200);
            Splasher.Status = "加载运行参数......";

            Splasher.Status = "参数初始化完成，启动主窗体";
            Thread.Sleep(200);

            //开启UI控制窗体
            if (Workbench == null)
                Workbench = new Workbench();
            Workbench.FormClosed += (s, e) => Application.Exit();
            Workbench.Activated += WorkbenchOnActivated;
            Workbench.Show();
            _Logger.Info("主窗体启动完成..");

            var thread = new Thread(BeginInitializeServices) {IsBackground = true};
            thread.Start();

            Workbench.Activate();
        }

        private void WorkbenchOnActivated(object sender, EventArgs eventArgs)
        {
            Splasher.Close();
            ((Form) sender).Activated -= WorkbenchOnActivated;
        }

        private void BeginInitializeServices()
        {
            _Logger.Info("寻找启动时的应用程序服务项..");

            Assembly ass = typeof (MeterKernel).Assembly;
            foreach (var type in ass.GetTypes())
            {
                if ((type.ContainsInterface(typeof(IEnvironmentItem)) && !type.IsAbstract))
                {
                    try
                    {
                        object obj = Activator.CreateInstance(type);
                        _StarterItems.Add((IEnvironmentItem)obj);
                    }
                    catch (Exception e)
                    {
                        _Logger.Warn($"寻找启动时的应用程序服务项异常.{e.Message}", e);
                    }
                }
            }
            string info = $"找到应用程序环境服务项{_StarterItems.Count}个。";
            _Logger.Info(info);
            if (_StarterItems.Count > 0)
            {
                //按定义的顺序进行排序
                _StarterItems.Sort((a,b) => b.Order - a.Order);
                _StarterItems.TrimExcess();
                //按顺序进行启动(初始化)
                for (int i = 0; i < _StarterItems.Count; i++)
                {
                    try
                    {
                        _StarterItems[i].StartService();
                        info = $"启动\"{_StarterItems[i].Description}\"服务完成。-- {i}";
                        _Logger.Info(info);
                    }
                    catch (Exception e)
                    {
                        _Logger.Warn($"应用程序服务项初始化异常.{e.Message}", e);
                    }
                }
            }
            _Logger.Info("应用程序环境的初始化完成。");
            _Logger.Info("启动Care通讯服务");
            _careComm = DI.Get<BaseCareCommunicationService>();
            _careComm.Initialize();
        }

        /// <summary>
        ///     应用程序退出
        /// </summary>
        private static void OnApplicationExit(object sender, EventArgs ex)
        {
            try
            {
                //处理程序退出前要处理的东西
                foreach (var item in _StarterItems)
                {
                    item.CloseService();
                }
                _careComm.Destroy();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                _Logger.Warn(e.Message);
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