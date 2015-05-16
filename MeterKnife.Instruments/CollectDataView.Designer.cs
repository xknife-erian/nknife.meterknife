namespace MeterKnife.Instruments
{
    partial class CollectDataView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectDataView));
            this._MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this._LeftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._ParamsPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._FiguredDataPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._MainTabControl = new System.Windows.Forms.TabControl();
            this._PlotPage = new System.Windows.Forms.TabPage();
            this._PlotSplitContainer = new System.Windows.Forms.SplitContainer();
            this._DataGridPage = new System.Windows.Forms.TabPage();
            this._CollectDataList = new System.Windows.Forms.ListBox();
            this._PlotToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartStripButton = new System.Windows.Forms.ToolStripButton();
            this._StopStripButton = new System.Windows.Forms.ToolStripButton();
            this._SaveStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._ExportStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._PhotoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ZoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this._MainSplitContainer)).BeginInit();
            this._MainSplitContainer.Panel1.SuspendLayout();
            this._MainSplitContainer.Panel2.SuspendLayout();
            this._MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LeftSplitContainer)).BeginInit();
            this._LeftSplitContainer.Panel1.SuspendLayout();
            this._LeftSplitContainer.Panel2.SuspendLayout();
            this._LeftSplitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this._MainTabControl.SuspendLayout();
            this._PlotPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PlotSplitContainer)).BeginInit();
            this._PlotSplitContainer.SuspendLayout();
            this._DataGridPage.SuspendLayout();
            this._PlotToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MainSplitContainer
            // 
            this._MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._MainSplitContainer.Location = new System.Drawing.Point(2, 2);
            this._MainSplitContainer.Name = "_MainSplitContainer";
            // 
            // _MainSplitContainer.Panel1
            // 
            this._MainSplitContainer.Panel1.Controls.Add(this._LeftSplitContainer);
            this._MainSplitContainer.Panel1MinSize = 250;
            // 
            // _MainSplitContainer.Panel2
            // 
            this._MainSplitContainer.Panel2.Controls.Add(this._MainTabControl);
            this._MainSplitContainer.Panel2.Controls.Add(this._PlotToolStrip);
            this._MainSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._MainSplitContainer.Size = new System.Drawing.Size(899, 475);
            this._MainSplitContainer.SplitterDistance = 250;
            this._MainSplitContainer.TabIndex = 0;
            // 
            // _LeftSplitContainer
            // 
            this._LeftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LeftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this._LeftSplitContainer.Name = "_LeftSplitContainer";
            this._LeftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _LeftSplitContainer.Panel1
            // 
            this._LeftSplitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // _LeftSplitContainer.Panel2
            // 
            this._LeftSplitContainer.Panel2.Controls.Add(this.groupBox2);
            this._LeftSplitContainer.Size = new System.Drawing.Size(250, 475);
            this._LeftSplitContainer.SplitterDistance = 227;
            this._LeftSplitContainer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._ParamsPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 227);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // _ParamsPanel
            // 
            this._ParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ParamsPanel.Location = new System.Drawing.Point(3, 17);
            this._ParamsPanel.Name = "_ParamsPanel";
            this._ParamsPanel.Size = new System.Drawing.Size(244, 207);
            this._ParamsPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._FiguredDataPropertyGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 244);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "实时分析";
            // 
            // _FiguredDataPropertyGrid
            // 
            this._FiguredDataPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FiguredDataPropertyGrid.HelpVisible = false;
            this._FiguredDataPropertyGrid.Location = new System.Drawing.Point(3, 17);
            this._FiguredDataPropertyGrid.Name = "_FiguredDataPropertyGrid";
            this._FiguredDataPropertyGrid.Size = new System.Drawing.Size(244, 224);
            this._FiguredDataPropertyGrid.TabIndex = 0;
            this._FiguredDataPropertyGrid.ToolbarVisible = false;
            // 
            // _MainTabControl
            // 
            this._MainTabControl.Controls.Add(this._PlotPage);
            this._MainTabControl.Controls.Add(this._DataGridPage);
            this._MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainTabControl.ItemSize = new System.Drawing.Size(100, 24);
            this._MainTabControl.Location = new System.Drawing.Point(0, 27);
            this._MainTabControl.Name = "_MainTabControl";
            this._MainTabControl.Padding = new System.Drawing.Point(18, 3);
            this._MainTabControl.SelectedIndex = 0;
            this._MainTabControl.Size = new System.Drawing.Size(645, 448);
            this._MainTabControl.TabIndex = 0;
            // 
            // _PlotPage
            // 
            this._PlotPage.Controls.Add(this._PlotSplitContainer);
            this._PlotPage.Location = new System.Drawing.Point(4, 28);
            this._PlotPage.Name = "_PlotPage";
            this._PlotPage.Padding = new System.Windows.Forms.Padding(3);
            this._PlotPage.Size = new System.Drawing.Size(637, 416);
            this._PlotPage.TabIndex = 0;
            this._PlotPage.Text = "实时图表";
            this._PlotPage.UseVisualStyleBackColor = true;
            // 
            // _PlotSplitContainer
            // 
            this._PlotSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PlotSplitContainer.Location = new System.Drawing.Point(3, 3);
            this._PlotSplitContainer.Name = "_PlotSplitContainer";
            this._PlotSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this._PlotSplitContainer.Size = new System.Drawing.Size(631, 410);
            this._PlotSplitContainer.SplitterDistance = 250;
            this._PlotSplitContainer.TabIndex = 2;
            // 
            // _DataGridPage
            // 
            this._DataGridPage.Controls.Add(this._CollectDataList);
            this._DataGridPage.Location = new System.Drawing.Point(4, 28);
            this._DataGridPage.Name = "_DataGridPage";
            this._DataGridPage.Padding = new System.Windows.Forms.Padding(3);
            this._DataGridPage.Size = new System.Drawing.Size(637, 416);
            this._DataGridPage.TabIndex = 1;
            this._DataGridPage.Text = "实时数据";
            this._DataGridPage.UseVisualStyleBackColor = true;
            // 
            // _CollectDataList
            // 
            this._CollectDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._CollectDataList.FormattingEnabled = true;
            this._CollectDataList.Location = new System.Drawing.Point(3, 3);
            this._CollectDataList.Name = "_CollectDataList";
            this._CollectDataList.Size = new System.Drawing.Size(631, 410);
            this._CollectDataList.TabIndex = 0;
            // 
            // _PlotToolStrip
            // 
            this._PlotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartStripButton,
            this._StopStripButton,
            this._SaveStripButton3,
            this.toolStripSeparator1,
            this._ExportStripButton1,
            this._PhotoToolStripButton,
            this.toolStripSeparator2,
            this._ZoomInToolStripButton,
            this._ZoomOutToolStripButton,
            this.toolStripSeparator3,
            this.toolStripLabel2});
            this._PlotToolStrip.Location = new System.Drawing.Point(0, 2);
            this._PlotToolStrip.Name = "_PlotToolStrip";
            this._PlotToolStrip.Size = new System.Drawing.Size(645, 25);
            this._PlotToolStrip.TabIndex = 0;
            this._PlotToolStrip.Text = "toolStrip1";
            // 
            // _StartStripButton
            // 
            this._StartStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_StartStripButton.Image")));
            this._StartStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartStripButton.Name = "_StartStripButton";
            this._StartStripButton.Size = new System.Drawing.Size(23, 22);
            this._StartStripButton.Text = "开始";
            this._StartStripButton.Click += new System.EventHandler(this._StartStripButton_Click);
            // 
            // _StopStripButton
            // 
            this._StopStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StopStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_StopStripButton.Image")));
            this._StopStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopStripButton.Name = "_StopStripButton";
            this._StopStripButton.Size = new System.Drawing.Size(23, 22);
            this._StopStripButton.Text = "停止";
            this._StopStripButton.Click += new System.EventHandler(this._StopStripButton_Click);
            // 
            // _SaveStripButton3
            // 
            this._SaveStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._SaveStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("_SaveStripButton3.Image")));
            this._SaveStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveStripButton3.Name = "_SaveStripButton3";
            this._SaveStripButton3.Size = new System.Drawing.Size(23, 22);
            this._SaveStripButton3.Text = "保存";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _ExportStripButton1
            // 
            this._ExportStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ExportStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("_ExportStripButton1.Image")));
            this._ExportStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ExportStripButton1.Name = "_ExportStripButton1";
            this._ExportStripButton1.Size = new System.Drawing.Size(23, 22);
            this._ExportStripButton1.Text = "导出";
            // 
            // _PhotoToolStripButton
            // 
            this._PhotoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PhotoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_PhotoToolStripButton.Image")));
            this._PhotoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PhotoToolStripButton.Name = "_PhotoToolStripButton";
            this._PhotoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._PhotoToolStripButton.Text = "截图";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _ZoomInToolStripButton
            // 
            this._ZoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomInToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_ZoomInToolStripButton.Image")));
            this._ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomInToolStripButton.Name = "_ZoomInToolStripButton";
            this._ZoomInToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._ZoomInToolStripButton.Text = "放大";
            // 
            // _ZoomOutToolStripButton
            // 
            this._ZoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomOutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_ZoomOutToolStripButton.Image")));
            this._ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomOutToolStripButton.Name = "_ZoomOutToolStripButton";
            this._ZoomOutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._ZoomOutToolStripButton.Text = "缩小";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // CollectDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 479);
            this.Controls.Add(this._MainSplitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "CollectDataView";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "CollectDataView";
            this._MainSplitContainer.Panel1.ResumeLayout(false);
            this._MainSplitContainer.Panel2.ResumeLayout(false);
            this._MainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._MainSplitContainer)).EndInit();
            this._MainSplitContainer.ResumeLayout(false);
            this._LeftSplitContainer.Panel1.ResumeLayout(false);
            this._LeftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._LeftSplitContainer)).EndInit();
            this._LeftSplitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this._MainTabControl.ResumeLayout(false);
            this._PlotPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._PlotSplitContainer)).EndInit();
            this._PlotSplitContainer.ResumeLayout(false);
            this._DataGridPage.ResumeLayout(false);
            this._PlotToolStrip.ResumeLayout(false);
            this._PlotToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _MainSplitContainer;
        private System.Windows.Forms.SplitContainer _LeftSplitContainer;
        private System.Windows.Forms.TabControl _MainTabControl;
        private System.Windows.Forms.TabPage _PlotPage;
        private System.Windows.Forms.TabPage _DataGridPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip _PlotToolStrip;
        private System.Windows.Forms.SplitContainer _PlotSplitContainer;
        private System.Windows.Forms.ToolStripButton _StartStripButton;
        private System.Windows.Forms.ToolStripButton _StopStripButton;
        private System.Windows.Forms.ToolStripButton _SaveStripButton3;
        private System.Windows.Forms.PropertyGrid _FiguredDataPropertyGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _ExportStripButton1;
        private System.Windows.Forms.ToolStripButton _PhotoToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _ZoomInToolStripButton;
        private System.Windows.Forms.ToolStripButton _ZoomOutToolStripButton;
        private System.Windows.Forms.Panel _ParamsPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ListBox _CollectDataList;
    }
}