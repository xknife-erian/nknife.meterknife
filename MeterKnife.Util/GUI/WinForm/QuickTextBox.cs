using System;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    /// <summary>
    ///     一个已设置为多行显示的TextBox，有简单的copy,paste等右键菜单
    /// </summary>
    public sealed class QuickTextBox : TextBox
    {
        public QuickTextBox()
        {
            SuspendLayout();

            Multiline = true;
            MaxLength = 640000;
            ScrollBars = ScrollBars.Vertical;
            Font = new Font("Tahoma", 8.25F);

            var contextMenuStrip = new ContextMenuStrip();
            var toolStripMenuItem = new ToolStripMenuItem("全选(&A)");
            toolStripMenuItem.Click += SelectAllEx;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            toolStripMenuItem = new ToolStripMenuItem("拷贝(&C)");
            toolStripMenuItem.Click += CopyEx;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            toolStripMenuItem = new ToolStripMenuItem("粘贴(&P)");
            toolStripMenuItem.Click += PasteEx;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            toolStripMenuItem = new ToolStripMenuItem("剪切(&T)");
            toolStripMenuItem.Click += CutEx;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            var separator = new ToolStripSeparator();
            contextMenuStrip.Items.Add(separator);
            toolStripMenuItem = new ToolStripMenuItem("还原(&F)");
            toolStripMenuItem.Click += ClearEx;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            ContextMenuStrip = contextMenuStrip;
            ResumeLayout(false);
        }

        public override bool Multiline
        {
            get { return base.Multiline; }
            set { base.Multiline = value; }
        }

        private void PasteEx(object sender, EventArgs e)
        {
            Paste();
        }

        private void CutEx(object sender, EventArgs e)
        {
            Cut();
        }

        private void ClearEx(object sender, EventArgs e)
        {
            Clear();
        }

        private void SelectAllEx(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void CopyEx(object sender, EventArgs e)
        {
            Copy();
        }
    }
}