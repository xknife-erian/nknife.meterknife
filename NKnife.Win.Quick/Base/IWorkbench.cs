using System;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Base
{
    /// <summary>
    /// 描述软件的主窗体的接口
    /// </summary>
    public interface IWorkbench
    {
        /// <summary>
        /// 主窗体使用的Dock控件
        /// </summary>
        DockPanel MainDockPanel { get; }
        
        /// <summary>
        /// 当主窗体的Close方法被调用后，是立即关闭还是先隐藏窗体
        /// </summary>
        bool HideOnClosing { get; set; }

        /// <summary>
        /// 窗体的选项面板
        /// </summary>
        List<IOptionPanel> OptionPanelList { get; set; }

        /// <summary>
        ///     尝试获取指定Key的使用习惯的值
        /// </summary>
        Func<string, string, object> GetHabitValueFunc { get; }

        /// <summary>
        ///     设置指定Key的使用习惯的值，值对象序列化成Json保存
        /// </summary>
        Action<string, object> SetHabitAction { get; }

        /// <summary>
        ///     尝试获取指定Key的选项的值
        /// </summary>
        Func<string, string, object> GetOptionValueFunc { get; }

        /// <summary>
        ///     设置指定Key的选项的值，值对象序列化成Json保存
        /// </summary>
        Action<string, object> SetOptionAction { get; }
    }
}
