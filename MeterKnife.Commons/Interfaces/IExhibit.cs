using System;
using NKnife.Interface;

namespace MeterKnife.Interfaces
{
    /// <summary>
    /// 被问询的设备可能工作于不同的子对象,子工作。通常该接口存在于ISwap接口中，并与IDevice接口互为绑定。
    /// 例如：HP3488作为一个扫描设备，它将采集、观察的对象可以是非常多个。
    /// exhibit:展览,陈列;展览品;
    /// </summary>
    public interface IExhibit : IId
    {
        /// <summary>
        /// 关于本观察点的描述
        /// </summary>
        string Detail { get; set; }

        /// <summary>
        /// 创建本观察点对象的时间(非物理的制造时间，一般来讲描述的是采集数据的开始时间)
        /// </summary>
        DateTime CreatedTime { get; set; }
    }
}
