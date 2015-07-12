﻿namespace MeterKnife.Common.Controls
{
    partial class CustomerScpiCommandPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup17 = new System.Windows.Forms.ListViewGroup("初始指令集", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup18 = new System.Windows.Forms.ListViewGroup("采集指令集", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "INIT?",
            "1000"}, -1);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "READ?",
            "2000"}, -1);
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._LoadButton = new System.Windows.Forms.ToolStripButton();
            this._SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._DeleteButton = new System.Windows.Forms.ToolStripButton();
            this._EditButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._DownButton = new System.Windows.Forms.ToolStripButton();
            this._UpButton = new System.Windows.Forms.ToolStripButton();
            this._ListView = new System.Windows.Forms.ListView();
            this._NumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._CommandHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._TimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this._AddInitButton = new System.Windows.Forms.ToolStripMenuItem();
            this._AddCollectButton = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LoadButton,
            this._SaveButton,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this._DeleteButton,
            this._EditButton,
            this.toolStripSeparator2,
            this._DownButton,
            this._UpButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(251, 25);
            this._ToolStrip.TabIndex = 0;
            // 
            // _LoadButton
            // 
            this._LoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._LoadButton.Image = global::MeterKnife.Common.Properties.Resources.download;
            this._LoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._LoadButton.Name = "_LoadButton";
            this._LoadButton.Size = new System.Drawing.Size(23, 22);
            this._LoadButton.Text = "toolStripButton1";
            this._LoadButton.Click += new System.EventHandler(this._LoadButton_Click);
            // 
            // _SaveButton
            // 
            this._SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._SaveButton.Image = global::MeterKnife.Common.Properties.Resources.upload;
            this._SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveButton.Name = "_SaveButton";
            this._SaveButton.Size = new System.Drawing.Size(23, 22);
            this._SaveButton.Text = "toolStripButton2";
            this._SaveButton.Click += new System.EventHandler(this._SaveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _DeleteButton
            // 
            this._DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteButton.Image = global::MeterKnife.Common.Properties.Resources.delete;
            this._DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteButton.Name = "_DeleteButton";
            this._DeleteButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteButton.Text = "toolStripButton3";
            this._DeleteButton.Click += new System.EventHandler(this._DeleteButton_Click);
            // 
            // _EditButton
            // 
            this._EditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._EditButton.Image = global::MeterKnife.Common.Properties.Resources.edit;
            this._EditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditButton.Name = "_EditButton";
            this._EditButton.Size = new System.Drawing.Size(23, 22);
            this._EditButton.Text = "toolStripButton4";
            this._EditButton.Click += new System.EventHandler(this._EditButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _DownButton
            // 
            this._DownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DownButton.Image = global::MeterKnife.Common.Properties.Resources.down;
            this._DownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DownButton.Name = "_DownButton";
            this._DownButton.Size = new System.Drawing.Size(23, 22);
            this._DownButton.Text = "toolStripButton5";
            this._DownButton.Click += new System.EventHandler(this._DownButton_Click);
            // 
            // _UpButton
            // 
            this._UpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._UpButton.Image = global::MeterKnife.Common.Properties.Resources.up;
            this._UpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._UpButton.Name = "_UpButton";
            this._UpButton.Size = new System.Drawing.Size(23, 22);
            this._UpButton.Text = "toolStripButton6";
            this._UpButton.Click += new System.EventHandler(this._UpButton_Click);
            // 
            // _ListView
            // 
            this._ListView.CheckBoxes = true;
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._NumHeader,
            this._CommandHeader,
            this._TimeHeader});
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup17.Header = "初始指令集";
            listViewGroup17.Name = "INIT";
            listViewGroup18.Header = "采集指令集";
            listViewGroup18.Name = "COLLECT";
            this._ListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup17,
            listViewGroup18});
            listViewItem17.Checked = true;
            listViewItem17.Group = listViewGroup17;
            listViewItem17.StateImageIndex = 1;
            listViewItem18.Checked = true;
            listViewItem18.Group = listViewGroup18;
            listViewItem18.StateImageIndex = 1;
            this._ListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem17,
            listViewItem18});
            this._ListView.Location = new System.Drawing.Point(0, 25);
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(251, 246);
            this._ListView.TabIndex = 1;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // _NumHeader
            // 
            this._NumHeader.Text = "";
            this._NumHeader.Width = 20;
            // 
            // _CommandHeader
            // 
            this._CommandHeader.Text = "指令";
            this._CommandHeader.Width = 160;
            // 
            // _TimeHeader
            // 
            this._TimeHeader.Text = "时长";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddInitButton,
            this._AddCollectButton});
            this.toolStripSplitButton1.Image = global::MeterKnife.Common.Properties.Resources.add;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // _AddInitButton
            // 
            this._AddInitButton.Name = "_AddInitButton";
            this._AddInitButton.Size = new System.Drawing.Size(152, 22);
            this._AddInitButton.Text = "初始指令";
            this._AddInitButton.Click += new System.EventHandler(this._AddInitButton_Click);
            // 
            // _AddCollectButton
            // 
            this._AddCollectButton.Name = "_AddCollectButton";
            this._AddCollectButton.Size = new System.Drawing.Size(152, 22);
            this._AddCollectButton.Text = "采集指令";
            this._AddCollectButton.Click += new System.EventHandler(this._AddCollectButton_Click);
            // 
            // CustomerScpiCommandPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListView);
            this.Controls.Add(this._ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "CustomerScpiCommandPanel";
            this.Size = new System.Drawing.Size(251, 271);
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.ColumnHeader _NumHeader;
        private System.Windows.Forms.ColumnHeader _CommandHeader;
        private System.Windows.Forms.ColumnHeader _TimeHeader;
        private System.Windows.Forms.ToolStripButton _LoadButton;
        private System.Windows.Forms.ToolStripButton _SaveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _DeleteButton;
        private System.Windows.Forms.ToolStripButton _EditButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _DownButton;
        private System.Windows.Forms.ToolStripButton _UpButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem _AddInitButton;
        private System.Windows.Forms.ToolStripMenuItem _AddCollectButton;
    }
}
