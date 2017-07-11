using System;

namespace MeterKnife.Interfaces.Plugin
{
    /// <summary>
    ///     排队核心服务（领域模型）的基接口，描述取号程序的核心事件与事件函数
    ///     核心服务通过事件把消息广播通知所有的插件，而插件通过方法把消息和命令传递给核心服务
    ///     事件分成柜台事件、Client事件、客户事件、触摸屏事件四类，
    ///     方法没有分类，通常方法的调用会引起核心服务的对应动作，并随机激发响应的事件，
    ///     这是排队系统最核心的通讯机制
    /// </summary>
    public interface IEventExtender
    {
        #region 事件

        /// <summary>
        ///     呼叫后
        /// </summary>
        event EventHandler<CounterActionEventArgs> AfterCall;

        /// <summary>
        ///     重呼后
        /// </summary>
        event EventHandler<CounterActionEventArgs> AfterReCall;

        /// <summary>
        ///     请评价后
        /// </summary>
        event EventHandler<CounterActionEventArgs> AfterAskEval;

        /// <summary>
        ///     评价后
        /// </summary>
        event EventHandler<CounterActionEventArgs> AfterEval;

        /// <summary>
        ///     排队程序启动后
        /// </summary>
        event EventHandler<ClientActionEventArgs> AfterLoad;

        /// <summary>
        ///     业务等候人数发生变化后
        /// </summary>
        event EventHandler<ClientActionEventArgs> BusinessTypeWaitCountChanged;

        #endregion

        #region 异步方法

        /// <summary>
        ///     表示有客户通过刷卡或者号码输入等途径，提交客户号码信息，
        ///     通常由触摸屏（拥有刷卡器、二代证读卡器）调用，Core通常会触发AfterCustomerIdDetected事件，由客户识别模块相应该事件
        /// </summary>
        /// <param name="parms">客户参数</param>
        void VerifyCustomerId(CustomerParams parms);

        /// <summary>
        ///     对客户Id进行校验（本地或远程），
        ///     校验通过后得到客户信息，生成CustomerInfo类加入字典，通知ICore，Core通常会触发AfterCustomerVerified，
        ///     通常由客户识别模块调用该方法，客户识别模块响应AfterCustomerIdDetected事件后调用。
        /// </summary>
        /// <param name="parms">客户参数</param>
        void AcceptCustomer(CustomerParams parms);

        /// <summary>
        ///     获取号票
        /// </summary>
        /// <param name="parms">号票参数</param>
        void GetTicket(TicketParams parms);

        /// <summary>
        ///     呼叫
        /// </summary>
        /// <param name="parms">柜台参数</param>
        void CallCustomerToCounter(CounterParams parms);

        /// <summary>
        ///     重呼
        /// </summary>
        /// <param name="parms"></param>
        void ReCallCustomerToCounter(CounterParams parms);

        /// <summary>
        ///     打印机缺纸
        /// </summary>
        void PrinterLackPaper();

        #endregion
    }

    public class CounterParams
    {
    }

    public class TicketParams
    {
    }
}

public class CustomerParams
{
}

public class ScreenActionEventArgs : EventArgs
{
}

public class CustomerActionEventArgs : EventArgs
{
}

public class ClientActionEventArgs : EventArgs
{
}

public class CounterActionEventArgs : EventArgs
{
}