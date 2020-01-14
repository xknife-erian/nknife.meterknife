using System;
using System.Drawing;
using NKnife.Configuring.Interfaces;

namespace NKnife.Configuring.Controls
{
    /// <summary>
    /// 2011.11.30放弃使用,改用定制特性
    /// </summary>
    public abstract class AbstractOptionBoxItem : IOptionBoxItem
    {
        /// <summary>
        /// 将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
        /// </summary>
        /// <param name="obj">与此实例进行比较的对象。</param>
        /// <returns>
        /// 一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此实例小于 <paramref name="obj"/>。零此实例等于 <paramref name="obj"/>。大于零此实例大于 <paramref name="obj"/>。
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="obj"/> 不具有与此实例相同的类型。</exception>
        public int CompareTo(object obj)
        {
            if (obj is IOptionBoxItem)
            {
                var item = (IOptionBoxItem) obj;
                return item.OrderIndex - OrderIndex;
            }
            return 0;
        }

        /// <summary>该项是否显示
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible
        {
            get { return true; }
        }

        /// <summary>获取一个配置的编辑面板的类型
        /// </summary>
        public abstract Type OptionUI { get; }

        /// <summary>
        /// 当Item做为菜单时的显示名
        /// </summary>
        /// <value>The name of the menu.</value>
        public abstract string MenuName { get; }

        /// <summary>
        /// 当Item做为菜单时的提示
        /// </summary>
        public abstract string MenuDescription { get; }

        /// <summary>当分组时Item所在的组
        /// </summary>
        /// <value>The menu group.</value>
        public abstract string MenuGroup { get; }

        /// <summary>当Item做为菜单时的显示的图标
        /// </summary>
        public abstract Image MenuIcon { get; }

        /// <summary>当Item在一个集合中的排序，数字越大，将排列越靠前
        /// </summary>
        /// <value>The index of the order.</value>
        public abstract int OrderIndex { get; }

    }
}