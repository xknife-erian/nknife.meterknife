using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MeterKnife.Models;
using MeterKnife.Utilities;
using MeterKnife.Views.InstrumentsDiscovery.Properties;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls.Instruments
{
    public sealed partial class InstrumentCell : UserControl
    {
        /// <summary>
        ///     上下的内部间距
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private const int T_B_PADDING = 5;

        /// <summary>
        ///     左右的内部间距
        /// </summary>
        private const int L_R_PADDING = 2;

        private Brush _BackgroundColor = Brushes.AliceBlue;
        private NameValueRect _NameValueRect = NameValueRect.Empty;

        private Rectangle _Border;
        private Rectangle _Inner;
        private Rectangle _ImgRectangle;
        private Instrument _Instrument;

        public InstrumentCell(Instrument instrument)
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            if (instrument != null)
            {
                Instrument = instrument;
            }
            UpdateRectangles();
            MouseClick += Cell_MouseClick;
            MouseEnter += Cell_MouseEnter;
            MouseLeave += Cell_MouseLeave;
        }

        private void UpdateRectangles()
        {
            _Border = new Rectangle(2, 2, Width - 5, Height - 5);
            _Inner = new Rectangle(3, 3, Width - 7, Height - 7);
            var s = Height - (T_B_PADDING + 3) * 2;
            _ImgRectangle = new Rectangle(7 + L_R_PADDING, 4 + T_B_PADDING, s, s);
            _NameValueRect = NameValueRect.Empty;
        }

        public Instrument Instrument
        {
            get => _Instrument;
            set
            {
                _Instrument = value;
                if (_Instrument != null && _Instrument.Image == null)
                    _Instrument.Image = Resources.meter64;
                Invalidate();
            }
        }

        private void Cell_MouseLeave(object sender, EventArgs e)
        {
            _BackgroundColor = Brushes.AliceBlue;
            Refresh();
        }

        private void Cell_MouseEnter(object sender, EventArgs e)
        {
            _BackgroundColor = Brushes.LightYellow;
            Refresh();
        }

        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            var me = new CellClickEventArgs(Instrument, e.Button, e.Clicks, e.X, e.Y, e.Delta);
            OnCellMouseClicked(me);
        }

        public event EventHandler<CellClickEventArgs> CellMouseClicked;

        private void OnCellMouseClicked(CellClickEventArgs e)
        {
            CellMouseClicked?.Invoke(this, e);
        }

        private NameValueRect CalculateNameValueRect(Graphics g, Cell cell)
        {
            if (_NameValueRect.IsEmpty())
            {
                var initX = Height;
                var fontSize = g.MeasureString("中国文字:", Font);

                var columnWidth = (Width - initX - L_R_PADDING) / cell.ColumnCount;
                var rowHeight = (Height - T_B_PADDING * 2) / cell.RowCount;

                var nameWidth = fontSize.Width + L_R_PADDING;
                var valueWidth = columnWidth - nameWidth;
                var fontHeightOffset = (rowHeight - fontSize.Height) / 2;

                _NameValueRect = new NameValueRect(initX, columnWidth, rowHeight, nameWidth, valueWidth, fontHeightOffset);
            }
            return _NameValueRect;
        }

        private void DrawBorder(Graphics g)
        {
            g.FillRectangle(_BackgroundColor, _Inner);
            g.DrawRectangle(Pens.Gray, _Border);
        }

        private void DrawLabel(Graphics g, Cell cell, NameValue nameValue)
        {
            var r = CalculateNameValueRect(g, cell);

            var beginX = r.InitX + (cell.TargetColumn - 1) * r.ColumnWidth;
            var beginY = (cell.TargetRow - 1) * r.RowHeight + T_B_PADDING;

            var nameRect = new RectangleF(beginX, beginY + r.FontHeightOffset, r.NameWidth, r.RowHeight);
            var valueRect = new RectangleF(beginX + r.NameWidth, beginY + r.FontHeightOffset, r.ValueWidth, r.RowHeight);

            g.DrawString(nameValue.Name, Font, Brushes.Black, nameRect);
            g.DrawString(nameValue.Value, Font, Brushes.Black, valueRect.X, valueRect.Y);
        }

        private void DrawImage(Graphics g)
        {
            /* 
             * 画仪器的图标在控件的左侧，设计为占用一个正方形的区域，该正方形的区域如无仪器图标也需要预留出空白位置，以保持整体的一致性。
             */
            if (Instrument.Image == null) return;
            var actualRect = Util.AdjustSize(_ImgRectangle, Instrument.Image.Width, Instrument.Image.Height);
            g.DrawImage(Instrument.Image, actualRect);
        }

        #region Inner Struct

        private struct Cell
        {
            public Cell(int rowCount, int columnCount, int targetRow, int targetColumn)
            {
                RowCount = rowCount;
                ColumnCount = columnCount;
                TargetRow = targetRow;
                TargetColumn = targetColumn;
            }

            public int RowCount { get; }
            public int ColumnCount { get; }
            public int TargetRow { get; }
            public int TargetColumn { get; }
        }

        private struct NameValue
        {
            public NameValue(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public string Value { get; }
        }

        private struct NameValueRect
        {
            public NameValueRect(int initX, float columnWidth, float rowHeight, float nameWidth, float valueWidth, float fontHeightOffset)
            {
                ColumnWidth = columnWidth;
                RowHeight = rowHeight;
                NameWidth = nameWidth;
                ValueWidth = valueWidth;
                FontHeightOffset = fontHeightOffset;
                InitX = initX;
            }

            public int InitX { get; }
            public float ColumnWidth { get; set; }
            public float RowHeight { get; }
            public float NameWidth { get; }
            public float ValueWidth { get; }
            public float FontHeightOffset { get; }

            public bool IsEmpty()
            {
                return Math.Abs(ColumnWidth) <= 0;
            }

            public static NameValueRect Empty => new NameValueRect {ColumnWidth = 0};
        }

        #endregion

        #region Overrides of Control

        /// <summary>引发 <see cref="E:System.Windows.Forms.Control.Paint" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.PaintEventArgs" />。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Instrument == null) return;
            var g = e.Graphics;
            DrawBorder(g);
            DrawImage(g);
            DrawLabel(g, new Cell(3, 2, 1, 1), new NameValue("制造商:", Instrument.Manufacturer));
            DrawLabel(g, new Cell(3, 2, 1, 2), new NameValue("型号:", Instrument.Model));
            DrawLabel(g, new Cell(3, 2, 2, 1), new NameValue("地址:", $"{Instrument.Address}"));
            DrawLabel(g, new Cell(3, 2, 2, 2), new NameValue("采集数据:", $"{Instrument.DatasCount}"));
            DrawLabel(g, new Cell(3, 2, 3, 1), new NameValue("自定义名:", Instrument.AbbrName));
            DrawLabel(g, new Cell(3, 2, 3, 2), new NameValue("最后采集:", $"{Instrument.LastUsingTime:yyyy/MM/dd HH:mm}"));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateRectangles();
            base.OnSizeChanged(e);
            Invalidate();
        }

        #endregion
    }
}