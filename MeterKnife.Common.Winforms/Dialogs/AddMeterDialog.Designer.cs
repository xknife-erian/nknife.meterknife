namespace MeterKnife.Common.Winforms.Dialogs
{
    partial class AddMeterDialog
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
            this._GroupBox = new System.Windows.Forms.GroupBox();
            this._NumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._AdvantestRadiobox = new System.Windows.Forms.RadioButton();
            this._Fluke8840Radiobox = new System.Windows.Forms.RadioButton();
            this._HP3478RadioBox = new System.Windows.Forms.RadioButton();
            this._StandardScpiRadiobox = new System.Windows.Forms.RadioButton();
            this._MeterTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._MeterBrandComboBox = new System.Windows.Forms.ComboBox();
            this._MeterTypeComboBox = new System.Windows.Forms.ComboBox();
            this._AutoFindMeterCheckbox = new System.Windows.Forms.CheckBox();
            this._GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumberBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this._MeterTypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _GroupBox
            // 
            this._GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GroupBox.Controls.Add(this._NumberBox);
            this._GroupBox.Controls.Add(this.label1);
            this._GroupBox.Location = new System.Drawing.Point(14, 17);
            this._GroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._GroupBox.Name = "_GroupBox";
            this._GroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._GroupBox.Size = new System.Drawing.Size(329, 80);
            this._GroupBox.TabIndex = 0;
            this._GroupBox.TabStop = false;
            // 
            // _NumberBox
            // 
            this._NumberBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this._NumberBox.Location = new System.Drawing.Point(127, 27);
            this._NumberBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._NumberBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._NumberBox.Name = "_NumberBox";
            this._NumberBox.Size = new System.Drawing.Size(168, 25);
            this._NumberBox.TabIndex = 0;
            this._NumberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._NumberBox.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "GPIB Address:";
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(246, 345);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(97, 36);
            this._CancelButton.TabIndex = 5;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(142, 345);
            this._AcceptButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(97, 36);
            this._AcceptButton.TabIndex = 4;
            this._AcceptButton.Text = "确认";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._AdvantestRadiobox);
            this.groupBox1.Controls.Add(this._Fluke8840Radiobox);
            this.groupBox1.Controls.Add(this._HP3478RadioBox);
            this.groupBox1.Controls.Add(this._StandardScpiRadiobox);
            this.groupBox1.Location = new System.Drawing.Point(14, 104);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(329, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "语言选择";
            // 
            // _AdvantestRadiobox
            // 
            this._AdvantestRadiobox.AutoSize = true;
            this._AdvantestRadiobox.Enabled = false;
            this._AdvantestRadiobox.Location = new System.Drawing.Point(173, 64);
            this._AdvantestRadiobox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AdvantestRadiobox.Name = "_AdvantestRadiobox";
            this._AdvantestRadiobox.Size = new System.Drawing.Size(83, 21);
            this._AdvantestRadiobox.TabIndex = 3;
            this._AdvantestRadiobox.Text = "Advantest";
            this._AdvantestRadiobox.UseVisualStyleBackColor = true;
            // 
            // _Fluke8840Radiobox
            // 
            this._Fluke8840Radiobox.AutoSize = true;
            this._Fluke8840Radiobox.Enabled = false;
            this._Fluke8840Radiobox.Location = new System.Drawing.Point(29, 64);
            this._Fluke8840Radiobox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Fluke8840Radiobox.Name = "_Fluke8840Radiobox";
            this._Fluke8840Radiobox.Size = new System.Drawing.Size(117, 21);
            this._Fluke8840Radiobox.TabIndex = 2;
            this._Fluke8840Radiobox.Text = "Fluke8840/8842";
            this._Fluke8840Radiobox.UseVisualStyleBackColor = true;
            // 
            // _HP3478RadioBox
            // 
            this._HP3478RadioBox.AutoSize = true;
            this._HP3478RadioBox.Enabled = false;
            this._HP3478RadioBox.Location = new System.Drawing.Point(173, 38);
            this._HP3478RadioBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._HP3478RadioBox.Name = "_HP3478RadioBox";
            this._HP3478RadioBox.Size = new System.Drawing.Size(70, 21);
            this._HP3478RadioBox.TabIndex = 1;
            this._HP3478RadioBox.Text = "HP3478";
            this._HP3478RadioBox.UseVisualStyleBackColor = true;
            // 
            // _StandardScpiRadiobox
            // 
            this._StandardScpiRadiobox.AutoSize = true;
            this._StandardScpiRadiobox.Checked = true;
            this._StandardScpiRadiobox.Location = new System.Drawing.Point(29, 38);
            this._StandardScpiRadiobox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._StandardScpiRadiobox.Name = "_StandardScpiRadiobox";
            this._StandardScpiRadiobox.Size = new System.Drawing.Size(76, 21);
            this._StandardScpiRadiobox.TabIndex = 0;
            this._StandardScpiRadiobox.TabStop = true;
            this._StandardScpiRadiobox.Text = "标准SCPI";
            this._StandardScpiRadiobox.UseVisualStyleBackColor = true;
            // 
            // _MeterTypeGroupBox
            // 
            this._MeterTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._MeterTypeGroupBox.Controls.Add(this.label3);
            this._MeterTypeGroupBox.Controls.Add(this.label2);
            this._MeterTypeGroupBox.Controls.Add(this._MeterBrandComboBox);
            this._MeterTypeGroupBox.Controls.Add(this._MeterTypeComboBox);
            this._MeterTypeGroupBox.Location = new System.Drawing.Point(14, 234);
            this._MeterTypeGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MeterTypeGroupBox.Name = "_MeterTypeGroupBox";
            this._MeterTypeGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MeterTypeGroupBox.Size = new System.Drawing.Size(329, 103);
            this._MeterTypeGroupBox.TabIndex = 2;
            this._MeterTypeGroupBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "型号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "品牌:";
            // 
            // _MeterBrandComboBox
            // 
            this._MeterBrandComboBox.FormattingEnabled = true;
            this._MeterBrandComboBox.Location = new System.Drawing.Point(13, 56);
            this._MeterBrandComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MeterBrandComboBox.Name = "_MeterBrandComboBox";
            this._MeterBrandComboBox.Size = new System.Drawing.Size(142, 25);
            this._MeterBrandComboBox.TabIndex = 0;
            // 
            // _MeterTypeComboBox
            // 
            this._MeterTypeComboBox.FormattingEnabled = true;
            this._MeterTypeComboBox.Location = new System.Drawing.Point(162, 56);
            this._MeterTypeComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MeterTypeComboBox.Name = "_MeterTypeComboBox";
            this._MeterTypeComboBox.Size = new System.Drawing.Size(157, 25);
            this._MeterTypeComboBox.TabIndex = 1;
            // 
            // _AutoFindMeterCheckbox
            // 
            this._AutoFindMeterCheckbox.AutoSize = true;
            this._AutoFindMeterCheckbox.Location = new System.Drawing.Point(27, 233);
            this._AutoFindMeterCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AutoFindMeterCheckbox.Name = "_AutoFindMeterCheckbox";
            this._AutoFindMeterCheckbox.Size = new System.Drawing.Size(123, 21);
            this._AutoFindMeterCheckbox.TabIndex = 3;
            this._AutoFindMeterCheckbox.Text = "自动确认仪器类型";
            this._AutoFindMeterCheckbox.UseVisualStyleBackColor = true;
            // 
            // AddMeterDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(357, 394);
            this.Controls.Add(this._AutoFindMeterCheckbox);
            this.Controls.Add(this._MeterTypeGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._GroupBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddMeterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加仪器";
            this._GroupBox.ResumeLayout(false);
            this._GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumberBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._MeterTypeGroupBox.ResumeLayout(false);
            this._MeterTypeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _GroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        protected System.Windows.Forms.NumericUpDown _NumberBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _AdvantestRadiobox;
        private System.Windows.Forms.RadioButton _Fluke8840Radiobox;
        private System.Windows.Forms.RadioButton _HP3478RadioBox;
        private System.Windows.Forms.RadioButton _StandardScpiRadiobox;
        protected System.Windows.Forms.GroupBox _MeterTypeGroupBox;
        protected System.Windows.Forms.ComboBox _MeterTypeComboBox;
        protected System.Windows.Forms.CheckBox _AutoFindMeterCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.ComboBox _MeterBrandComboBox;
    }
}