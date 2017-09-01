using MeterKnife.Views.InstrumentsDiscovery.Controls;

namespace MeterKnife.Views.InstrumentsDiscovery
{
    partial class InstrumentsDiscoveryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrumentsDiscoveryView));
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._LeftPanel = new System.Windows.Forms.Panel();
            this._LeftToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._LeftContentPanel = new System.Windows.Forms.Panel();
            this._LeftToolStrip = new System.Windows.Forms.ToolStrip();
            this._AddDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this._LeftPanel.SuspendLayout();
            this._LeftToolStripContainer.ContentPanel.SuspendLayout();
            this._LeftToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._LeftToolStripContainer.SuspendLayout();
            this._LeftToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._SplitContainer.Location = new System.Drawing.Point(0, 0);
            this._SplitContainer.Name = "_SplitContainer";
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._LeftPanel);
            this._SplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(1);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this._SplitContainer.Panel2.Controls.Add(this.listView1);
            this._SplitContainer.Size = new System.Drawing.Size(851, 505);
            this._SplitContainer.SplitterDistance = 400;
            this._SplitContainer.SplitterWidth = 8;
            this._SplitContainer.TabIndex = 0;
            // 
            // _LeftPanel
            // 
            this._LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LeftPanel.Controls.Add(this._LeftToolStripContainer);
            this._LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LeftPanel.Location = new System.Drawing.Point(1, 1);
            this._LeftPanel.Name = "_LeftPanel";
            this._LeftPanel.Size = new System.Drawing.Size(398, 503);
            this._LeftPanel.TabIndex = 0;
            // 
            // _LeftToolStripContainer
            // 
            this._LeftToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // _LeftToolStripContainer.ContentPanel
            // 
            this._LeftToolStripContainer.ContentPanel.Controls.Add(this._LeftContentPanel);
            this._LeftToolStripContainer.ContentPanel.Size = new System.Drawing.Size(396, 474);
            this._LeftToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LeftToolStripContainer.LeftToolStripPanelVisible = false;
            this._LeftToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._LeftToolStripContainer.Name = "_LeftToolStripContainer";
            this._LeftToolStripContainer.RightToolStripPanelVisible = false;
            this._LeftToolStripContainer.Size = new System.Drawing.Size(396, 501);
            this._LeftToolStripContainer.TabIndex = 0;
            this._LeftToolStripContainer.Text = "toolStripContainer1";
            // 
            // _LeftToolStripContainer.TopToolStripPanel
            // 
            this._LeftToolStripContainer.TopToolStripPanel.Controls.Add(this._LeftToolStrip);
            // 
            // _LeftContentPanel
            // 
            this._LeftContentPanel.AutoScroll = true;
            this._LeftContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LeftContentPanel.Location = new System.Drawing.Point(0, 0);
            this._LeftContentPanel.Name = "_LeftContentPanel";
            this._LeftContentPanel.Size = new System.Drawing.Size(396, 474);
            this._LeftContentPanel.TabIndex = 0;
            // 
            // _LeftToolStrip
            // 
            this._LeftToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._LeftToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddDropDownButton});
            this._LeftToolStrip.Location = new System.Drawing.Point(3, 0);
            this._LeftToolStrip.Name = "_LeftToolStrip";
            this._LeftToolStrip.Padding = new System.Windows.Forms.Padding(0, 3, 1, 0);
            this._LeftToolStrip.Size = new System.Drawing.Size(97, 27);
            this._LeftToolStrip.TabIndex = 0;
            // 
            // _AddDropDownButton
            // 
            this._AddDropDownButton.Image = global::MeterKnife.Views.InstrumentsDiscovery.Properties.Resources.add;
            this._AddDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddDropDownButton.Name = "_AddDropDownButton";
            this._AddDropDownButton.Size = new System.Drawing.Size(85, 21);
            this._AddDropDownButton.Text = "添加仪器";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(443, 505);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "采集时间";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "测量物";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "测量内容";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "";
            // 
            // InstrumentsDiscoveryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 505);
            this.Controls.Add(this._SplitContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "InstrumentsDiscoveryView";
            this.Text = "仪器管理";
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this._LeftPanel.ResumeLayout(false);
            this._LeftToolStripContainer.ContentPanel.ResumeLayout(false);
            this._LeftToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._LeftToolStripContainer.TopToolStripPanel.PerformLayout();
            this._LeftToolStripContainer.ResumeLayout(false);
            this._LeftToolStripContainer.PerformLayout();
            this._LeftToolStrip.ResumeLayout(false);
            this._LeftToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel _LeftPanel;
        private System.Windows.Forms.ToolStripContainer _LeftToolStripContainer;
        private System.Windows.Forms.ToolStrip _LeftToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton _AddDropDownButton;
        private System.Windows.Forms.Panel _LeftContentPanel;
    }
}