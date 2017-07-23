using System;
using System.Collections.Generic;
using MeterKnife.Interfaces;
using NKnife.Interface;

namespace MeterKnife.Gateway
{
    public class GateWayService : IGatewayService
    {
        private List<GateWayModel> _GateWayModels = new List<GateWayModel>();

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            return false;
        }

        public int Order { get; } = 10;
        public string Description { get; } = "";

        #endregion
    }
}