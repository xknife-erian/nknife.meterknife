using System.Collections.Generic;
using NKnife.MeterKnife.Slots;

namespace NKnife.MeterKnife
{
    /// <summary>
    /// 描述一个数据消息队列的管理器。
    /// 数据消息队列将是一个基于ZeroMQ的独立进程，采用发布与订阅模式来进行数据的管理。
    /// </summary>
    public interface IDataBus
    {
        /// <summary>
        /// 获取一个指定ID的被测物在队列里的数据
        /// </summary>
        /// <param name="uutId"></param>
        /// <returns></returns>
        IEnumerable<float> GetUutData(string uutId);

        void Bind(ISlot slot);
    }
}