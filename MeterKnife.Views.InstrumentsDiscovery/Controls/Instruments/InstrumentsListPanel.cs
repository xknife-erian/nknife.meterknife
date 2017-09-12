using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls.Instruments
{
    public partial class InstrumentsListPanel : UserControl
    {
        private InstrumentCell[] _HideCells;
        private bool _IsExpanded = true;

        public InstrumentsListPanel(IGatewayDiscover gatewayDiscover)
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            ContextMenuEventManager();

            _DropToolStripMenuItem.Enabled = false;
            _DropToolStripMenuItem.CheckState = CheckState.Checked;
            _ListHead.HeadMouseClicked += OnHeadMouseClicked;
            _ListHead.GatewayModel = gatewayDiscover.GatewayModel.ToString();

            //-----------------------------------------------------
            Tag = gatewayDiscover;
        }

        public int Count
        {
            get => _ListHead.Count;
            set => _ListHead.Count = value;
        }

        private void OnHeadMouseClicked(object sender, HeadClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    DropPanel();
                    break;
                }
                case MouseButtons.Right:
                {
                    var p = PointToScreen(_ListHead.Location);
                    foreach (var item in _HeadContextMenu.Items)
                    {
                        var menuitem = item as ToolStripMenuItem;
                        if (menuitem != null)
                            menuitem.Tag = e.GatewayModel;
                    }
                    _HeadContextMenu.Show(_ListHead, new Point(e.Location.X - p.X, e.Location.Y - p.Y));
                    break;
                }
            }
        }

        private void DropPanel()
        {
            _IsExpanded = !_IsExpanded;
            if (!_IsExpanded)
            {
                _DropToolStripMenuItem.Enabled = true;
                _UnDropToolStripMenuItem.Enabled = false;
                _DropToolStripMenuItem.CheckState = CheckState.Unchecked;
                _UnDropToolStripMenuItem.CheckState = CheckState.Checked;

                var count = Controls.Count - 1;
                _HideCells = new InstrumentCell[count];
                SuspendLayout();
                for (var i = 0; i < count; i++)
                {
                    _HideCells[i] = (InstrumentCell) Controls[0];
                    Height = Height - _HideCells[i].Height;
                    Controls.RemoveAt(0);
                }
                ResumeLayout(true);
            }
            else
            {
                _DropToolStripMenuItem.Enabled = false;
                _UnDropToolStripMenuItem.Enabled = true;
                _DropToolStripMenuItem.CheckState = CheckState.Checked;
                _UnDropToolStripMenuItem.CheckState = CheckState.Unchecked;

                if (_HideCells != null)
                    AddCells(_HideCells);
            }
        }

        private void OnCellMouseClicked(object sender, CellClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    OnInstrumentSelected(sender, e);
                    break;
                }
                case MouseButtons.Right:
                {
                    var sd = (Control) sender;
                    var p = PointToScreen(sd.Location);
                    foreach (var item in _CellContextMenu.Items)
                    {
                        var menuitem = item as ToolStripMenuItem;
                        if (menuitem != null)
                            menuitem.Tag = e.Instrument;
                    }
                    _CellContextMenu.Show(sd, new Point(e.Location.X - p.X, e.Location.Y - p.Y));
                    break;
                }
            }
        }

        public void AddInstruments(params Instrument[] instruments)
        {
            var cells = new InstrumentCell[instruments.Length];
            for (var i = 0; i < instruments.Length; i++)
            {
                var cell = new InstrumentCell(instruments[i]);
                cell.Dock = DockStyle.Top;
                cells[i] = cell;
            }
            AddCells(cells);
        }

        public void RemoveInstrument(Instrument inst)
        {
            SuspendLayout();
            var cs = new List<Control>(Controls.Count);
            foreach (Control control in Controls)
            {
                var cell = control as InstrumentCell;
                if (cell != null)
                {
                    var instrument = (Instrument) cell.Tag;
                    if (inst.Equals(instrument))
                        cs.Add(control);
                }
            }
            foreach (var control in cs)
            {
                Height -= control.Height;
                Controls.Remove(control);
            }
            ResumeLayout(true);
        }

        /// <summary>
        ///     因采用了Dock.Top属性控制，故需要调整控件加入到Controls中的顺序，以保证一般设计思路中的后添加的控件在最下方显示。
        ///     Controls集合的没有Insert方法,很麻烦。
        /// </summary>
        /// <param name="cells"></param>
        public void AddCells(params InstrumentCell[] cells)
        {
            SuspendLayout();
            var height = 0;
            var cs = new Control[Controls.Count];
            Controls.CopyTo(cs, 0); //先将原有的控件倒出来
            Controls.Clear();
            for (var i = cells.Length - 1; i >= 0; i--)
            {
                var cell = cells[i];
                cell.CellMouseClicked += OnCellMouseClicked;
                Controls.Add(cell); //倒序将新的控件放入
                height += cell.Height; //根据当前内部的控件数量，计算出整体应该有的高度
            }
            foreach (var control in cs)
            {
                Controls.Add(control); //原来的控件
                height += control.Height + 1; //根据当前内部的控件数量，计算出整体应该有的高度
            }
            Height = height + 4;
            ResumeLayout(true);
        }

        /// <summary>
        ///     右键菜单的事件管理
        /// </summary>
        private void ContextMenuEventManager()
        {
            _DropToolStripMenuItem.Click += (s, e) => { DropPanel(); };
            _UnDropToolStripMenuItem.Click += (s, e) => { DropPanel(); };

            _RefreshInstrumentsStateByGatewayToolStripMenuItem.Click += (s, e) => { OnGatewayModelRefreshInstrumentsState(s); };
            _DeleteGatewayToolStripMenuItem.Click += (s, e) => { OnGatewayModelDelete(s); };

            _DeleteInstrumentToolStripMenuItem.Click += (s, e) => { OnInstrumentDelete(s); };
            _ConnTestToolStripMenuItem.Click += (s, e) => { OnInstrumentConnectionTest(s); };
            _CommandsToolStripMenuItem.Click += (s, e) => { OnInstrumentCommandManager(s); };
            _DatasManagerToolStripMenuItem.Click += (s, e) => { OnInstrumentDatasManager(s); };
        }

        public event EventHandler GatewayModelRefreshInstrumentsState;
        public event EventHandler GatewayModelDelete;
        public event EventHandler InstrumentDelete;
        public event EventHandler InstrumentConnectionTest;
        public event EventHandler InstrumentCommandManager;
        public event EventHandler InstrumentDatasManager;
        public event EventHandler<CellClickEventArgs> InstrumentSelected;

        protected virtual void OnGatewayModelRefreshInstrumentsState(object sender)
        {
            GatewayModelRefreshInstrumentsState?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnGatewayModelDelete(object sender)
        {
            GatewayModelDelete?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnInstrumentDelete(object sender)
        {
            InstrumentDelete?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnInstrumentConnectionTest(object sender)
        {
            InstrumentConnectionTest?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnInstrumentCommandManager(object sender)
        {
            InstrumentCommandManager?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnInstrumentDatasManager(object sender)
        {
            InstrumentDatasManager?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnInstrumentSelected(object sender, CellClickEventArgs e)
        {
            InstrumentSelected?.Invoke(sender, e);
        }
    }
}