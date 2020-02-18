namespace NKnife.MeterKnife.Workbench.Options
{
    partial class PlotOptionPanel
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
            this._YSpaceTrackBar = new System.Windows.Forms.TrackBar();
            this._Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._YSpaceTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _YSpaceTrackBar
            // 
            this._YSpaceTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._YSpaceTrackBar.Location = new System.Drawing.Point(23, 47);
            this._YSpaceTrackBar.Minimum = 1;
            this._YSpaceTrackBar.Name = "_YSpaceTrackBar";
            this._YSpaceTrackBar.Size = new System.Drawing.Size(436, 45);
            this._YSpaceTrackBar.TabIndex = 0;
            this._YSpaceTrackBar.Value = 5;
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Location = new System.Drawing.Point(20, 27);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(296, 17);
            this._Label1.TabIndex = 1;
            this._Label1.Text = "数据折线图上下方留白,空间（1级最多，10级最少）：";
            // 
            // PlotOptionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Label1);
            this.Controls.Add(this._YSpaceTrackBar);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PlotOptionPanel";
            this.Size = new System.Drawing.Size(480, 320);
            ((System.ComponentModel.ISupportInitialize)(this._YSpaceTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar _YSpaceTrackBar;
        private System.Windows.Forms.Label _Label1;
    }
}
