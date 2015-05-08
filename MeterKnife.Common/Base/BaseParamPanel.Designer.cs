namespace MeterKnife.Instruments.Agilent
{
    partial class BaseParamPanel
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
            this._MainPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _MainPanel
            // 
            this._MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainPanel.Location = new System.Drawing.Point(0, 0);
            this._MainPanel.Name = "_MainPanel";
            this._MainPanel.Size = new System.Drawing.Size(270, 351);
            this._MainPanel.TabIndex = 0;
            // 
            // Ag34401AParamPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._MainPanel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "Ag34401AParamPanel";
            this.Size = new System.Drawing.Size(270, 351);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel _MainPanel;

    }
}
