using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class HorizontalLine : Control
    {
        // Fields
        private Color _Linecolor;
        private int _Lineheight;
        private ContentAlignment _Panelalign;
        private ContentAlignment _Textalign;
        private int _TotalWidth;
        private IContainer components = null;

        // Methods
        public HorizontalLine()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            TextAlign = ContentAlignment.MiddleCenter;
            PanelAlign = ContentAlignment.MiddleCenter;
            LineColor = SystemColors.WindowFrame;
            BackColor = Color.Transparent;
            LineHeight = 1;
            FontChanged += (sender, e) => Invalidate();
            TextChanged += (sender, e) => Invalidate();
            Anchor = AnchorStyles.Right | AnchorStyles.Left;
            TextFont = new Font("Tahoma", 9f);
            TotalWidth = 100;
            DoubleBuffered = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            float startX;
            float startY;
            int textStartX;
            int lineStartX;
            int lineStartY;
            Pen p;
            int singleLienWidth;
            base.OnPaint(pe);
            float rectwidth = (Width * TotalWidth) / 100;
            Graphics g = pe.Graphics;
            SizeF textSize = g.MeasureString(this.Text, this.Font);
            float textHeight = textSize.Height;
            float textWidth = textSize.Width;
            float rectHeight = (textHeight > this.LineHeight) ? textHeight : ((float)this.LineHeight);
            if (rectHeight > base.Height)
            {
                rectHeight = base.Height;
            }
            if (this.LineHeight > rectHeight)
            {
                this.LineHeight = (int)rectHeight;
            }
            if (((this.TextAlign == ContentAlignment.BottomCenter) || (this.TextAlign == ContentAlignment.MiddleCenter)) || (this.TextAlign == ContentAlignment.TopCenter))
            {
                startX = (base.Width - rectwidth) / 2f;
            }
            else if (((this.TextAlign == ContentAlignment.BottomLeft) || (this.TextAlign == ContentAlignment.MiddleLeft)) || (this.TextAlign == ContentAlignment.TopLeft))
            {
                startX = 0f;
            }
            else
            {
                startX = base.Width - rectwidth;
            }
            if (((this.TextAlign == ContentAlignment.BottomCenter) || (this.TextAlign == ContentAlignment.BottomLeft)) || (this.TextAlign == ContentAlignment.BottomRight))
            {
                startY = base.Height - rectHeight;
            }
            else if (((this.TextAlign == ContentAlignment.MiddleCenter) || (this.TextAlign == ContentAlignment.MiddleLeft)) || (this.TextAlign == ContentAlignment.MiddleRight))
            {
                startY = (base.Height - rectHeight) / 2f;
            }
            else
            {
                startY = 0f;
            }
            float endX = startX + rectwidth;
            float endY = startY + rectHeight;
            int textStartY = textStartX = lineStartY = lineStartX = 0;
            switch (this._Textalign)
            {
                case ContentAlignment.TopLeft:
                    textStartY = 0;
                    textStartX = 0;
                    lineStartX = 0;
                    lineStartY = (int)(textHeight + 1f);
                    goto Label_0354;

                case ContentAlignment.TopCenter:
                    textStartY = 0;
                    textStartX = (int)((rectwidth - textWidth) / 2f);
                    lineStartX = 0;
                    lineStartY = (int)(textHeight + 1f);
                    goto Label_0354;

                case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                    goto Label_0354;

                case ContentAlignment.TopRight:
                    textStartY = 0;
                    textStartX = (int)(rectwidth - textWidth);
                    lineStartX = 0;
                    lineStartY = (int)(textHeight + 1f);
                    goto Label_0354;

                case ContentAlignment.MiddleLeft:
                    textStartY = (int)((rectHeight - textHeight) / 2f);
                    textStartX = 0;
                    lineStartX = (int)((textStartY + textWidth) + 1f);
                    lineStartY = (int)(rectHeight / 2f);
                    goto Label_0354;

                case ContentAlignment.MiddleCenter:
                    textStartY = (int)((rectHeight - textHeight) / 2f);
                    textStartX = (int)((rectwidth - textWidth) / 2f);
                    lineStartX = 0;
                    lineStartY = (int)(rectHeight / 2f);
                    goto Label_0354;

                case ContentAlignment.MiddleRight:
                    textStartY = (int)((rectHeight - textHeight) / 2f);
                    textStartX = (int)(rectwidth - textWidth);
                    lineStartX = 0;
                    lineStartY = (int)(rectHeight / 2f);
                    goto Label_0354;

                case ContentAlignment.BottomLeft:
                    textStartY = ((int)(rectHeight - textHeight)) + 1;
                    textStartX = 0;
                    lineStartX = 0;
                    lineStartY = textStartY - 1;
                    goto Label_0354;

                case ContentAlignment.BottomCenter:
                    textStartY = ((int)(rectHeight - textHeight)) + 1;
                    textStartX = (int)((rectwidth - textWidth) / 2f);
                    lineStartX = 0;
                    lineStartY = textStartY - 1;
                    break;

                case ContentAlignment.BottomRight:
                    textStartY = ((int)(rectHeight - textHeight)) + 1;
                    textStartX = (int)(rectwidth - textWidth);
                    lineStartX = 0;
                    lineStartY = textStartY - 1;
                    goto Label_0354;
            }
        Label_0354:
            p = new Pen(this.LineColor);
            if (this.TextAlign == ContentAlignment.MiddleCenter)
            {
                singleLienWidth = (int)((rectwidth / 2f) - (textWidth / 2f));
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(singleLienWidth, this.LineHeight)));
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point((lineStartX + singleLienWidth) + ((int)textWidth), lineStartY), new Size(singleLienWidth, this.LineHeight)));
            }
            else if ((this._Textalign == ContentAlignment.MiddleLeft) || (this._Textalign == ContentAlignment.MiddleRight))
            {
                singleLienWidth = (int)(rectwidth - textWidth);
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(singleLienWidth, this.LineHeight)));
            }
            else
            {
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size((int)rectwidth, this.LineHeight)));
            }
            g.DrawString(this.Text, this.TextFont, new SolidBrush(this.ForeColor), new PointF((float)textStartX, (float)textStartY));
        }

        // Properties
        [Description("背景色"), DisplayName("背景色")]
        public override sealed Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams ps = base.CreateParams;
                ps.ExStyle |= 0x20;
                return ps;
            }
        }

        [Description("文本颜色"), DisplayName("文本颜色")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                base.Invalidate();
            }
        }

        [Description("线条颜色"), DisplayName("线条颜色")]
        public Color LineColor
        {
            get
            {
                return this._Linecolor;
            }
            set
            {
                this._Linecolor = value;
                base.Invalidate();
            }
        }

        [Description("线条高度"), DisplayName("线条高度"), DefaultValue(1)]
        public int LineHeight
        {
            get
            {
                return this._Lineheight;
            }
            set
            {
                this._Lineheight = value;
                base.Invalidate();
            }
        }

        [Description("对象在显示时如何对齐"), DisplayName("对象对齐")]
        public ContentAlignment PanelAlign
        {
            get
            {
                return this._Panelalign;
            }
            set
            {
                this._Panelalign = value;
                base.Invalidate();
            }
        }

        [DisplayName("文字对齐"), Description("文字在显示时如何对齐")]
        public ContentAlignment TextAlign
        {
            get
            {
                return this._Textalign;
            }
            set
            {
                this._Textalign = value;
                base.Invalidate();
            }
        }

        [DisplayName("文本字体"), Description("文本字体")]
        public Font TextFont
        {
            get
            {
                return this.Font;
            }
            set
            {
                this.Font = value;
                base.Invalidate();
            }
        }

        [DisplayName("线条总宽度"), DefaultValue(100), Description("线条总宽度")]
        public int TotalWidth
        {
            get
            {
                return this._TotalWidth;
            }
            set
            {
                if ((this.TotalWidth < 0) || (this.TotalWidth > 100))
                {
                    throw new ArgumentOutOfRangeException("TotalWidth", "总宽度必须位于 0 - 100 之间");
                }
                this._TotalWidth = value;
                base.Invalidate();
            }
        }
    }


}
