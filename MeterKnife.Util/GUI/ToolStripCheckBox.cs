using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class ToolStripCheckBox : ToolStripControlHost
    {
        public ToolStripCheckBox()
            : base(new CheckBox())
        {
        }
    }
}
