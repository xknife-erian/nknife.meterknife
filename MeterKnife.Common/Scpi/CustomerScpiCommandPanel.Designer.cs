using System.Windows.Forms;

namespace MeterKnife.Common.Scpi
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
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._LoadButton = new System.Windows.Forms.ToolStripButton();
            this._SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this._AddInitButton = new System.Windows.Forms.ToolStripMenuItem();
            this._AddCollectButton = new System.Windows.Forms.ToolStripMenuItem();
            this._DeleteButton = new System.Windows.Forms.ToolStripButton();
            this._EditButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._DownButton = new System.Windows.Forms.ToolStripButton();
            this._UpButton = new System.Windows.Forms.ToolStripButton();
            this._ListView = new System.Windows.Forms.ListView();
            this._NumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._CommandHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._TimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._StripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ToolStrip.SuspendLayout();
            this._StatusStrip.SuspendLayout();
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
            this._AddInitButton.Size = new System.Drawing.Size(124, 22);
            this._AddInitButton.Text = "初始指令";
            this._AddInitButton.Click += new System.EventHandler(this._AddInitButton_Click);
            // 
            // _AddCollectButton
            // 
            this._AddCollectButton.Name = "_AddCollectButton";
            this._AddCollectButton.Size = new System.Drawing.Size(124, 22);
            this._AddCollectButton.Text = "采集指令";
            this._AddCollectButton.Click += new System.EventHandler(this._AddCollectButton_Click);
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
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._ListView.Location = new System.Drawing.Point(0, 25);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(251, 224);
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
            this._TimeHeader.Text = "时长(ms)";
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StripLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 249);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(251, 22);
            this._StatusStrip.TabIndex = 2;
            // 
            // _StripLabel
            // 
            this._StripLabel.Name = "_StripLabel";
            this._StripLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // CustomerScpiCommandPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListView);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "CustomerScpiCommandPanel";
            this.Size = new System.Drawing.Size(251, 271);
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip _ToolStrip;
        private ListView _ListView;
        private ColumnHeader _NumHeader;
        private ColumnHeader _CommandHeader;
        private ColumnHeader _TimeHeader;
        private ToolStripButton _LoadButton;
        private ToolStripButton _SaveButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton _DeleteButton;
        private ToolStripButton _EditButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton _DownButton;
        private ToolStripButton _UpButton;
        private ToolStripDropDownButton toolStripSplitButton1;
        private ToolStripMenuItem _AddInitButton;
        private ToolStripMenuItem _AddCollectButton;
        private StatusStrip _StatusStrip;
        private ToolStripStatusLabel _StripLabel;

    }
}
