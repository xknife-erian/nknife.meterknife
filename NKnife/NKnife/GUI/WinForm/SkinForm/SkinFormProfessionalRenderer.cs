using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.SkinForm
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class SkinFormProfessionalRenderer : SkinFormRenderer
    {
        private SkinFormColorTable _colorTable;

        public SkinFormProfessionalRenderer()
            : base()
        {
        }

        public SkinFormProfessionalRenderer(SkinFormColorTable colortable)
            : base()
        {
            _colorTable = colortable;
        }

        public SkinFormColorTable ColorTable
        {
            get 
            {
                if (_colorTable == null)
                {
                    _colorTable = new SkinFormColorTable();
                }
                return _colorTable;
            }
        }

        public override Region CreateRegion(SkinForm form)
        {
            Rectangle rect = new Rectangle(Point.Empty, form.Size);

            using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                rect,
                form.Radius,
                form.RoundStyle,
                false))
            {
                return new Region(path);
            }
        }

        public override void InitSkinForm(SkinForm form)
        {
            form.BackColor = ColorTable.Back;
        }

        protected override void OnRenderSkinFormCaption(
            SkinFormCaptionRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            SkinForm form = e.SkinForm;
            Rectangle iconRect = form.IconRect;
            Rectangle textRect = Rectangle.Empty;

            bool closeBox = form.ControlBox;
            bool minimizeBox = form.ControlBox && form.MinimizeBox;
            bool maximizeBox = form.ControlBox && form.MaximizeBox;

            int textWidthDec = 0;
            if (closeBox)
            {
                textWidthDec += form.CloseBoxSize.Width + form.ControlBoxOffset.X;
            }

            if (maximizeBox)
            {
                textWidthDec += form.MaximizeBoxSize.Width + form.ControlBoxSpace;
            }

            if (minimizeBox)
            {
                textWidthDec += form.MinimizeBoxSize.Width + form.ControlBoxSpace;
            }

            textRect = new Rectangle(
                iconRect.Right + 3,
                form.BorderWidth,
                rect.Width - iconRect.Right - textWidthDec - 6,
                rect.Height - form.BorderWidth);

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                DrawCaptionBackground(
                    g,
                    rect,
                    e.Active);

                if (form.ShowIcon && form.Icon != null)
                {
                    DrawIcon(g, iconRect, form.Icon);
                }

                if (!string.IsNullOrEmpty(form.Text))
                {
                    DrawCaptionText(
                        g,
                        textRect,
                        form.Text,
                        form.CaptionFont);
                }
            }
        }

        protected override void OnRenderSkinFormBorder(
            SkinFormBorderRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                DrawBorder(
                    g, 
                    e.ClipRectangle, 
                    e.SkinForm.RoundStyle, 
                    e.SkinForm.Radius);
            }
        }

        protected override void OnRenderSkinFormBackground(
            SkinFormBackgroundRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            SkinForm form = e.SkinForm;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                using (Brush brush = new SolidBrush(ColorTable.Back))
                {
                    using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                        rect, form.Radius, form.RoundStyle, false))
                    {
                        g.FillPath(brush, path);
                    }
                }
            }
        }

        protected override void OnRenderSkinFormControlBox(
            SkinFormControlBoxRenderEventArgs e)
        {
            SkinForm form = e.Form;
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            ControlBoxState state = e.ControlBoxtate;
            bool active = e.Active;

            bool minimizeBox = form.ControlBox && form.MinimizeBox;
            bool maximizeBox = form.ControlBox && form.MaximizeBox;

            switch (e.ControlBoxStyle)
            {
                case ControlBoxStyle.Close:
                    RenderSkinFormCloseBoxInternal(
                        g,
                        rect,
                        state,
                        active,
                        minimizeBox,
                        maximizeBox);
                    break;
                case ControlBoxStyle.Maximize:
                    RenderSkinFormMaximizeBoxInternal(
                        g,
                        rect,
                        state,
                        active,
                        minimizeBox,
                        form.WindowState == FormWindowState.Maximized);
                    break;
                case ControlBoxStyle.Minimize:
                    RenderSkinFormMinimizeBoxInternal(
                       g,
                       rect,
                       state,
                       active);
                    break;
            }
        }

        #region Draw Methods

        private void DrawCaptionBackground(
            Graphics g, Rectangle captionRect, bool active)
        {
            Color baseColor = active ?
                ColorTable.CaptionActive : ColorTable.CaptionDeactive;

            RenderHelper.RenderBackgroundInternal(
                g,
                captionRect,
                baseColor,
                ColorTable.Border,
                ColorTable.InnerBorder,
                RoundStyle.None,
                0,
                .25f,
                false,
                false,
                LinearGradientMode.Vertical);
        }

        private void DrawIcon(
            Graphics g, Rectangle iconRect, Icon icon)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawIcon(
                icon,
                iconRect);
        }

        private void DrawCaptionText(
            Graphics g, Rectangle textRect, string text, Font font)
        {
            TextRenderer.DrawText(
                g,
                text,
                font,
                textRect,
                ColorTable.CaptionText,
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.Left |
                TextFormatFlags.SingleLine |
                TextFormatFlags.WordEllipsis);
        }

        private void DrawBorder(
            Graphics g, Rectangle rect,RoundStyle roundStyle, int radius)
        {
            rect.Width -= 1;
            rect.Height -= 1;
            using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                rect, radius, roundStyle, false))
            {
                using (Pen pen = new Pen(ColorTable.Border))
                {
                    g.DrawPath(pen, path);
                }
            }

            rect.Inflate(-1, -1);
            using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                rect, radius, roundStyle, false))
            {
                using (Pen pen = new Pen(ColorTable.InnerBorder))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private void RenderSkinFormMinimizeBoxInternal(
           Graphics g,
           Rectangle rect,
           ControlBoxState state,
           bool active)
        {
            Color baseColor = ColorTable.ControlBoxActive;

            if (state == ControlBoxState.Pressed)
            {
                baseColor = ColorTable.ControlBoxPressed;
            }
            else if (state == ControlBoxState.Hover)
            {
                baseColor = ColorTable.ControlBoxHover;
            }
            else
            {
                baseColor = active ?
                    ColorTable.ControlBoxActive :
                    ColorTable.ControlBoxDeactive;
            }

            RoundStyle roundStyle = RoundStyle.BottomLeft;

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                RenderHelper.RenderBackgroundInternal(
                    g,
                    rect,
                    baseColor,
                    baseColor,
                    ColorTable.ControlBoxInnerBorder,
                    roundStyle,
                    6,
                    .38F,
                    true,
                    false,
                    LinearGradientMode.Vertical);

                using (Pen pen = new Pen(ColorTable.Border))
                {
                    g.DrawLine(pen, rect.X, rect.Y, rect.Right, rect.Y);
                }

                using (GraphicsPath path = CreateMinimizeFlagPath(rect))
                {
                    g.FillPath(Brushes.White, path);
                    using (Pen pen = new Pen(baseColor))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        private void RenderSkinFormMaximizeBoxInternal(
            Graphics g,
            Rectangle rect,
            ControlBoxState state,
            bool active,
            bool minimizeBox,
            bool maximize)
        {
            Color baseColor = ColorTable.ControlBoxActive;

            if (state == ControlBoxState.Pressed)
            {
                baseColor = ColorTable.ControlBoxPressed;
            }
            else if (state == ControlBoxState.Hover)
            {
                baseColor = ColorTable.ControlBoxHover;
            }
            else
            {
                baseColor = active ?
                    ColorTable.ControlBoxActive :
                    ColorTable.ControlBoxDeactive;
            }

            RoundStyle roundStyle = minimizeBox ?
                RoundStyle.None : RoundStyle.BottomLeft;

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                RenderHelper.RenderBackgroundInternal(
                    g,
                    rect,
                    baseColor,
                    baseColor,
                    ColorTable.ControlBoxInnerBorder,
                    roundStyle,
                    6,
                    .38F,
                    true,
                    false,
                    LinearGradientMode.Vertical);

                using (Pen pen = new Pen(ColorTable.Border))
                {
                    g.DrawLine(pen, rect.X, rect.Y, rect.Right, rect.Y);
                }

                using (GraphicsPath path = CreateMaximizeFlafPath(rect, maximize))
                {
                    g.FillPath(Brushes.White, path);
                    using (Pen pen = new Pen(baseColor))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        private void RenderSkinFormCloseBoxInternal(
            Graphics g,
            Rectangle rect,
            ControlBoxState state,
            bool active,
            bool minimizeBox,
            bool maximizeBox)
        {
            Color baseColor = ColorTable.ControlBoxActive;

            if (state == ControlBoxState.Pressed)
            {
                baseColor = ColorTable.ControlCloseBoxPressed;
            }
            else if (state == ControlBoxState.Hover)
            {
                baseColor = ColorTable.ControlCloseBoxHover;
            }
            else
            {
                baseColor = active ?
                    ColorTable.ControlBoxActive :
                    ColorTable.ControlBoxDeactive;
            }

            RoundStyle roundStyle = minimizeBox || maximizeBox ?
                RoundStyle.BottomRight : RoundStyle.Bottom;

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                RenderHelper.RenderBackgroundInternal(
                    g,
                    rect,
                    baseColor,
                    baseColor,
                    ColorTable.ControlBoxInnerBorder,
                    roundStyle,
                    6,
                    .38F,
                    true,
                    false,
                    LinearGradientMode.Vertical);

                using (Pen pen = new Pen(ColorTable.Border))
                {
                    g.DrawLine(pen, rect.X, rect.Y, rect.Right, rect.Y);
                }

                using (GraphicsPath path = CreateCloseFlagPath(rect))
                {
                    g.FillPath(Brushes.White, path);
                    using (Pen pen = new Pen(baseColor))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        #endregion

        private GraphicsPath CreateCloseFlagPath(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2.0f,
                rect.Y + rect.Height / 2.0f);

            GraphicsPath path = new GraphicsPath();

            path.AddLine(
                centerPoint.X,
                centerPoint.Y - 2,
                centerPoint.X - 2,
                centerPoint.Y - 4);
            path.AddLine(
                centerPoint.X - 2,
                centerPoint.Y - 4,
                centerPoint.X - 6,
                centerPoint.Y - 4);
            path.AddLine(
                centerPoint.X - 6,
                centerPoint.Y - 4,
                centerPoint.X - 2,
                centerPoint.Y);
            path.AddLine(
                centerPoint.X - 2,
                centerPoint.Y,
                centerPoint.X - 6,
                centerPoint.Y + 4);
            path.AddLine(
                centerPoint.X - 6,
                centerPoint.Y + 4,
                centerPoint.X - 2,
                centerPoint.Y + 4);
            path.AddLine(
                centerPoint.X - 2,
                centerPoint.Y + 4,
                centerPoint.X,
                centerPoint.Y + 2);
            path.AddLine(
                centerPoint.X,
                centerPoint.Y + 2,
                centerPoint.X + 2,
                centerPoint.Y + 4);
            path.AddLine(
               centerPoint.X + 2,
               centerPoint.Y + 4,
               centerPoint.X + 6,
               centerPoint.Y + 4);
            path.AddLine(
              centerPoint.X + 6,
              centerPoint.Y + 4,
              centerPoint.X + 2,
              centerPoint.Y);
            path.AddLine(
             centerPoint.X + 2,
             centerPoint.Y,
             centerPoint.X + 6,
             centerPoint.Y - 4);
            path.AddLine(
             centerPoint.X + 6,
             centerPoint.Y - 4,
             centerPoint.X + 2,
             centerPoint.Y - 4);

            path.CloseFigure();
            return path;
        }

        private GraphicsPath CreateMinimizeFlagPath(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2.0f,
                rect.Y + rect.Height / 2.0f);

            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(
                centerPoint.X - 6,
                centerPoint.Y + 1,
                12,
                3));
            return path;
        }

        private GraphicsPath CreateMaximizeFlafPath(
            Rectangle rect, bool maximize)
        {
            PointF centerPoint = new PointF(
               rect.X + rect.Width / 2.0f,
               rect.Y + rect.Height / 2.0f);

            GraphicsPath path = new GraphicsPath();

            if (maximize)
            {
                path.AddLine(
                    centerPoint.X - 3,
                    centerPoint.Y - 3,
                    centerPoint.X - 6,
                    centerPoint.Y - 3);
                path.AddLine(
                    centerPoint.X - 6,
                    centerPoint.Y - 3,
                    centerPoint.X - 6,
                    centerPoint.Y + 5);
                path.AddLine(
                    centerPoint.X - 6,
                    centerPoint.Y + 5,
                    centerPoint.X + 3,
                    centerPoint.Y + 5);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y + 5,
                    centerPoint.X + 3,
                    centerPoint.Y + 1);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y + 1,
                    centerPoint.X + 6,
                    centerPoint.Y + 1);
                path.AddLine(
                    centerPoint.X + 6,
                    centerPoint.Y + 1,
                    centerPoint.X + 6,
                    centerPoint.Y - 6);
                path.AddLine(
                    centerPoint.X + 6,
                    centerPoint.Y - 6,
                    centerPoint.X - 3,
                    centerPoint.Y - 6);
                path.CloseFigure();

                path.AddRectangle(new RectangleF(
                    centerPoint.X - 4,
                    centerPoint.Y,
                    5,
                    3));

                path.AddLine(
                    centerPoint.X - 1,
                    centerPoint.Y - 4,
                    centerPoint.X + 4,
                    centerPoint.Y - 4);
                path.AddLine(
                    centerPoint.X + 4,
                    centerPoint.Y - 4,
                    centerPoint.X + 4,
                    centerPoint.Y - 1);
                path.AddLine(
                    centerPoint.X + 4,
                    centerPoint.Y - 1,
                    centerPoint.X + 3,
                    centerPoint.Y - 1);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y - 1,
                    centerPoint.X + 3,
                    centerPoint.Y - 3);
                path.AddLine(
                    centerPoint.X + 3,
                    centerPoint.Y - 3,
                    centerPoint.X - 1,
                    centerPoint.Y - 3);
                path.CloseFigure();
            }
            else
            {
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 6,
                    centerPoint.Y - 4,
                    12,
                    8));
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 3,
                    centerPoint.Y - 1,
                    6,
                    3));
            }

            return path;
        }
    }
}
