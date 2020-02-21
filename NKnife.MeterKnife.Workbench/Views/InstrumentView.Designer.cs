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
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._ToolStrip.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
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
            this._InstrumentListView.Size = new System.Drawing.Size(544, 164);
            this._InstrumentListView.TabIndex = 5;
            this._InstrumentListView.UseCompatibleStateImageBehavior = false;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripButton,
            this._EditToolStripButton,
            this._DeleteToolStripButton});
            this._ToolStrip.Location = new System.Drawing.Point(3, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(112, 25);
            this._ToolStrip.TabIndex = 4;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // _NewToolStripButton
            // 
            this._NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._NewToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Res.eng_add;
            this._NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripButton.Name = "_NewToolStripButton";
            this._NewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._NewToolStripButton.Text = "新建";
            // 
            // _EditToolStripButton
            // 
            this._EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._EditToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Res.eng_edit;
            this._EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditToolStripButton.Name = "_EditToolStripButton";
            this._EditToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._EditToolStripButton.Text = "修改";
            // 
            // _DeleteToolStripButton
            // 
            this._DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Res.ints_delete;
            this._DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteToolStripButton.Name = "_DeleteToolStripButton";
            this._DeleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteToolStripButton.Text = "删除";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this._StatusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._InstrumentListView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(544, 164);
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
            // _StatusStrip
            // 
            this._StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(544, 22);
            this._StatusStrip.TabIndex = 0;
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
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
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
        private System.Windows.Forms.StatusStrip _StatusStrip;
    }
}