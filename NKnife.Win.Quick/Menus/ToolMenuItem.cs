using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class ToolMenuItem : ToolStripMenuItem
    {
        public ToolMenuItem()
        {
            Text = this.Language($"{nameof(FileMenuItem)}.工具(&T)");
        }
    }
}
