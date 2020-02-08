using System.Drawing;
using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class HelpMenuItem : ToolStripMenuItem
    {
        public HelpMenuItem()
        {
            Text = this.Language("帮助(&H)");
            var update = new ToolStripMenuItem(this.Language("更新(&U)"));
            var about = new ToolStripMenuItem(this.Language("关于(&A)"));
            DropDownItems.Add(update);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(about);
        }
    }
}