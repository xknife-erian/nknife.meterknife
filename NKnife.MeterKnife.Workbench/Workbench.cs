using System.Windows.Forms;
using NKnife.MeterKnife.Base;
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
    public class Workbench : QuickForm
    {
        private readonly WorkbenchViewModel _viewModel;
        private readonly IHabitManager _habitManager;
        private readonly EngineeringView _engineeringView;
        private readonly SlotView _slotView;

        public Workbench(WorkbenchViewModel viewModel, IHabitManager habitManager, EngineeringView engineeringView,
            DebuggerManager debuggerManager, SlotView slotView)
        {
            _viewModel = viewModel;
            _habitManager = habitManager;
            _engineeringView = engineeringView;
            _slotView = slotView;
            GetHabitValueFunc = _habitManager.GetHabitValue;
            SetHabitAction = _habitManager.SetHabitValue;
            GetOptionValueFunc = _habitManager.GetOptionValue;
            SetOptionAction = _habitManager.SetOptionValue;
            GithubUpdateUser = "xknife-erian";
            GithubUpdateProject = "nknife.serial-protocol-debugger";

            Icon = Resources.meterknife_24px;
            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.meterknife_48px;
            BindNotifyIcon(notifyIcon);

            var fileMenuItem = BuildFileMenu();
            var dataMenuItem = BuildDataMenu();
            var measureMenuItem = BuildMeasureMenu();
            var toolMenuItem = BuildToolMenu();
            var viewMenuItem = BuildViewMenu();
            var helpMenuItem = BuildHelpMenu();
            BindMainMenu(fileMenuItem, dataMenuItem, measureMenuItem, toolMenuItem, viewMenuItem, helpMenuItem);
            BindTrayMenu(DebuggerManager.GetMockItem(), DebuggerManager.GetMockItem());
#if DEBUG
            BindMainMenu(debuggerManager.GetDebugMenu());
#endif
            OptionPanelList.AddRange(new IOptionPanel[]
            {
                new GeneralOptionPanel(), 
                new DataOptionPanel(),
                new PlotOptionPanel(), 
            });
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

        private DataMenuItem BuildDataMenu()
        {
            var dataMenu = new DataMenuItem();
            var dut = new ToolStripMenuItem(this.Res("被测物管理(&D)"));
            dataMenu.DropDownItems.Add(dut);
            return dataMenu;
        }

        private MeasureMenuItem BuildMeasureMenu()
        {
            var measureMenu = new MeasureMenuItem();
            var start = new ToolStripMenuItem(this.Res("启动"));
            start.ShortcutKeys = Keys.Control | Keys.F5;
            measureMenu.DropDownItems.Add(start);
            var pause = new ToolStripMenuItem(this.Res("暂停"));
            pause.ShortcutKeys = Keys.Control | Keys.F6;
            measureMenu.DropDownItems.Add(pause);
            var stop = new ToolStripMenuItem(this.Res("停止"));
            stop.ShortcutKeys = Keys.Control | Keys.F4;
            measureMenu.DropDownItems.Add(stop);
            return measureMenu;
        }

        private ToolMenuItem BuildToolMenu()
        {
            var toolMenu = new ToolMenuItem();
            toolMenu.DropDownItems.Insert(0, new ToolStripSeparator());

            var intMenu = new ToolStripMenuItem(this.Res("仪表管理(&I)"));
            toolMenu.DropDownItems.Insert(0, intMenu);
            var connMenu = new ToolStripMenuItem(this.Res("接驳器管理(&C)"));
            connMenu.Click += (sender, args) =>
            {
                _slotView.Show(MainDockPanel, DockState.DockLeft);
            };
            toolMenu.DropDownItems.Insert(0, connMenu);
            return toolMenu;
        }

        private ViewMenuItem BuildViewMenu()
        {
            var viewMenu = new ViewMenuItem();
            viewMenu.DropDownItems.Insert(0, new ToolStripSeparator());
            viewMenu.DropDownItems.Insert(0, BuildViewMenu_EngineeringManagerMenu());

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

        private ToolStripMenuItem BuildViewMenu_EngineeringManagerMenu()
        {
            var engManagerMenu = new ToolStripMenuItem(this.Res("工程管理(&E)")) {Checked = true};
            _engineeringView.Show(MainDockPanel, DockState.DockRight);
            engManagerMenu.Click += (s, e) =>
            {
                if (engManagerMenu.Checked)
                    _engineeringView.Close();
                else
                    _engineeringView.Show(MainDockPanel, DockState.DockRight);
                engManagerMenu.Checked = !engManagerMenu.Checked;
            };
            return engManagerMenu;
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

    }
}