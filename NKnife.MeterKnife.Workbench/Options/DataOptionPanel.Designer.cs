namespace NKnife.MeterKnife.Workbench.Options
{
    partial class DataOptionPanel
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
            this._dataSavePathLabel = new System.Windows.Forms.Label();
            this._dataSavePathTextBox = new System.Windows.Forms.TextBox();
            this._brownPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _dataSavePathLabel
            // 
            this._dataSavePathLabel.AutoSize = true;
            this._dataSavePathLabel.Location = new System.Drawing.Point(14, 15);
            this._dataSavePathLabel.Name = "_dataSavePathLabel";
            this._dataSavePathLabel.Size = new System.Drawing.Size(83, 17);
            this._dataSavePathLabel.TabIndex = 0;
            this._dataSavePathLabel.Text = "数据基础路径:";
            // 
            // _dataSavePathTextBox
            // 
            this._dataSavePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dataSavePathTextBox.Location = new System.Drawing.Point(103, 12);
            this._dataSavePathTextBox.Name = "_dataSavePathTextBox";
            this._dataSavePathTextBox.ReadOnly = true;
            this._dataSavePathTextBox.Size = new System.Drawing.Size(289, 23);
            this._dataSavePathTextBox.TabIndex = 1;
            // 
            // _brownPathButton
            // 
            this._brownPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._brownPathButton.Location = new System.Drawing.Point(398, 10);
            this._brownPathButton.Name = "_brownPathButton";
            this._brownPathButton.Size = new System.Drawing.Size(55, 26);
            this._brownPathButton.TabIndex = 2;
            this._brownPathButton.Text = "浏览";
            this._brownPathButton.UseVisualStyleBackColor = true;
            // 
            // DataOptionPanel
            // 
            this.AutoScroll = true;
            this.Controls.Add(this._brownPathButton);
            this.Controls.Add(this._dataSavePathTextBox);
            this.Controls.Add(this._dataSavePathLabel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DataOptionPanel";
            this.Size = new System.Drawing.Size(480, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _dataSavePathLabel;
        private System.Windows.Forms.TextBox _dataSavePathTextBox;
        private System.Windows.Forms.Button _brownPathButton;
    }
}
