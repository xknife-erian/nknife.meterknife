namespace NKnife.MeterKnife.Workbench.Views
{
    partial class EngineeringView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineeringView));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._TreeView = new System.Windows.Forms.TreeView();
            this._EngPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._CreateEngStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteStripButton = new System.Windows.Forms.ToolStripButton();
            this._RefreshStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this._statusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._SplitContainer);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(272, 685);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(272, 732);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this._toolStrip);
            // 
            // _statusStrip
            // 
            this._statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._statusStrip.Location = new System.Drawing.Point(0, 0);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(272, 22);
            this._statusStrip.TabIndex = 0;
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.Location = new System.Drawing.Point(0, 0);
            this._SplitContainer.Name = "_SplitContainer";
            this._SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._TreeView);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.Controls.Add(this._EngPropertyGrid);
            this._SplitContainer.Size = new System.Drawing.Size(272, 685);
            this._SplitContainer.SplitterDistance = 504;
            this._SplitContainer.TabIndex = 1;
            // 
            // _TreeView
            // 
            this._TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TreeView.FullRowSelect = true;
            this._TreeView.HideSelection = false;
            this._TreeView.Location = new System.Drawing.Point(0, 0);
            this._TreeView.Name = "_TreeView";
            this._TreeView.Size = new System.Drawing.Size(272, 504);
            this._TreeView.TabIndex = 0;
            // 
            // _EngPropertyGrid
            // 
            this._EngPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._EngPropertyGrid.HelpVisible = false;
            this._EngPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this._EngPropertyGrid.Name = "_EngPropertyGrid";
            this._EngPropertyGrid.Size = new System.Drawing.Size(272, 177);
            this._EngPropertyGrid.TabIndex = 0;
            this._EngPropertyGrid.ToolbarVisible = false;
            // 
            // _toolStrip
            // 
            this._toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CreateEngStripButton,
            this._DeleteStripButton,
            this.toolStripSeparator1,
            this._RefreshStripButton});
            this._toolStrip.Location = new System.Drawing.Point(3, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(157, 25);
            this._toolStrip.TabIndex = 0;
            // 
            // _CreateEngStripButton
            // 
            this._CreateEngStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._CreateEngStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_CreateEngStripButton.Image")));
            this._CreateEngStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CreateEngStripButton.Name = "_CreateEngStripButton";
            this._CreateEngStripButton.Size = new System.Drawing.Size(36, 22);
            this._CreateEngStripButton.Text = "新建";
            // 
            // _DeleteStripButton
            // 
            this._DeleteStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteStripButton.Image")));
            this._DeleteStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteStripButton.Name = "_DeleteStripButton";
            this._DeleteStripButton.Size = new System.Drawing.Size(36, 22);
            this._DeleteStripButton.Text = "删除";
            // 
            // _RefreshStripButton
            // 
            this._RefreshStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._RefreshStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_RefreshStripButton.Image")));
            this._RefreshStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._RefreshStripButton.Name = "_RefreshStripButton";
            this._RefreshStripButton.Size = new System.Drawing.Size(36, 22);
            this._RefreshStripButton.Text = "刷新";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // EngineeringView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 732);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EngineeringView";
            this.Text = "EngineeringView";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.TreeView _TreeView;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.PropertyGrid _EngPropertyGrid;
        private System.Windows.Forms.ToolStripButton _CreateEngStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteStripButton;
        private System.Windows.Forms.ToolStripButton _RefreshStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}