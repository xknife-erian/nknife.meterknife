namespace MerterKnife.Common.Winforms.Dialogs
{
    partial class FilterSettingDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this._InStatisticalCheckBox = new System.Windows.Forms.CheckBox();
            this._IsSaveCheckBox = new System.Windows.Forms.CheckBox();
            this._NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._AcceptButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._InStatisticalCheckBox);
            this.groupBox1.Controls.Add(this._IsSaveCheckBox);
            this.groupBox1.Controls.Add(this._NumericUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "剔除突变异值倍数设定";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(61, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "• 当设为\"0\"时，过滤功能关闭。";
            // 
            // _InStatisticalCheckBox
            // 
            this._InStatisticalCheckBox.AutoSize = true;
            this._InStatisticalCheckBox.Location = new System.Drawing.Point(64, 128);
            this._InStatisticalCheckBox.Name = "_InStatisticalCheckBox";
            this._InStatisticalCheckBox.Size = new System.Drawing.Size(134, 17);
            this._InStatisticalCheckBox.TabIndex = 4;
            this._InStatisticalCheckBox.Text = "异常值是否参与统计";
            this._InStatisticalCheckBox.UseVisualStyleBackColor = true;
            // 
            // _IsSaveCheckBox
            // 
            this._IsSaveCheckBox.AutoSize = true;
            this._IsSaveCheckBox.Location = new System.Drawing.Point(64, 109);
            this._IsSaveCheckBox.Name = "_IsSaveCheckBox";
            this._IsSaveCheckBox.Size = new System.Drawing.Size(110, 17);
            this._IsSaveCheckBox.TabIndex = 3;
            this._IsSaveCheckBox.Text = "异常值是否记录";
            this._IsSaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // _NumericUpDown
            // 
            this._NumericUpDown.Location = new System.Drawing.Point(64, 31);
            this._NumericUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this._NumericUpDown.Name = "_NumericUpDown";
            this._NumericUpDown.Size = new System.Drawing.Size(61, 21);
            this._NumericUpDown.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(61, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "• 当采集值与前值的峰-峰值的绝对值大于前50个值的平均差到设定的倍数时，该采集值将会被过滤。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "倍数:";
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(215, 181);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(75, 28);
            this._AcceptButton.TabIndex = 1;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(296, 181);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 28);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // FilterSettingDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(383, 219);
            this.ControlBox = false;
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterSettingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "异值剔除策略";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox _InStatisticalCheckBox;
        private System.Windows.Forms.CheckBox _IsSaveCheckBox;
        private System.Windows.Forms.NumericUpDown _NumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}