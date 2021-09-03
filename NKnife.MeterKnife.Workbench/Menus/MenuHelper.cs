using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;

namespace NKnife.MeterKnife.Workbench.Menus
{
    static class MenuHelper
    {
        private static IWorkbench _workbench;

        public static IWorkbench GetWorkbench(this ToolStripMenuItem menuItem)
        {
            if (_workbench != null)
                return _workbench;
            var form = menuItem.GetCurrentParent().FindForm();
            if (form != null && form is IWorkbench workbench)
                _workbench = workbench;
            return _workbench;
        }
    }
}
