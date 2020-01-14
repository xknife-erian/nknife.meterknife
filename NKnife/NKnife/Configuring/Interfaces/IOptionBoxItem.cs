using System;
using System.Drawing;

namespace NKnife.Configuring.Interfaces
{
    public interface IOptionBoxItem : IComparable
    {
        /// <summary>当Item做为菜单时的显示名
        /// </summary>
        /// <value>The name of the menu.</value>
        string MenuName { get; }

        /// <summary>当Item做为菜单时的提示
        /// </summary>
        string MenuDescription { get; }

        /// <summary>当分组时Item所在的组
        /// </summary>
        /// <value>The menu group.</value>
        string MenuGroup { get; }

        /// <summary>当Item做为菜单时的显示的图标
        /// </summary>
        Image MenuIcon { get; }

        /// <summary>获取一个配置的编辑面板的类型
        /// </summary>
        Type OptionUI { get; }

        /// <summary>该项是否显示
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; }

        /// <summary>当Item在一个集合中的排序，数字越大，将排列越靠前
        /// </summary>
        /// <value>The index of the order.</value>
        int OrderIndex { get; }
    }
}