using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.MeterKnife.Workbench.Menus;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick;
using NKnife.Win.Quick.Menus;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench
{
    public class Workbench : QuickForm
    {
        private readonly IHabitManager _habitManager;
        private MeasureView _measureView;

        public Workbench(IHabitManager habitManager, MeasureView measureView)
        {
            _habitManager = habitManager;
            GetHabitValueFunc = _habitManager.GetHabitValue;
            SetHabitAction = _habitManager.SetHabitValue;
            GetOptionValueFunc = _habitManager.GetOptionValue;
            SetOptionAction = _habitManager.SetOptionValue;
            _measureView = measureView;
            GithubUpdateUser = "xknife-erian";
            GithubUpdateProject = "nknife.serial-protocol-debugger";
            Icon = Resources.meterknife_24px;
            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.meterknife_24px;
            BindNotifyIcon(notifyIcon);

            var file = new FileMenuItem();
            file.DropDownItems.Insert(0, new ToolStripSeparator());
            file.DropDownItems.Insert(0, GetItem());

            var themeName = habitManager.GetHabitValue("MainTheme", nameof(VS2015BlueTheme));
            var viewMenu = new ViewMenuItem();
            viewMenu.SetActiveTheme(themeName);
            ActiveDockPanelTheme(themeName);
            BindMainMenu(file, new DataMenuItem(), new MeasureMenuItem(), new ToolMenuItem(), viewMenu, new HelpMenuItem());
            BindTrayMenu(GetItem(), GetItem());
#if DEBUG
            BindMainMenu(GetDebugMenu());
#endif
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

#if DEBUG
        private ToolStripMenuItem GetDebugMenu()
        {
            var debug = new ToolStripMenuItem("Debug");
            var plot = new ToolStripMenuItem("Plot");
            plot.Click += (s, e) =>
            {
                _measureView.Show(MainDockPanel, DockState.Document);
            };
            debug.DropDownItems.Add(plot);
            return debug;
        }

        private ToolStripMenuItem GetItem()
        {
            var t = new ToolStripMenuItem("Abc");
            t.Click += (s, e) => { MessageBox.Show("Test"); };
            return t;
        }
#endif
    }
}
