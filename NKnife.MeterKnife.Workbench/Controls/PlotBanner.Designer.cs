namespace NKnife.MeterKnife.Workbench.Controls
{
    partial class PlotBanner
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
            this._LeftFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._RightLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // _LeftFlowLayoutPanel
            // 
            this._LeftFlowLayoutPanel.BackColor = System.Drawing.Color.Honeydew;
            this._LeftFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this._LeftFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._LeftFlowLayoutPanel.Name = "_LeftFlowLayoutPanel";
            this._LeftFlowLayoutPanel.Size = new System.Drawing.Size(200, 52);
            this._LeftFlowLayoutPanel.TabIndex = 0;
            // 
            // _RightLayoutPanel
            // 
            this._RightLayoutPanel.BackColor = System.Drawing.Color.Honeydew;
            this._RightLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._RightLayoutPanel.Location = new System.Drawing.Point(607, 0);
            this._RightLayoutPanel.Name = "_RightLayoutPanel";
            this._RightLayoutPanel.Size = new System.Drawing.Size(200, 52);
            this._RightLayoutPanel.TabIndex = 1;
            // 
            // PlotBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._RightLayoutPanel);
            this.Controls.Add(this._LeftFlowLayoutPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PlotBanner";
            this.Size = new System.Drawing.Size(807, 52);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _LeftFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel _RightLayoutPanel;
    }
}
