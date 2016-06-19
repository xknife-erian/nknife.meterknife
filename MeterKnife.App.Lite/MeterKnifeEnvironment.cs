using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.App.Professional;
using MeterKnife.Common.Base;
using MeterKnife.Kernel;
using MeterKnife.Workbench;
using NKnife.GUI.WinForm;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.App.Lite
{
    internal class MeterKnifeEnvironment : ApplicationContext
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterKnifeEnvironment>();

        private static BaseCareCommunicationService _careComm;

        /// <summary>
        ///     所有启动项的集合，将在 Initialize() 函数中被初始化
        /// </summary>
        private static readonly List<IEnvironmentItem> _starterItems = new List<IEnvironmentItem>();

        public MeterKnifeEnvironment()
        {
            Initialize();
        }

        public static Form Workbench { get; set; }

        public void Initialize()
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
                Workbench = new MainWorkbench();
            Workbench.FormClosed += (s, e) => Application.Exit();
            Workbench.Activated += WorkbenchOnActivated;
            Workbench.Show();
            _logger.Info("主窗体启动完成..");

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
            _logger.Info("寻找启动时的应用程序服务项..");

            var asse = typeof (MeterKernel).Assembly;
            foreach (var type in asse.GetTypes())
            {
                if ((type.ContainsInterface(typeof (IEnvironmentItem)) && !type.IsAbstract))
                {
                    try
                    {
                        var obj = Activator.CreateInstance(type);
                        _starterItems.Add((IEnvironmentItem) obj);
                    }
                    catch (Exception e)
                    {
                        _logger.Warn(string.Format("寻找启动时的应用程序服务项异常.{0}", e));
                    }
                }
            }
            var info = string.Format("找到应用程序环境服务项{0}个。", _starterItems.Count);
            _logger.Info(info);
            if (_starterItems.Count > 0)
            {
                //按定义的顺序进行排序
                _starterItems.Sort((a, b) => b.Order - a.Order);
                _starterItems.TrimExcess();
                //按顺序进行启动(初始化)
                for (var i = 0; i < _starterItems.Count; i++)
                {
                    try
                    {
                        _starterItems[i].StartService();
                        info = string.Format("启动\"{0}\"服务完成。-- {1}", _starterItems[i].Description, i);
                        _logger.Info(info);
                    }
                    catch (Exception e)
                    {
                        _logger.Warn(string.Format("应用程序服务项初始化异常.{0}", e));
                    }
                }
            }
            _logger.Info("应用程序环境的初始化完成。");
            _logger.Info("启动Care通讯服务");
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
                foreach (var item in _starterItems)
                {
                    item.CloseService();
                }
                _careComm.Destroy();
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