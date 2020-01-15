using System;
using System.Drawing;
using NKnife.Configuring.Controls;

namespace NKnife.Configuring.Interfaces
{
    public interface IOptionListItem : IComparable
    {
        /// <summary>当Item做为菜单时的高度
        /// </summary>
        /// <value>The height of the menu.</value>
        int MenuHeight { get; }

        /// <summary>当Item做为菜单时的显示名
        /// </summary>
        /// <value>The name of the menu.</value>
        string MenuName { get; }

        /// <summary>当Item做为菜单时的提示
        /// </summary>
        string MenuHint { get; }

        /// <summary>当Item做为菜单时的显示的图标
        /// </summary>
        Image MenuIcon { get; }

        /// <summary>获取一个配置的编辑面板，请务必采用单建模式
        /// </summary>
        OptionControlBase OptionUI { get; }

        /// <summary>该项是否显示
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; set; }

        /// <summary>当Item在一个集合中的排序，数字越大，将排列越靠前
        /// </summary>
        /// <value>The index of the order.</value>
        int OrderIndex { get; }

        /// <summary>释放链接的OptionControlBase
        /// </summary>
        void DisposeUI();
    }
}
