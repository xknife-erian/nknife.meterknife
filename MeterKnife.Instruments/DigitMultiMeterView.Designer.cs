﻿using NKnife.GUI.WinForm;

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
            this._FiguredDataPropertyGrid = new MeterKnife.Common.Winforms.Controls.FiguredDataGrid();
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
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._StartStripButton = new System.Windows.Forms.ToolStripButton();
            this._StopStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this._SaveStripButton = new System.Windows.Forms.ToolStripButton();
            this._ClearDataToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._PrintStripButton = new System.Windows.Forms.ToolStripButton();
            this._ExportStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._FilterToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this._PhotoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this._MainSplitContainer)).BeginInit();
            this._MainSplitContainer.Panel1.SuspendLayout();
            this._MainSplitContainer.Panel2.SuspendLayout();
            this._MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LeftSplitContainer)).BeginInit();
            this._LeftSplitContainer.Panel1.SuspendLayout();
            this._LeftSplitContainer.SuspendLayout();
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
            this._MainSplitContainer.Size = new System.Drawing.Size(960, 518);
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
            this._LeftSplitContainer.Panel1.Controls.Add(this._FiguredDataPropertyGrid);
            this._LeftSplitContainer.Size = new System.Drawing.Size(250, 518);
            this._LeftSplitContainer.SplitterDistance = 376;
            this._LeftSplitContainer.TabIndex = 0;
            // 
            // _FiguredDataPropertyGrid
            // 
            this._FiguredDataPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FiguredDataPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this._FiguredDataPropertyGrid.Name = "_FiguredDataPropertyGrid";
            this._FiguredDataPropertyGrid.Size = new System.Drawing.Size(250, 376);
            this._FiguredDataPropertyGrid.TabIndex = 0;
            // 
            // _MainTabControl
            // 
            this._MainTabControl.Controls.Add(this._PlotPage);
            this._MainTabControl.Controls.Add(this._DataGridPage);
            this._MainTabControl.Controls.Add(this._FeaturesPage);
            this._MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainTabControl.ItemSize = new System.Drawing.Size(100, 30);
            this._MainTabControl.Location = new System.Drawing.Point(0, 37);
            this._MainTabControl.Name = "_MainTabControl";
            this._MainTabControl.Padding = new System.Drawing.Point(18, 3);
            this._MainTabControl.SelectedIndex = 0;
            this._MainTabControl.Size = new System.Drawing.Size(688, 481);
            this._MainTabControl.TabIndex = 0;
            // 
            // _PlotPage
            // 
            this._PlotPage.Controls.Add(this._RealtimePlotSplitContainer);
            this._PlotPage.Location = new System.Drawing.Point(4, 34);
            this._PlotPage.Name = "_PlotPage";
            this._PlotPage.Padding = new System.Windows.Forms.Padding(3);
            this._PlotPage.Size = new System.Drawing.Size(680, 443);
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
            this._RealtimePlotSplitContainer.Size = new System.Drawing.Size(674, 437);
            this._RealtimePlotSplitContainer.SplitterDistance = 265;
            this._RealtimePlotSplitContainer.TabIndex = 2;
            // 
            // _DataGridPage
            // 
            this._DataGridPage.Controls.Add(this._FiguredDataGridView);
            this._DataGridPage.Location = new System.Drawing.Point(4, 34);
            this._DataGridPage.Name = "_DataGridPage";
            this._DataGridPage.Padding = new System.Windows.Forms.Padding(3);
            this._DataGridPage.Size = new System.Drawing.Size(680, 443);
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
            this._FiguredDataGridView.Size = new System.Drawing.Size(674, 437);
            this._FiguredDataGridView.TabIndex = 0;
            // 
            // _FeaturesPage
            // 
            this._FeaturesPage.Controls.Add(this._FeaturesTabControl);
            this._FeaturesPage.Location = new System.Drawing.Point(4, 34);
            this._FeaturesPage.Name = "_FeaturesPage";
            this._FeaturesPage.Padding = new System.Windows.Forms.Padding(3);
            this._FeaturesPage.Size = new System.Drawing.Size(680, 443);
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
            this._FeaturesTabControl.Size = new System.Drawing.Size(674, 437);
            this._FeaturesTabControl.TabIndex = 0;
            // 
            // _TemperatureFeaturesTabPage
            // 
            this._TemperatureFeaturesTabPage.Controls.Add(this.splitContainer1);
            this._TemperatureFeaturesTabPage.Location = new System.Drawing.Point(4, 26);
            this._TemperatureFeaturesTabPage.Name = "_TemperatureFeaturesTabPage";
            this._TemperatureFeaturesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._TemperatureFeaturesTabPage.Size = new System.Drawing.Size(666, 407);
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
            this.splitContainer1.Size = new System.Drawing.Size(660, 401);
            this.splitContainer1.SplitterDistance = 200;
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
            this.splitContainer2.Size = new System.Drawing.Size(660, 200);
            this.splitContainer2.SplitterDistance = 338;
            this.splitContainer2.TabIndex = 0;
            // 
            // _TempFeaturesPanel
            // 
            this._TempFeaturesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TempFeaturesPanel.Location = new System.Drawing.Point(0, 0);
            this._TempFeaturesPanel.Name = "_TempFeaturesPanel";
            this._TempFeaturesPanel.Size = new System.Drawing.Size(338, 200);
            this._TempFeaturesPanel.TabIndex = 0;
            // 
            // _SdPanel
            // 
            this._SdPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SdPanel.Location = new System.Drawing.Point(0, 0);
            this._SdPanel.Name = "_SdPanel";
            this._SdPanel.Size = new System.Drawing.Size(318, 200);
            this._SdPanel.TabIndex = 0;
            // 
            // _TempTrendPanel
            // 
            this._TempTrendPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TempTrendPanel.Location = new System.Drawing.Point(0, 0);
            this._TempTrendPanel.Name = "_TempTrendPanel";
            this._TempTrendPanel.Size = new System.Drawing.Size(660, 197);
            this._TempTrendPanel.TabIndex = 0;
            // 
            // _PlotToolStrip
            // 
            this._PlotToolStrip.CanOverflow = false;
            this._PlotToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._PlotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this._StartStripButton,
            this._StopStripButton,
            this.toolStripSeparator6,
            this._SaveStripButton,
            this._ClearDataToolStripButton,
            this._PrintStripButton,
            this._ExportStripButton,
            this.toolStripSeparator1,
            this._FilterToolStripButton,
            this.toolStripSeparator7,
            this._PhotoToolStripButton,
            this._ZoomInToolStripButton,
            this._ZoomOutToolStripButton,
            this.toolStripLabel2});
            this._PlotToolStrip.Location = new System.Drawing.Point(0, 2);
            this._PlotToolStrip.Name = "_PlotToolStrip";
            this._PlotToolStrip.Size = new System.Drawing.Size(688, 35);
            this._PlotToolStrip.TabIndex = 0;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 35);
            // 
            // _StartStripButton
            // 
            this._StartStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.start;
            this._StartStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._StartStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._StartStripButton.Name = "_StartStripButton";
            this._StartStripButton.Size = new System.Drawing.Size(28, 28);
            this._StartStripButton.Text = "开始";
            this._StartStripButton.Click += new System.EventHandler(this._StartStripButton_Click);
            // 
            // _StopStripButton
            // 
            this._StopStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StopStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.stop;
            this._StopStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._StopStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._StopStripButton.Name = "_StopStripButton";
            this._StopStripButton.Size = new System.Drawing.Size(28, 28);
            this._StopStripButton.Text = "停止";
            this._StopStripButton.Click += new System.EventHandler(this._StopStripButton_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 35);
            // 
            // _SaveStripButton
            // 
            this._SaveStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._SaveStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.save;
            this._SaveStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._SaveStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._SaveStripButton.Name = "_SaveStripButton";
            this._SaveStripButton.Size = new System.Drawing.Size(28, 28);
            this._SaveStripButton.Text = "保存";
            this._SaveStripButton.Click += new System.EventHandler(this._SaveStripButton_Click);
            // 
            // _ClearDataToolStripButton
            // 
            this._ClearDataToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ClearDataToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.clear;
            this._ClearDataToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ClearDataToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ClearDataToolStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._ClearDataToolStripButton.Name = "_ClearDataToolStripButton";
            this._ClearDataToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._ClearDataToolStripButton.Text = "清除数据";
            this._ClearDataToolStripButton.Click += new System.EventHandler(this._ClearDataToolStripButton_Click);
            // 
            // _PrintStripButton
            // 
            this._PrintStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PrintStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.print;
            this._PrintStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._PrintStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PrintStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._PrintStripButton.Name = "_PrintStripButton";
            this._PrintStripButton.Size = new System.Drawing.Size(28, 28);
            this._PrintStripButton.Text = "打印";
            // 
            // _ExportStripButton
            // 
            this._ExportStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ExportStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.export;
            this._ExportStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ExportStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ExportStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._ExportStripButton.Name = "_ExportStripButton";
            this._ExportStripButton.Size = new System.Drawing.Size(28, 28);
            this._ExportStripButton.Text = "导出到Excel";
            this._ExportStripButton.Click += new System.EventHandler(this._ExportStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // _FilterToolStripButton
            // 
            this._FilterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._FilterToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.filter;
            this._FilterToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._FilterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._FilterToolStripButton.Name = "_FilterToolStripButton";
            this._FilterToolStripButton.Size = new System.Drawing.Size(28, 32);
            this._FilterToolStripButton.Click += new System.EventHandler(this._FilterToolStripButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 35);
            // 
            // _PhotoToolStripButton
            // 
            this._PhotoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PhotoToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.photo;
            this._PhotoToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._PhotoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PhotoToolStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._PhotoToolStripButton.Name = "_PhotoToolStripButton";
            this._PhotoToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._PhotoToolStripButton.Text = "截图";
            this._PhotoToolStripButton.Click += new System.EventHandler(this._PhotoToolStripButton_Click);
            // 
            // _ZoomInToolStripButton
            // 
            this._ZoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomInToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.zoom_in;
            this._ZoomInToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomInToolStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._ZoomInToolStripButton.Name = "_ZoomInToolStripButton";
            this._ZoomInToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._ZoomInToolStripButton.Text = "放大";
            this._ZoomInToolStripButton.Click += new System.EventHandler(this._ZoomInToolStripButton_Click);
            // 
            // _ZoomOutToolStripButton
            // 
            this._ZoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomOutToolStripButton.Image = global::MeterKnife.Instruments.Properties.Resources.zoom_out;
            this._ZoomOutToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomOutToolStripButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 4);
            this._ZoomOutToolStripButton.Name = "_ZoomOutToolStripButton";
            this._ZoomOutToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._ZoomOutToolStripButton.Text = "缩小";
            this._ZoomOutToolStripButton.Click += new System.EventHandler(this._ZoomOutToolStripButton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 32);
            // 
            // DigitMultiMeterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 522);
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
            ((System.ComponentModel.ISupportInitialize)(this._LeftSplitContainer)).EndInit();
            this._LeftSplitContainer.ResumeLayout(false);
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
        protected System.Windows.Forms.ToolStrip _PlotToolStrip;
        protected System.Windows.Forms.SplitContainer _RealtimePlotSplitContainer;
        protected System.Windows.Forms.ToolStripButton _StartStripButton;
        protected System.Windows.Forms.ToolStripButton _StopStripButton;
        protected System.Windows.Forms.ToolStripButton _SaveStripButton;
        protected System.Windows.Forms.ToolStripButton _ExportStripButton;
        protected System.Windows.Forms.ToolStripButton _PhotoToolStripButton;
        protected System.Windows.Forms.ToolStripButton _ZoomInToolStripButton;
        protected System.Windows.Forms.ToolStripButton _ZoomOutToolStripButton;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.TabPage _FeaturesPage;
        private System.Windows.Forms.ToolStripButton _ClearDataToolStripButton;
        private System.Windows.Forms.TabControl _FeaturesTabControl;
        private System.Windows.Forms.TabPage _TemperatureFeaturesTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel _TempFeaturesPanel;
        private System.Windows.Forms.Panel _SdPanel;
        private System.Windows.Forms.Panel _TempTrendPanel;
        private System.Windows.Forms.DataGridView _FiguredDataGridView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripButton _PrintStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton _FilterToolStripButton;
        private MeterKnife.Common.Winforms.Controls.FiguredDataGrid _FiguredDataPropertyGrid;
    }
}