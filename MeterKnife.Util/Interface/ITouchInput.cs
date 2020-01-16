using System.Drawing;

namespace MeterKnife.Util.Interface
{
    /// <summary>
    ///     描述一个屏幕输入界面的接口
    /// </summary>
    public interface ITouchInput
    {
        Size OwnSize { get; }
        Point OwnLocation { get; }

        /// <summary>
        ///     显示输入界面
        /// </summary>
        /// <param name="mode">输入模式:0.拼音;1.手写;2.符号;3.小写英文;4.大写英文;5.数字</param>
        /// <param name="location">界面的左上角坐标</param>
        void ShowInputView(short mode, Point location);

        /// <summary>
        ///     隐藏输入界面
        /// </summary>
        void HideInputView();

        /// <summary>
        ///     退出输入界面
        /// </summary>
        void Exit();
    }
}