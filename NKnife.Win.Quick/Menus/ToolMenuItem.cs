using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class ToolMenuItem : ToolStripMenuItem
    {
        public ToolMenuItem()
        {
            Text = this.String("工具(&T)");
        }
    }
}
