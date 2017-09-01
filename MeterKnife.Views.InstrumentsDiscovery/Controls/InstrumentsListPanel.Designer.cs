namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    partial class InstrumentsListPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._ListHead = new MeterKnife.Views.InstrumentsDiscovery.Controls.InstrumentsListHead();
            this._CellContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._UpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UnDropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HeadContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._ConnTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._CommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DatasManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._CellContextMenu.SuspendLayout();
            this._HeadContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ListHead
            // 
            this._ListHead.Count = 0;
            this._ListHead.Dock = System.Windows.Forms.DockStyle.Top;
            this._ListHead.GatewayModel = "GatewayModelLabel";
            this._ListHead.Location = new System.Drawing.Point(0, 0);
            this._ListHead.Name = "_ListHead";
            this._ListHead.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._ListHead.Size = new System.Drawing.Size(410, 32);
            this._ListHead.TabIndex = 0;
            // 
            // _CellContextMenu
            // 
            this._CellContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._UpdateToolStripMenuItem,
            this.toolStripSeparator1,
            this.UnDropToolStripMenuItem,
            this._DropToolStripMenuItem,
            this.toolStripSeparator2,
            this._DeleteToolStripMenuItem});
            this._CellContextMenu.Name = "_ContextMenuStrip";
            this._CellContextMenu.Size = new System.Drawing.Size(118, 104);
            // 
            // _UpdateToolStripMenuItem
            // 
            this._UpdateToolStripMenuItem.Name = "_UpdateToolStripMenuItem";
            this._UpdateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._UpdateToolStripMenuItem.Text = "刷新(&R)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // UnDropToolStripMenuItem
            // 
            this.UnDropToolStripMenuItem.Name = "UnDropToolStripMenuItem";
            this.UnDropToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UnDropToolStripMenuItem.Text = "折叠(&E)";
            // 
            // _DropToolStripMenuItem
            // 
            this._DropToolStripMenuItem.Name = "_DropToolStripMenuItem";
            this._DropToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._DropToolStripMenuItem.Text = "展开(&P)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // _DeleteToolStripMenuItem
            // 
            this._DeleteToolStripMenuItem.Name = "_DeleteToolStripMenuItem";
            this._DeleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._DeleteToolStripMenuItem.Text = "删除(&D)";
            // 
            // _HeadContextMenu
            // 
            this._HeadContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ConnTestToolStripMenuItem,
            this._CommandsToolStripMenuItem,
            this._DatasManagerToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem1});
            this._HeadContextMenu.Name = "_ContextMenuStrip";
            this._HeadContextMenu.Size = new System.Drawing.Size(145, 98);
            // 
            // _ConnTestToolStripMenuItem
            // 
            this._ConnTestToolStripMenuItem.Name = "_ConnTestToolStripMenuItem";
            this._ConnTestToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this._ConnTestToolStripMenuItem.Text = "通讯测试(&C)";
            // 
            // _CommandsToolStripMenuItem
            // 
            this._CommandsToolStripMenuItem.Name = "_CommandsToolStripMenuItem";
            this._CommandsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this._CommandsToolStripMenuItem.Text = "指令集(&O)";
            // 
            // _DatasManagerToolStripMenuItem
            // 
            this._DatasManagerToolStripMenuItem.Name = "_DatasManagerToolStripMenuItem";
            this._DatasManagerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this._DatasManagerToolStripMenuItem.Text = "数据管理(&M)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem1.Text = "删除(&D)";
            // 
            // InstrumentsListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this._ListHead);
            this.Name = "InstrumentsListPanel";
            this.Size = new System.Drawing.Size(410, 56);
            this._CellContextMenu.ResumeLayout(false);
            this._HeadContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private InstrumentsListHead _ListHead;
        private System.Windows.Forms.ContextMenuStrip _CellContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _UpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem UnDropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DropToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _DeleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip _HeadContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _ConnTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _CommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DatasManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
