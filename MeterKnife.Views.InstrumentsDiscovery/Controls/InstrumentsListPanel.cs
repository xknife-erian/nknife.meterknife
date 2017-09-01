using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Models;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsListPanel : UserControl
    {
        private InstrumentsCell[] _Cells;
        private bool _IsExpanded = true;

        public InstrumentsListPanel()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            _ListHead.HeadMouseClicked += OnHeadMouseClicked;
        }

        public string GatewayModel
        {
            get => _ListHead.GatewayModel;
            set => _ListHead.GatewayModel = value;
        }

        public int Count
        {
            get => _ListHead.Count;
            set => _ListHead.Count = value;
        }

        public void AddInstruments(params Instrument[] instruments)
        {
            var cells = new InstrumentsCell[instruments.Length];
            for (var i = 0; i < instruments.Length; i++)
            {
                var cell = new InstrumentsCell();
                cell.Dock = DockStyle.Top;
                cell.SetInstruments(instruments[i]);
                cell.CellMouseClicked += OnCellMouseClicked;
                cells[i] = cell;
            }
            AddCells(cells);
        }

        private void OnHeadMouseClicked(object s, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    _IsExpanded = !_IsExpanded;
                    if (!_IsExpanded)
                    {
                        var count = Controls.Count - 1;
                        _Cells = new InstrumentsCell[count];
                        SuspendLayout();
                        for (var i = 0; i < count; i++)
                        {
                            _Cells[i] = (InstrumentsCell)Controls[0];
                            Height = Height - _Cells[i].Height;
                            Controls.RemoveAt(0);
                        }
                        ResumeLayout(true);
                    }
                    else
                    {
                        if (_Cells != null)
                            AddCells(_Cells);
                    }
                    break;
                }
                case MouseButtons.Right:
                {
                    var p = PointToScreen(_ListHead.Location);
                    _HeadContextMenu.Show(_ListHead, new Point(e.Location.X - p.X, e.Location.Y - p.Y));
                    break;
                }
            }
        }

        private void OnCellMouseClicked(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    var sd = (Control) sender;
                    var p = PointToScreen(sd.Location);
                    _CellContextMenu.Show(sd, new Point(e.Location.X - p.X, e.Location.Y - p.Y));
                    break;
                }
            }
        }

        /// <summary>
        ///     因采用了Dock.Top属性控制，故需要调整控件加入到Controls中的顺序，以保证一般设计思路中的后添加的控件在最下方显示。
        ///     Controls集合的没有Insert方法,很麻烦。
        /// </summary>
        /// <param name="cells"></param>
        public void AddCells(params InstrumentsCell[] cells)
        {
            SuspendLayout();
            var height = 0;
            var cs = new Control[Controls.Count];
            Controls.CopyTo(cs, 0); //先将原有的控件倒出来
            Controls.Clear();
            for (var i = cells.Length - 1; i >= 0; i--)
            {
                var cell = cells[i];
                Controls.Add(cell); //倒序将新的控件放入
                height += cell.Height; //根据当前内部的控件数量，计算出整体应该有的高度
            }
            foreach (var control in cs)
            {
                Controls.Add(control); //原来的控件
                height += control.Height + 1; //根据当前内部的控件数量，计算出整体应该有的高度
            }
            Height = height + 3;
            ResumeLayout(true);
        }

        public void RemoveInstruments(Instrument inst)
        {
            SuspendLayout();
            var cs = new List<Control>(Controls.Count);
            foreach (Control control in Controls)
            {
                var cell = control as InstrumentsCell;
                if (cell != null)
                {
                    var instrument = (Instrument) cell.Tag;
                    if (inst.Equals(instrument))
                        cs.Add(control);
                }
            }
            foreach (var control in cs)
                Controls.Remove(control);
            ResumeLayout(true);
        }
    }
}