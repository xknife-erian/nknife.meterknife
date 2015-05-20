namespace MeterKnife.Workbench
{
    partial class MainWorkbench
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWorkbench));
            this._StripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AddMeterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._PrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._PringPreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._TestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InterfaceTreeViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DataManagerViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._CommandConsoleViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._LoggerViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ResetViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StripContainer.BottomToolStripPanel.SuspendLayout();
            this._StripContainer.TopToolStripPanel.SuspendLayout();
            this._StripContainer.SuspendLayout();
            this._MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StripContainer
            // 
            // 
            // _StripContainer.BottomToolStripPanel
            // 
            this._StripContainer.BottomToolStripPanel.Controls.Add(this._StatusStrip);
            // 
            // _StripContainer.ContentPanel
            // 
            this._StripContainer.ContentPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this._StripContainer.ContentPanel.Size = new System.Drawing.Size(784, 515);
            this._StripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._StripContainer.Location = new System.Drawing.Point(0, 0);
            this._StripContainer.Name = "_StripContainer";
            this._StripContainer.Size = new System.Drawing.Size(784, 562);
            this._StripContainer.TabIndex = 0;
            // 
            // _StripContainer.TopToolStripPanel
            // 
            this._StripContainer.TopToolStripPanel.Controls.Add(this._MenuStrip);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(784, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileMenuItem,
            this._TestMenuItem,
            this._ToolMenuItem,
            this._ViewMenuItem,
            this._HelpMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(784, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // _FileMenuItem
            // 
            this._FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddMeterMenuItem,
            this.toolStripSeparator,
            this._SaveMenuItem,
            this._ExportMenuItem,
            this.toolStripSeparator1,
            this._PrintMenuItem,
            this._PringPreviewMenuItem,
            this.toolStripSeparator2,
            this._ExitMenuItem});
            this._FileMenuItem.Name = "_FileMenuItem";
            this._FileMenuItem.Size = new System.Drawing.Size(58, 21);
            this._FileMenuItem.Text = "文件(&F)";
            // 
            // _AddMeterMenuItem
            // 
            this._AddMeterMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_AddMeterMenuItem.Image")));
            this._AddMeterMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddMeterMenuItem.Name = "_AddMeterMenuItem";
            this._AddMeterMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this._AddMeterMenuItem.Size = new System.Drawing.Size(189, 22);
            this._AddMeterMenuItem.Text = "新建仪器(&N)";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(186, 6);
            // 
            // _SaveMenuItem
            // 
            this._SaveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_SaveMenuItem.Image")));
            this._SaveMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveMenuItem.Name = "_SaveMenuItem";
            this._SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._SaveMenuItem.Size = new System.Drawing.Size(189, 22);
            this._SaveMenuItem.Text = "保存(&S)";
            // 
            // _ExportMenuItem
            // 
            this._ExportMenuItem.Name = "_ExportMenuItem";
            this._ExportMenuItem.Size = new System.Drawing.Size(189, 22);
            this._ExportMenuItem.Text = "导出(&E)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // _PrintMenuItem
            // 
            this._PrintMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_PrintMenuItem.Image")));
            this._PrintMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PrintMenuItem.Name = "_PrintMenuItem";
            this._PrintMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this._PrintMenuItem.Size = new System.Drawing.Size(189, 22);
            this._PrintMenuItem.Text = "打印(&P)";
            // 
            // _PringPreviewMenuItem
            // 
            this._PringPreviewMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_PringPreviewMenuItem.Image")));
            this._PringPreviewMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PringPreviewMenuItem.Name = "_PringPreviewMenuItem";
            this._PringPreviewMenuItem.Size = new System.Drawing.Size(189, 22);
            this._PringPreviewMenuItem.Text = "打印预览(&V)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // _ExitMenuItem
            // 
            this._ExitMenuItem.Name = "_ExitMenuItem";
            this._ExitMenuItem.Size = new System.Drawing.Size(189, 22);
            this._ExitMenuItem.Text = "退出(&X)";
            // 
            // _TestMenuItem
            // 
            this._TestMenuItem.Name = "_TestMenuItem";
            this._TestMenuItem.Size = new System.Drawing.Size(59, 21);
            this._TestMenuItem.Text = "测试(&S)";
            // 
            // _ToolMenuItem
            // 
            this._ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选项OToolStripMenuItem});
            this._ToolMenuItem.Name = "_ToolMenuItem";
            this._ToolMenuItem.Size = new System.Drawing.Size(59, 21);
            this._ToolMenuItem.Text = "工具(&T)";
            // 
            // 选项OToolStripMenuItem
            // 
            this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
            this.选项OToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.选项OToolStripMenuItem.Text = "选项(&O)";
            // 
            // _ViewMenuItem
            // 
            this._ViewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._InterfaceTreeViewMenuItem,
            this._DataManagerViewMenuItem,
            this._CommandConsoleViewMenuItem,
            this._LoggerViewMenuItem,
            this.toolStripSeparator3,
            this._ResetViewMenuItem});
            this._ViewMenuItem.Name = "_ViewMenuItem";
            this._ViewMenuItem.Size = new System.Drawing.Size(60, 21);
            this._ViewMenuItem.Text = "视图(&V)";
            // 
            // _InterfaceTreeViewMenuItem
            // 
            this._InterfaceTreeViewMenuItem.Checked = true;
            this._InterfaceTreeViewMenuItem.CheckOnClick = true;
            this._InterfaceTreeViewMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._InterfaceTreeViewMenuItem.Name = "_InterfaceTreeViewMenuItem";
            this._InterfaceTreeViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this._InterfaceTreeViewMenuItem.Text = "仪器列表";
            // 
            // _DataManagerViewMenuItem
            // 
            this._DataManagerViewMenuItem.Checked = true;
            this._DataManagerViewMenuItem.CheckOnClick = true;
            this._DataManagerViewMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._DataManagerViewMenuItem.Name = "_DataManagerViewMenuItem";
            this._DataManagerViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this._DataManagerViewMenuItem.Text = "数据管理器";
            // 
            // _CommandConsoleViewMenuItem
            // 
            this._CommandConsoleViewMenuItem.Checked = true;
            this._CommandConsoleViewMenuItem.CheckOnClick = true;
            this._CommandConsoleViewMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._CommandConsoleViewMenuItem.Name = "_CommandConsoleViewMenuItem";
            this._CommandConsoleViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this._CommandConsoleViewMenuItem.Text = "命令控制台";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // _LoggerViewMenuItem
            // 
            this._LoggerViewMenuItem.CheckOnClick = true;
            this._LoggerViewMenuItem.Name = "_LoggerViewMenuItem";
            this._LoggerViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this._LoggerViewMenuItem.Text = "程序日志";
            // 
            // _HelpMenuItem
            // 
            this._HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AboutMenuItem});
            this._HelpMenuItem.Name = "_HelpMenuItem";
            this._HelpMenuItem.Size = new System.Drawing.Size(61, 21);
            this._HelpMenuItem.Text = "帮助(&H)";
            // 
            // _AboutMenuItem
            // 
            this._AboutMenuItem.Name = "_AboutMenuItem";
            this._AboutMenuItem.Size = new System.Drawing.Size(125, 22);
            this._AboutMenuItem.Text = "关于(&A)...";
            this._AboutMenuItem.Click += new System.EventHandler(this._AboutMenuItem_Click);
            // 
            // _ResetViewMenuItem
            // 
            this._ResetViewMenuItem.Name = "_ResetViewMenuItem";
            this._ResetViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this._ResetViewMenuItem.Text = "重置视图";
            // 
            // MainWorkbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._StripContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "MainWorkbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWorkbench";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._StripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._StripContainer.BottomToolStripPanel.PerformLayout();
            this._StripContainer.TopToolStripPanel.ResumeLayout(false);
            this._StripContainer.TopToolStripPanel.PerformLayout();
            this._StripContainer.ResumeLayout(false);
            this._StripContainer.PerformLayout();
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _StripContainer;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _SaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ExportMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _PrintMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _PringPreviewMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutMenuItem;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripMenuItem _AddMeterMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem _TestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _InterfaceTreeViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DataManagerViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _CommandConsoleViewMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem _LoggerViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ResetViewMenuItem;
    }
}

