namespace MeterKnife.Plots.Dialogs
{
    partial class ThemeColorItem
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
            this._ColorPanel = new System.Windows.Forms.Panel();
            this._Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ColorPanel
            // 
            this._ColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ColorPanel.BackColor = System.Drawing.Color.White;
            this._ColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ColorPanel.Location = new System.Drawing.Point(3, 3);
            this._ColorPanel.Name = "_ColorPanel";
            this._ColorPanel.Size = new System.Drawing.Size(80, 24);
            this._ColorPanel.TabIndex = 1;
            // 
            // _Button
            // 
            this._Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Button.Location = new System.Drawing.Point(89, 3);
            this._Button.Name = "_Button";
            this._Button.Size = new System.Drawing.Size(50, 25);
            this._Button.TabIndex = 2;
            this._Button.Text = "设置";
            this._Button.UseVisualStyleBackColor = true;
            // 
            // ThemeColorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Button);
            this.Controls.Add(this._ColorPanel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ThemeColorItem";
            this.Size = new System.Drawing.Size(142, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _ColorPanel;
        private System.Windows.Forms.Button _Button;
    }
}
