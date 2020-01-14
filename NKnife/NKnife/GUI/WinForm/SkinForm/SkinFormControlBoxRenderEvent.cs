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

    public delegate void SkinFormControlBoxRenderEventHandler(
        object sender,
        SkinFormControlBoxRenderEventArgs e);

    public class SkinFormControlBoxRenderEventArgs : PaintEventArgs
    {
        private SkinForm _form;
        private bool _active;
        private ControlBoxState _controlBoxState;
        private ControlBoxStyle _controlBoxStyle;

        public SkinFormControlBoxRenderEventArgs(
            SkinForm form,
            Graphics graphics,
            Rectangle clipRect,
            bool active,
            ControlBoxStyle controlBoxStyle,
            ControlBoxState controlBoxState)
            : base(graphics, clipRect)
        {
            _form = form;
            _active = active;
            _controlBoxState = controlBoxState;
            _controlBoxStyle = controlBoxStyle;
        }

        public SkinForm Form
        {
            get { return _form; }
        }

        public bool Active
        {
            get { return _active; }
        }

        public ControlBoxStyle ControlBoxStyle
        {
            get { return _controlBoxStyle; }
        }

        public ControlBoxState ControlBoxtate
        {
            get { return _controlBoxState; }
        }
    }
}
