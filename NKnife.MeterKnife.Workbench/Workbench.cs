using System.Windows.Forms;
using NKnife.MeterKnife.Base;
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
        private readonly IHabitManager _habitManager;
        private readonly EngineeringView _engineeringView;

        public Workbench(IHabitManager habitManager, EngineeringView engineeringView,
            DebuggerManager debuggerManager)
        {
            _habitManager = habitManager;
            _engineeringView = engineeringView;
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

            var fileMenuItem = new FileMenuItem();
            fileMenuItem.DropDownItems.Insert(0, new ToolStripSeparator());
            fileMenuItem.DropDownItems.Insert(0, DebuggerManager.GetMockItem());

            var viewMenuItem = BuildViewMenu();
            BindMainMenu(fileMenuItem, new DataMenuItem(), new MeasureMenuItem(), new ToolMenuItem(), viewMenuItem, new HelpMenuItem());
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