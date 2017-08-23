namespace MeterKnife.Views.InstrumentsDiscovery.Controls
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
            this._ModelLabel = new System.Windows.Forms.Label();
            this._ConnStringLabel = new System.Windows.Forms.Label();
            this._InformationLabel = new System.Windows.Forms.Label();
            this._ManufacturerLabel = new System.Windows.Forms.Label();
            this._DatasCountLabel = new System.Windows.Forms.Label();
            this._UsingTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._PictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _ModelLabel
            // 
            this._ModelLabel.AutoSize = true;
            this._ModelLabel.Location = new System.Drawing.Point(63, 0);
            this._ModelLabel.Name = "_ModelLabel";
            this._ModelLabel.Size = new System.Drawing.Size(58, 17);
            this._ModelLabel.TabIndex = 1;
            this._ModelLabel.Text = "E36103A";
            this._ModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ConnStringLabel
            // 
            this._ConnStringLabel.AutoSize = true;
            this._ConnStringLabel.Location = new System.Drawing.Point(63, 18);
            this._ConnStringLabel.Name = "_ConnStringLabel";
            this._ConnStringLabel.Size = new System.Drawing.Size(91, 17);
            this._ConnStringLabel.TabIndex = 2;
            this._ConnStringLabel.Text = "GPIB0:22:INST";
            this._ConnStringLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _InformationLabel
            // 
            this._InformationLabel.AutoSize = true;
            this._InformationLabel.Location = new System.Drawing.Point(63, 36);
            this._InformationLabel.Name = "_InformationLabel";
            this._InformationLabel.Size = new System.Drawing.Size(29, 17);
            this._InformationLabel.TabIndex = 3;
            this._InformationLabel.Text = ".......";
            this._InformationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ManufacturerLabel
            // 
            this._ManufacturerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ManufacturerLabel.Location = new System.Drawing.Point(233, 0);
            this._ManufacturerLabel.Name = "_ManufacturerLabel";
            this._ManufacturerLabel.Size = new System.Drawing.Size(94, 18);
            this._ManufacturerLabel.TabIndex = 4;
            this._ManufacturerLabel.Text = "Aglient";
            this._ManufacturerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _DatasCountLabel
            // 
            this._DatasCountLabel.Location = new System.Drawing.Point(233, 36);
            this._DatasCountLabel.Name = "_DatasCountLabel";
            this._DatasCountLabel.Size = new System.Drawing.Size(70, 20);
            this._DatasCountLabel.TabIndex = 5;
            this._DatasCountLabel.Text = "23";
            this._DatasCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _UsingTimeLabel
            // 
            this._UsingTimeLabel.Location = new System.Drawing.Point(233, 18);
            this._UsingTimeLabel.Name = "_UsingTimeLabel";
            this._UsingTimeLabel.Size = new System.Drawing.Size(70, 18);
            this._UsingTimeLabel.TabIndex = 6;
            this._UsingTimeLabel.Text = "2017/8/22";
            this._UsingTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(168, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "数据管理:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(168, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "最后采集:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this._PictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 64);
            this.panel1.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._ModelLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._DatasCountLabel, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this._ConnStringLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._UsingTimeLabel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this._InformationLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._ManufacturerLabel, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(68, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(330, 56);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(3, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "描述:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(3, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "连接字:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(168, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "厂商:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "型号:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _PictureBox
            // 
            this._PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._PictureBox.BackgroundImage = global::MeterKnife.Views.InstrumentsDiscovery.Properties.Resources.meter64;
            this._PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._PictureBox.Location = new System.Drawing.Point(3, 2);
            this._PictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._PictureBox.Name = "_PictureBox";
            this._PictureBox.Size = new System.Drawing.Size(60, 60);
            this._PictureBox.TabIndex = 0;
            this._PictureBox.TabStop = false;
            // 
            // InstrumentsDetailCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InstrumentsDetailCell";
            this.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.Size = new System.Drawing.Size(404, 70);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
