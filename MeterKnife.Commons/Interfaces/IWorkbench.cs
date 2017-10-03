using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Interfaces
{
    public interface IWorkbench
    {
        DockPanel MainDockPanel { get; set; }

        /// <summary>
        /// 是否是应用程序正确请求关闭窗体
        /// </summary>
        bool KernelCallFormClose { get; set; }
    }
}
