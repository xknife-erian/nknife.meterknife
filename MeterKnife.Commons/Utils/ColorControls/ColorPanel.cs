using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MeterKnife.Utils.ColorControls
{
    public class ColorPanel : Control
    {
        private Color clrBottomLeft = Color.Transparent;
        private Color clrBottomRight = Color.Transparent;
        private Color clrTopLeft = Color.Transparent;
        private Color clrTopRight = Color.Transparent;
        private bool designSerializeColor;
        private bool grabbed;
        private PointF pickerPos = new PointF(0.5f, 0.5f);
        private int pickerSize = 8;
        private Bitmap srcImage;
        private Color valTemp = Color.Transparent;


        public ColorPanel()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetupHueBrightnessGradient();
        }


        public ControlRenderer Renderer { get; } = new ControlRenderer();

        public Rectangle ColorAreaRectangle => new Rectangle(
            ClientRectangle.X + 2,
            ClientRectangle.Y + 2,
            ClientRectangle.Width - 4,
            ClientRectangle.Height - 4);

        [DefaultValue(8)]
        public int PickerSize
        {
            get => pickerSize;
            set
            {
                pickerSize = value;
                Invalidate();
            }
        }

        [DefaultValue(0.5f)]
        public PointF ValuePercentual
        {
            get => pickerPos;
            set
            {
                var last = pickerPos;
                pickerPos = new PointF(
                    Math.Min(1.0f, Math.Max(0.0f, value.X)),
                    Math.Min(1.0f, Math.Max(0.0f, value.Y)));
                if (pickerPos != last)
                {
                    OnPercentualValueChanged();
                    UpdateColorValue();
                    Invalidate();
                }
            }
        }

        public Color Value => valTemp;

        public Color TopLeftColor
        {
            get => clrTopLeft;
            set
            {
                if (clrTopLeft != value)
                {
                    SetupGradient(value, clrTopRight, clrBottomLeft, clrBottomRight);
                    designSerializeColor = true;
                }
            }
        }

        public Color TopRightColor
        {
            get => clrTopRight;
            set
            {
                if (clrTopRight != value)
                {
                    SetupGradient(clrTopLeft, value, clrBottomLeft, clrBottomRight);
                    designSerializeColor = true;
                }
            }
        }

        public Color BottomLeftColor
        {
            get => clrBottomLeft;
            set
            {
                if (clrBottomLeft != value)
                {
                    SetupGradient(clrTopLeft, clrTopRight, value, clrBottomRight);
                    designSerializeColor = true;
                }
            }
        }

        public Color BottomRightColor
        {
            get => clrBottomRight;
            set
            {
                if (clrBottomRight != value)
                {
                    SetupGradient(clrTopLeft, clrTopRight, clrBottomLeft, value);
                    designSerializeColor = true;
                }
            }
        }


        public event EventHandler ValueChanged;
        public event EventHandler PercentualValueChanged;

        public void SetupGradient(Color tl, Color tr, Color bl, Color br, int accuracy = 256)
        {
            accuracy = Math.Max(1, accuracy);

            clrTopLeft = tl;
            clrTopRight = tr;
            clrBottomLeft = bl;
            clrBottomRight = br;
            srcImage = new Bitmap(accuracy, accuracy);
            var tempImg = new Bitmap(2, 2);
            using (var g = Graphics.FromImage(tempImg))
            {
                g.DrawRectangle(new Pen(tl), 0, 0, 1, 1);
                g.DrawRectangle(new Pen(tr), 1, 0, 1, 1);
                g.DrawRectangle(new Pen(bl), 0, 1, 1, 1);
                g.DrawRectangle(new Pen(br), 1, 1, 1, 1);
            }
            using (var g = Graphics.FromImage(srcImage))
            {
                g.DrawImage(
                    tempImg,
                    new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                    0, 0, tempImg.Width - 1, tempImg.Height - 1,
                    GraphicsUnit.Pixel);
            }
            UpdateColorValue();
            Invalidate();
        }

        public void SetupXYGradient(Color left, Color right, Color bottom, Color top, int accuracy = 256)
        {
            accuracy = Math.Max(1, accuracy);

            srcImage = new Bitmap(accuracy, accuracy);
            using (var g = Graphics.FromImage(srcImage))
            {
                LinearGradientBrush gradient;
                gradient = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(srcImage.Width - 1, 0),
                    left,
                    right);
                g.FillRectangle(gradient, g.ClipBounds);
                gradient = new LinearGradientBrush(
                    new Point(0, srcImage.Height - 1),
                    new Point(0, 0),
                    bottom,
                    top);
                g.FillRectangle(gradient, g.ClipBounds);
            }
            clrTopLeft = srcImage.GetPixel(0, 0);
            clrTopRight = srcImage.GetPixel(srcImage.Width - 1, 0);
            clrBottomLeft = srcImage.GetPixel(0, srcImage.Height - 1);
            clrBottomRight = srcImage.GetPixel(srcImage.Width - 1, srcImage.Height - 1);
            UpdateColorValue();
            Invalidate();
        }

        public void SetupXYGradient(ColorBlend blendX, ColorBlend blendY, int accuracy = 256)
        {
            accuracy = Math.Max(1, accuracy);

            srcImage = new Bitmap(accuracy, accuracy);
            using (var g = Graphics.FromImage(srcImage))
            {
                LinearGradientBrush gradient;
                gradient = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(srcImage.Width - 1, 0),
                    Color.Transparent,
                    Color.Transparent);
                gradient.InterpolationColors = blendX;
                g.FillRectangle(gradient, g.ClipBounds);
                gradient = new LinearGradientBrush(
                    new Point(0, srcImage.Height - 1),
                    new Point(0, 0),
                    Color.Transparent,
                    Color.Transparent);
                gradient.InterpolationColors = blendY;
                g.FillRectangle(gradient, g.ClipBounds);
            }
            clrTopLeft = srcImage.GetPixel(0, 0);
            clrTopRight = srcImage.GetPixel(srcImage.Width - 1, 0);
            clrBottomLeft = srcImage.GetPixel(0, srcImage.Height - 1);
            clrBottomRight = srcImage.GetPixel(srcImage.Width - 1, srcImage.Height - 1);
            UpdateColorValue();
            Invalidate();
        }

        public void SetupHueBrightnessGradient(float saturation = 1.0f, int accuracy = 256)
        {
            var blendX = new ColorBlend();
            blendX.Colors = new[]
            {
                ExtMethodsColor.ColorFromHSV(0.0f, saturation, 1.0f),
                ExtMethodsColor.ColorFromHSV(1.0f / 6.0f, saturation, 1.0f),
                ExtMethodsColor.ColorFromHSV(2.0f / 6.0f, saturation, 1.0f),
                ExtMethodsColor.ColorFromHSV(3.0f / 6.0f, saturation, 1.0f),
                ExtMethodsColor.ColorFromHSV(4.0f / 6.0f, saturation, 1.0f),
                ExtMethodsColor.ColorFromHSV(5.0f / 6.0f, saturation, 1.0f),
                ExtMethodsColor.ColorFromHSV(1.0f, saturation, 1.0f)
            };
            blendX.Positions = new[]
            {
                0.0f,
                1.0f / 6.0f,
                2.0f / 6.0f,
                3.0f / 6.0f,
                4.0f / 6.0f,
                5.0f / 6.0f,
                1.0f
            };

            var blendY = new ColorBlend();
            blendY.Colors = new[]
            {
                Color.FromArgb(0, 0, 0),
                Color.Transparent
            };
            blendY.Positions = new[]
            {
                0.0f,
                1.0f
            };
            SetupXYGradient(blendX, blendY, accuracy);
        }

        public void SetupHueSaturationGradient(float brightness = 1.0f, int accuracy = 256)
        {
            var blendX = new ColorBlend();
            blendX.Colors = new[]
            {
                ExtMethodsColor.ColorFromHSV(0.0f, 1.0f, brightness),
                ExtMethodsColor.ColorFromHSV(1.0f / 6.0f, 1.0f, brightness),
                ExtMethodsColor.ColorFromHSV(2.0f / 6.0f, 1.0f, brightness),
                ExtMethodsColor.ColorFromHSV(3.0f / 6.0f, 1.0f, brightness),
                ExtMethodsColor.ColorFromHSV(4.0f / 6.0f, 1.0f, brightness),
                ExtMethodsColor.ColorFromHSV(5.0f / 6.0f, 1.0f, brightness),
                ExtMethodsColor.ColorFromHSV(1.0f, 1.0f, brightness)
            };
            blendX.Positions = new[]
            {
                0.0f,
                1.0f / 6.0f,
                2.0f / 6.0f,
                3.0f / 6.0f,
                4.0f / 6.0f,
                5.0f / 6.0f,
                1.0f
            };

            var blendY = new ColorBlend();
            blendY.Colors = new[]
            {
                Color.FromArgb((int) Math.Round(255.0f * brightness), (int) Math.Round(255.0f * brightness), (int) Math.Round(255.0f * brightness)),
                Color.Transparent
            };
            blendY.Positions = new[]
            {
                0.0f,
                1.0f
            };
            SetupXYGradient(blendX, blendY, accuracy);
        }

        protected void UpdateColorValue()
        {
            var oldVal = valTemp;
            valTemp = srcImage.GetPixel(
                (int) Math.Round((srcImage.Width - 1) * pickerPos.X),
                (int) Math.Round((srcImage.Height - 1) * (1.0f - pickerPos.Y)));
            if (oldVal != valTemp) OnValueChanged();
        }

        protected void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, null);
        }

        protected void OnPercentualValueChanged()
        {
            if (PercentualValueChanged != null)
                PercentualValueChanged(this, null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var colorArea = ColorAreaRectangle;
            var pickerVisualPos = new Point(
                colorArea.X + (int) Math.Round(pickerPos.X * colorArea.Width),
                colorArea.Y + (int) Math.Round((1.0f - pickerPos.Y) * colorArea.Height));

            if (clrBottomLeft.A < 255 || clrBottomRight.A < 255 || clrTopLeft.A < 255 || clrTopRight.A < 255)
                e.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeCheckerBoard, Renderer.ColorLightBackground, Renderer.ColorDarkBackground), colorArea);

            e.Graphics.DrawImage(srcImage, colorArea, 0, 0, srcImage.Width - 1, srcImage.Height - 1, GraphicsUnit.Pixel);

            var innerPickerPen = valTemp.GetLuminance() > 0.5f ? Pens.Black : Pens.White;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.DrawEllipse(innerPickerPen,
                pickerVisualPos.X - pickerSize / 2,
                pickerVisualPos.Y - pickerSize / 2,
                pickerSize,
                pickerSize);
            e.Graphics.SmoothingMode = SmoothingMode.Default;

            if (!Enabled)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, Renderer.ColorBackground)), colorArea);

            Renderer.DrawBorder(e.Graphics, ClientRectangle, BorderStyle.ContentBox, BorderState.Normal);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Focus();
                ValuePercentual = new PointF(
                    (e.X - ColorAreaRectangle.X) / (float) ColorAreaRectangle.Width,
                    1.0f - (e.Y - ColorAreaRectangle.Y) / (float) ColorAreaRectangle.Height);
                grabbed = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            grabbed = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (grabbed)
                ValuePercentual = new PointF(
                    (e.X - ColorAreaRectangle.X) / (float) ColorAreaRectangle.Width,
                    1.0f - (e.Y - ColorAreaRectangle.Y) / (float) ColorAreaRectangle.Height);
        }

        private void ResetTopLeftColor()
        {
            SetupHueBrightnessGradient();
            designSerializeColor = false;
        }

        private void ResetTopRightColor()
        {
            SetupHueBrightnessGradient();
            designSerializeColor = false;
        }

        private void ResetBottomLeftColor()
        {
            SetupHueBrightnessGradient();
            designSerializeColor = false;
        }

        private void ResetBottomRightColor()
        {
            SetupHueBrightnessGradient();
            designSerializeColor = false;
        }

        private bool ShouldSerializeTopLeftColor()
        {
            return designSerializeColor;
        }

        private bool ShouldSerializeTopRightColor()
        {
            return designSerializeColor;
        }

        private bool ShouldSerializeBottomLeftColor()
        {
            return designSerializeColor;
        }

        private bool ShouldSerializeBottomRightColor()
        {
            return designSerializeColor;
        }
    }
}