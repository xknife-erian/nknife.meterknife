using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Models;

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

        private Rectangle _Border;
        private Rectangle _Inner;

        public InstrumentCell(Instrument instrument)
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            if (instrument != null)
                Instrument = instrument;
            _Border = new Rectangle(2, 2, Width - 3, Height - 3);
            _Inner = new Rectangle(3, 3, Width - 5, Height - 5);
            MouseClick += ControlMouseClick;
            MouseEnter += InstrumentCell_MouseEnter;
            MouseLeave += InstrumentCell_MouseLeave;
        }

        private void InstrumentCell_MouseLeave(object sender, EventArgs e)
        {
            _BackgroundColor = Brushes.AliceBlue;
            Refresh();
        }

        private void InstrumentCell_MouseEnter(object sender, EventArgs e)
        {
            _BackgroundColor = Brushes.LightYellow;
            Refresh();
        }

        public Instrument Instrument { get; set; }

        public event EventHandler<CellClickEventArgs> CellMouseClicked;

        private void OnCellMouseClicked(CellClickEventArgs e)
        {
            CellMouseClicked?.Invoke(this, e);
        }

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

        #region Overrides of Control

        protected override void OnSizeChanged(EventArgs e)
        {
            _Border = new Rectangle(2, 2, Width - 5, Height - 3);
            _Inner = new Rectangle(3, 3, Width - 7, Height - 5);
            base.OnSizeChanged(e);
            Invalidate();
        }

        #endregion

        private void ControlMouseClick(object sender, MouseEventArgs e)
        {
            var me = new CellClickEventArgs((Instrument)Tag, e.Button, e.Clicks, e.X, e.Y, e.Delta);
            OnCellMouseClicked(me);
        }

        private void DrawBorder(Graphics g)
        {
            g.FillRectangle(_BackgroundColor, _Inner);
            g.DrawRectangle(Pens.Gray, _Border);
        }

        private void DrawLabel(Graphics g, Cell cell, NameValue nameValue)
        {
            var r = GetNameValueRect(g, cell);

            var beginX = r.InitX + (cell.TargetColumn - 1) * r.ColumnWidth;
            var beginY = (cell.TargetRow - 1) * r.RowHeight + T_B_PADDING;

            var nameRect = new RectangleF(beginX, beginY + r.FontHeightOffset, r.NameWidth, r.RowHeight);
            var valueRect = new RectangleF(beginX + r.NameWidth, beginY + r.FontHeightOffset, r.ValueWidth, r.RowHeight);

            g.DrawString(nameValue.Name, Font, Brushes.Black, nameRect);
            g.DrawString(nameValue.Value, Font, Brushes.Black, valueRect.X, valueRect.Y);
        }

        private NameValueRect GetNameValueRect(Graphics g, Cell cell)
        {
            var initX = Height;
            var fontSize = g.MeasureString("中国文字:", Font);

            var columnWidth = (Width - initX - L_R_PADDING) / cell.ColumnCount;
            var rowHeight = (Height - T_B_PADDING * 2) / cell.RowCount;

            var nameWidth = fontSize.Width + L_R_PADDING;
            var valueWidth = columnWidth - nameWidth;
            var fontHeightOffset = (rowHeight - fontSize.Height) / 2;

            return new NameValueRect(initX, columnWidth, rowHeight, nameWidth, valueWidth, fontHeightOffset);
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

        private void DrawImage(Graphics g)
        {
            /* 
             * 画仪器的图标在控件的左侧，设计为占用一个正方形的区域，该正方形的区域如无仪器图标也需要预留出空白位置，以保持整体的一致性。
             */
            if (Instrument.Image == null) return;
        }

        #endregion
    }


/*****
    public partial class InstrumentCell : UserControl
    {
        public InstrumentCell(Instrument instrument)
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            ControlSameEventManager();
            _MainPanel.Paint += (s, e) =>
            {
                var g = e.Graphics;
                var pen = new Pen(Color.DarkGray);

                var leftTop = new Point(0, 0);
                var rightTop = new Point(_MainPanel.Width - 1, 0);
                var leftBottom = new Point(0, _MainPanel.Height - 1);
                var rightBottom = new Point(_MainPanel.Width - 1, _MainPanel.Height - 1);

                g.DrawLine(pen, leftTop, rightTop);
                g.DrawLine(pen, leftTop, leftBottom);
                g.DrawLine(pen, rightTop, rightBottom);
                g.DrawLine(pen, leftBottom, rightBottom);
            };

            if (instrument != null)
            {
                Tag = instrument;
                if (instrument.Image != null)
                    Image = instrument.Image;
                Model = instrument.Model;
                Manufacturer = instrument.Manufacturer;
                Address = instrument.Address.ToString();
                Information = instrument.Information;
                DatasCount = instrument.DatasCount.ToString();
                UsingTime = instrument.LastUsingTime.ToString("yyyy/MM/dd");
            }

            var topPanel = new OpacityPanel();
            topPanel.Dock = DockStyle.Fill;
            topPanel.MouseEnter += ControlMouseEnter;
            topPanel.MouseLeave += CoutrolMouseLeave;
            topPanel.MouseClick += ControlMouseClick;
            _MainPanel.Controls.Add(topPanel);
            topPanel.BringToFront();
        }

        /// <summary>
        ///     当鼠标在控件上时，整个控件颜色发生变化
        /// </summary>
        private void ControlSameEventManager()
        {
            var list = new List<Control>();
            AddToControlList(_MainPanel, list);

            foreach (var ctrl in list)
            {
//                ctrl.MouseEnter += ControlMouseEnter;
//                ctrl.MouseLeave += CoutrolMouseLeave;
//                ctrl.MouseClick += ControlMouseClick;
            }
        }

        private void ControlMouseClick(object sender, MouseEventArgs e)
        {
            var p = ((Control) sender).PointToScreen(e.Location);
            var me = new CellClickEventArgs((Instrument) Tag, e.Button, e.Clicks, p.X, p.Y, e.Delta);
            OnMouseClicked(me);
        }

        private void CoutrolMouseLeave(object sender, EventArgs e)
        {
            _MainPanel.BackColor = SystemColors.ControlLight;
        }

        private void ControlMouseEnter(object sender, EventArgs e)
        {
            _MainPanel.BackColor = Color.LightGoldenrodYellow;
        }

        public event EventHandler<CellClickEventArgs> CellMouseClicked;

        private void AddToControlList(Control ctrl, List<Control> list)
        {
            list.Add(ctrl);
            if (ctrl.Controls.Count > 0)
                foreach (Control subControl in ctrl.Controls)
                    AddToControlList(subControl, list);
        }

        protected virtual void OnMouseClicked(CellClickEventArgs e)
        {
            CellMouseClicked?.Invoke(this, e);
        }

        #region base model property

        public Image Image
        {
            get => _PictureBox.BackgroundImage;
            set => _PictureBox.BackgroundImage = value;
        }

        public string Model
        {
            get => _ModelLabel.Text;
            set => _ModelLabel.Text = value;
        }

        public string Manufacturer
        {
            get => _ManufacturerLabel.Text;
            set => _ManufacturerLabel.Text = value;
        }

        public string Address
        {
            get => _AddressLabel.Text;
            set => _AddressLabel.Text = value;
        }

        public string Information
        {
            get => _InformationLabel.Text;
            set => _InformationLabel.Text = value;
        }

        public string DatasCount
        {
            get => _DatasCountLabel.Text;
            set => _DatasCountLabel.Text = value;
        }

        public string UsingTime
        {
            get => _UsingTimeLabel.Text;
            set => _UsingTimeLabel.Text = value;
        }

        #endregion
    }

    public sealed class OpacityPanel : Control
    {
        public OpacityPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }
    }
*****/
}