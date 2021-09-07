using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Menus;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Menus
{
    public sealed class DataMenuItem : ToolStripMenuItem
    {
        private readonly ProjectView _projectView;
        private readonly InstrumentView _instrumentView;
        private readonly SlotView _slotView;
        private readonly DUTView _dutView;

        public DataMenuItem(SlotView slotView, ProjectView projectView, InstrumentView instrumentView, DUTView dutView)
        {
            _slotView = slotView;
            _projectView = projectView;
            _instrumentView = instrumentView;
            _dutView = dutView;
            Text = this.Res("数据(&D)");

            var eng = BuildEngineeringManagerMenu();
            DropDownItems.Add(eng);

            var instMenu = BuildInstrumentManagerMenu();
            DropDownItems.Add(instMenu);

            var connMenu = BuildSlotManagerMenu();
            DropDownItems.Add(connMenu);
            DropDownItems.Add(new ToolStripSeparator());

            var dut = BuildDUTManagerMenu();
            DropDownItems.Add(dut);
        }

        private ToolStripMenuItem BuildInstrumentManagerMenu()
        {
            var instMenu = new ToolStripMenuItem(this.Res("仪表管理(&I)"));
            instMenu.ShortcutKeys = Keys.F4;
            instMenu.Click += (sender, args) =>
            {
                IWorkbench wb = this.GetWorkbench();
                if (wb == null) return;
                if (instMenu.Checked)
                    _instrumentView.Close();
                else
                    _instrumentView.Show(wb.MainDockPanel, DockState.DockBottom);
                instMenu.Checked = !instMenu.Checked;
            };
            return instMenu;
        }

        private ToolStripMenuItem BuildSlotManagerMenu()
        {
            var slotMenu = new ToolStripMenuItem(this.Res("接驳器管理(&C)"));
            slotMenu.ShortcutKeys = Keys.F8;
            slotMenu.Click += (sender, args) =>
            {
                IWorkbench wb = this.GetWorkbench();
                if (wb == null) return;
                if (slotMenu.Checked)
                    _slotView.Close();
                else
                    _slotView.Show(wb.MainDockPanel, DockState.DockLeft);
                slotMenu.Checked = !slotMenu.Checked;
            };
            return slotMenu;
        }

        private ToolStripMenuItem BuildEngineeringManagerMenu()
        {
            var engManagerMenu = new ToolStripMenuItem(this.Res("工程管理(&E)"));
            engManagerMenu.ShortcutKeys = Keys.F7;
            engManagerMenu.Click += (s, e) =>
            {
                IWorkbench wb = this.GetWorkbench();
                if (wb == null) return;
                if (!engManagerMenu.Checked)
                    _projectView.Show(wb.MainDockPanel, DockState.DockRight);
                else
                    _projectView.Close();
                engManagerMenu.Checked = !engManagerMenu.Checked;
            };
            return engManagerMenu;
        }


        private ToolStripMenuItem BuildDUTManagerMenu()
        {
            var dutMenu = new ToolStripMenuItem(this.Res("被测物管理(&D)"));
            dutMenu.ShortcutKeys = Keys.F6;
            dutMenu.Click += (sender, args) =>
            {
                IWorkbench wb = this.GetWorkbench();
                if (wb == null) return;
                if (dutMenu.Checked)
                    _dutView.Close();
                else
                    _dutView.Show(wb.MainDockPanel, DockState.Document);
                dutMenu.Checked = !dutMenu.Checked;
            };
            return dutMenu;
        }
    }
}
