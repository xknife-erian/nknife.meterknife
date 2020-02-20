namespace NKnife.MeterKnife.Workbench.Controls
{
    partial class ScpiDetailPanel
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
            this._ScpiNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._CommandTextBox = new System.Windows.Forms.TextBox();
            this._ScpiDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _ScpiNameTextBox
            // 
            this._ScpiNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiNameTextBox.Location = new System.Drawing.Point(14, 31);
            this._ScpiNameTextBox.Name = "_ScpiNameTextBox";
            this._ScpiNameTextBox.Size = new System.Drawing.Size(307, 23);
            this._ScpiNameTextBox.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "助记名";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "描述";
            // 
            // _CommandTextBox
            // 
            this._CommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CommandTextBox.Location = new System.Drawing.Point(14, 75);
            this._CommandTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CommandTextBox.Multiline = true;
            this._CommandTextBox.Name = "_CommandTextBox";
            this._CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._CommandTextBox.Size = new System.Drawing.Size(307, 75);
            this._CommandTextBox.TabIndex = 12;
            // 
            // _ScpiDescriptionTextBox
            // 
            this._ScpiDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiDescriptionTextBox.Location = new System.Drawing.Point(14, 182);
            this._ScpiDescriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ScpiDescriptionTextBox.Multiline = true;
            this._ScpiDescriptionTextBox.Name = "_ScpiDescriptionTextBox";
            this._ScpiDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ScpiDescriptionTextBox.Size = new System.Drawing.Size(307, 41);
            this._ScpiDescriptionTextBox.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(196, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Ctrl+Enter添加新行";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "指令";
            // 
            // ScpiDetailPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this._ScpiNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._CommandTextBox);
            this.Controls.Add(this._ScpiDescriptionTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ScpiDetailPanel";
            this.Size = new System.Drawing.Size(336, 236);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _ScpiNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _CommandTextBox;
        private System.Windows.Forms.TextBox _ScpiDescriptionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
