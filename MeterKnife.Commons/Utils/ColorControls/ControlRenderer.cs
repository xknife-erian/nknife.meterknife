using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MeterKnife.Utils.ColorControls
{
    public class ControlRenderer
    {
        public Color ColorVeryDarkBackground { get; set; }
        public Color ColorLightBackground { get; set; }
        public Color ColorDarkBackground { get; set; }
        public Color ColorBackground { get; set; }
        public Color ColorGrayText { get; set; }
        public Color ColorMultiple { get; set; }
        public Color ColorText { get; set; }
        public Color ColorVeryLightBackground { get; set; }
        public Color ColorHightlight { get; set; }

        public ControlRenderer()
        {
            ColorHightlight = SystemColors.Highlight;
            ColorVeryDarkBackground = SystemColors.ControlDarkDark;
            ColorDarkBackground = SystemColors.ControlDark;
            ColorBackground = SystemColors.Control;
            ColorLightBackground = SystemColors.ControlLight;
            ColorVeryLightBackground = SystemColors.ControlLightLight;
            ColorText = SystemColors.ControlText;
            ColorMultiple = Color.Bisque;
            ColorGrayText = SystemColors.GrayText;
        }

        public void DrawBorder(Graphics g, Rectangle rect, BorderStyle style, BorderState state)
        {
            var darkColor = ColorDarkBackground;
            var lightColor = ColorLightBackground;

            if (style == BorderStyle.Simple || style == BorderStyle.Focus)
            {
                darkColor = ColorVeryDarkBackground;
            }
            else if (style == BorderStyle.Sunken)
            {
                darkColor = Color.FromArgb(128, ColorDarkBackground);
                lightColor = ColorLightBackground;
            }

            var darkPen = new Pen(state == BorderState.Disabled ? Color.FromArgb(darkColor.A / 2, darkColor) : darkColor);
            var lightPen = new Pen(state == BorderState.Disabled ? Color.FromArgb(lightColor.A / 2, lightColor) : lightColor);

            if (style == BorderStyle.ContentBox)
            {
                g.DrawRectangle(lightPen, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
                g.DrawRectangle(darkPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
            else if (style == BorderStyle.Simple)
            {
                g.DrawRectangle(darkPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
            else if (style == BorderStyle.Focus)
            {
                darkPen.DashStyle = DashStyle.Dot;
                g.DrawRectangle(darkPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
            else if (style == BorderStyle.Sunken)
            {
                g.DrawRectangle(lightPen, rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 1);
                g.DrawRectangle(darkPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
        }
    }

    public enum BorderState
    {
        Normal,
        Disabled
    }

    public enum BorderStyle
    {
        Simple,
        Focus,
        ContentBox,
        Sunken
    }
}