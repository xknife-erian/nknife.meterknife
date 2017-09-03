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
            this._HeadContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._RefreshInstrumentsStateByGatewayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this._UnDropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this._DeleteGatewayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._CellContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._ConnTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._CommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DatasManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._Separator3 = new System.Windows.Forms.ToolStripSeparator();
            this._DeleteInstrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HeadContextMenu.SuspendLayout();
            this._CellContextMenu.SuspendLayout();
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
            // _HeadContextMenu
            // 
            this._HeadContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._RefreshInstrumentsStateByGatewayToolStripMenuItem,
            this._Separator1,
            this._UnDropToolStripMenuItem,
            this._DropToolStripMenuItem,
            this._Separator2,
            this._DeleteGatewayToolStripMenuItem});
            this._HeadContextMenu.Name = "_ContextMenuStrip";
            this._HeadContextMenu.Size = new System.Drawing.Size(118, 104);
            // 
            // _RefreshInstrumentsStateByGatewayToolStripMenuItem
            // 
            this._RefreshInstrumentsStateByGatewayToolStripMenuItem.Name = "_RefreshInstrumentsStateByGatewayToolStripMenuItem";
            this._RefreshInstrumentsStateByGatewayToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this._RefreshInstrumentsStateByGatewayToolStripMenuItem.Text = "刷新(&R)";
            // 
            // _Separator1
            // 
            this._Separator1.Name = "_Separator1";
            this._Separator1.Size = new System.Drawing.Size(114, 6);
            // 
            // _UnDropToolStripMenuItem
            // 
            this._UnDropToolStripMenuItem.Name = "_UnDropToolStripMenuItem";
            this._UnDropToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this._UnDropToolStripMenuItem.Text = "折叠(&E)";
            // 
            // _DropToolStripMenuItem
            // 
            this._DropToolStripMenuItem.Name = "_DropToolStripMenuItem";
            this._DropToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this._DropToolStripMenuItem.Text = "展开(&P)";
            // 
            // _Separator2
            // 
            this._Separator2.Name = "_Separator2";
            this._Separator2.Size = new System.Drawing.Size(114, 6);
            // 
            // _DeleteGatewayToolStripMenuItem
            // 
            this._DeleteGatewayToolStripMenuItem.Name = "_DeleteGatewayToolStripMenuItem";
            this._DeleteGatewayToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this._DeleteGatewayToolStripMenuItem.Text = "删除(&D)";
            // 
            // _CellContextMenu
            // 
            this._CellContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ConnTestToolStripMenuItem,
            this._CommandsToolStripMenuItem,
            this._DatasManagerToolStripMenuItem,
            this._Separator3,
            this._DeleteInstrumentToolStripMenuItem});
            this._CellContextMenu.Name = "_ContextMenuStrip";
            this._CellContextMenu.Size = new System.Drawing.Size(145, 98);
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
            // _Separator3
            // 
            this._Separator3.Name = "_Separator3";
            this._Separator3.Size = new System.Drawing.Size(141, 6);
            // 
            // _DeleteInstrumentToolStripMenuItem
            // 
            this._DeleteInstrumentToolStripMenuItem.Name = "_DeleteInstrumentToolStripMenuItem";
            this._DeleteInstrumentToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this._DeleteInstrumentToolStripMenuItem.Text = "删除(&D)";
            // 
            // InstrumentsListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListHead);
            this.Name = "InstrumentsListPanel";
            this.Size = new System.Drawing.Size(410, 56);
            this._HeadContextMenu.ResumeLayout(false);
            this._CellContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private InstrumentsListHead _ListHead;
        private System.Windows.Forms.ContextMenuStrip _HeadContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _RefreshInstrumentsStateByGatewayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _UnDropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DeleteGatewayToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip _CellContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _ConnTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _CommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DatasManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DeleteInstrumentToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator _Separator1;
        private System.Windows.Forms.ToolStripSeparator _Separator2;
        private System.Windows.Forms.ToolStripSeparator _Separator3;
    }
}
