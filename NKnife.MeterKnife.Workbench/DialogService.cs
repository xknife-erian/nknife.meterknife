using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Interface;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class DialogService : IDialogService
    {
        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get; } = 10;
        public string Description { get; } = "弹出窗体服务";

        #endregion
    }
}
