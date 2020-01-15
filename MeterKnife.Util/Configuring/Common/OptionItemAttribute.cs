using System;

namespace NKnife.Configuring.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class OptionPanelAttribute : Attribute, IComparable
    {
        /// <summary>描述一个选项面板的特性
        /// </summary>
        /// <param name="name">面板在左侧的名字.</param>
        /// <param name="panel">面板的实际类型</param>
        /// <param name="plugin">面板所属的插件(当为Null时，该选项为公共选项，必须加载).</param>
        /// <param name="precondition">面板是否加载的先决条件</param>
        /// <param name="iconIndex">面板的图标的索引值.</param>
        /// <param name="order">面板在左侧列表中的顺序.</param>
        /// <param name="group">面板所必的组.</param>
        /// <param name="description">面板的描述.</param>
        /// <param name="enable">if set to <c>true</c> 则面板启用，否则不启用(不加载).</param>
        public OptionPanelAttribute(string name, Type panel, Type plugin, Type precondition, ushort iconIndex = 0, int order = 0, string @group = "BASE", string description = "", bool enable = true)
        {
            Name = name;
            Panel = panel;
            Precondition = precondition;
            Description = string.IsNullOrWhiteSpace(description) ? name : description;
            IconIndex = iconIndex;
            OrderIndex = order;
            Enable = enable;
            Plugin = plugin;
            MenuGroup = group;
        }

        public Type Panel { get; set; }

        public Type Precondition { get; set; }

        /// <summary>插件的类型(为Null时表示为公共选项)
        /// </summary>
        public Type Plugin { get; set; }

        /// <summary>是否启用
        /// </summary>
        /// <value>The name of the menu.</value>
        public bool Enable { get; set; }

        /// <summary>当Item做为菜单时的显示名
        /// </summary>
        /// <value>The name of the menu.</value>
        public string Name { get; set; }

        /// <summary>菜单分组
        /// </summary>
        public string MenuGroup { get; set; }

        /// <summary>当Item做为菜单时的提示
        /// </summary>
        public string Description { get; set; }

        /// <summary>左侧的选项List中的Icon
        /// </summary>
        /// <value>
        /// The index of the icon.
        /// </value>
        public ushort IconIndex { get; set; }

        /// <summary>当Item在一个集合中的排序，数字越大，将排列越靠前
        /// </summary>
        /// <value>The index of the order.</value>
        public int OrderIndex { get; set; }

        #region Implementation of IComparable

        public int CompareTo(object obj)
        {
            var attribute = obj as OptionPanelAttribute;
            if (attribute != null)
            {
                var item = attribute;
                return item.OrderIndex - OrderIndex;
            }
            return 0;
        }

        #endregion
    }
}