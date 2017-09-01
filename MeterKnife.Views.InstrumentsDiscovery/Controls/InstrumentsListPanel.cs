using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Models;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsListPanel : UserControl
    {
        private bool _IsExpanded = true;
        private InstrumentsCell[] _Cells;

        public InstrumentsListPanel()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            _ListHead.HeadLeftMouseClicked += OnHeadLeftMouseClicked;
            _ListHead.MouseClick += ListHeadMouseClick;
        }

        private void ListHeadMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _HeadContextMenu.Show(_HeadContextMenu, e.Location);
            }
        }

        private void OnHeadLeftMouseClicked(object s, EventArgs e)
        {
            _IsExpanded = !_IsExpanded;
            if (!_IsExpanded)
            {
                var count = Controls.Count - 1;
                _Cells = new InstrumentsCell[count];
                SuspendLayout();
                for (int i = 0; i < count; i++)
                {
                    _Cells[i] = (InstrumentsCell) Controls[0];
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
            for (int i = 0; i < instruments.Length; i++)
            {
                var cell = new InstrumentsCell();
                cell.Dock = DockStyle.Top;
                cell.SetInstruments(instruments[i]);
                cells[i] = cell;
            }
            AddCells(cells);
        }

        /// <summary>
        /// 因采用了Dock.Top属性控制，故需要调整控件加入到Controls中的顺序，以保证一般设计思路中的后添加的控件在最下方显示。
        /// Controls集合的没有Insert方法,很麻烦。
        /// </summary>
        /// <param name="cells"></param>
        public void AddCells(params InstrumentsCell[] cells)
        {
            SuspendLayout();
            int height = 0;
            var cs = new Control[Controls.Count];
            Controls.CopyTo(cs, 0); //先将原有的控件倒出来
            Controls.Clear();
            for (int i = cells.Length - 1; i >= 0; i--)
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
            List<Control> cs = new List<Control>(Controls.Count);
            foreach (Control control in Controls)
            {
                var cell = control as InstrumentsCell;
                if (cell != null)
                {
                    Instrument instrument = (Instrument) cell.Tag;
                    if(inst.Equals(instrument))
                        cs.Add(control);
                }
            }
            foreach (var control in cs)
            {
                Controls.Remove(control);
            }
            ResumeLayout(true);
        }
    }
}