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
            _ListHead.HeadClicked += OnListHeadOnHeadClicked;
        }

        private void OnListHeadOnHeadClicked(object s, EventArgs e)
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
                    Controls.RemoveAt(0);
                }
                ResumeLayout(false);
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

        public void AddInstruments(params Instrument[] instruments)
        {
            var cells = new InstrumentsCell[instruments.Length];
            for (int i = 0; i < instruments.Length; i++)
            {
                var cell = new InstrumentsCell();
                cell.SetInstruments(instruments[i]);
                cells[i] = cell;
            }
            AddCells(cells);
        }

        /// <summary>
        /// 因采用了Dock.Top属性控制，故需要调整控件加入到Controls中的顺序，以保证一般设计思路中的后添加的控件在最下方显示。
        /// </summary>
        /// <param name="cells"></param>
        public void AddCells(params InstrumentsCell[] cells)
        {
            SuspendLayout();
            var cs = new Control[Controls.Count];
            Controls.CopyTo(cs, 0);
            Controls.Clear();
            for (int i = cells.Length - 1; i >= 0; i--)
            {
                Controls.Add(cells[i]);
            }
            foreach (var control in cs)
            {
                Controls.Add(control);
            }
            ResumeLayout(false);
        }

        #region Overrides of ScrollableControl

        /// <summary>计算到指定子控件的滚动偏移量。</summary>
        /// <returns>显示区域的左上 <see cref="T:System.Drawing.Point" />（相对于将控件滚动到视图所需的工作区）。</returns>
        /// <param name="activeControl">要滚动到视图中的子控件。</param>
        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }

        #endregion
    }
}