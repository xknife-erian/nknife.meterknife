using System;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls.Instruments
{
    public sealed class TransparentPanel : Panel
    {
        private int _Alpha;
        private readonly bool _Drag = false;
        private Color _FillColor = Color.White;
        private int _Opacity = 50;

        public TransparentPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            BackColor = Color.Transparent;
        }

        public Color FillColor
        {
            get => _FillColor;
            set
            {
                _FillColor = value;
                Parent?.Invalidate(Bounds, true);
            }
        }

        public int Opacity
        {
            get
            {
                if (_Opacity > 100)
                    _Opacity = 100;
                else if (_Opacity < 1)
                    _Opacity = 1;
                return _Opacity;
            }
            set
            {
                _Opacity = value;
                Parent?.Invalidate(Bounds, true);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var bounds = new Rectangle(0, 0, Width - 1, Height - 1);

            var parentBackColor = Parent.BackColor;
            Brush brushColor;
            Brush bckColor;

            _Alpha = _Opacity * 255 / 100;
            if (_Alpha > 255)
                _Alpha = 255;

            if (_Drag)
            {
                Color dragFillColor;
                Color dragBackColor;

                if (BackColor != Color.Transparent)
                {
                    var red =   BackColor.R * _Alpha / 255 + parentBackColor.R * (255 - _Alpha) / 255;
                    var green = BackColor.G * _Alpha / 255 + parentBackColor.G * (255 - _Alpha) / 255;
                    var blue =  BackColor.B * _Alpha / 255 + parentBackColor.B * (255 - _Alpha) / 255;
                    dragBackColor = Color.FromArgb(red, green, blue);
                }
                else
                {
                    dragBackColor = parentBackColor;
                }

                if (_FillColor != Color.Transparent)
                {
                    var red =   _FillColor.R * _Alpha / 255 + parentBackColor.R * (255 - _Alpha) / 255;
                    var green = _FillColor.G * _Alpha / 255 + parentBackColor.G * (255 - _Alpha) / 255;
                    var blue =  _FillColor.B * _Alpha / 255 + parentBackColor.B * (255 - _Alpha) / 255;
                    dragFillColor = Color.FromArgb(red, green, blue);
                }
                else
                {
                    dragFillColor = dragBackColor;
                }

                _Alpha = 255;
                brushColor = new SolidBrush(Color.FromArgb(_Alpha, dragFillColor));
                bckColor = new SolidBrush(Color.FromArgb(_Alpha, dragBackColor));
            }
            else
            {
                var color = _FillColor;
                brushColor = new SolidBrush(Color.FromArgb(_Alpha, color));
                bckColor = new SolidBrush(Color.FromArgb(_Alpha, BackColor));
            }

            if ((BackColor != Color.Transparent) | _Drag)
                g.FillRectangle(bckColor, bounds);

            brushColor.Dispose();
            bckColor.Dispose();
            g.Dispose();

            base.OnPaint(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            Parent?.Invalidate(Bounds, true);
            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnParentBackColorChanged(e);
        }
    }
}