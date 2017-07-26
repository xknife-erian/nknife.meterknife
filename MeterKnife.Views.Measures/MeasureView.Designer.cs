﻿using System.Windows.Forms;
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
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
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
            this._MeasurePlotPage.BackColor = System.Drawing.Color.DarkGray;
            this._MeasurePlotPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._MeasurePlotPage.Controls.Add(this._PlotView);
            this._MeasurePlotPage.Location = new System.Drawing.Point(4, 4);
            this._MeasurePlotPage.Name = "_MeasurePlotPage";
            this._MeasurePlotPage.Padding = new System.Windows.Forms.Padding(3);
            this._MeasurePlotPage.Size = new System.Drawing.Size(616, 385);
            this._MeasurePlotPage.TabIndex = 0;
            this._MeasurePlotPage.Text = "实时测量";
            // 
            // _PlotView
            // 
            this._PlotView.BackColor = System.Drawing.Color.DarkGray;
            this._PlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PlotView.Location = new System.Drawing.Point(3, 3);
            this._PlotView.Name = "_PlotView";
            this._PlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this._PlotView.Size = new System.Drawing.Size(608, 377);
            this._PlotView.TabIndex = 1;
            this._PlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this._PlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this._PlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // _TabControl
            // 
            this._TabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this._TabControl.Controls.Add(this._MeasurePlotPage);
            this._TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TabControl.ItemSize = new System.Drawing.Size(60, 22);
            this._TabControl.Location = new System.Drawing.Point(0, 0);
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(624, 415);
            this._TabControl.TabIndex = 3;
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this._TabControl);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(624, 415);
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
            this.toolStripButton3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton4,
            this.toolStripButton5});
            this._ToolStrip.Location = new System.Drawing.Point(3, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(127, 25);
            this._ToolStrip.TabIndex = 0;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::MeterKnife.Views.Measures.Properties.Resources.original;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MeterKnife.Views.Measures.Properties.Resources.zoom_in;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::MeterKnife.Views.Measures.Properties.Resources.zoom_out;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::MeterKnife.Views.Measures.Properties.Resources.horizontal_zoom;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::MeterKnife.Views.Measures.Properties.Resources.vertical_zoom;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
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
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
    }
}