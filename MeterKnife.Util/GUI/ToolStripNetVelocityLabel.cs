using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    /// <summary>
    /// 显示在ToolStrip上的一个表示网络速度的控件
    /// </summary>
    public class ToolStripNetVelocityLabel : ToolStripLabel
    {
        public NetVelocity NetVeloctiy { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }

    /// <summary>
    /// 网络速度
    /// </summary>
    public enum NetVelocity
    {
        /// <summary>
        /// 很好
        /// </summary>
        VeryGood = 5,
        /// <summary>
        /// 好
        /// </summary>
        Good = 3,
        /// <summary>
        /// 一般
        /// </summary>
        General = 1,
        /// <summary>
        /// 不好
        /// </summary>
        Pool = 0,
        /// <summary>
        /// 坏的
        /// </summary>
        Bad = -1
    }
}
