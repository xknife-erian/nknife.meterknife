using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.SkinForm
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class SkinForm : Form
    {
        #region Fields

        private SkinFormRenderer _renderer;
        private RoundStyle _roundStyle = RoundStyle.All;
        private int _radius = 8;
        private int _captionHeight = 24;
        private Font _captionFont = SystemFonts.CaptionFont;
        private int _borderWidth = 3;
        private Size _minimizeBoxSize = new Size(32, 18);
        private Size _maximizeBoxSize = new Size(32, 18);
        private Size _closeBoxSize = new Size(32, 18);
        private Point _controlBoxOffset = new Point(6, 0);
        private int _controlBoxSpace = -1;
        private bool _active;
        private ControlBoxManager _controlBoxManager;
        private Padding _padding;
        private bool _canResize = true;
        private bool _inPosChanged;
        private ToolTip _toolTip;

        private static readonly object EventRendererChanged = new object();

        #endregion

        #region Constructors

        public SkinForm()
            : base()
        {
            SetStyles();
            Init();
        }

        #endregion

        #region Events

        public event EventHandler RendererChangled
        {
            add { base.Events.AddHandler(EventRendererChanged, value); }
            remove { base.Events.RemoveHandler(EventRendererChanged, value); }
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SkinFormRenderer Renderer
        {
            get
            {
                if (_renderer == null)
                {
                    _renderer = new SkinFormProfessionalRenderer();
                }
                return _renderer;
            }
            set
            {
                _renderer = value;
                OnRendererChanged(EventArgs.Empty);
            }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                base.Invalidate(new Rectangle(
                    0,
                    0,
                    Width,
                    CaptionHeight + 1));
            }
        }

        [DefaultValue(typeof(RoundStyle),"1")]
        public RoundStyle RoundStyle
        {
            get { return _roundStyle; }
            set
            {
                if (_roundStyle != value)
                {
                    _roundStyle = value;
                    SetReion();
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(8)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value < 4 ? 4 : value;
                    SetReion();
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(24)]
        public int CaptionHeight
        {
            get { return _captionHeight; }
            set
            {
                if (_captionHeight != value)
                {
                    _captionHeight = value < _borderWidth ? 
                                    _borderWidth : value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(3)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (_borderWidth != value)
                {
                    _borderWidth = value < 1 ? 1 : value;
                }
            }
        }

        [DefaultValue(typeof(Font), "CaptionFont")]
        public Font CaptionFont
        {
            get { return _captionFont; }
            set
            {
                if (value == null)
                {
                    _captionFont = SystemFonts.CaptionFont;
                }
                else
                {
                    _captionFont = value;
                }
                base.Invalidate(CaptionRect);
            }
        }

        [DefaultValue(typeof(Size),"32, 18")]
        public Size MinimizeBoxSize
        {
            get { return _minimizeBoxSize; }
            set
            {
                if (_minimizeBoxSize != value)
                {
                    _minimizeBoxSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Size), "32, 18")]
        public Size MaximizeBoxSize
        {
            get { return _maximizeBoxSize; }
            set
            {
                if (_maximizeBoxSize != value)
                {
                    _maximizeBoxSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Size), "32, 18")]
        public Size CloseBoxSize
        {
            get { return _closeBoxSize; }
            set
            {
                if (_closeBoxSize != value)
                {
                    _closeBoxSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Point), "6, 0")]
        public Point ControlBoxOffset
        {
            get { return _controlBoxOffset; }
            set
            {
                if (_controlBoxOffset != value)
                {
                    _controlBoxOffset = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(-1)]
        public int ControlBoxSpace
        {
            get { return _controlBoxSpace; }
            set
            {
                if (_controlBoxSpace != value)
                {
                    _controlBoxSpace = value < 0 ? 0 : value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(true)]
        public bool CanResize
        {
            get { return _canResize; }
            set { _canResize = value; }
        }

        [DefaultValue(typeof(Padding), "0")]
        public new Padding Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                base.Padding = new Padding(
                    BorderWidth + _padding.Left,
                    CaptionHeight + _padding.Top,
                    BorderWidth + _padding.Right,
                    BorderWidth + _padding.Bottom);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(
                    BorderWidth,
                    CaptionHeight,
                    BorderWidth,
                    BorderWidth);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (!DesignMode)
                {
                    cp.Style |= (int)NativeMethods.WindowStyle.WS_THICKFRAME;

                    if (ControlBox)
                    {
                        cp.Style |= (int)NativeMethods.WindowStyle.WS_SYSMENU;
                    }

                    if (MinimizeBox)
                    {
                        cp.Style |= (int)NativeMethods.WindowStyle.WS_MINIMIZEBOX;
                    }

                    if (!MaximizeBox)
                    {
                        cp.Style &= ~(int)NativeMethods.WindowStyle.WS_MAXIMIZEBOX;
                    }

                    if (_inPosChanged)
                    {
                        cp.Style &= ~((int)NativeMethods.WindowStyle.WS_THICKFRAME |
                            (int)NativeMethods.WindowStyle.WS_SYSMENU);
                        cp.ExStyle &= ~((int)NativeMethods.WindowStyleEx.WS_EX_DLGMODALFRAME |
                            (int)NativeMethods.WindowStyleEx.WS_EX_WINDOWEDGE);
                    }
                }

                return cp;
            }
        }

        internal Rectangle CaptionRect
        {
            get { return new Rectangle(0, 0, Width, CaptionHeight); }
        }

        internal ControlBoxManager ControlBoxManager
        {
            get
            {
                if (_controlBoxManager == null)
                {
                    _controlBoxManager = new ControlBoxManager(this);
                }
                return _controlBoxManager;
            }
        }

        internal Rectangle IconRect
        {
            get
            {
                if (base.ShowIcon && base.Icon != null)
                {
                    int width = SystemInformation.SmallIconSize.Width;
                    if (CaptionHeight - BorderWidth - 4 < width)
                    {
                        width = CaptionHeight - BorderWidth - 4;
                    }
                    return new Rectangle(
                        BorderWidth,
                        BorderWidth + (CaptionHeight - BorderWidth - width) / 2,
                        width,
                        width);
                }
                return Rectangle.Empty;
            }
        }

        internal ToolTip ToolTip
        {
            get { return _toolTip; }
        }

        #endregion

        #region Override Methods

        protected virtual void OnRendererChanged(EventArgs e)
        {
            Renderer.InitSkinForm(this);
            EventHandler handler =
                base.Events[EventRendererChanged] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
            base.Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetReion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetReion();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ControlBoxManager.ProcessMouseOperate(
                e.Location, MouseOperate.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ControlBoxManager.ProcessMouseOperate(
                e.Location, MouseOperate.Down);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ControlBoxManager.ProcessMouseOperate(
                e.Location, MouseOperate.Up);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ControlBoxManager.ProcessMouseOperate(
                Point.Empty, MouseOperate.Leave);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            ControlBoxManager.ProcessMouseOperate(
                PointToClient(MousePosition), MouseOperate.Hover);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;

            SkinFormRenderer renderer = Renderer;
            renderer.DrawSkinFormBackground(
                new SkinFormBackgroundRenderEventArgs(
                this, g, rect));
            renderer.DrawSkinFormCaption(
                new SkinFormCaptionRenderEventArgs(
                this, g, CaptionRect, _active));
            renderer.DrawSkinFormBorder(
                new SkinFormBorderRenderEventArgs(
                this, g, rect, _active));

            if (ControlBoxManager.CloseBoxVisibale)
            {
                renderer.DrawSkinFormControlBox(
                    new SkinFormControlBoxRenderEventArgs(
                    this,
                    g,
                    ControlBoxManager.CloseBoxRect,
                    _active,
                    ControlBoxStyle.Close,
                    ControlBoxManager.CloseBoxState));
            }

            if (ControlBoxManager.MaximizeBoxVisibale)
            {
                renderer.DrawSkinFormControlBox(
                    new SkinFormControlBoxRenderEventArgs(
                    this,
                    g,
                    ControlBoxManager.MaximizeBoxRect,
                    _active,
                    ControlBoxStyle.Maximize,
                    ControlBoxManager.MaximizeBoxState));
            }

            if (ControlBoxManager.MinimizeBoxVisibale)
            {
                renderer.DrawSkinFormControlBox(
                    new SkinFormControlBoxRenderEventArgs(
                    this,
                    g,
                    ControlBoxManager.MinimizeBoxRect,
                    _active,
                    ControlBoxStyle.Minimize,
                    ControlBoxManager.MinimizeBoxState));
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)NativeMethods.WindowMessages.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                case (int)NativeMethods.WindowMessages.WM_NCPAINT:
                case (int)NativeMethods.WindowMessages.WM_NCCALCSIZE:
                    break;
                case (int)NativeMethods.WindowMessages.WM_WINDOWPOSCHANGED:
                    _inPosChanged = true;
                    base.WndProc(ref m);
                    _inPosChanged = false;
                    break;
                case (int)NativeMethods.WindowMessages.WM_GETMINMAXINFO:
                    WmGetMinMaxInfo(ref m);
                    break;
                case (int)NativeMethods.WindowMessages.WM_NCACTIVATE:
                    WmNcActive(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_controlBoxManager != null)
                {
                    _controlBoxManager.Dispose();
                    _controlBoxManager = null;
                }

                _renderer = null;
                _toolTip.Dispose();
            }
        }

        #endregion

        #region Message Methods

        private void WmNcHitTest(ref Message m)
        {
            int wparam = m.LParam.ToInt32();
            Point point = new Point(
                NativeMethods.LOWORD(wparam),
                NativeMethods.HIWORD(wparam));
            point = PointToClient(point);

            if (IconRect.Contains(point))
            {
                m.Result = new IntPtr(
                    (int)NativeMethods.NCHITTEST.HTSYSMENU);
                return;
            }

            if (_canResize)
            {
                if (point.X < 5 && point.Y < 5)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTTOPLEFT);
                    return;
                }

                if (point.X > Width - 5 && point.Y < 5)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTTOPRIGHT);
                    return;
                }

                if (point.X < 5 && point.Y > Height - 5)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTBOTTOMLEFT);
                    return;
                }

                if (point.X > Width - 5 && point.Y > Height - 5)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTBOTTOMRIGHT);
                    return;
                }

                if (point.Y < 3)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTTOP);
                    return;
                }

                if (point.Y > Height - 3)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTBOTTOM);
                    return;
                }

                if (point.X < 3)
                {
                    m.Result = new IntPtr(
                       (int)NativeMethods.NCHITTEST.HTLEFT);
                    return;
                }

                if (point.X > Width - 3)
                {
                    m.Result = new IntPtr(
                       (int)NativeMethods.NCHITTEST.HTRIGHT);
                    return;
                }
            }

            if (point.Y < CaptionHeight)
            {
                if (!ControlBoxManager.CloseBoxRect.Contains(point) &&
                    !ControlBoxManager.MaximizeBoxRect.Contains(point) &&
                    !ControlBoxManager.MinimizeBoxRect.Contains(point))
                {
                    m.Result = new IntPtr(
                      (int)NativeMethods.NCHITTEST.HTCAPTION);
                    return;
                }
            }
            m.Result = new IntPtr(
                     (int)NativeMethods.NCHITTEST.HTCLIENT);
        }

        private void WmGetMinMaxInfo(ref Message m)
        {
            NativeMethods.MINMAXINFO minmax =
                (NativeMethods.MINMAXINFO)Marshal.PtrToStructure(
                m.LParam, typeof(NativeMethods.MINMAXINFO));

            if (MaximumSize != Size.Empty)
            {
                minmax.maxTrackSize = MaximumSize;
            }
            else
            {
                Rectangle rect = Screen.GetWorkingArea(this);

                minmax.maxPosition = new Point(
                    rect.X - BorderWidth, 
                    rect.Y);
                minmax.maxTrackSize = new Size(
                    rect.Width + BorderWidth * 2,
                    rect.Height + BorderWidth);
            }

            if (MinimumSize != Size.Empty)
            {
                minmax.minTrackSize = MinimumSize;
            }
            else
            {
                minmax.minTrackSize = new Size(
                    CloseBoxSize.Width + MinimizeBoxSize.Width +
                    MaximizeBoxSize.Width + ControlBoxOffset.X +
                    ControlBoxSpace * 2 + SystemInformation.SmallIconSize.Width +
                    BorderWidth * 2 + 3,
                    CaptionHeight);
            }

            Marshal.StructureToPtr(minmax, m.LParam, false);
        }

        private void WmNcActive(ref Message m)
        {
            if (m.WParam.ToInt32() == 1)
            {
                _active = true;
            }
            else
            {
                _active = false;
            }
            m.Result = NativeMethods.TRUE;
            base.Invalidate();
        }

        #endregion

        #region Private Methods

        private void SetStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }

        private void SetReion()
        {
            if (base.Region != null)
            {
                base.Region.Dispose();
            }
            base.Region = Renderer.CreateRegion(this);
        }

        private void Init()
        {
            _toolTip = new ToolTip();
            base.FormBorderStyle = FormBorderStyle.None;
            Renderer.InitSkinForm(this);
            base.Padding = DefaultPadding;
        }

        #endregion
    }
}
