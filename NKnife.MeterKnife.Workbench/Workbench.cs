using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Resources;
using NKnife.MeterKnife.ViewModels;
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

        public Workbench(WorkbenchViewModel viewModel, IHabitManager habitManager, 
            DataMenuItem dataMenu, MeasureMenuItem measureMenu,
            DebuggerManager debuggerManager)
        {
            _viewModel = viewModel;
            _habitManager = habitManager;

            GetHabitValueFunc = _habitManager.GetHabitValue;
            SetHabitAction = _habitManager.SetHabitValue;
            GetOptionValueFunc = _habitManager.GetOptionValue;
            SetOptionAction = _habitManager.SetOptionValue;
            GithubUpdateUser = "xknife-erian";
            GithubUpdateProject = "nknife.serial-protocol-debugger";

            InitializeComponent();
            var about = new QuickAbout();
            Text = $"{about.AssemblyTitle} - {about.AssemblyVersion}";
            Icon = IconResource.app_32px;

            var notifyIcon = new NotifyIcon {Icon = IconResource.app_32px };
            BindNotifyIcon(notifyIcon);

            var fileMenuItem = BuildFileMenu();
            var toolMenuItem = BuildToolMenu();
            var viewMenuItem = BuildViewMenu();
            var helpMenuItem = BuildHelpMenu();
            BindMainMenu(fileMenuItem, dataMenu, measureMenu, toolMenuItem, viewMenuItem, helpMenuItem);
#if DEBUG
            BindMainMenu(debuggerManager.GetDebugMenu());
#endif
            OptionPanelList.AddRange(new IOptionPanel[]
            {
                new GeneralOptionPanel(), 
                new DataOptionPanel(),
                new PlotOptionPanel(), 
            });

            MainDockPanel.DockLeftPortion = 115;
            MainDockPanel.DockRightPortion = 280;
            MainDockPanel.DockBottomPortion = 175;
        }

        private FileMenuItem BuildFileMenu()
        {
            var fileMenuItem = new FileMenuItem();
            fileMenuItem.DropDownItems.Insert(0, new ToolStripSeparator());

            var newQuickEng = new ToolStripMenuItem(this.Res("新建快速测量..."));
            newQuickEng.ShortcutKeys = Keys.Control | Keys.N;
            fileMenuItem.DropDownItems.Insert(0, newQuickEng);
            var newEng = new ToolStripMenuItem(this.Res("新建测量(&N)..."));
            fileMenuItem.DropDownItems.Insert(0, newEng);
            return fileMenuItem;
        }

        private ToolMenuItem BuildToolMenu()
        {
            var toolMenu = new ToolMenuItem();
            return toolMenu;
        }

        private ViewMenuItem BuildViewMenu()
        {
            var viewMenu = new ViewMenuItem();
            var culture = _habitManager.GetHabitValue(nameof(Global.Culture), Global.Culture);
            var themeName = _habitManager.GetHabitValue("MainTheme", nameof(VS2015BlueTheme));
            viewMenu.SetActiveCulture(culture);
            viewMenu.SetActiveTheme(themeName);
            ActiveDockPanelTheme(themeName);
            return viewMenu;
        }

        private HelpMenuItem BuildHelpMenu()
        {
            var dataMenu = new HelpMenuItem();
            return dataMenu;
        }

        private void ActiveDockPanelTheme(string themeName)
        {
            switch (themeName)
            {
                case nameof(VS2015BlueTheme):
                default:
                    MainDockPanel.Theme = new VS2015BlueTheme();
                    break;
                case nameof(VS2015DarkTheme):
                    MainDockPanel.Theme = new VS2015DarkTheme();
                    break;
                case nameof(VS2015LightTheme):
                    MainDockPanel.Theme = new VS2015LightTheme();
                    break;
                case nameof(VS2013BlueTheme):
                    MainDockPanel.Theme = new VS2013BlueTheme();
                    break;
                case nameof(VS2013DarkTheme):
                    MainDockPanel.Theme = new VS2013DarkTheme();
                    break;
                case nameof(VS2013LightTheme):
                    MainDockPanel.Theme = new VS2013LightTheme();
                    break;
                case nameof(VS2012BlueTheme):
                    MainDockPanel.Theme = new VS2012BlueTheme();
                    break;
                case nameof(VS2012DarkTheme):
                    MainDockPanel.Theme = new VS2012DarkTheme();
                    break;
                case nameof(VS2012LightTheme):
                    MainDockPanel.Theme = new VS2012LightTheme();
                    break;
                case nameof(VS2005MultithreadingTheme):
                    MainDockPanel.Theme = new VS2005MultithreadingTheme();
                    break;
                case nameof(VS2005Theme):
                    MainDockPanel.Theme = new VS2005Theme();
                    break;
                case nameof(VS2003Theme):
                    MainDockPanel.Theme = new VS2003Theme();
                    break;
            }
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