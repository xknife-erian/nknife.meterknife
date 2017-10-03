namespace MeterKnife.Views.Measures
{
    sealed partial class MeasureSettingDialog
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._GateWayModelComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumberBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this._MeterTypeGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _GroupBox
            // 
            this._GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GroupBox.Controls.Add(this._NumberBox);
            this._GroupBox.Controls.Add(this.label1);
            this._GroupBox.Location = new System.Drawing.Point(12, 65);
            this._GroupBox.Name = "_GroupBox";
            this._GroupBox.Size = new System.Drawing.Size(282, 61);
            this._GroupBox.TabIndex = 0;
            this._GroupBox.TabStop = false;
            // 
            // _NumberBox
            // 
            this._NumberBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this._NumberBox.Location = new System.Drawing.Point(109, 20);
            this._NumberBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._NumberBox.Name = "_NumberBox";
            this._NumberBox.Size = new System.Drawing.Size(144, 25);
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
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "GPIB Address:";
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(211, 332);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(83, 28);
            this._CancelButton.TabIndex = 5;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(122, 332);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(83, 28);
            this._AcceptButton.TabIndex = 4;
            this._AcceptButton.Text = "确认";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this._AdvantestRadiobox);
            this.groupBox1.Controls.Add(this._Fluke8840Radiobox);
            this.groupBox1.Controls.Add(this._HP3478RadioBox);
            this.groupBox1.Controls.Add(this._StandardScpiRadiobox);
            this.groupBox1.Location = new System.Drawing.Point(12, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 99);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指令选择";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(25, 66);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Text = "自定义指令";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // _AdvantestRadiobox
            // 
            this._AdvantestRadiobox.AutoSize = true;
            this._AdvantestRadiobox.Enabled = false;
            this._AdvantestRadiobox.Location = new System.Drawing.Point(148, 46);
            this._AdvantestRadiobox.Name = "_AdvantestRadiobox";
            this._AdvantestRadiobox.Size = new System.Drawing.Size(75, 17);
            this._AdvantestRadiobox.TabIndex = 3;
            this._AdvantestRadiobox.Text = "Advantest";
            this._AdvantestRadiobox.UseVisualStyleBackColor = true;
            // 
            // _Fluke8840Radiobox
            // 
            this._Fluke8840Radiobox.AutoSize = true;
            this._Fluke8840Radiobox.Enabled = false;
            this._Fluke8840Radiobox.Location = new System.Drawing.Point(25, 46);
            this._Fluke8840Radiobox.Name = "_Fluke8840Radiobox";
            this._Fluke8840Radiobox.Size = new System.Drawing.Size(102, 17);
            this._Fluke8840Radiobox.TabIndex = 2;
            this._Fluke8840Radiobox.Text = "Fluke8840/8842";
            this._Fluke8840Radiobox.UseVisualStyleBackColor = true;
            // 
            // _HP3478RadioBox
            // 
            this._HP3478RadioBox.AutoSize = true;
            this._HP3478RadioBox.Enabled = false;
            this._HP3478RadioBox.Location = new System.Drawing.Point(148, 26);
            this._HP3478RadioBox.Name = "_HP3478RadioBox";
            this._HP3478RadioBox.Size = new System.Drawing.Size(62, 17);
            this._HP3478RadioBox.TabIndex = 1;
            this._HP3478RadioBox.Text = "HP3478";
            this._HP3478RadioBox.UseVisualStyleBackColor = true;
            // 
            // _StandardScpiRadiobox
            // 
            this._StandardScpiRadiobox.AutoSize = true;
            this._StandardScpiRadiobox.Checked = true;
            this._StandardScpiRadiobox.Location = new System.Drawing.Point(25, 26);
            this._StandardScpiRadiobox.Name = "_StandardScpiRadiobox";
            this._StandardScpiRadiobox.Size = new System.Drawing.Size(72, 17);
            this._StandardScpiRadiobox.TabIndex = 0;
            this._StandardScpiRadiobox.TabStop = true;
            this._StandardScpiRadiobox.Text = "标准SCPI";
            this._StandardScpiRadiobox.UseVisualStyleBackColor = true;
            // 
            // _MeterTypeGroupBox
            // 
            this._MeterTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._MeterTypeGroupBox.Controls.Add(this.label3);
            this._MeterTypeGroupBox.Controls.Add(this.label2);
            this._MeterTypeGroupBox.Controls.Add(this._MeterBrandComboBox);
            this._MeterTypeGroupBox.Controls.Add(this._MeterTypeComboBox);
            this._MeterTypeGroupBox.Location = new System.Drawing.Point(12, 239);
            this._MeterTypeGroupBox.Name = "_MeterTypeGroupBox";
            this._MeterTypeGroupBox.Size = new System.Drawing.Size(282, 83);
            this._MeterTypeGroupBox.TabIndex = 2;
            this._MeterTypeGroupBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "型号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "品牌:";
            // 
            // _MeterBrandComboBox
            // 
            this._MeterBrandComboBox.FormattingEnabled = true;
            this._MeterBrandComboBox.Location = new System.Drawing.Point(11, 43);
            this._MeterBrandComboBox.Name = "_MeterBrandComboBox";
            this._MeterBrandComboBox.Size = new System.Drawing.Size(122, 21);
            this._MeterBrandComboBox.TabIndex = 0;
            // 
            // _MeterTypeComboBox
            // 
            this._MeterTypeComboBox.FormattingEnabled = true;
            this._MeterTypeComboBox.Location = new System.Drawing.Point(139, 43);
            this._MeterTypeComboBox.Name = "_MeterTypeComboBox";
            this._MeterTypeComboBox.Size = new System.Drawing.Size(135, 21);
            this._MeterTypeComboBox.TabIndex = 1;
            // 
            // _AutoFindMeterCheckbox
            // 
            this._AutoFindMeterCheckbox.AutoSize = true;
            this._AutoFindMeterCheckbox.Location = new System.Drawing.Point(23, 238);
            this._AutoFindMeterCheckbox.Name = "_AutoFindMeterCheckbox";
            this._AutoFindMeterCheckbox.Size = new System.Drawing.Size(122, 17);
            this._AutoFindMeterCheckbox.TabIndex = 3;
            this._AutoFindMeterCheckbox.Text = "自动确认仪器类型";
            this._AutoFindMeterCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._GateWayModelComboBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 56);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // _GateWayModelComboBox
            // 
            this._GateWayModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._GateWayModelComboBox.FormattingEnabled = true;
            this._GateWayModelComboBox.Items.AddRange(new object[] {
            "Care One",
            "Care Two",
            "Agilent 82357A",
            "Agilent 82357B",
            "SerialPort",
            "TCP/IP"});
            this._GateWayModelComboBox.Location = new System.Drawing.Point(110, 23);
            this._GateWayModelComboBox.Name = "_GateWayModelComboBox";
            this._GateWayModelComboBox.Size = new System.Drawing.Size(143, 21);
            this._GateWayModelComboBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "连接模式:";
            // 
            // MeasureSettingDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(306, 376);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._AutoFindMeterCheckbox);
            this.Controls.Add(this._MeterTypeGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._GroupBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MeasureSettingDialog";
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _GroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.NumericUpDown _NumberBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _AdvantestRadiobox;
        private System.Windows.Forms.RadioButton _Fluke8840Radiobox;
        private System.Windows.Forms.RadioButton _HP3478RadioBox;
        private System.Windows.Forms.RadioButton _StandardScpiRadiobox;
        private System.Windows.Forms.GroupBox _MeterTypeGroupBox;
        private System.Windows.Forms.ComboBox _MeterTypeComboBox;
        private System.Windows.Forms.CheckBox _AutoFindMeterCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _MeterBrandComboBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox _GateWayModelComboBox;
        private System.Windows.Forms.Label label4;
    }
}