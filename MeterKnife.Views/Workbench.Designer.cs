namespace MeterKnife.Views
{
    partial class Workbench
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workbench));
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._CenterToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this._SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._PrintPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DatasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._OptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this._MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.BottomToolStripPanel
            // 
            this._ToolStripContainer.BottomToolStripPanel.Controls.Add(this._StatusStrip);
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(784, 514);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(784, 561);
            this._ToolStripContainer.TabIndex = 0;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._MenuStrip);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._CenterToolStripStatusLabel,
            this.toolStripStatusLabel3});
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(784, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "就绪";
            // 
            // _CenterToolStripStatusLabel
            // 
            this._CenterToolStripStatusLabel.Name = "_CenterToolStripStatusLabel";
            this._CenterToolStripStatusLabel.Size = new System.Drawing.Size(692, 17);
            this._CenterToolStripStatusLabel.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel3.Text = "COM4";
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileToolStripMenuItem,
            this._DatasToolStripMenuItem,
            this._ToolsToolStripMenuItem,
            this._HelpToolStripMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(784, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // _FileToolStripMenuItem
            // 
            this._FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripMenuItem,
            this._OpenToolStripMenuItem,
            this._ToolStripSeparator0,
            this._SaveToolStripMenuItem,
            this._SaveAsToolStripMenuItem,
            this._ToolStripSeparator1,
            this._PrintToolStripMenuItem,
            this._PrintPreviewToolStripMenuItem,
            this._ToolStripSeparator2,
            this._ExitToolStripMenuItem});
            this._FileToolStripMenuItem.Name = "_FileToolStripMenuItem";
            this._FileToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this._FileToolStripMenuItem.Text = "文件(&F)";
            // 
            // _NewToolStripMenuItem
            // 
            this._NewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_NewToolStripMenuItem.Image")));
            this._NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripMenuItem.Name = "_NewToolStripMenuItem";
            this._NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this._NewToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._NewToolStripMenuItem.Text = "新建(&N)";
            // 
            // _OpenToolStripMenuItem
            // 
            this._OpenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_OpenToolStripMenuItem.Image")));
            this._OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OpenToolStripMenuItem.Name = "_OpenToolStripMenuItem";
            this._OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._OpenToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._OpenToolStripMenuItem.Text = "打开(&O)";
            // 
            // _ToolStripSeparator0
            // 
            this._ToolStripSeparator0.Name = "_ToolStripSeparator0";
            this._ToolStripSeparator0.Size = new System.Drawing.Size(162, 6);
            // 
            // _SaveToolStripMenuItem
            // 
            this._SaveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_SaveToolStripMenuItem.Image")));
            this._SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveToolStripMenuItem.Name = "_SaveToolStripMenuItem";
            this._SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._SaveToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._SaveToolStripMenuItem.Text = "保存(&S)";
            // 
            // _SaveAsToolStripMenuItem
            // 
            this._SaveAsToolStripMenuItem.Name = "_SaveAsToolStripMenuItem";
            this._SaveAsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._SaveAsToolStripMenuItem.Text = "另存为(&A)";
            // 
            // _ToolStripSeparator1
            // 
            this._ToolStripSeparator1.Name = "_ToolStripSeparator1";
            this._ToolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // _PrintToolStripMenuItem
            // 
            this._PrintToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_PrintToolStripMenuItem.Image")));
            this._PrintToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PrintToolStripMenuItem.Name = "_PrintToolStripMenuItem";
            this._PrintToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this._PrintToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._PrintToolStripMenuItem.Text = "打印(&P)";
            // 
            // _PrintPreviewToolStripMenuItem
            // 
            this._PrintPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_PrintPreviewToolStripMenuItem.Image")));
            this._PrintPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PrintPreviewToolStripMenuItem.Name = "_PrintPreviewToolStripMenuItem";
            this._PrintPreviewToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._PrintPreviewToolStripMenuItem.Text = "打印预览(&V)";
            // 
            // _ToolStripSeparator2
            // 
            this._ToolStripSeparator2.Name = "_ToolStripSeparator2";
            this._ToolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            // 
            // _ExitToolStripMenuItem
            // 
            this._ExitToolStripMenuItem.Name = "_ExitToolStripMenuItem";
            this._ExitToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._ExitToolStripMenuItem.Text = "退出(&X)";
            // 
            // _DatasToolStripMenuItem
            // 
            this._DatasToolStripMenuItem.Name = "_DatasToolStripMenuItem";
            this._DatasToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this._DatasToolStripMenuItem.Text = "数据(&D)";
            // 
            // _ToolsToolStripMenuItem
            // 
            this._ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OptionToolStripMenuItem});
            this._ToolsToolStripMenuItem.Name = "_ToolsToolStripMenuItem";
            this._ToolsToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this._ToolsToolStripMenuItem.Text = "工具(&T)";
            // 
            // _OptionToolStripMenuItem
            // 
            this._OptionToolStripMenuItem.Name = "_OptionToolStripMenuItem";
            this._OptionToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this._OptionToolStripMenuItem.Text = "选项(&O)";
            // 
            // _HelpToolStripMenuItem
            // 
            this._HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AboutToolStripMenuItem});
            this._HelpToolStripMenuItem.Name = "_HelpToolStripMenuItem";
            this._HelpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this._HelpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // _AboutToolStripMenuItem
            // 
            this._AboutToolStripMenuItem.Name = "_AboutToolStripMenuItem";
            this._AboutToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this._AboutToolStripMenuItem.Text = "关于(&A)...";
            // 
            // Workbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this._ToolStripContainer);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Workbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workbench";
            this._ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _CenterToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator _ToolStripSeparator0;
        private System.Windows.Forms.ToolStripMenuItem _SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator _ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _PrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _PrintPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator _ToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _OptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DatasToolStripMenuItem;
    }
}