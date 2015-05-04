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
            this._MeterParamPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._MainTabControl = new System.Windows.Forms.TabControl();
            this._PlotPage = new System.Windows.Forms.TabPage();
            this._PlotSplitContainer = new System.Windows.Forms.SplitContainer();
            this._PlotToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartStripButton = new System.Windows.Forms.ToolStripButton();
            this._StopStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this._DataGridPage = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this._FiguredDataPropertyGrid = new System.Windows.Forms.PropertyGrid();
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
            this._PlotToolStrip.SuspendLayout();
            this._DataGridPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this._MainSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._MainSplitContainer.Size = new System.Drawing.Size(620, 438);
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
            this._LeftSplitContainer.Size = new System.Drawing.Size(250, 438);
            this._LeftSplitContainer.SplitterDistance = 191;
            this._LeftSplitContainer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._MeterParamPropertyGrid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // _MeterParamPropertyGrid
            // 
            this._MeterParamPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MeterParamPropertyGrid.HelpVisible = false;
            this._MeterParamPropertyGrid.Location = new System.Drawing.Point(3, 17);
            this._MeterParamPropertyGrid.Name = "_MeterParamPropertyGrid";
            this._MeterParamPropertyGrid.Size = new System.Drawing.Size(244, 171);
            this._MeterParamPropertyGrid.TabIndex = 1;
            this._MeterParamPropertyGrid.ToolbarVisible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._FiguredDataPropertyGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 243);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "实时分析";
            // 
            // _MainTabControl
            // 
            this._MainTabControl.Controls.Add(this._PlotPage);
            this._MainTabControl.Controls.Add(this._DataGridPage);
            this._MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainTabControl.ItemSize = new System.Drawing.Size(100, 24);
            this._MainTabControl.Location = new System.Drawing.Point(0, 2);
            this._MainTabControl.Name = "_MainTabControl";
            this._MainTabControl.Padding = new System.Drawing.Point(18, 3);
            this._MainTabControl.SelectedIndex = 0;
            this._MainTabControl.Size = new System.Drawing.Size(366, 436);
            this._MainTabControl.TabIndex = 0;
            // 
            // _PlotPage
            // 
            this._PlotPage.Controls.Add(this._PlotSplitContainer);
            this._PlotPage.Controls.Add(this._PlotToolStrip);
            this._PlotPage.Location = new System.Drawing.Point(4, 28);
            this._PlotPage.Name = "_PlotPage";
            this._PlotPage.Padding = new System.Windows.Forms.Padding(3);
            this._PlotPage.Size = new System.Drawing.Size(358, 404);
            this._PlotPage.TabIndex = 0;
            this._PlotPage.Text = "实时图表";
            this._PlotPage.UseVisualStyleBackColor = true;
            // 
            // _PlotSplitContainer
            // 
            this._PlotSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PlotSplitContainer.Location = new System.Drawing.Point(3, 28);
            this._PlotSplitContainer.Name = "_PlotSplitContainer";
            this._PlotSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this._PlotSplitContainer.Size = new System.Drawing.Size(352, 373);
            this._PlotSplitContainer.SplitterDistance = 228;
            this._PlotSplitContainer.TabIndex = 2;
            // 
            // _PlotToolStrip
            // 
            this._PlotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartStripButton,
            this._StopStripButton,
            this.toolStripButton3});
            this._PlotToolStrip.Location = new System.Drawing.Point(3, 3);
            this._PlotToolStrip.Name = "_PlotToolStrip";
            this._PlotToolStrip.Size = new System.Drawing.Size(352, 25);
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
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // _DataGridPage
            // 
            this._DataGridPage.Controls.Add(this.dataGridView1);
            this._DataGridPage.Location = new System.Drawing.Point(4, 28);
            this._DataGridPage.Name = "_DataGridPage";
            this._DataGridPage.Padding = new System.Windows.Forms.Padding(3);
            this._DataGridPage.Size = new System.Drawing.Size(358, 404);
            this._DataGridPage.TabIndex = 1;
            this._DataGridPage.Text = "实时数据";
            this._DataGridPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(352, 398);
            this.dataGridView1.TabIndex = 0;
            // 
            // _FiguredDataPropertyGrid
            // 
            this._FiguredDataPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FiguredDataPropertyGrid.HelpVisible = false;
            this._FiguredDataPropertyGrid.Location = new System.Drawing.Point(3, 17);
            this._FiguredDataPropertyGrid.Name = "_FiguredDataPropertyGrid";
            this._FiguredDataPropertyGrid.Size = new System.Drawing.Size(244, 223);
            this._FiguredDataPropertyGrid.TabIndex = 0;
            this._FiguredDataPropertyGrid.ToolbarVisible = false;
            // 
            // CollectDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this._MainSplitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "CollectDataView";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "CollectDataView";
            this._MainSplitContainer.Panel1.ResumeLayout(false);
            this._MainSplitContainer.Panel2.ResumeLayout(false);
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
            this._PlotPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PlotSplitContainer)).EndInit();
            this._PlotSplitContainer.ResumeLayout(false);
            this._PlotToolStrip.ResumeLayout(false);
            this._PlotToolStrip.PerformLayout();
            this._DataGridPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.PropertyGrid _MeterParamPropertyGrid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton _StartStripButton;
        private System.Windows.Forms.ToolStripButton _StopStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.PropertyGrid _FiguredDataPropertyGrid;
    }
}