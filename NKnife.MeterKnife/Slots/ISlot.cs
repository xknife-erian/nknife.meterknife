using System;
using System.Collections.Generic;

namespace NKnife.MeterKnife.Slots
{
    /// <summary>
    ///     描述一种采集技术路线。比如串口，Socket，某厂驱动等等。
    ///     可能是实体设备如：Care1，Care2，Agilent 82357，串口直连等。也可能是一个虚拟驱动。
    /// </summary>
    public interface ISlot : IRouteEnable
    {
        string Id { get; set; }

        void AttachToDataBus(IDataBus bus);
        void Setup(StatementQueue prepare, StatementQueue sustainable, StatementQueue maintain);
        void StartAsync();
        void PauseAsync();
        void StopAsync();
        event EventHandler<SlotWorkingEventArgs> WorkNodeCompleted;
    }
}