using System;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Base
{
    public interface IWorkbench
    {
        DockPanel MainDockPanel { get; }
        
        bool HideOnClosing { get; set; }
        string GithubUpdateUser { get; set; }
        string GithubUpdateProject { get; set; }

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
