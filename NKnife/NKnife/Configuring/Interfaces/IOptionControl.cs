using NKnife.Configuring.Common;
using NKnife.Configuring.OptionCase;

namespace NKnife.Configuring.Interfaces
{
    /// <summary>一类“配置”的配置界面
    /// </summary>
    public interface IOptionControl
    {
        /// <summary>时间方案
        /// </summary>
        OptionCaseItem CaseItem { get; set; }

        /// <summary>本界面中的值已经发生修改
        /// </summary>
        bool IsModified { get; }

        /// <summary>是否需要重启应用程序
        /// </summary>
        bool NeedReStartApp { get; }

        /// <summary>是否需要重启计算机
        /// </summary>
        bool NeedReStartComputer { get; }

        /// <summary>初始化
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>从界面中取出所有的值进行保存
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>应用设置值
        /// </summary>
        /// <returns></returns>
        bool FillApplication();

        /// <summary>撤消本次界面所做的修改，加载本次修改前的设置
        /// </summary>
        /// <returns></returns>
        bool Retract();

        /// <summary>加载默认值
        /// </summary>
        /// <returns></returns>
        bool LoadDefault();

        /// <summary>
        /// 当有数据改变后发生的事件
        /// </summary>
        event HasModifiedEventHandler HasModifiedEvent;

        /*命令模式，由于有难度，暂时不纳入了。2011-01-25 0:12:33 lukan*/
        ///// <summary>
        ///// 实现命令模式，以实现“重做”
        ///// </summary>
        //void Execute();
        ///// <summary>
        ///// 实现命令模式，以实现“撤消”
        ///// </summary>
        //void UnExecute();
    }

    public delegate void HasModifiedEventHandler(object sender, HasModifiedEventArgs e);
}