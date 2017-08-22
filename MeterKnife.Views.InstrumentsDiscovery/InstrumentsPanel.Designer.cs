namespace MeterKnife.Views.InstrumentsDiscovery
{
    partial class InstrumentsPanel
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
            this.instrumentsDetailCell1 = new MeterKnife.Views.InstrumentsDiscovery.InstrumentsDetailCell();
            this.instrumentsDetailCell2 = new MeterKnife.Views.InstrumentsDiscovery.InstrumentsDetailCell();
            this.SuspendLayout();
            // 
            // instrumentsDetailCell1
            // 
            this.instrumentsDetailCell1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.instrumentsDetailCell1.ConnString = "GPIB0:22:INST";
            this.instrumentsDetailCell1.DatasCount = "23";
            this.instrumentsDetailCell1.Dock = System.Windows.Forms.DockStyle.Top;
            this.instrumentsDetailCell1.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.instrumentsDetailCell1.Image = null;
            this.instrumentsDetailCell1.Information = ".......";
            this.instrumentsDetailCell1.Location = new System.Drawing.Point(5, 5);
            this.instrumentsDetailCell1.Manufacturer = "Aglient";
            this.instrumentsDetailCell1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.instrumentsDetailCell1.Model = "E36103A";
            this.instrumentsDetailCell1.Name = "instrumentsDetailCell1";
            this.instrumentsDetailCell1.Size = new System.Drawing.Size(490, 64);
            this.instrumentsDetailCell1.TabIndex = 0;
            this.instrumentsDetailCell1.UsingTime = "2017/8/22";
            // 
            // instrumentsDetailCell2
            // 
            this.instrumentsDetailCell2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.instrumentsDetailCell2.ConnString = "GPIB0:22:INST";
            this.instrumentsDetailCell2.DatasCount = "23";
            this.instrumentsDetailCell2.Dock = System.Windows.Forms.DockStyle.Top;
            this.instrumentsDetailCell2.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.instrumentsDetailCell2.Image = null;
            this.instrumentsDetailCell2.Information = ".......";
            this.instrumentsDetailCell2.Location = new System.Drawing.Point(5, 69);
            this.instrumentsDetailCell2.Manufacturer = "Aglient";
            this.instrumentsDetailCell2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.instrumentsDetailCell2.Model = "E36103A";
            this.instrumentsDetailCell2.Name = "instrumentsDetailCell2";
            this.instrumentsDetailCell2.Size = new System.Drawing.Size(490, 64);
            this.instrumentsDetailCell2.TabIndex = 1;
            this.instrumentsDetailCell2.UsingTime = "2017/8/22";
            // 
            // InstrumentsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.instrumentsDetailCell2);
            this.Controls.Add(this.instrumentsDetailCell1);
            this.Name = "InstrumentsPanel";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(500, 400);
            this.ResumeLayout(false);

        }

        #endregion

        private InstrumentsDetailCell instrumentsDetailCell1;
        private InstrumentsDetailCell instrumentsDetailCell2;
    }
}
