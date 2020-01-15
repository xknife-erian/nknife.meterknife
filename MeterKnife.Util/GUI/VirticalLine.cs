using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public class VirticalLine : Control
    {
        // Fields
        private Color _Linecolor;
        private int _Lineheight;
        private ContentAlignment _Panelalign;
        private ContentAlignment _Textalign;
        private int _TotalWidth;
        private IContainer components = null;

        // Methods
        public VirticalLine()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            TextAlign = ContentAlignment.MiddleCenter;
            PanelAlign = ContentAlignment.MiddleCenter;
            LineColor = SystemColors.WindowFrame;
            BackColor = Color.Transparent;
            LineHeight = 1; //线条粗细
            FontChanged += (sender, e) => Invalidate();
            TextChanged += (sender, e) => Invalidate();
            Anchor = AnchorStyles.Right | AnchorStyles.Left;
            TextFont = new Font("Tahoma", 9f);
            TotalHeight = 100;
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
            float startX; //起始坐标X
            float startY; //起始坐标Y
            int textStartX; //文本起始坐标X
            int lineStartX; //线条起始坐标X
            int lineStartY; //线条起始坐标Y
            Pen p;
            int singleLienWidth;
            base.OnPaint(pe);
            float rectheight = (Height * TotalHeight) / 100; //长方形宽度
            Graphics g = pe.Graphics;
            SizeF textSize = g.MeasureString(Text, Font); //文字大小
            float textHeight = textSize.Height; //文字高度
            float textWidth = textSize.Width; //文字宽度
            float rectWidth = (textHeight > this.LineHeight) ? textHeight : ((float)this.LineHeight); //长方形高度取文字高度和线条高度中较大的
            
            //Width >= rectWidth >= LineHeight
            if (rectWidth > Width) //如果长方形高度大于控件高度，则取控件高度
            {
                rectWidth = Width;
            }
            if (LineHeight > rectWidth) //如果线条粗细大于长方形高度，则取长方形高度
            {
                LineHeight = (int)rectWidth;
            }

            //根据文本对齐方式确定起始坐标Y
            if (((TextAlign == ContentAlignment.BottomCenter) || (TextAlign == ContentAlignment.MiddleCenter)) || (TextAlign == ContentAlignment.TopCenter))
            {
                startY = (Height - rectheight) / 2f;
            }
            else if (((TextAlign == ContentAlignment.BottomLeft) || (TextAlign == ContentAlignment.MiddleLeft)) || (TextAlign == ContentAlignment.TopLeft))
            {
                startY = 0f;
            }
            else
            {
                startY = base.Height - rectheight;
            }

            //根据文本对齐方式确定起始坐标X
            if (((this.TextAlign == ContentAlignment.BottomCenter) || (this.TextAlign == ContentAlignment.BottomLeft)) || (this.TextAlign == ContentAlignment.BottomRight))
            {
                startX = Width - rectWidth;
            }
            else if (((this.TextAlign == ContentAlignment.MiddleCenter) || (this.TextAlign == ContentAlignment.MiddleLeft)) || (this.TextAlign == ContentAlignment.MiddleRight))
            {
                startX = (Width - rectWidth) / 2f;
            }
            else
            {
                startX = 0f;
            }

            float endX = startX + rectWidth;
            float endY = startY + rectheight;

            int textStartY = textStartX = lineStartY = lineStartX = 0;

            switch (_Textalign)
            {
                case ContentAlignment.TopLeft:
                    textStartY = 0;
                    textStartX = ((int)(rectWidth - textHeight)) + 1;
                    lineStartX = textStartX -1;
                    lineStartY = 0;
                    goto Label_0354;

                case ContentAlignment.TopCenter:
                    textStartY = (int)((rectheight - textWidth) / 2f);
                    textStartX = ((int)(rectWidth - textHeight)) + 1;
                    lineStartX = textStartX -1;
                    lineStartY = 0;
                    goto Label_0354;

                case ContentAlignment.TopRight:
                    textStartY = (int)(rectheight - textWidth);
                    textStartX = ((int)(rectWidth - textHeight)) + 1;
                    lineStartX = textStartX -1;
                    lineStartY = 0;
                    goto Label_0354;

                case ContentAlignment.MiddleLeft:
                    textStartY = 0;
                    textStartX = (int)((rectWidth - textHeight) / 2f);
                    lineStartX = (int)(rectWidth / 2f);
                    lineStartY = (int)textWidth + 1;
                    goto Label_0354;

                case ContentAlignment.MiddleCenter:
                    textStartY = (int)((rectheight - textWidth) /2f);
                    textStartX = (int)((rectWidth - textHeight) / 2f);
                    lineStartX = (int)(rectWidth / 2f);
                    lineStartY = 0;
                    goto Label_0354;

                case ContentAlignment.MiddleRight:
                    textStartY = (int)((rectheight - textWidth));
                    textStartX = (int)((rectWidth - textHeight) / 2f);
                    lineStartX = (int)(rectWidth / 2f);;
                    lineStartY = 0;
                    goto Label_0354;

                case ContentAlignment.BottomLeft:
                    textStartY = 0;
                    textStartX = 0;
                    lineStartX = (int)(textStartX + textHeight + 1);
                    lineStartY = 0;
                    goto Label_0354;

                case ContentAlignment.BottomCenter:
                    textStartY = (int)((rectheight - textWidth) / 2f);
                    textStartX = 0;
                    lineStartX = (int)(textStartX + textHeight + 1);;
                    lineStartY = 0;
                    break;

                case ContentAlignment.BottomRight:
                    textStartY = ((int)(rectheight - textWidth));
                    textStartX = 0;
                    lineStartX = (int)(textStartX + textHeight + 1);;
                    lineStartY = 0;
                    goto Label_0354;
            }

        Label_0354:
            //TODO:使用DrawLine替换FillRectangle
            //p = new Pen(this.LineColor);
            if (this.TextAlign == ContentAlignment.MiddleCenter)
            {
                singleLienWidth = (int)((rectheight / 2f) - (textWidth / 2f));
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(this.LineHeight,singleLienWidth)));
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY + singleLienWidth + (int)textWidth), new Size(LineHeight, singleLienWidth)));

                //g.DrawLine(new Pen(new SolidBrush(LineColor),singleLienWidth), new Point(lineStartX, lineStartY), new Point(lineStartX,lineStartY + LineHeight));
            }
            else if ((this._Textalign == ContentAlignment.MiddleLeft) || (this._Textalign == ContentAlignment.MiddleRight))
            {
                singleLienWidth = (int)(rectheight - textWidth);
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(LineHeight,singleLienWidth )));
                
                //g.DrawLine(new Pen(new SolidBrush(LineColor),singleLienWidth), new Point(lineStartX,lineStartY),new Point(lineStartX,lineStartY + LineHeight) );
            }
            else
            {
                singleLienWidth = 1;
                g.FillRectangle(new SolidBrush(this.LineColor), new Rectangle(new Point(lineStartX, lineStartY), new Size(LineHeight,(int)rectheight )));
                
                //g.DrawLine(new Pen(new SolidBrush(LineColor),singleLienWidth),new Point(lineStartX,lineStartY),new Point(lineStartX,lineStartY + LineHeight)  );
            }

            StringFormat StrF = new StringFormat();
            StrF.FormatFlags = StringFormatFlags.DirectionVertical;
            g.DrawString(this.Text, this.TextFont, new SolidBrush(this.ForeColor), new PointF((float)textStartX, (float)textStartY), StrF);
            

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

        [Description("线条粗细"), DisplayName("线条粗细"), DefaultValue(1)]
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

        [DisplayName("线条总高度"), DefaultValue(100), Description("线条总高度")]
        public int TotalHeight
        {
            get
            {
                return this._TotalWidth;
            }
            set
            {
                if ((this.TotalHeight < 0) || (this.TotalHeight > 100))
                {
                    throw new ArgumentOutOfRangeException("TotalHeight", "总高度必须位于 0 - 100 之间");
                }
                this._TotalWidth = value;
                base.Invalidate();
            }
        }
    }


}
