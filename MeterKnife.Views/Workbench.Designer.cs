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
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._StatusStrip.SuspendLayout();
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
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(784, 515);
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
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(784, 24);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _CenterToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.MenuStrip _MenuStrip;
    }
}