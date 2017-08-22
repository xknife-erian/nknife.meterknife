namespace MeterKnife.Views.InstrumentsDiscovery
{
    partial class InstrumentsDetailCell
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._PictureBox = new System.Windows.Forms.PictureBox();
            this._ModelLabel = new System.Windows.Forms.Label();
            this._ConnStringLabel = new System.Windows.Forms.Label();
            this._InformationLabel = new System.Windows.Forms.Label();
            this._ManufacturerLabel = new System.Windows.Forms.Label();
            this._DatasCountLabel = new System.Windows.Forms.Label();
            this._UsingTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _PictureBox
            // 
            this._PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._PictureBox.Location = new System.Drawing.Point(0, 0);
            this._PictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._PictureBox.Name = "_PictureBox";
            this._PictureBox.Size = new System.Drawing.Size(64, 64);
            this._PictureBox.TabIndex = 0;
            this._PictureBox.TabStop = false;
            // 
            // _ModelLabel
            // 
            this._ModelLabel.Location = new System.Drawing.Point(67, 3);
            this._ModelLabel.Name = "_ModelLabel";
            this._ModelLabel.Size = new System.Drawing.Size(127, 20);
            this._ModelLabel.TabIndex = 1;
            this._ModelLabel.Text = "E36103A";
            // 
            // _ConnStringLabel
            // 
            this._ConnStringLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ConnStringLabel.Location = new System.Drawing.Point(67, 23);
            this._ConnStringLabel.Name = "_ConnStringLabel";
            this._ConnStringLabel.Size = new System.Drawing.Size(296, 20);
            this._ConnStringLabel.TabIndex = 2;
            this._ConnStringLabel.Text = "GPIB0:22:INST";
            // 
            // _InformationLabel
            // 
            this._InformationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._InformationLabel.Location = new System.Drawing.Point(67, 43);
            this._InformationLabel.Name = "_InformationLabel";
            this._InformationLabel.Size = new System.Drawing.Size(296, 24);
            this._InformationLabel.TabIndex = 3;
            this._InformationLabel.Text = ".......";
            // 
            // _ManufacturerLabel
            // 
            this._ManufacturerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ManufacturerLabel.Location = new System.Drawing.Point(200, 3);
            this._ManufacturerLabel.Name = "_ManufacturerLabel";
            this._ManufacturerLabel.Size = new System.Drawing.Size(139, 20);
            this._ManufacturerLabel.TabIndex = 4;
            this._ManufacturerLabel.Text = "Aglient";
            // 
            // _DatasCountLabel
            // 
            this._DatasCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._DatasCountLabel.Location = new System.Drawing.Point(431, 23);
            this._DatasCountLabel.Name = "_DatasCountLabel";
            this._DatasCountLabel.Size = new System.Drawing.Size(120, 20);
            this._DatasCountLabel.TabIndex = 5;
            this._DatasCountLabel.Text = "23";
            // 
            // _UsingTimeLabel
            // 
            this._UsingTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._UsingTimeLabel.Location = new System.Drawing.Point(431, 43);
            this._UsingTimeLabel.Name = "_UsingTimeLabel";
            this._UsingTimeLabel.Size = new System.Drawing.Size(120, 24);
            this._UsingTimeLabel.TabIndex = 6;
            this._UsingTimeLabel.Text = "2017/8/22";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(366, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "数据管理:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(366, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "最后采集:";
            // 
            // InstrumentsDetailCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._UsingTimeLabel);
            this.Controls.Add(this._DatasCountLabel);
            this.Controls.Add(this._ManufacturerLabel);
            this.Controls.Add(this._InformationLabel);
            this.Controls.Add(this._ConnStringLabel);
            this.Controls.Add(this._ModelLabel);
            this.Controls.Add(this._PictureBox);
            this.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InstrumentsDetailCell";
            this.Size = new System.Drawing.Size(554, 64);
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _PictureBox;
        private System.Windows.Forms.Label _ModelLabel;
        private System.Windows.Forms.Label _ConnStringLabel;
        private System.Windows.Forms.Label _InformationLabel;
        private System.Windows.Forms.Label _ManufacturerLabel;
        private System.Windows.Forms.Label _DatasCountLabel;
        private System.Windows.Forms.Label _UsingTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
