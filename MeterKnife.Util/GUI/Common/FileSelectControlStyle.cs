namespace MeterKnife.Util.GUI.Common
{
    /// <summary>
    /// 文件选择控件样式枚举
    /// </summary>
    public enum FileSelectControlStyle
    {
        None = 0,

        /// <summary>
        /// 文件名显示在TextBox中, 按钮上显示文字
        /// </summary>
        TextBoxAndTextButton = 1,

        /// <summary>
        /// 文件名显示在TextBox中, 按钮上显示图像
        /// </summary>
        TextBoxAndImageButton = 2,

        /// <summary>
        /// 文件名显示在TextBox中, 按钮上显示文字与图像
        /// </summary>
        TextBoxAndTextImageButton = 3,

        /// <summary>
        /// 文件名显示在ComboBox中, 按钮上显示文字
        /// </summary>
        ComboBoxAndTextButton = 4,

        /// <summary>
        /// 文件名显示在ComboBox中, 按钮上显示图像
        /// </summary>
        ComboBoxAndImageButton = 5,

        /// <summary>
        /// 文件名显示在ComboBox中, 按钮上显示文字与图像
        /// </summary>
        ComboBoxAndTextImageButton = 6,

        /// <summary>
        /// 仅按钮, 按钮上显示文字
        /// </summary>
        OnlyTextButton = 7,

        /// <summary>
        /// 仅按钮, 按钮上显示图像
        /// </summary>
        OnlyImageButton = 8,

        /// <summary>
        /// 仅按钮, 按钮上显示文字与图像
        /// </summary>
        OnlyTextImageButton = 9
    }
}