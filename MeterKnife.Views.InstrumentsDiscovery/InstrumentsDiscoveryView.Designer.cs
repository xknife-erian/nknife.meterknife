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
            this.dataListPanel1 = new MeterKnife.Views.InstrumentsDiscovery.Controls.Datas.DatasListPanel();
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
            this._SplitContainer.Panel2.Controls.Add(this.dataListPanel1);
            this._SplitContainer.Size = new System.Drawing.Size(1008, 495);
            this._SplitContainer.SplitterDistance = 460;
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
            this._LeftPanel.Size = new System.Drawing.Size(458, 493);
            this._LeftPanel.TabIndex = 0;
            // 
            // _LeftToolStripContainer
            // 
            this._LeftToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // _LeftToolStripContainer.ContentPanel
            // 
            this._LeftToolStripContainer.ContentPanel.Controls.Add(this._LeftContentPanel);
            this._LeftToolStripContainer.ContentPanel.Size = new System.Drawing.Size(456, 464);
            this._LeftToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LeftToolStripContainer.LeftToolStripPanelVisible = false;
            this._LeftToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._LeftToolStripContainer.Name = "_LeftToolStripContainer";
            this._LeftToolStripContainer.RightToolStripPanelVisible = false;
            this._LeftToolStripContainer.Size = new System.Drawing.Size(456, 491);
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
            this._LeftContentPanel.Size = new System.Drawing.Size(456, 464);
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
            // dataListPanel1
            // 
            this.dataListPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListPanel1.Location = new System.Drawing.Point(0, 0);
            this.dataListPanel1.Name = "dataListPanel1";
            this.dataListPanel1.Size = new System.Drawing.Size(540, 495);
            this.dataListPanel1.TabIndex = 0;
            // 
            // InstrumentsDiscoveryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 495);
            this.Controls.Add(this._SplitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Panel _LeftPanel;
        private System.Windows.Forms.ToolStripContainer _LeftToolStripContainer;
        private System.Windows.Forms.ToolStrip _LeftToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton _AddDropDownButton;
        private System.Windows.Forms.Panel _LeftContentPanel;
        private Controls.Datas.DatasListPanel dataListPanel1;
    }
}