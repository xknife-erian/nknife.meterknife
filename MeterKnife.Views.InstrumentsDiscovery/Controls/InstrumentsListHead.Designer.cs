namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    partial class InstrumentsListHead
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
            this._GatewayModelLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PictureBox
            // 
            this._PictureBox.BackgroundImage = global::MeterKnife.Views.InstrumentsDiscovery.Properties.Resources.up;
            this._PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._PictureBox.Location = new System.Drawing.Point(8, 2);
            this._PictureBox.Name = "_PictureBox";
            this._PictureBox.Size = new System.Drawing.Size(26, 26);
            this._PictureBox.TabIndex = 0;
            this._PictureBox.TabStop = false;
            // 
            // _GatewayModelLabel
            // 
            this._GatewayModelLabel.AutoSize = true;
            this._GatewayModelLabel.Location = new System.Drawing.Point(40, 9);
            this._GatewayModelLabel.Name = "_GatewayModelLabel";
            this._GatewayModelLabel.Size = new System.Drawing.Size(107, 12);
            this._GatewayModelLabel.TabIndex = 1;
            this._GatewayModelLabel.Text = "GatewayModelLabel";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._GatewayModelLabel);
            this.panel1.Controls.Add(this._PictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 30);
            this.panel1.TabIndex = 2;
            // 
            // InstrumentsListHead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "InstrumentsListHead";
            this.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Size = new System.Drawing.Size(404, 32);
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _PictureBox;
        private System.Windows.Forms.Label _GatewayModelLabel;
        private System.Windows.Forms.Panel panel1;
    }
}
