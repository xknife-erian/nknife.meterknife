
namespace NKnife.Win.Quick
{
    partial class QuickForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickForm));
            this._StripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._SpringStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._VersionUpdateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._StripContainer.BottomToolStripPanel.SuspendLayout();
            this._StripContainer.TopToolStripPanel.SuspendLayout();
            this._StripContainer.SuspendLayout();
            this._StatusStrip.SuspendLayout();
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
            this._StripContainer.ContentPanel.Size = new System.Drawing.Size(864, 469);
            this._StripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._StripContainer.Location = new System.Drawing.Point(0, 0);
            this._StripContainer.Name = "_StripContainer";
            this._StripContainer.Size = new System.Drawing.Size(864, 515);
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
            this._StatusStripLabel,
            this._SpringStripLabel,
            this._VersionUpdateLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(864, 22);
            this._StatusStrip.SizingGrip = false;
            this._StatusStrip.TabIndex = 0;
            // 
            // _StatusStripLabel
            // 
            this._StatusStripLabel.Name = "_StatusStripLabel";
            this._StatusStripLabel.Size = new System.Drawing.Size(17, 17);
            this._StatusStripLabel.Text = "...";
            // 
            // _SpringStripLabel
            // 
            this._SpringStripLabel.Name = "_SpringStripLabel";
            this._SpringStripLabel.Size = new System.Drawing.Size(815, 17);
            this._SpringStripLabel.Spring = true;
            // 
            // _VersionUpdateLabel
            // 
            this._VersionUpdateLabel.Name = "_VersionUpdateLabel";
            this._VersionUpdateLabel.Size = new System.Drawing.Size(17, 17);
            this._VersionUpdateLabel.Text = "...";
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(864, 24);
            this._MenuStrip.TabIndex = 0;
            // 
            // QuickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(864, 515);
            this.Controls.Add(this._StripContainer);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "QuickForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workbench";
            this._StripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._StripContainer.BottomToolStripPanel.PerformLayout();
            this._StripContainer.TopToolStripPanel.ResumeLayout(false);
            this._StripContainer.TopToolStripPanel.PerformLayout();
            this._StripContainer.ResumeLayout(false);
            this._StripContainer.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _StripContainer;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripStatusLabel _StatusStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel _SpringStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel _VersionUpdateLabel;
    }
}

