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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineeringView));
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._TreeView = new NKnife.MeterKnife.Workbench.Controls.EngineeringTree();
            this._EngPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._CreateEngStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._RefreshStripButton = new System.Windows.Forms.ToolStripButton();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this._SplitContainer);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(272, 699);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(272, 732);
            this._ToolStripContainer.TabIndex = 0;
            this._ToolStripContainer.Text = "_ToolStripContainer";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._ToolStrip);
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
            this._SplitContainer.Size = new System.Drawing.Size(272, 699);
            this._SplitContainer.SplitterDistance = 514;
            this._SplitContainer.TabIndex = 1;
            // 
            // _TreeView
            // 
            this._TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TreeView.FullRowSelect = true;
            this._TreeView.HideSelection = false;
            this._TreeView.ImageIndex = 0;
            this._TreeView.Location = new System.Drawing.Point(0, 0);
            this._TreeView.Name = "_TreeView";
            this._TreeView.SelectedImageIndex = 0;
            this._TreeView.Size = new System.Drawing.Size(272, 514);
            this._TreeView.TabIndex = 0;
            // 
            // _EngPropertyGrid
            // 
            this._EngPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._EngPropertyGrid.HelpVisible = false;
            this._EngPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this._EngPropertyGrid.Name = "_EngPropertyGrid";
            this._EngPropertyGrid.Size = new System.Drawing.Size(272, 181);
            this._EngPropertyGrid.TabIndex = 0;
            this._EngPropertyGrid.ToolbarVisible = false;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._ToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CreateEngStripButton,
            this._EditToolStripButton,
            this._DeleteStripButton,
            this.toolStripSeparator1,
            this._RefreshStripButton});
            this._ToolStrip.Location = new System.Drawing.Point(3, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(202, 33);
            this._ToolStrip.TabIndex = 0;
            // 
            // _CreateEngStripButton
            // 
            this._CreateEngStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._CreateEngStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_CreateEngStripButton.Image")));
            this._CreateEngStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CreateEngStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._CreateEngStripButton.Name = "_CreateEngStripButton";
            this._CreateEngStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._CreateEngStripButton.Size = new System.Drawing.Size(46, 31);
            this._CreateEngStripButton.Text = "新建";
            // 
            // _EditToolStripButton
            // 
            this._EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._EditToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_EditToolStripButton.Image")));
            this._EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._EditToolStripButton.Name = "_EditToolStripButton";
            this._EditToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._EditToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._EditToolStripButton.Text = "编辑";
            // 
            // _DeleteStripButton
            // 
            this._DeleteStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteStripButton.Image")));
            this._DeleteStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._DeleteStripButton.Name = "_DeleteStripButton";
            this._DeleteStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._DeleteStripButton.Size = new System.Drawing.Size(46, 31);
            this._DeleteStripButton.Text = "删除";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(5);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // _RefreshStripButton
            // 
            this._RefreshStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._RefreshStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_RefreshStripButton.Image")));
            this._RefreshStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._RefreshStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._RefreshStripButton.Name = "_RefreshStripButton";
            this._RefreshStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._RefreshStripButton.Size = new System.Drawing.Size(46, 31);
            this._RefreshStripButton.Text = "刷新";
            // 
            // EngineeringView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(272, 732);
            this.Controls.Add(this._ToolStripContainer);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EngineeringView";
            this.Text = "工程管理";
            this._ToolStripContainer.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private NKnife.MeterKnife.Workbench.Controls.EngineeringTree _TreeView;
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.PropertyGrid _EngPropertyGrid;
        private System.Windows.Forms.ToolStripButton _CreateEngStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteStripButton;
        private System.Windows.Forms.ToolStripButton _RefreshStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _EditToolStripButton;
    }
}