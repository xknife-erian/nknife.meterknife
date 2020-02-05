using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Base
{
    public interface IWorkbench
    {
        DockPanel MainDockPanel { get; }
    }
}
