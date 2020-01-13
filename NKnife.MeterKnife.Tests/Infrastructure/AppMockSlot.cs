using System;
using System.Collections.Generic;
using System.Text;
using NKnife.MeterKnife.Slots;

namespace NKnife.MeterKnife.Tests.Infrastructure
{
    public class AppMockSlot : ISlot
    {
        private int _prepareIndex =0;
        private StatementQueue _prepareQueue;

        private int _sustainableIndex = 0;
        private StatementQueue _sustainableQueue;

        private int _maintainIndex = 0;
        private StatementQueue _maintainQueue;

        #region Implementation of IRouteEnable

        /// <summary>
        ///     启用路由（即启用多路）
        /// </summary>
        public bool EnableMultiplexing { get; } = true;

        #endregion

        #region Implementation of ISlot

        public string Id { get; set; }

        public void AttachToDataBus(IDataBus bus)
        {
            bus.Bind(this);
        }

        public void Setup(StatementQueue prepare, StatementQueue sustainable, StatementQueue maintain)
        {
            _prepareQueue = prepare;
            _sustainableQueue = sustainable;
            _maintainQueue = maintain;
        }

        public void StartAsync()
        {
            for (_prepareIndex = 0; _prepareIndex < _prepareQueue.Count; _prepareIndex++)
            {
                var statement = _prepareQueue[_prepareIndex];

            }
            for (_sustainableIndex = 0; _sustainableIndex < _sustainableQueue.Count; _sustainableIndex++)
            {
                var statement = _sustainableQueue[_sustainableIndex];
            }
            for (_maintainIndex = 0; _maintainIndex < _maintainQueue.Count; _maintainIndex++)
            {
                var statement = _maintainQueue[_maintainIndex];
            }
        }

        public void PauseAsync()
        {
            throw new NotImplementedException();
        }

        public void StopAsync()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<SlotWorkingEventArgs> WorkNodeCompleted;

        #endregion
    }
}
