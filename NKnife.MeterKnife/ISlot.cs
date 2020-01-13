using System;

namespace NKnife.MeterKnife
{
    /// <summary>
    /// 描述一种采集技术路线。比如串口，Socket，某厂驱动等等。
    /// </summary>
    public interface ISlot
    {
        string Id { get; set; }
        void AttachToDataBus(IDataBus bus); 
        void Setup(IStatement prepare, IStatement sustainable, IStatement maintain);
        void StartAsync();
        void PauseAsync();
        void StopAsync();
        event EventHandler<SlotWorkingEventArgs> WorkNodeCompleted;
    }

    public class SlotWorkingEventArgs : EventArgs
    {
        public string SlotId { get; set; }
        public string UutId { get; set; }
    }

    /// <summary>
    /// Unit Under Test(UUT) 待测单元。
    /// </summary>
    public interface IUnitUnderTest
    {
        string Id { get; set; }
    }

    public interface ISerialSlot : ISlot
    {
    }

    public interface ISocketSlot : ISlot
    {
    }

    public interface ICareOneSlot : ISerialSlot
    {
    }

}
