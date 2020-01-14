using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using NKnife.Interface;

namespace NKnife.GUI.WinForm.CloudsControl
{
    /// <summary>
    /// 来自显示国际棋谱形成的需求。
    /// 设计形成的一个Label以流状的模式显示在一个Panel中的控件。
    /// </summary>
    public class CloudsPanel : ScrollableControl
    {

        #region Property

        /// <summary>
        /// 子控件集合
        /// </summary>
        public CloudCollection CloudItems { get; set; }

        /// <summary>
        /// 行高
        /// </summary>
        public float RowHeight
        {
            get { return this._rowHeight; }
            internal set { this._rowHeight = value; }
        }
        private float _rowHeight;

        /// <summary>
        /// 容器的总宽度
        /// </summary>
        public float ContainerWidth
        {
            get { return this._containerWidth; }
            internal set { this._containerWidth = value; }
        }
        private float _containerWidth;

        /// <summary>
        /// 行间距
        /// </summary>
        public float RowSpace
        {
            get { return this._rowSpace; }
            internal set { this._rowSpace = value; }
        }
        private float _rowSpace;

        /// <summary>
        /// 列间距
        /// </summary>
        public float ColumnSpace
        {
            internal get { return this._columnSpace; }
            set { this._columnSpace = value; }
        }
        private float _columnSpace;


        #endregion

        #region protected member variable

        /// <summary>
        /// 双缓冲绘制时的绘制成功的图片
        /// </summary>
        protected Image _pntedImage;
        protected PointF _defaultPoint;
        protected PointF _currPoint;
        /// <summary>
        /// 总的行数量
        /// </summary>
        protected int _lineNum = 0;
        /// <summary>
        /// 一个存放“行编号”与行的起始Label的索引号的集合
        /// </summary>
        protected Dictionary<int, int> _lines = new Dictionary<int, int>();

        #endregion

        #region Constructor

        public CloudsPanel()
        {
            this.SuspendLayout();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);//控件大小发生改变的时候应重绘
            this.SetStyle(ControlStyles.Selectable, true);//控件可接收焦点
            this.BackColor = Color.Gainsboro;
            this.AutoScroll = false;
            this.Font = new Font("Consolas", 10);
            this.ResumeLayout(false);

            _rowSpace = 4;
            _columnSpace = 4;
            _rowHeight = this.CreateGraphics().MeasureString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", this.Font).Height;
            _defaultPoint = new PointF(_columnSpace, _rowSpace);
            _currPoint = _defaultPoint;
            this.CloudItems = new CloudCollection();

            this.Scroll += new ScrollEventHandler(CloudsPanel_Scroll);
        }

        #endregion

        #region Add, AddRange, Clear

        /// <summary>
        /// Adds the specified cloud.
        /// </summary>
        /// <param name="cloud">The cloud.</param>
        public void Add(IGenerator cloud)
        {
            SizeF stringSize;
            using (Graphics g = this.CreateGraphics())
            {
                string text = cloud.Generator();
                stringSize = g.MeasureString(text, Font);
            }
            CloudLabel cloudLabel = new CloudLabel(cloud, this.CloudItems.Count, stringSize);
            this.CloudItems.Add(cloudLabel);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="clouds">The clouds.</param>
        public void AddRange(IGenerator[] clouds)
        {
            foreach (var item in clouds)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.CloudItems.Clear();
        }

        #endregion

        #region Override: OnResize, OnCreateControl

        //1.
        /// <summary>
        /// 引发 <see cref="E:System.Windows.Forms.Control.Resize"/> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.InitDefalutData();
            this.Paint_Rectangle(_lineNum);
        }

        //2. 该方法仅会在最初执行一次
        /// <summary>
        /// 引发 <see cref="M:System.Windows.Forms.Control.CreateControl"/> 方法。
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        #endregion

        #region Override: OnPaintBackground, OnPaint……

        //3.
        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            base.OnPaintBackground(pe);

            foreach (var item in this.CloudItems)
            {
                item.Location = Point.Ceiling(this.GetPointF(item.Size, item));
            }
            _currPoint = _defaultPoint;

            this.AutoScrollMinSize = new Size(this.Width - 20, (int)(_lineNum * _rowHeight));
            this.VerticalScroll.SmallChange = (int)_rowHeight;
            this.Paint_Rectangle(this.VerticalScroll.Value);
            pe.Graphics.DrawImage(_pntedImage, 0, 0);
        }

        //4.
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        #endregion

        #region Override: OnFontChanged
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Graphics g = this.CreateGraphics();
            _rowHeight = g.MeasureString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", this.Font).Height;
        }
        #endregion

        #region Inner methods

        /// <summary>
        /// Inits the defalut data.仅仅在OnResize中被调用
        /// </summary>
        protected virtual void InitDefalutData()
        {
            _lineNum = 0;
            _lines = new Dictionary<int, int>();
            _pntedImage = new Bitmap(this.Width, this.Height);
        }

        protected virtual void CloudsPanel_Scroll(object sender, ScrollEventArgs e)
        {
            switch (e.ScrollOrientation)
            {
                case ScrollOrientation.VerticalScroll:
                    break;
                case ScrollOrientation.HorizontalScroll:
                default:
                    break;
            }
        }

        /// <summary>
        /// 制图，根据指定的起始行号绘制当前控件的贴图
        /// </summary>
        /// <param name="lineNum">指定的起始行号</param>
        protected virtual void Paint_Rectangle(int lineNum)
        {
            if (this.CloudItems.Count <= 0 || _lines.Count == 0)
            {
                return;
            }

            using (Graphics g = Graphics.FromImage(_pntedImage))
            {
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                SolidBrush backcolorBrush = new SolidBrush(Color.Wheat);//new SolidBrush(this.BackColor);//
                SolidBrush txtBrush = new SolidBrush(Color.FromArgb(30, 30, 30));

                int innerLines = (_pntedImage.Height / (int)_rowHeight) + 1;

                for (int i = _lines[lineNum]; i < _lines[lineNum + innerLines + 1] - 1; i++)
                {
                    g.FillRectangle(backcolorBrush, new RectangleF(this.CloudItems[i].Location, this.CloudItems[i].Size));
                    g.DrawString(this.CloudItems[i].Text, this.Font, txtBrush, this.CloudItems[i].Location);
                }
            }
        }

        /// <summary>
        /// 获取指定的CloudLabel的左上角坐标
        /// </summary>
        /// <param name="sf">已计算出的Label的大小</param>
        /// <param name="label">指定的CloudLabel</param>
        /// <returns></returns>
        protected virtual PointF GetPointF(SizeF sf, CloudLabel label)
        {
            PointF pf = _currPoint;
            if ((pf.X + sf.Width) > this.Width - 20)
            {
                pf = new PointF(RowSpace, pf.Y + sf.Height + RowSpace);
                _lines.Add(_lineNum++, label.Number);
            }
            _currPoint = new PointF(pf.X + sf.Width + ColumnSpace, pf.Y);
            return pf;
        }

        #endregion

    }

}
