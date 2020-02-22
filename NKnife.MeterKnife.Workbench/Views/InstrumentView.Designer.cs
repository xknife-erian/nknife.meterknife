namespace NKnife.MeterKnife.Workbench.Views
{
    partial class InstrumentView
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
            this._InstrumentListView = new System.Windows.Forms.ListView();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._ToolStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _InstrumentListView
            // 
            this._InstrumentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._InstrumentListView.HideSelection = false;
            this._InstrumentListView.Location = new System.Drawing.Point(0, 0);
            this._InstrumentListView.MultiSelect = false;
            this._InstrumentListView.Name = "_InstrumentListView";
            this._InstrumentListView.Size = new System.Drawing.Size(544, 178);
            this._InstrumentListView.TabIndex = 5;
            this._InstrumentListView.UseCompatibleStateImageBehavior = false;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._ToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripButton,
            this._EditToolStripButton,
            this._DeleteToolStripButton});
            this._ToolStrip.Location = new System.Drawing.Point(3, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(150, 33);
            this._ToolStrip.TabIndex = 4;
            // 
            // _NewToolStripButton
            // 
            this._NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._NewToolStripButton.Name = "_NewToolStripButton";
            this._NewToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._NewToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._NewToolStripButton.Text = "新建";
            // 
            // _EditToolStripButton
            // 
            this._EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._EditToolStripButton.Name = "_EditToolStripButton";
            this._EditToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._EditToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._EditToolStripButton.Text = "修改";
            // 
            // _DeleteToolStripButton
            // 
            this._DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._DeleteToolStripButton.Name = "_DeleteToolStripButton";
            this._DeleteToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._DeleteToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._DeleteToolStripButton.Text = "删除";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._InstrumentListView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(544, 178);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(544, 211);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this._ToolStrip);
            // 
            // InstrumentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(544, 211);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "InstrumentView";
            this.Text = "仪器管理";
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _InstrumentListView;
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ToolStripButton _EditToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteToolStripButton;
        private System.Windows.Forms.ToolStripButton _NewToolStripButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    }
}