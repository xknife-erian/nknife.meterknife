using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class FileMenuItem : ToolStripMenuItem
    {
        public FileMenuItem()
        {
            Text = this.String("文件(&F)");
            var exit = new ToolStripMenuItem(this.String("退出(&X)"));
            exit.Click += (e, s) =>
            {
                var form = this.Parent.FindForm();
                form?.Close();
            };
            this.DropDownItems.Add(exit);
        }
    }
}
