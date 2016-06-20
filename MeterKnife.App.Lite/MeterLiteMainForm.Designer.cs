namespace MeterKnife.App.Lite
{
    partial class MeterLiteMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeterLiteMainForm));
            this._StripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._PortLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AddMeterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._CareOptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._LoggerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._StripContainer.BottomToolStripPanel.SuspendLayout();
            this._StripContainer.TopToolStripPanel.SuspendLayout();
            this._StripContainer.SuspendLayout();
            this._StatusStrip.SuspendLayout();
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
            this._StripContainer.ContentPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this._StripContainer.ContentPanel.Size = new System.Drawing.Size(784, 515);
            this._StripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._StripContainer.Location = new System.Drawing.Point(0, 0);
            this._StripContainer.Name = "_StripContainer";
            this._StripContainer.Size = new System.Drawing.Size(784, 562);
            this._StripContainer.TabIndex = 0;
            this._StripContainer.Text = "toolStripContainer1";
            // 
            // _StripContainer.TopToolStripPanel
            // 
            this._StripContainer.TopToolStripPanel.Controls.Add(this._MenuStrip);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._PortLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(784, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "Care at:";
            // 
            // _PortLabel
            // 
            this._PortLabel.Name = "_PortLabel";
            this._PortLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileMenuItem,
            this._SettingMenuItem,
            this._AboutMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(784, 25);
            this._MenuStrip.TabIndex = 0;
            // 
            // _FileMenuItem
            // 
            this._FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddMeterMenuItem,
            this.toolStripSeparator2,
            this._ExitMenuItem});
            this._FileMenuItem.Name = "_FileMenuItem";
            this._FileMenuItem.Size = new System.Drawing.Size(58, 21);
            this._FileMenuItem.Text = "文件(&F)";
            // 
            // _AddMeterMenuItem
            // 
            this._AddMeterMenuItem.Name = "_AddMeterMenuItem";
            this._AddMeterMenuItem.Size = new System.Drawing.Size(152, 22);
            this._AddMeterMenuItem.Text = "新建仪器(&N)";
            this._AddMeterMenuItem.Click += new System.EventHandler(this._AddMeterMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // _ExitMenuItem
            // 
            this._ExitMenuItem.Name = "_ExitMenuItem";
            this._ExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this._ExitMenuItem.Text = "退出(&X)";
            this._ExitMenuItem.Click += new System.EventHandler(this._ExitMenuItem_Click);
            // 
            // _SettingMenuItem
            // 
            this._SettingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CareOptionMenuItem,
            this.toolStripSeparator1,
            this._LoggerMenuItem});
            this._SettingMenuItem.Name = "_SettingMenuItem";
            this._SettingMenuItem.Size = new System.Drawing.Size(59, 21);
            this._SettingMenuItem.Text = "设置(&T)";
            // 
            // _CareOptionMenuItem
            // 
            this._CareOptionMenuItem.Name = "_CareOptionMenuItem";
            this._CareOptionMenuItem.Size = new System.Drawing.Size(152, 22);
            this._CareOptionMenuItem.Text = "Care选项(&O)";
            this._CareOptionMenuItem.Click += new System.EventHandler(this._CareOptionMenuItem_Click);
            // 
            // _AboutMenuItem
            // 
            this._AboutMenuItem.Name = "_AboutMenuItem";
            this._AboutMenuItem.Size = new System.Drawing.Size(60, 21);
            this._AboutMenuItem.Text = "关于(&A)";
            this._AboutMenuItem.Click += new System.EventHandler(this._AboutMenuItem_Click);
            // 
            // _LoggerMenuItem
            // 
            this._LoggerMenuItem.Name = "_LoggerMenuItem";
            this._LoggerMenuItem.Size = new System.Drawing.Size(152, 22);
            this._LoggerMenuItem.Text = "程序日志(&L)";
            this._LoggerMenuItem.Click += new System.EventHandler(this._LoggerMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // MeterLiteMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._StripContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MeterLiteMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MeterKnife 2015 Lite";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._StripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._StripContainer.BottomToolStripPanel.PerformLayout();
            this._StripContainer.TopToolStripPanel.ResumeLayout(false);
            this._StripContainer.TopToolStripPanel.PerformLayout();
            this._StripContainer.ResumeLayout(false);
            this._StripContainer.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _StripContainer;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _FileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _SettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _CareOptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AddMeterMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _PortLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _LoggerMenuItem;
    }
}