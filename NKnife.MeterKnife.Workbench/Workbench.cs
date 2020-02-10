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
        private IHabitManager _habitManager;
        private MeasureView _measureView;

        public Workbench(IHabitManager habitManager, MeasureView measureView)
        {
            _habitManager = habitManager;
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

            BindMainMenu(file, new DataMenuItem(), new MeasureMenuItem(), new ToolMenuItem(), new ViewMenuItem(), new HelpMenuItem());
            BindTrayMenu(GetItem(), GetItem());
#if DEBUG
            BindMainMenu(GetDebugMenu());
#endif
        }

        private ToolStripMenuItem GetItem()
        {
            var t = new ToolStripMenuItem("Abc");
            t.Click += (s, e) => { MessageBox.Show("Test"); };
            return t;
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
#endif
    }
}
