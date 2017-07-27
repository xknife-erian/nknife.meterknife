using System.Windows.Forms;
using OxyPlot.WindowsForms;

namespace MeterKnife.Views.Measures
{
    partial class MeasureView
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
            this._MeasurePlotPage = new System.Windows.Forms.TabPage();
            this._PlotView = new OxyPlot.WindowsForms.PlotView();
            this._TabControl = new System.Windows.Forms.TabControl();
            this._MeasureDataPage = new System.Windows.Forms.TabPage();
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._OriginalToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ZoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._TimeZoomToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ValueRangeZoomToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._ThemeToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._DefaultThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自定义主题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._MeasurePlotPage.SuspendLayout();
            this._TabControl.SuspendLayout();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MeasurePlotPage
            // 
            this._MeasurePlotPage.BackColor = System.Drawing.Color.MidnightBlue;
            this._MeasurePlotPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._MeasurePlotPage.Controls.Add(this._PlotView);
            this._MeasurePlotPage.Location = new System.Drawing.Point(4, 4);
            this._MeasurePlotPage.Name = "_MeasurePlotPage";
            this._MeasurePlotPage.Padding = new System.Windows.Forms.Padding(3);
            this._MeasurePlotPage.Size = new System.Drawing.Size(616, 377);
            this._MeasurePlotPage.TabIndex = 0;
            this._MeasurePlotPage.Text = "实时测量";
            // 
            // _PlotView
            // 
            this._PlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PlotView.Location = new System.Drawing.Point(3, 3);
            this._PlotView.Margin = new System.Windows.Forms.Padding(1);
            this._PlotView.Name = "_PlotView";
            this._PlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this._PlotView.Size = new System.Drawing.Size(608, 369);
            this._PlotView.TabIndex = 1;
            this._PlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this._PlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this._PlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // _TabControl
            // 
            this._TabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this._TabControl.Controls.Add(this._MeasurePlotPage);
            this._TabControl.Controls.Add(this._MeasureDataPage);
            this._TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TabControl.ItemSize = new System.Drawing.Size(60, 22);
            this._TabControl.Location = new System.Drawing.Point(0, 0);
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(624, 407);
            this._TabControl.TabIndex = 3;
            // 
            // _MeasureDataPage
            // 
            this._MeasureDataPage.Location = new System.Drawing.Point(4, 4);
            this._MeasureDataPage.Name = "_MeasureDataPage";
            this._MeasureDataPage.Padding = new System.Windows.Forms.Padding(3);
            this._MeasureDataPage.Size = new System.Drawing.Size(616, 377);
            this._MeasureDataPage.TabIndex = 1;
            this._MeasureDataPage.Text = "数据";
            this._MeasureDataPage.UseVisualStyleBackColor = true;
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this._TabControl);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(624, 407);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(624, 440);
            this._ToolStripContainer.TabIndex = 4;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._ToolStrip);
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OriginalToolStripButton,
            this.toolStripSeparator2,
            this._ZoomInToolStripButton,
            this._ZoomOutToolStripButton,
            this.toolStripSeparator3,
            this._TimeZoomToolStripButton,
            this._ValueRangeZoomToolStripButton,
            this.toolStripSeparator1,
            this._ThemeToolStripDropDownButton});
            this._ToolStrip.Location = new System.Drawing.Point(3, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(207, 33);
            this._ToolStrip.TabIndex = 0;
            // 
            // _OriginalToolStripButton
            // 
            this._OriginalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._OriginalToolStripButton.Image = global::MeterKnife.Views.Measures.Properties.Resources.original;
            this._OriginalToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._OriginalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OriginalToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._OriginalToolStripButton.Name = "_OriginalToolStripButton";
            this._OriginalToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._OriginalToolStripButton.Text = "恢复";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // _ZoomInToolStripButton
            // 
            this._ZoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomInToolStripButton.Image = global::MeterKnife.Views.Measures.Properties.Resources.zoom_in;
            this._ZoomInToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomInToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ZoomInToolStripButton.Name = "_ZoomInToolStripButton";
            this._ZoomInToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._ZoomInToolStripButton.Text = "图表放大";
            // 
            // _ZoomOutToolStripButton
            // 
            this._ZoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomOutToolStripButton.Image = global::MeterKnife.Views.Measures.Properties.Resources.zoom_out;
            this._ZoomOutToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomOutToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ZoomOutToolStripButton.Name = "_ZoomOutToolStripButton";
            this._ZoomOutToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._ZoomOutToolStripButton.Text = "图表缩小";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // _TimeZoomToolStripButton
            // 
            this._TimeZoomToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._TimeZoomToolStripButton.Image = global::MeterKnife.Views.Measures.Properties.Resources.horizontal_zoom;
            this._TimeZoomToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._TimeZoomToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._TimeZoomToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._TimeZoomToolStripButton.Name = "_TimeZoomToolStripButton";
            this._TimeZoomToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._TimeZoomToolStripButton.Text = "横轴放大";
            // 
            // _ValueRangeZoomToolStripButton
            // 
            this._ValueRangeZoomToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ValueRangeZoomToolStripButton.Image = global::MeterKnife.Views.Measures.Properties.Resources.vertical_zoom;
            this._ValueRangeZoomToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ValueRangeZoomToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ValueRangeZoomToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ValueRangeZoomToolStripButton.Name = "_ValueRangeZoomToolStripButton";
            this._ValueRangeZoomToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._ValueRangeZoomToolStripButton.Text = "纵轴放大";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // _ThemeToolStripDropDownButton
            // 
            this._ThemeToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ThemeToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._DefaultThemeToolStripMenuItem,
            this.自定义主题ToolStripMenuItem});
            this._ThemeToolStripDropDownButton.Image = global::MeterKnife.Views.Measures.Properties.Resources.theme;
            this._ThemeToolStripDropDownButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ThemeToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ThemeToolStripDropDownButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ThemeToolStripDropDownButton.Name = "_ThemeToolStripDropDownButton";
            this._ThemeToolStripDropDownButton.Size = new System.Drawing.Size(37, 28);
            this._ThemeToolStripDropDownButton.Text = "图表主题";
            // 
            // _DefaultThemeToolStripMenuItem
            // 
            this._DefaultThemeToolStripMenuItem.Name = "_DefaultThemeToolStripMenuItem";
            this._DefaultThemeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._DefaultThemeToolStripMenuItem.Text = "默认主题";
            // 
            // 自定义主题ToolStripMenuItem
            // 
            this.自定义主题ToolStripMenuItem.Name = "自定义主题ToolStripMenuItem";
            this.自定义主题ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.自定义主题ToolStripMenuItem.Text = "自定义主题";
            // 
            // MeasureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 440);
            this.Controls.Add(this._ToolStripContainer);
            this.Name = "MeasureView";
            this.Text = "MeasureView";
            this._MeasurePlotPage.ResumeLayout(false);
            this._TabControl.ResumeLayout(false);
            this._ToolStripContainer.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TabPage _MeasurePlotPage;
        private PlotView _PlotView;
        private TabControl _TabControl;
        private ToolStripContainer _ToolStripContainer;
        private ToolStrip _ToolStrip;
        private ToolStripButton _OriginalToolStripButton;
        private ToolStripButton _ZoomInToolStripButton;
        private ToolStripButton _ZoomOutToolStripButton;
        private ToolStripButton _TimeZoomToolStripButton;
        private ToolStripButton _ValueRangeZoomToolStripButton;
        private TabPage _MeasureDataPage;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton _ThemeToolStripDropDownButton;
        private ToolStripMenuItem _DefaultThemeToolStripMenuItem;
        private ToolStripMenuItem 自定义主题ToolStripMenuItem;
    }
}