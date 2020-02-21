namespace NKnife.MeterKnife.Workbench.Views
{
    partial class DUTView
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
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.BottomToolStripPanel
            // 
            this._ToolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this.listView1);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(493, 725);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(493, 772);
            this._ToolStripContainer.TabIndex = 0;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(493, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(493, 725);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripButton,
            this._EditToolStripButton,
            this._DeleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(81, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // _NewToolStripButton
            // 
            this._NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._NewToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.dut_add;
            this._NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripButton.Name = "_NewToolStripButton";
            this._NewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._NewToolStripButton.Text = "新建";
            // 
            // _EditToolStripButton
            // 
            this._EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._EditToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.dut_edit;
            this._EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditToolStripButton.Name = "_EditToolStripButton";
            this._EditToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._EditToolStripButton.Text = "修改";
            // 
            // _DeleteToolStripButton
            // 
            this._DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.dut_delete;
            this._DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteToolStripButton.Name = "_DeleteToolStripButton";
            this._DeleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteToolStripButton.Text = "删除";
            // 
            // DUTView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(493, 772);
            this.Controls.Add(this._ToolStripContainer);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DUTView";
            this.Text = "被测物(DUT)管理";
            this._ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this._ToolStripContainer.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _NewToolStripButton;
        private System.Windows.Forms.ToolStripButton _EditToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteToolStripButton;
    }
}