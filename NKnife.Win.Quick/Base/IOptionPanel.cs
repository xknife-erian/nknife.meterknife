using System;
using System.Collections.Generic;
using System.Drawing;

namespace NKnife.Win.Quick.Base
{
    /// <summary>
    /// 描述一个选项面板的接口
    /// </summary>
    public interface IOptionPanel
    {
        /// <summary>
        /// 选项面板的名字
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 选项面板的图标
        /// </summary>
        Icon Icon { get; set; }
        /// <summary>
        /// 是否有选项数据发生变化
        /// </summary>
        bool HasDataChanged { get; set; }
        /// <summary>
        /// 发生了变化的数据
        /// </summary>
        IDictionary<string, object> OptionMap { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="getOptionValueFunc">获取选项值的方法</param>
        /// <returns>初始化是否成功</returns>
        bool Initialize(Func<string, string, object> getOptionValueFunc);
    }
}