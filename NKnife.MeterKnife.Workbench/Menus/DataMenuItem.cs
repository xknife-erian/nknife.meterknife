using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Menus;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Menus
{
    public sealed class DataMenuItem : ToolStripMenuItem
    {
        private readonly EngineeringView _engineeringView;
        private readonly InstrumentView _instrumentView;
        private readonly SlotView _slotView;

        public DataMenuItem(SlotView slotView, EngineeringView engineeringView, InstrumentView instrumentView)
        {
            _slotView = slotView;
            _engineeringView = engineeringView;
            _instrumentView = instrumentView;
            Text = this.Res("数据(&D)");

            var eng = BuildEngineeringManagerMenu();
            DropDownItems.Add(eng);

            var instMenu = BuildInstrumentManagerMenu();
            DropDownItems.Add(instMenu);

            var connMenu = BuildSlotManagerMenu();
            DropDownItems.Add(connMenu);
            DropDownItems.Add(new ToolStripSeparator());

            var dut = new ToolStripMenuItem(this.Res("被测物管理(&D)"));
            DropDownItems.Add(dut);
        }

        private ToolStripMenuItem BuildInstrumentManagerMenu()
        {
            var instMenu = new ToolStripMenuItem(this.Res("仪表管理(&I)"));
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
            engManagerMenu.Click += (s, e) =>
            {
                IWorkbench wb = this.GetWorkbench();
                if (wb == null) return;
                if (engManagerMenu.Checked)
                    _engineeringView.Close();
                else
                    _engineeringView.Show(wb.MainDockPanel, DockState.DockRight);
                engManagerMenu.Checked = !engManagerMenu.Checked;
            };
            return engManagerMenu;
        }
    }
}
