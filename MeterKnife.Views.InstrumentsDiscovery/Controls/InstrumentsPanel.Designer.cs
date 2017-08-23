namespace MeterKnife.Views.InstrumentsDiscovery.Controls
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
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this.instrumentsDetailCell1 = new MeterKnife.Views.InstrumentsDiscovery.Controls.InstrumentsDetailCell();
            this.instrumentsDetailCell2 = new MeterKnife.Views.InstrumentsDiscovery.Controls.InstrumentsDetailCell();
            this.instrumentsDetailCell3 = new MeterKnife.Views.InstrumentsDiscovery.Controls.InstrumentsDetailCell();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(400, 25);
            this._ToolStrip.TabIndex = 3;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // instrumentsDetailCell1
            // 
            this.instrumentsDetailCell1.BackColor = System.Drawing.Color.Transparent;
            this.instrumentsDetailCell1.ConnString = "GPIB0:22:INST";
            this.instrumentsDetailCell1.DatasCount = "23";
            this.instrumentsDetailCell1.Dock = System.Windows.Forms.DockStyle.Top;
            this.instrumentsDetailCell1.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.instrumentsDetailCell1.Image = null;
            this.instrumentsDetailCell1.Information = ".......";
            this.instrumentsDetailCell1.Location = new System.Drawing.Point(0, 25);
            this.instrumentsDetailCell1.Manufacturer = "Aglient";
            this.instrumentsDetailCell1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.instrumentsDetailCell1.Model = "E36103A";
            this.instrumentsDetailCell1.Name = "instrumentsDetailCell1";
            this.instrumentsDetailCell1.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.instrumentsDetailCell1.Size = new System.Drawing.Size(400, 70);
            this.instrumentsDetailCell1.TabIndex = 4;
            this.instrumentsDetailCell1.UsingTime = "2017/8/22";
            // 
            // instrumentsDetailCell2
            // 
            this.instrumentsDetailCell2.BackColor = System.Drawing.Color.Transparent;
            this.instrumentsDetailCell2.ConnString = "GPIB0:22:INST";
            this.instrumentsDetailCell2.DatasCount = "23";
            this.instrumentsDetailCell2.Dock = System.Windows.Forms.DockStyle.Top;
            this.instrumentsDetailCell2.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.instrumentsDetailCell2.Image = null;
            this.instrumentsDetailCell2.Information = ".......";
            this.instrumentsDetailCell2.Location = new System.Drawing.Point(0, 95);
            this.instrumentsDetailCell2.Manufacturer = "Aglient";
            this.instrumentsDetailCell2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.instrumentsDetailCell2.Model = "E36103A";
            this.instrumentsDetailCell2.Name = "instrumentsDetailCell2";
            this.instrumentsDetailCell2.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.instrumentsDetailCell2.Size = new System.Drawing.Size(400, 70);
            this.instrumentsDetailCell2.TabIndex = 5;
            this.instrumentsDetailCell2.UsingTime = "2017/8/22";
            // 
            // instrumentsDetailCell3
            // 
            this.instrumentsDetailCell3.BackColor = System.Drawing.Color.Transparent;
            this.instrumentsDetailCell3.ConnString = "GPIB0:22:INST";
            this.instrumentsDetailCell3.DatasCount = "23";
            this.instrumentsDetailCell3.Dock = System.Windows.Forms.DockStyle.Top;
            this.instrumentsDetailCell3.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.instrumentsDetailCell3.Image = null;
            this.instrumentsDetailCell3.Information = ".......";
            this.instrumentsDetailCell3.Location = new System.Drawing.Point(0, 165);
            this.instrumentsDetailCell3.Manufacturer = "Aglient";
            this.instrumentsDetailCell3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.instrumentsDetailCell3.Model = "E36103A";
            this.instrumentsDetailCell3.Name = "instrumentsDetailCell3";
            this.instrumentsDetailCell3.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.instrumentsDetailCell3.Size = new System.Drawing.Size(400, 70);
            this.instrumentsDetailCell3.TabIndex = 6;
            this.instrumentsDetailCell3.UsingTime = "2017/8/22";
            // 
            // InstrumentsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.instrumentsDetailCell3);
            this.Controls.Add(this.instrumentsDetailCell2);
            this.Controls.Add(this.instrumentsDetailCell1);
            this.Controls.Add(this._ToolStrip);
            this.Name = "InstrumentsPanel";
            this.Size = new System.Drawing.Size(400, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private InstrumentsDetailCell instrumentsDetailCell1;
        private InstrumentsDetailCell instrumentsDetailCell2;
        private InstrumentsDetailCell instrumentsDetailCell3;
    }
}
