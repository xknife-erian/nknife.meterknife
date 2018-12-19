using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Views.Properties;

namespace MeterKnife.Views.Controls.Instruments
{
    public partial class InstrumentsListHead : UserControl
    {
        /// <summary>
        ///     上下的内部间距
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private const int T_B_PADDING = 5;

        /// <summary>
        ///     左右的内部间距
        /// </summary>
        private const int LRPadding = 2;

        private Brush _backgroundBrush = Brushes.DarkOliveGreen;
        private Brush _fontBrush = Brushes.White;

        private Rectangle _border;
        private Rectangle _imgRectangle;
        private Rectangle _inner;
        private bool _isDown = true;

        private Image _leftImage = Resources.InstrumentsListHead_down;

        public InstrumentsListHead()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            UpdateRectangles();
            MouseClick += HeadMouseClick;
            MouseEnter += HeadMouseEnter;
            MouseLeave += HeadMouseLeave;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateRectangles();
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.FillRectangle(_backgroundBrush, _inner);
            g.DrawRectangle(Pens.Gray, _border);
            g.DrawImage(_leftImage, _imgRectangle);

            var s = g.MeasureString(GatewayModel, Font);
            var strX = _imgRectangle.X + _imgRectangle.Width + LRPadding * 2;
            var strY = (Height - s.Height) / 2 + 1;
            g.DrawString(GatewayModel, Font, _fontBrush, strX, strY);

            var c = $"{Count}";
            s = g.MeasureString(c, Font);
            strX = (int) (Width - s.Width - LRPadding * 6);

            g.DrawString(c, Font, _fontBrush, strX, strY);
        }

        public string GatewayModel { get; set; }

        public int Count { get; set; }

        private void UpdateRectangles()
        {
            const int baseW = 18;
            _border = new Rectangle(2, 2, Width - 5, Height - 5);
            _inner = new Rectangle(3, 3, Width - 5, Height - 5);
            _imgRectangle = new Rectangle(14 + LRPadding, (Height - baseW) / 2 + 1, baseW, baseW);
        }

        private void HeadMouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    _isDown = !_isDown;
                    //当点击时更替左侧的图形，给出上缩下拉的两种工作模式
                    _leftImage = !_isDown ? Resources.InstrumentsListHead_up : Resources.InstrumentsListHead_down;
                    break;
                }
            }
            GatewayModel model;
            Enum.TryParse(GatewayModel, out model);
            var me = new HeadClickEventArgs(model, e.Button, e.Clicks, e.X, e.Y, e.Delta);
            OnHeadMouseClicked(me);
            Refresh();
        }

        private void HeadMouseLeave(object sender, EventArgs e)
        {
            _backgroundBrush = Brushes.DarkOliveGreen;
            _fontBrush = Brushes.White;
            Refresh();
        }

        private void HeadMouseEnter(object sender, EventArgs e)
        {
            _backgroundBrush = Brushes.DarkKhaki;
            _fontBrush = Brushes.Black;
            Refresh();
        }

        public event EventHandler<HeadClickEventArgs> HeadMouseClicked;

        protected virtual void OnHeadMouseClicked(HeadClickEventArgs e)
        {
            HeadMouseClicked?.Invoke(this, e);
        }
    }
}