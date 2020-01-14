using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.SkinForm
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public delegate void SkinFormCaptionRenderEventHandler(
        object sender,
        SkinFormCaptionRenderEventArgs e);

    public class SkinFormCaptionRenderEventArgs : PaintEventArgs
    {
        private SkinForm _skinForm;
        private bool _active;

        public SkinFormCaptionRenderEventArgs(
            SkinForm skinForm,
            Graphics g,
            Rectangle clipRect,
            bool active)
            : base(g, clipRect)
        {
            _skinForm = skinForm;
            _active = active;
        }

        public SkinForm SkinForm
        {
            get { return _skinForm; }
        }

        public bool Active
        {
            get { return _active; }
        }
    }
}
