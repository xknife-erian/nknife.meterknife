using NKnife.GUI.WinForm;

namespace MeterKnife.Instruments
{
    partial class DigitMultiMeterView
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
            this._MainSplitContainer = new NKnife.GUI.WinForm.CollapsibleSplitContainer();
            this._LeftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._FiguredDataPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._ParamsGroupBox = new System.Windows.Forms.GroupBox();
            this._ParamsPanel = new System.Windows.Forms.Panel();
            this._MainTabControl = new System.Windows.Forms.TabControl();
            this._PlotPage = new System.Windows.Forms.TabPage();
            this._RealtimePlotSplitContainer = new System.Windows.Forms.SplitContainer();
            this._DataGridPage = new System.Windows.Forms.TabPage();
            this._FiguredDataGridView = new System.Windows.Forms.DataGridView();
            this._FeaturesPage = new System.Windows.Forms.TabPage();
            this._FeaturesTabControl = new System.Windows.Forms.TabControl();
            this._TemperatureFeaturesTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._TempFeaturesPanel = new System.Windows.Forms.Panel();
            this._SdPanel = new System.Windows.Forms.Panel();
            this._TempTrendPanel = new System.Windows.Forms.Panel();
            this._PlotToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartStripButton = new System.Windows.Forms.ToolStripButton();
            this._StopStripButton = new System.Windows.Forms.ToolStripButton();
            this._ClearDataToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._SaveStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._ExportStripButton = new System.Windows.Forms.ToolStripButton();
            this._PhotoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._StandardDeviationRangeComboBox = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._MainSplitContainer)).BeginInit();
            this._MainSplitContainer.Panel1.SuspendLayout();
            this._MainSplitContainer.Panel2.SuspendLayout();
            this._MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LeftSplitContainer)).BeginInit();
            this._LeftSplitContainer.Panel1.SuspendLayout();
            this._LeftSplitContainer.Panel2.SuspendLayout();
            this._LeftSplitContainer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this._ParamsGroupBox.SuspendLayout();
            this._MainTabControl.SuspendLayout();
            this._PlotPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._RealtimePlotSplitContainer)).BeginInit();
            this._RealtimePlotSplitContainer.SuspendLayout();
            this._DataGridPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._FiguredDataGridView)).BeginInit();
            this._FeaturesPage.SuspendLayout();
            this._FeaturesTabControl.SuspendLayout();
            this._TemperatureFeaturesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._PlotToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MainSplitContainer
            // 
            this._MainSplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this._MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._MainSplitContainer.Location = new System.Drawing.Point(2, 2);
            this._MainSplitContainer.Name = "_MainSplitContainer";
            // 
            // _MainSplitContainer.Panel1
            // 
            this._MainSplitContainer.Panel1.Controls.Add(this._LeftSplitContainer);
            this._MainSplitContainer.Panel1MinSize = 1;
            // 
            // _MainSplitContainer.Panel2
            // 
            this._MainSplitContainer.Panel2.Controls.Add(this._MainTabControl);
            this._MainSplitContainer.Panel2.Controls.Add(this._PlotToolStrip);
            this._MainSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._MainSplitContainer.Panel2MinSize = 100;
            this._MainSplitContainer.Size = new System.Drawing.Size(932, 475);
            this._MainSplitContainer.SplitterButtonStyle = NKnife.GUI.WinForm.CollapsibleSplitContainer.ButtonStyle.ScrollBar;
            this._MainSplitContainer.SplitterDistance = 250;
            this._MainSplitContainer.SplitterWidth = 22;
            this._MainSplitContainer.TabIndex = 0;
            // 
            // _LeftSplitContainer
            // 
            this._LeftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LeftSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._LeftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this._LeftSplitContainer.Name = "_LeftSplitContainer";
            this._LeftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _LeftSplitContainer.Panel1
            // 
            this._LeftSplitContainer.Panel1.Controls.Add(this.groupBox2);
            // 
            // _LeftSplitContainer.Panel2
            // 
            this._LeftSplitContainer.Panel2.Controls.Add(this._ParamsGroupBox);
            this._LeftSplitContainer.Size = new System.Drawing.Size(250, 475);
            this._LeftSplitContainer.SplitterDistance = 274;
            this._LeftSplitContainer.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._FiguredDataPropertyGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 274);
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
            this._FiguredDataPropertyGrid.Size = new System.Drawing.Size(244, 254);
            this._FiguredDataPropertyGrid.TabIndex = 0;
            this._FiguredDataPropertyGrid.ToolbarVisible = false;
            // 
            // _ParamsGroupBox
            // 
            this._ParamsGroupBox.Controls.Add(this._ParamsPanel);
            this._ParamsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ParamsGroupBox.Location = new System.Drawing.Point(0, 0);
            this._ParamsGroupBox.Name = "_ParamsGroupBox";
            this._ParamsGroupBox.Size = new System.Drawing.Size(250, 197);
            this._ParamsGroupBox.TabIndex = 0;
            this._ParamsGroupBox.TabStop = false;
            this._ParamsGroupBox.Text = "参数";
            // 
            // _ParamsPanel
            // 
            this._ParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ParamsPanel.Location = new System.Drawing.Point(3, 17);
            this._ParamsPanel.Name = "_ParamsPanel";
            this._ParamsPanel.Size = new System.Drawing.Size(244, 177);
            this._ParamsPanel.TabIndex = 0;
            // 
            // _MainTabControl
            // 
            this._MainTabControl.Controls.Add(this._PlotPage);
            this._MainTabControl.Controls.Add(this._DataGridPage);
            this._MainTabControl.Controls.Add(this._FeaturesPage);
            this._MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainTabControl.ItemSize = new System.Drawing.Size(100, 30);
            this._MainTabControl.Location = new System.Drawing.Point(0, 27);
            this._MainTabControl.Name = "_MainTabControl";
            this._MainTabControl.Padding = new System.Drawing.Point(18, 3);
            this._MainTabControl.SelectedIndex = 0;
            this._MainTabControl.Size = new System.Drawing.Size(660, 448);
            this._MainTabControl.TabIndex = 0;
            // 
            // _PlotPage
            // 
            this._PlotPage.Controls.Add(this._RealtimePlotSplitContainer);
            this._PlotPage.Location = new System.Drawing.Point(4, 34);
            this._PlotPage.Name = "_PlotPage";
            this._PlotPage.Padding = new System.Windows.Forms.Padding(3);
            this._PlotPage.Size = new System.Drawing.Size(652, 410);
            this._PlotPage.TabIndex = 0;
            this._PlotPage.Text = "实时图";
            this._PlotPage.UseVisualStyleBackColor = true;
            // 
            // _RealtimePlotSplitContainer
            // 
            this._RealtimePlotSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._RealtimePlotSplitContainer.Location = new System.Drawing.Point(3, 3);
            this._RealtimePlotSplitContainer.Name = "_RealtimePlotSplitContainer";
            this._RealtimePlotSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _RealtimePlotSplitContainer.Panel1
            // 
            this._RealtimePlotSplitContainer.Panel1.BackColor = System.Drawing.Color.Transparent;
            // 
            // _RealtimePlotSplitContainer.Panel2
            // 
            this._RealtimePlotSplitContainer.Panel2.BackColor = System.Drawing.Color.Transparent;
            this._RealtimePlotSplitContainer.Size = new System.Drawing.Size(646, 404);
            this._RealtimePlotSplitContainer.SplitterDistance = 246;
            this._RealtimePlotSplitContainer.TabIndex = 2;
            // 
            // _DataGridPage
            // 
            this._DataGridPage.Controls.Add(this._FiguredDataGridView);
            this._DataGridPage.Location = new System.Drawing.Point(4, 34);
            this._DataGridPage.Name = "_DataGridPage";
            this._DataGridPage.Padding = new System.Windows.Forms.Padding(3);
            this._DataGridPage.Size = new System.Drawing.Size(652, 410);
            this._DataGridPage.TabIndex = 1;
            this._DataGridPage.Text = "实时数据";
            this._DataGridPage.UseVisualStyleBackColor = true;
            // 
            // _FiguredDataGridView
            // 
            this._FiguredDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._FiguredDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FiguredDataGridView.Location = new System.Drawing.Point(3, 3);
            this._FiguredDataGridView.Name = "_FiguredDataGridView";
            this._FiguredDataGridView.RowTemplate.Height = 23;
            this._FiguredDataGridView.Size = new System.Drawing.Size(646, 404);
            this._FiguredDataGridView.TabIndex = 0;
            // 
            // _FeaturesPage
            // 
            this._FeaturesPage.Controls.Add(this._FeaturesTabControl);
            this._FeaturesPage.Location = new System.Drawing.Point(4, 34);
            this._FeaturesPage.Name = "_FeaturesPage";
            this._FeaturesPage.Padding = new System.Windows.Forms.Padding(3);
            this._FeaturesPage.Size = new System.Drawing.Size(652, 410);
            this._FeaturesPage.TabIndex = 2;
            this._FeaturesPage.Text = "特性";
            this._FeaturesPage.UseVisualStyleBackColor = true;
            // 
            // _FeaturesTabControl
            // 
            this._FeaturesTabControl.Controls.Add(this._TemperatureFeaturesTabPage);
            this._FeaturesTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FeaturesTabControl.ItemSize = new System.Drawing.Size(60, 22);
            this._FeaturesTabControl.Location = new System.Drawing.Point(3, 3);
            this._FeaturesTabControl.Multiline = true;
            this._FeaturesTabControl.Name = "_FeaturesTabControl";
            this._FeaturesTabControl.Padding = new System.Drawing.Point(10, 3);
            this._FeaturesTabControl.SelectedIndex = 0;
            this._FeaturesTabControl.Size = new System.Drawing.Size(646, 404);
            this._FeaturesTabControl.TabIndex = 0;
            // 
            // _TemperatureFeaturesTabPage
            // 
            this._TemperatureFeaturesTabPage.Controls.Add(this.splitContainer1);
            this._TemperatureFeaturesTabPage.Location = new System.Drawing.Point(4, 26);
            this._TemperatureFeaturesTabPage.Name = "_TemperatureFeaturesTabPage";
            this._TemperatureFeaturesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._TemperatureFeaturesTabPage.Size = new System.Drawing.Size(638, 374);
            this._TemperatureFeaturesTabPage.TabIndex = 0;
            this._TemperatureFeaturesTabPage.Text = "温度特性";
            this._TemperatureFeaturesTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._TempTrendPanel);
            this.splitContainer1.Size = new System.Drawing.Size(632, 368);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._TempFeaturesPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._SdPanel);
            this.splitContainer2.Size = new System.Drawing.Size(632, 185);
            this.splitContainer2.SplitterDistance = 324;
            this.splitContainer2.TabIndex = 0;
            // 
            // _TempFeaturesPanel
            // 
            this._TempFeaturesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TempFeaturesPanel.Location = new System.Drawing.Point(0, 0);
            this._TempFeaturesPanel.Name = "_TempFeaturesPanel";
            this._TempFeaturesPanel.Size = new System.Drawing.Size(324, 185);
            this._TempFeaturesPanel.TabIndex = 0;
            // 
            // _SdPanel
            // 
            this._SdPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SdPanel.Location = new System.Drawing.Point(0, 0);
            this._SdPanel.Name = "_SdPanel";
            this._SdPanel.Size = new System.Drawing.Size(304, 185);
            this._SdPanel.TabIndex = 0;
            // 
            // _TempTrendPanel
            // 
            this._TempTrendPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TempTrendPanel.Location = new System.Drawing.Point(0, 0);
            this._TempTrendPanel.Name = "_TempTrendPanel";
            this._TempTrendPanel.Size = new System.Drawing.Size(632, 179);
            this._TempTrendPanel.TabIndex = 0;
            // 
            // _PlotToolStrip
            // 
            this._PlotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartStripButton,
            this._StopStripButton,
            this._ClearDataToolStripButton,
            this._SaveStripButton,
            this.toolStripSeparator1,
            this._ExportStripButton,
            this._PhotoToolStripButton,
            this._ZoomInToolStripButton,
            this._ZoomOutToolStripButton,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.toolStripLabel1,
            this._StandardDeviationRangeComboBox});
            this._PlotToolStrip.Location = new System.Drawing.Point(0, 2);
            this._PlotToolStrip.Name = "_PlotToolStrip";
            this._PlotToolStrip.Size = new System.Drawing.Size(660, 25);
            this._PlotToolStrip.TabIndex = 0;
            this._PlotToolStrip.Text = "toolStrip1";
            // 
            // _StartStripButton
            // 
            this._StartStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.start;
            this._StartStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartStripButton.Name = "_StartStripButton";
            this._StartStripButton.Size = new System.Drawing.Size(23, 22);
            this._StartStripButton.Text = "开始";
            this._StartStripButton.Click += new System.EventHandler(this._StartStripButton_Click);
            // 
            // _StopStripButton
            // 
            this._StopStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StopStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.stop;
            this._StopStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopStripButton.Name = "_StopStripButton";
            this._StopStripButton.Size = new System.Drawing.Size(23, 22);
            this._StopStripButton.Text = "停止";
            this._StopStripButton.Click += new System.EventHandler(this._StopStripButton_Click);
            // 
            // _ClearDataToolStripButton
            // 
            this._ClearDataToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ClearDataToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.clear;
            this._ClearDataToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ClearDataToolStripButton.Name = "_ClearDataToolStripButton";
            this._ClearDataToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._ClearDataToolStripButton.Text = "清除数据";
            this._ClearDataToolStripButton.Click += new System.EventHandler(this._ClearDataToolStripButton_Click);
            // 
            // _SaveStripButton
            // 
            this._SaveStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._SaveStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.save;
            this._SaveStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveStripButton.Name = "_SaveStripButton";
            this._SaveStripButton.Size = new System.Drawing.Size(23, 22);
            this._SaveStripButton.Text = "保存";
            this._SaveStripButton.Click += new System.EventHandler(this._SaveStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _ExportStripButton
            // 
            this._ExportStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ExportStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.export;
            this._ExportStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ExportStripButton.Name = "_ExportStripButton";
            this._ExportStripButton.Size = new System.Drawing.Size(23, 22);
            this._ExportStripButton.Text = "导出";
            this._ExportStripButton.Click += new System.EventHandler(this._ExportStripButton_Click);
            // 
            // _PhotoToolStripButton
            // 
            this._PhotoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PhotoToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.photo;
            this._PhotoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PhotoToolStripButton.Name = "_PhotoToolStripButton";
            this._PhotoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._PhotoToolStripButton.Text = "截图";
            this._PhotoToolStripButton.Click += new System.EventHandler(this._PhotoToolStripButton_Click);
            // 
            // _ZoomInToolStripButton
            // 
            this._ZoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomInToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.zoom_in;
            this._ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomInToolStripButton.Name = "_ZoomInToolStripButton";
            this._ZoomInToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._ZoomInToolStripButton.Text = "放大";
            this._ZoomInToolStripButton.Click += new System.EventHandler(this._ZoomInToolStripButton_Click);
            // 
            // _ZoomOutToolStripButton
            // 
            this._ZoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomOutToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.zoom_out;
            this._ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomOutToolStripButton.Name = "_ZoomOutToolStripButton";
            this._ZoomOutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._ZoomOutToolStripButton.Text = "缩小";
            this._ZoomOutToolStripButton.Click += new System.EventHandler(this._ZoomOutToolStripButton_Click);
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "标准差范围:";
            // 
            // _StandardDeviationRangeComboBox
            // 
            this._StandardDeviationRangeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._StandardDeviationRangeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._StandardDeviationRangeComboBox.Items.AddRange(new object[] {
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this._StandardDeviationRangeComboBox.Name = "_StandardDeviationRangeComboBox";
            this._StandardDeviationRangeComboBox.Size = new System.Drawing.Size(75, 25);
            this._StandardDeviationRangeComboBox.Text = "100";
            // 
            // DigitMultiMeterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 479);
            this.Controls.Add(this._MainSplitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "DigitMultiMeterView";
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
            this.groupBox2.ResumeLayout(false);
            this._ParamsGroupBox.ResumeLayout(false);
            this._MainTabControl.ResumeLayout(false);
            this._PlotPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._RealtimePlotSplitContainer)).EndInit();
            this._RealtimePlotSplitContainer.ResumeLayout(false);
            this._DataGridPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._FiguredDataGridView)).EndInit();
            this._FeaturesPage.ResumeLayout(false);
            this._FeaturesTabControl.ResumeLayout(false);
            this._TemperatureFeaturesTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this._PlotToolStrip.ResumeLayout(false);
            this._PlotToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected CollapsibleSplitContainer _MainSplitContainer;
        protected System.Windows.Forms.SplitContainer _LeftSplitContainer;
        protected System.Windows.Forms.TabControl _MainTabControl;
        protected System.Windows.Forms.TabPage _PlotPage;
        protected System.Windows.Forms.TabPage _DataGridPage;
        protected System.Windows.Forms.GroupBox _ParamsGroupBox;
        protected System.Windows.Forms.GroupBox groupBox2;
        protected System.Windows.Forms.ToolStrip _PlotToolStrip;
        protected System.Windows.Forms.SplitContainer _RealtimePlotSplitContainer;
        protected System.Windows.Forms.ToolStripButton _StartStripButton;
        protected System.Windows.Forms.ToolStripButton _StopStripButton;
        protected System.Windows.Forms.ToolStripButton _SaveStripButton;
        protected System.Windows.Forms.PropertyGrid _FiguredDataPropertyGrid;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton _ExportStripButton;
        protected System.Windows.Forms.ToolStripButton _PhotoToolStripButton;
        protected System.Windows.Forms.ToolStripButton _ZoomInToolStripButton;
        protected System.Windows.Forms.ToolStripButton _ZoomOutToolStripButton;
        protected System.Windows.Forms.Panel _ParamsPanel;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.TabPage _FeaturesPage;
        private System.Windows.Forms.ToolStripButton _ClearDataToolStripButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox _StandardDeviationRangeComboBox;
        private System.Windows.Forms.TabControl _FeaturesTabControl;
        private System.Windows.Forms.TabPage _TemperatureFeaturesTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel _TempFeaturesPanel;
        private System.Windows.Forms.Panel _SdPanel;
        private System.Windows.Forms.Panel _TempTrendPanel;
        private System.Windows.Forms.DataGridView _FiguredDataGridView;
    }
}