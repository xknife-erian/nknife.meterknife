using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NKnife.GUI.WinForm.SkinForm
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class AntiAliasGraphics : IDisposable
    {
        private SmoothingMode _oldMode;
        private Graphics _graphics;

        public AntiAliasGraphics(Graphics graphics)
        {
            _graphics = graphics;
            _oldMode = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.SmoothingMode = _oldMode;
        }

        #endregion
    }
}
