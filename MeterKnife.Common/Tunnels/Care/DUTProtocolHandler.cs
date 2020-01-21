using System;
using NKnife.Events;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    // ReSharper disable once InconsistentNaming
    public class DUTProtocolHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _tempStorage;

        public DUTProtocolHandler(IPerformStorageLogic tempStorage)
        {
            _tempStorage = tempStorage;
            Commands.Add(new byte[] { 0xAA, 0x00 });
            Commands.Add(new byte[] { 0xAB, 0x00 });
            //---
            Commands.Add(new byte[] { 0xAA, 0x01 });
        }

        public override void Received(CareTalking protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                OnProtocolReceived(new EventArgs<CareTalking>(protocol));
            }
        }

        public event EventHandler<EventArgs<CareTalking>> ProtocolReceived;

        protected virtual void OnProtocolReceived(EventArgs<CareTalking> e)
        {
            _Logger.Debug($"> {e.Item}");
            ProtocolReceived?.Invoke(this, e);
        }

    }
}
