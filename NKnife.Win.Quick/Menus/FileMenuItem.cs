using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class FileMenuItem : ToolStripMenuItem
    {
        public FileMenuItem()
        {
            Text = this.Language("文件(&F)");
        }
    }
}
