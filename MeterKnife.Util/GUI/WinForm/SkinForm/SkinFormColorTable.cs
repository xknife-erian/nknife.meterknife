using System.Drawing;

namespace NKnife.GUI.WinForm.SkinForm
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class SkinFormColorTable
    {
        private static readonly Color _captionActive = 
            Color.FromArgb(75, 188, 254);
        private static readonly Color _captionDeactive = 
            Color.FromArgb(131, 209, 255);
        private static readonly Color _captionText =
            Color.FromArgb(40, 111, 152);
        private static readonly Color _border = 
            Color.FromArgb(55, 126, 168);
        private static readonly Color _innerBorder =
            Color.FromArgb(200, 250, 250, 250);
        private static readonly Color _back = 
            Color.FromArgb(128, 208, 255);
        private static readonly Color _controlBoxActive =
            Color.FromArgb(51, 153, 204);
        private static readonly Color _controlBoxDeactive =
            Color.FromArgb(88, 172, 218);
        private static readonly Color _controlBoxHover =
            Color.FromArgb(37, 114, 151);
        private static readonly Color _controlBoxPressed =
           Color.FromArgb(27, 84, 111);
        private static readonly Color _controlCloseBoxHover =
            Color.FromArgb(213, 66, 22);
        private static readonly Color _controlCloseBoxPressed =
            Color.FromArgb(171, 53, 17);
        private static readonly Color _controlBoxInnerBorder =
            Color.FromArgb(128, 250, 250, 250);

        public virtual Color CaptionActive
        {
            get { return _captionActive; }
        }

        public virtual Color CaptionDeactive
        {
            get { return _captionDeactive; }
        }

        public virtual Color CaptionText
        {
            get { return _captionText; }
        }

        public virtual Color Border
        {
            get { return _border; }
        }

        public virtual Color InnerBorder
        {
            get { return _innerBorder; }
        }

        public virtual Color Back
        {
            get { return _back; }
        }

        public virtual Color ControlBoxActive
        {
            get { return _controlBoxActive; }
        }

        public virtual Color ControlBoxDeactive
        {
            get { return _controlBoxDeactive; }
        }

        public virtual Color ControlBoxHover
        {
            get { return _controlBoxHover; }
        }

        public virtual Color ControlBoxPressed
        {
            get { return _controlBoxPressed; }
        }

        public virtual Color ControlCloseBoxHover
        {
            get { return _controlCloseBoxHover; }
        }

        public virtual Color ControlCloseBoxPressed
        {
            get { return _controlCloseBoxPressed; }
        }

        public virtual Color ControlBoxInnerBorder
        {
            get { return _controlBoxInnerBorder; }
        }
    }
}
