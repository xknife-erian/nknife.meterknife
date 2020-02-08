using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class HelpMenuItem : ToolStripMenuItem
    {
        public HelpMenuItem()
        {
            Text = this.Language($"{nameof(FileMenuItem)}.帮助(&H)");
        }
    }
}