namespace NKnife.GUI.WinForm.DataSelector
{
    partial class DataItemSelector<T>
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
            this._TextBox = new System.Windows.Forms.TextBox();
            this._Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _SelectedBox
            // 
            this._TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TextBox.Location = new System.Drawing.Point(3, 3);
            this._TextBox.Name = "_SelectedBox";
            this._TextBox.ReadOnly = true;
            this._TextBox.Size = new System.Drawing.Size(262, 21);
            this._TextBox.TabIndex = 2;
            // 
            // _CompanySelectedBtn
            // 
            this._Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Button.Location = new System.Drawing.Point(266, 1);
            this._Button.Name = "_CompanySelectedBtn";
            this._Button.Size = new System.Drawing.Size(40, 25);
            this._Button.TabIndex = 3;
            this._Button.Text = "选择";
            this._Button.UseVisualStyleBackColor = true;
            // 
            // DataItemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._TextBox);
            this.Controls.Add(this._Button);
            this.Name = "DataItemSelector";
            this.Size = new System.Drawing.Size(306, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
