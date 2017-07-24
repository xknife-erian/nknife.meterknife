using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Interfaces
{
    public interface IWorkbench
    {
        /// <summary>
        /// 是否是应用程序正确请求关闭窗体
        /// </summary>
        bool KernelCallFormClose { get; set; }
    }
}
