namespace MeterKnife.MiscDemo
{
    partial class MiscDemoMainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiscDemoMainForm));
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this.plot测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._MainPlotTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ThemeManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._GatewayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._CareOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._KeysightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InstrumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InstrumentsDiscoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._UserControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InstrumentCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._TipStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MenuStrip.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plot测试ToolStripMenuItem,
            this.主题ToolStripMenuItem,
            this._GatewayToolStripMenuItem,
            this._InstrumentsToolStripMenuItem,
            this._UserControlToolStripMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(951, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // plot测试ToolStripMenuItem
            // 
            this.plot测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._MainPlotTestToolStripMenuItem});
            this.plot测试ToolStripMenuItem.Name = "plot测试ToolStripMenuItem";
            this.plot测试ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.plot测试ToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.plot测试ToolStripMenuItem.Text = "Plots";
            // 
            // _MainPlotTestToolStripMenuItem
            // 
            this._MainPlotTestToolStripMenuItem.Name = "_MainPlotTestToolStripMenuItem";
            this._MainPlotTestToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this._MainPlotTestToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this._MainPlotTestToolStripMenuItem.Text = "主折线图测试";
            this._MainPlotTestToolStripMenuItem.Click += new System.EventHandler(this._MainPlotTestToolStripMenuItem_Click);
            // 
            // 主题ToolStripMenuItem
            // 
            this.主题ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ThemeManagerToolStripMenuItem});
            this.主题ToolStripMenuItem.Name = "主题ToolStripMenuItem";
            this.主题ToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.主题ToolStripMenuItem.Text = "Dialog";
            // 
            // _ThemeManagerToolStripMenuItem
            // 
            this._ThemeManagerToolStripMenuItem.Name = "_ThemeManagerToolStripMenuItem";
            this._ThemeManagerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this._ThemeManagerToolStripMenuItem.Text = "主题管理器窗体";
            this._ThemeManagerToolStripMenuItem.Click += new System.EventHandler(this._ThemeManagerToolStripMenuItem_Click);
            // 
            // _GatewayToolStripMenuItem
            // 
            this._GatewayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CareOneToolStripMenuItem,
            this._KeysightToolStripMenuItem});
            this._GatewayToolStripMenuItem.Name = "_GatewayToolStripMenuItem";
            this._GatewayToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this._GatewayToolStripMenuItem.Text = "Gateway";
            // 
            // _CareOneToolStripMenuItem
            // 
            this._CareOneToolStripMenuItem.Name = "_CareOneToolStripMenuItem";
            this._CareOneToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this._CareOneToolStripMenuItem.Text = "CareOne";
            // 
            // _KeysightToolStripMenuItem
            // 
            this._KeysightToolStripMenuItem.Name = "_KeysightToolStripMenuItem";
            this._KeysightToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this._KeysightToolStripMenuItem.Text = "Keysight";
            this._KeysightToolStripMenuItem.Click += new System.EventHandler(this._KeysightToolStripMenuItem_Click);
            // 
            // _InstrumentsToolStripMenuItem
            // 
            this._InstrumentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._InstrumentsDiscoveryToolStripMenuItem});
            this._InstrumentsToolStripMenuItem.Name = "_InstrumentsToolStripMenuItem";
            this._InstrumentsToolStripMenuItem.Size = new System.Drawing.Size(88, 21);
            this._InstrumentsToolStripMenuItem.Text = "Instruments";
            // 
            // _InstrumentsDiscoveryToolStripMenuItem
            // 
            this._InstrumentsDiscoveryToolStripMenuItem.Name = "_InstrumentsDiscoveryToolStripMenuItem";
            this._InstrumentsDiscoveryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._InstrumentsDiscoveryToolStripMenuItem.Text = "仪器管理窗口";
            this._InstrumentsDiscoveryToolStripMenuItem.Click += new System.EventHandler(this._InstrumentsDiscoveryToolStripMenuItem_Click);
            // 
            // _UserControlToolStripMenuItem
            // 
            this._UserControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._InstrumentCellToolStripMenuItem});
            this._UserControlToolStripMenuItem.Name = "_UserControlToolStripMenuItem";
            this._UserControlToolStripMenuItem.Size = new System.Drawing.Size(90, 21);
            this._UserControlToolStripMenuItem.Text = "UserControl";
            // 
            // _InstrumentCellToolStripMenuItem
            // 
            this._InstrumentCellToolStripMenuItem.Name = "_InstrumentCellToolStripMenuItem";
            this._InstrumentCellToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this._InstrumentCellToolStripMenuItem.Text = "InstrumentCell";
            this._InstrumentCellToolStripMenuItem.Click += new System.EventHandler(this._InstrumentCellToolStripMenuItem_Click);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._TipStatusLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 604);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(951, 22);
            this._StatusStrip.TabIndex = 1;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // _TipStatusLabel
            // 
            this._TipStatusLabel.Name = "_TipStatusLabel";
            this._TipStatusLabel.Size = new System.Drawing.Size(26, 17);
            this._TipStatusLabel.Text = "......";
            // 
            // MiscDemoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 626);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this._MenuStrip;
            this.MinimumSize = new System.Drawing.Size(120, 148);
            this.Name = "MiscDemoMainForm";
            this.Text = "MeterKnife Misc Demo Form";
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripMenuItem plot测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _MainPlotTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 主题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ThemeManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _GatewayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _CareOneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _KeysightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _InstrumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _InstrumentsDiscoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _UserControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _InstrumentCellToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _TipStatusLabel;
    }
}

