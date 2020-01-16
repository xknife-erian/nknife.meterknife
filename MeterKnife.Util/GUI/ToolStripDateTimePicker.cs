using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class ToolStripDateTimePicker : ToolStripControlHost
    {
        public ToolStripDateTimePicker()
            : base(new DateTimePicker())
        {
        }
    }


}
