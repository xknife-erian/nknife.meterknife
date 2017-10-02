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
        private const int L_R_PADDING = 2;

        private Brush _BackgroundBrush = Brushes.DarkOliveGreen;
        private Brush _FontBrush = Brushes.White;

        private Rectangle _Border;
        private Rectangle _ImgRectangle;
        private Rectangle _Inner;
        private bool _IsDown = true;

        private Image _LeftImage = Resources.InstrumentsListHead_down;

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
            g.FillRectangle(_BackgroundBrush, _Inner);
            g.DrawRectangle(Pens.Gray, _Border);
            g.DrawImage(_LeftImage, _ImgRectangle);

            var s = g.MeasureString(GatewayModel, Font);
            var strX = _ImgRectangle.X + _ImgRectangle.Width + L_R_PADDING * 2;
            var strY = (Height - s.Height) / 2 + 1;
            g.DrawString(GatewayModel, Font, _FontBrush, strX, strY);

            var c = $"{Count}";
            s = g.MeasureString(c, Font);
            strX = (int) (Width - s.Width - L_R_PADDING * 6);

            g.DrawString(c, Font, _FontBrush, strX, strY);
        }

        public string GatewayModel { get; set; }

        public int Count { get; set; }

        private void UpdateRectangles()
        {
            const int baseW = 18;
            _Border = new Rectangle(2, 2, Width - 5, Height - 5);
            _Inner = new Rectangle(3, 3, Width - 5, Height - 5);
            _ImgRectangle = new Rectangle(14 + L_R_PADDING, (Height - baseW) / 2 + 1, baseW, baseW);
        }

        private void HeadMouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    _IsDown = !_IsDown;
                    //当点击时更替左侧的图形，给出上缩下拉的两种工作模式
                    _LeftImage = !_IsDown ? Resources.InstrumentsListHead_up : Resources.InstrumentsListHead_down;
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
            _BackgroundBrush = Brushes.DarkOliveGreen;
            _FontBrush = Brushes.White;
            Refresh();
        }

        private void HeadMouseEnter(object sender, EventArgs e)
        {
            _BackgroundBrush = Brushes.DarkKhaki;
            _FontBrush = Brushes.Black;
            Refresh();
        }

        public event EventHandler<HeadClickEventArgs> HeadMouseClicked;

        protected virtual void OnHeadMouseClicked(HeadClickEventArgs e)
        {
            HeadMouseClicked?.Invoke(this, e);
        }
    }
}