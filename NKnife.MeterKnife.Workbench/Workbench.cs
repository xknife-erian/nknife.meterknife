using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Holistic;
using NKnife.MeterKnife.Resources;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.MeterKnife.Workbench.Debugs;
using NKnife.MeterKnife.Workbench.Menus;
using NKnife.MeterKnife.Workbench.Options;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.Win.Quick;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Menus;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench
{
    /// <summary>
    /// 应用程序主窗体
    /// </summary>
    public sealed class Workbench : QuickForm
    {
        private readonly WorkbenchViewModel _viewModel;
        private readonly IHabitManager _habitManager;

        /// <summary>
        /// 已打开历史数据查看面板字典，Key是工程ID，Value是数据查看窗体
        /// </summary>
        private readonly Dictionary<string, StaticDataPlotView> _staticDataPlotViewMap = new Dictionary<string, StaticDataPlotView>();

        public Workbench(WorkbenchViewModel viewModel, IHabitManager habitManager,
            DataMenuItem dataMenu, MeasureMenuItem measureMenu, EngineeringView engineeringView,
            DebuggerManager debuggerManager)
        {
            _viewModel = viewModel;
            _habitManager = habitManager;

            GetHabitValueFunc = _habitManager.GetHabitValue;
            SetHabitAction = _habitManager.SetHabitValue;
            GetOptionValueFunc = _habitManager.GetOptionValue;
            SetOptionAction = _habitManager.SetOptionValue;

            InitializeComponent();

            var about = new QuickAbout();
            Text = $"{about.AssemblyTitle} - {about.AssemblyVersion}";
            Icon = IconResource.app_32px;

            var notifyIcon = new NotifyIcon {Icon = IconResource.app_32px};
            BindNotifyIcon(notifyIcon);

            var fileMenuItem = BuildFileMenu();
            var helpMenuItem = BuildHelpMenu();
            BindMainMenu(fileMenuItem, dataMenu, measureMenu, helpMenuItem);
#if DEBUG
            HideOnClosing = false;
            BindMainMenu(debuggerManager.GetDebugMenu());
#endif
            OptionPanelList.AddRange(new IOptionPanel[]
            {
                new GeneralOptionPanel(),
                new DataOptionPanel(),
                new PlotOptionPanel(),
            });

            MainDockPanel.DockLeftPortion = 230;
            MainDockPanel.DockRightPortion = 280;
            MainDockPanel.DockBottomPortion = 170;

            RespondToViewModel();
            Shown += delegate { engineeringView.Show(MainDockPanel, DockState.DockRight); };
        }

        private FileMenuItem BuildFileMenu()
        {
            var fileMenuItem = new FileMenuItem(this);
            fileMenuItem.DropDownItems.Insert(0, new ToolStripSeparator());

            var newQuickEng = new ToolStripMenuItem(this.Res("新建快速工程..."));
            newQuickEng.ShortcutKeys = Keys.Control | Keys.N;
            fileMenuItem.DropDownItems.Insert(0, newQuickEng);
            var newEng = new ToolStripMenuItem(this.Res("新建工程(&N)..."));
            fileMenuItem.DropDownItems.Insert(0, newEng);
            return fileMenuItem;
        }

        private HelpMenuItem BuildHelpMenu()
        {
            var menu = new HelpMenuItem(this);
            // var culture = _habitManager.GetHabitValue(nameof(Global.Culture), Global.Culture);
            // var themeName = _habitManager.GetHabitValue("MainTheme", "VS2015BlueTheme");//nameof(VS2015BlueTheme));
            // menu.SetActiveCultureAtMenu(culture);
            // menu.SetActiveThemeAtMenu(themeName);
            //ActiveDockPanelTheme(themeName);
            return menu;
        }

        // private void ActiveDockPanelTheme(string themeName)
        // {
        //     switch (themeName)
        //     {
        //         case nameof(VS2015BlueTheme):
        //         default:
        //             MainDockPanel.Theme = new VS2015BlueTheme();
        //             break;
        //         case nameof(VS2015DarkTheme):
        //             MainDockPanel.Theme = new VS2015DarkTheme();
        //             break;
        //         case nameof(VS2015LightTheme):
        //             MainDockPanel.Theme = new VS2015LightTheme();
        //             break;
        //         case nameof(VS2013BlueTheme):
        //             MainDockPanel.Theme = new VS2013BlueTheme();
        //             break;
        //         case nameof(VS2013DarkTheme):
        //             MainDockPanel.Theme = new VS2013DarkTheme();
        //             break;
        //         case nameof(VS2013LightTheme):
        //             MainDockPanel.Theme = new VS2013LightTheme();
        //             break;
        //         case nameof(VS2012BlueTheme):
        //             MainDockPanel.Theme = new VS2012BlueTheme();
        //             break;
        //         case nameof(VS2012DarkTheme):
        //             MainDockPanel.Theme = new VS2012DarkTheme();
        //             break;
        //         case nameof(VS2012LightTheme):
        //             MainDockPanel.Theme = new VS2012LightTheme();
        //             break;
        //         case nameof(VS2005Theme):
        //             MainDockPanel.Theme = new VS2005Theme();
        //             break;
        //         case nameof(VS2005MultithreadingTheme):
        //             MainDockPanel.Theme = new VS2005MultithreadingTheme();
        //             break;
        //         case nameof(VS2003Theme):
        //             MainDockPanel.Theme = new VS2003Theme();
        //             break;
        //     }
        // }

        private void RespondToViewModel()
        {
            //打开已采集数据的窗口
            _viewModel.OpenedEngineerings.CollectionChanged += (sender, args) =>
            {
                var eng = _viewModel.OpenedEngineerings[args.NewStartingIndex];
                //每个工程只打开一个窗口
                if (!_staticDataPlotViewMap.TryGetValue(eng.Id, out var view) || view.IsDisposed)
                {
                    view = Kernel.Container.Resolve<StaticDataPlotView>();
                    view.SetEngineering(eng);
                    _staticDataPlotViewMap.Remove(eng.Id);
                    _staticDataPlotViewMap.Add(eng.Id, view);
                }

                view.Show(MainDockPanel, DockState.Document);
            };
            //打开正在采集数据工作中的窗口
            _viewModel.AcquiringEngineerings.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
            {
                var eng = _viewModel.AcquiringEngineerings[args.NewStartingIndex];
                var view = Kernel.Container.Resolve<RealTimeDataPlotView>();
                var sol = DUTSeriesStyleSolution.GetSolution(eng.CommandPools.ToArray());
                view.SetSolution(sol);
                view.Show(MainDockPanel, DockState.Document);
            };
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Workbench
            // 
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1440 , 1050); //1024,768; 1440,1050; 1600,1200;
            ResumeLayout(false);
        }
    }
}