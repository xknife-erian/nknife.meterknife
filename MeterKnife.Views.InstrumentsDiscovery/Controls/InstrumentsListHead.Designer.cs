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
            this._Panel = new System.Windows.Forms.Panel();
            this._CountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).BeginInit();
            this._Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PictureBox
            // 
            this._PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._PictureBox.BackgroundImage = global::MeterKnife.Views.InstrumentsDiscovery.Properties.Resources.up;
            this._PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._PictureBox.Location = new System.Drawing.Point(8, 5);
            this._PictureBox.Name = "_PictureBox";
            this._PictureBox.Size = new System.Drawing.Size(26, 26);
            this._PictureBox.TabIndex = 0;
            this._PictureBox.TabStop = false;
            // 
            // _GatewayModelLabel
            // 
            this._GatewayModelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GatewayModelLabel.AutoSize = true;
            this._GatewayModelLabel.Location = new System.Drawing.Point(36, 10);
            this._GatewayModelLabel.Name = "_GatewayModelLabel";
            this._GatewayModelLabel.Size = new System.Drawing.Size(120, 13);
            this._GatewayModelLabel.TabIndex = 1;
            this._GatewayModelLabel.Text = "GatewayModelLabel";
            // 
            // _Panel
            // 
            this._Panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this._Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._Panel.Controls.Add(this._CountLabel);
            this._Panel.Controls.Add(this._GatewayModelLabel);
            this._Panel.Controls.Add(this._PictureBox);
            this._Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Panel.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._Panel.Location = new System.Drawing.Point(2, 1);
            this._Panel.Name = "_Panel";
            this._Panel.Size = new System.Drawing.Size(400, 30);
            this._Panel.TabIndex = 2;
            // 
            // _CountLabel
            // 
            this._CountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CountLabel.AutoSize = true;
            this._CountLabel.Location = new System.Drawing.Point(372, 10);
            this._CountLabel.Name = "_CountLabel";
            this._CountLabel.Size = new System.Drawing.Size(14, 13);
            this._CountLabel.TabIndex = 2;
            this._CountLabel.Text = "0";
            // 
            // InstrumentsListHead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Panel);
            this.Name = "InstrumentsListHead";
            this.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Size = new System.Drawing.Size(404, 32);
            ((System.ComponentModel.ISupportInitialize)(this._PictureBox)).EndInit();
            this._Panel.ResumeLayout(false);
            this._Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _PictureBox;
        private System.Windows.Forms.Label _GatewayModelLabel;
        private System.Windows.Forms.Panel _Panel;
        private System.Windows.Forms.Label _CountLabel;
    }
}
