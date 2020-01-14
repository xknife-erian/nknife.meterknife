namespace MeterKnife.Common.Winforms.Dialogs
{
    partial class ExportFileSelectorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._FilePathTextBox = new System.Windows.Forms.TextBox();
            this._BrowserButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._CancelButton = new System.Windows.Forms.Button();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _FilePathTextBox
            // 
            this._FilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._FilePathTextBox.Location = new System.Drawing.Point(26, 34);
            this._FilePathTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._FilePathTextBox.Name = "_FilePathTextBox";
            this._FilePathTextBox.Size = new System.Drawing.Size(394, 23);
            this._FilePathTextBox.TabIndex = 0;
            // 
            // _BrowserButton
            // 
            this._BrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._BrowserButton.Location = new System.Drawing.Point(427, 30);
            this._BrowserButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._BrowserButton.Name = "_BrowserButton";
            this._BrowserButton.Size = new System.Drawing.Size(59, 33);
            this._BrowserButton.TabIndex = 1;
            this._BrowserButton.Text = "浏览";
            this._BrowserButton.UseVisualStyleBackColor = true;
            this._BrowserButton.Click += new System.EventHandler(this._BrowserButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._FilePathTextBox);
            this.groupBox1.Controls.Add(this._BrowserButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(509, 79);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设定导出文件的目录";
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(434, 100);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 34);
            this._CancelButton.TabIndex = 3;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(339, 100);
            this._ConfirmButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(87, 34);
            this._ConfirmButton.TabIndex = 4;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // ExportFileSelectorDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(533, 143);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(137, 171);
            this.Name = "ExportFileSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "导出文件";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _FilePathTextBox;
        private System.Windows.Forms.Button _BrowserButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _ConfirmButton;
    }
}