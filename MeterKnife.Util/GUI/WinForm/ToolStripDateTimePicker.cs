using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public class ToolStripDateTimePicker : ToolStripControlHost
    {
        public ToolStripDateTimePicker()
            : base(new DateTimePicker())
        {
        }
    }


}
