using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    /// <summary>一个可以快速通过Graphics绘制指定符号在Button上的控件
    /// </summary>
    public sealed class ImageButton : Button
    {
        #region ImageButtonStyle enum

        public enum ImageButtonStyle
        {
            /// <summary>
            /// 加号
            /// </summary>
            Add,
            /// <summary>
            /// 减号
            /// </summary>
            Subtract,
            /// <summary>
            /// 向上箭头
            /// </summary>
            ArrowUp,
            /// <summary>
            /// 向下箭头
            /// </summary>
            ArrowDown,
            /// <summary>
            /// 向左箭头
            /// </summary>
            ArrowLeft,
            /// <summary>
            /// 向右箭头
            /// </summary>
            ArrowRight,
        }

        #endregion

        private Color _ButtonColor = Color.FromArgb(0, 120, 0);

        public ImageButton()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        /// <summary>符号的样式
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [Category("ImageButton")]
        public ImageButtonStyle Style { get; set; }

        /// <summary>符号的颜色
        /// </summary>
        /// <value>
        /// The color of the button.
        /// </value>
        [Category("ImageButton")]
        public Color ButtonColor
        {
            get { return _ButtonColor; }
            set { _ButtonColor = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            float offsetX = (float) Width/10 + (float) Width/10;
            float offsetY = (float) Height/10 + (float) Height/10;
            Graphics g = pevent.Graphics;
            switch (Style)
            {
                case ImageButtonStyle.Add:
                    {
                        // 画加号的横线
                        var locationH = new PointF(offsetX, (float)Height / 2 - offsetY / 2);
                        float widthH = Width - offsetX - offsetX;
                        float heightH = offsetY;
                        Brush brush = new SolidBrush(_ButtonColor);
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        // 画加号的竖线
                        var locationV = new PointF((float) Width/2 - offsetX/2, offsetY);
                        float widthV = offsetX;
                        float heightV = Height - offsetY - offsetY;
                        var rV = new RectangleF(locationV, new SizeF(widthV, heightV));
                        g.FillRectangle(brush, rV);
                        break;
                    }
                case ImageButtonStyle.Subtract:
                    {
                        // 画减号的横线
                        var locationH = new PointF(offsetX, (float)Height / 2 - offsetY / 2);
                        float widthH = Width - offsetX - offsetX;
                        float heightH = offsetY;
                        Brush brush = new SolidBrush(_ButtonColor);
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case ImageButtonStyle.ArrowUp:
                    {
                        // 画箭头
                        var pointF1 = new PointF((float) Width/2, offsetY);
                        var pointF2 = new PointF(offsetX, (float) Height/2);
                        var pointF3 = new PointF(Width - offsetX, (float) Height/2);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // 画箭头的竖线
                        var locationH = new PointF((float) Width/2 - offsetX/2, (float) Height/2);
                        float widthH = offsetX;
                        float heightH = (float) Height/2 - offsetY;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case ImageButtonStyle.ArrowDown:
                    {
                        // 画箭头
                        var pointF1 = new PointF((float) Width/2, Height - offsetY);
                        var pointF2 = new PointF(offsetX, (float) Height/2);
                        var pointF3 = new PointF(Width - offsetX, (float) Height/2);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // 画箭头的竖线
                        var locationH = new PointF((float) Width/2 - offsetX/2, offsetY);
                        float widthH = offsetX;
                        float heightH = (float) Height/2 - offsetY;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case ImageButtonStyle.ArrowLeft:
                    {
                        // 画箭头
                        var pointF1 = new PointF(offsetX, (float) Height/2);
                        var pointF2 = new PointF((float) Width/2, offsetY);
                        var pointF3 = new PointF((float) Width/2, Height - offsetY);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // 画箭头的横线
                        var locationH = new PointF((float)Width / 2, (float)Height / 2 - offsetY / 2);
                        float widthH = (float)Width / 2 - offsetX;
                        float heightH = offsetY + 1;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }

                case ImageButtonStyle.ArrowRight:
                    {
                        // 画箭头
                        var pointF1 = new PointF(Width - offsetX, (float) Height/2);
                        var pointF2 = new PointF((float) Width/2, offsetY);
                        var pointF3 = new PointF((float) Width/2, Height - offsetY);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // 画箭头的横线
                        var locationH = new PointF(offsetX, (float)Height / 2 - offsetY / 2);
                        float widthH = (float)Width / 2 - offsetX + 2;
                        float heightH = offsetY + 1;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Text = string.Empty;
        }
    }
}