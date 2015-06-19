namespace MeterKnife.Workbench.Dialogs
{
    partial class CareParameterDialog
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
            this._MainGroupBox = new System.Windows.Forms.GroupBox();
            this._DhcpDisableRadioButton = new System.Windows.Forms.RadioButton();
            this._DhcpEnableRadioButton = new System.Windows.Forms.RadioButton();
            this._GpibNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._Dht11RadioButton = new System.Windows.Forms.RadioButton();
            this._18b20RadioButton = new System.Windows.Forms.RadioButton();
            this._Dht22RadioButton = new System.Windows.Forms.RadioButton();
            this._TcpNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this._DhcpGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._MacTextBox = new System.Windows.Forms.TextBox();
            this._MaskAddressControl = new NKnife.GUI.WinForm.IPAddressControl.IpAddressControl();
            this.label3 = new System.Windows.Forms.Label();
            this._GatwayAddressControl = new NKnife.GUI.WinForm.IPAddressControl.IpAddressControl();
            this.label4 = new System.Windows.Forms.Label();
            this._IpAddressControl = new NKnife.GUI.WinForm.IPAddressControl.IpAddressControl();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._Usart1NumberBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this._Usart2NumberBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this._UsartGroupBox = new System.Windows.Forms.GroupBox();
            this._UsartSwitchCheckBox = new System.Windows.Forms.CheckBox();
            this._DefaultSettingButton = new System.Windows.Forms.Button();
            this._MainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._GpibNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._TcpNumericUpDown)).BeginInit();
            this._DhcpGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._Usart1NumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._Usart2NumberBox)).BeginInit();
            this._UsartGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MainGroupBox
            // 
            this._MainGroupBox.Controls.Add(this._UsartGroupBox);
            this._MainGroupBox.Controls.Add(this._DhcpDisableRadioButton);
            this._MainGroupBox.Controls.Add(this._DhcpEnableRadioButton);
            this._MainGroupBox.Controls.Add(this._GpibNumericUpDown);
            this._MainGroupBox.Controls.Add(this.label1);
            this._MainGroupBox.Controls.Add(this.groupBox2);
            this._MainGroupBox.Controls.Add(this._TcpNumericUpDown);
            this._MainGroupBox.Controls.Add(this.label5);
            this._MainGroupBox.Controls.Add(this._DhcpGroupBox);
            this._MainGroupBox.Location = new System.Drawing.Point(12, 4);
            this._MainGroupBox.Name = "_MainGroupBox";
            this._MainGroupBox.Size = new System.Drawing.Size(284, 392);
            this._MainGroupBox.TabIndex = 3;
            this._MainGroupBox.TabStop = false;
            // 
            // _DhcpDisableRadioButton
            // 
            this._DhcpDisableRadioButton.AutoSize = true;
            this._DhcpDisableRadioButton.Location = new System.Drawing.Point(27, 19);
            this._DhcpDisableRadioButton.Name = "_DhcpDisableRadioButton";
            this._DhcpDisableRadioButton.Size = new System.Drawing.Size(76, 17);
            this._DhcpDisableRadioButton.TabIndex = 0;
            this._DhcpDisableRadioButton.TabStop = true;
            this._DhcpDisableRadioButton.Text = "启用DHCP";
            this._DhcpDisableRadioButton.UseVisualStyleBackColor = true;
            // 
            // _DhcpEnableRadioButton
            // 
            this._DhcpEnableRadioButton.AutoSize = true;
            this._DhcpEnableRadioButton.Location = new System.Drawing.Point(106, 19);
            this._DhcpEnableRadioButton.Name = "_DhcpEnableRadioButton";
            this._DhcpEnableRadioButton.Size = new System.Drawing.Size(88, 17);
            this._DhcpEnableRadioButton.TabIndex = 1;
            this._DhcpEnableRadioButton.TabStop = true;
            this._DhcpEnableRadioButton.Text = "不启用DHCP";
            this._DhcpEnableRadioButton.UseVisualStyleBackColor = true;
            // 
            // _GpibNumericUpDown
            // 
            this._GpibNumericUpDown.Location = new System.Drawing.Point(145, 356);
            this._GpibNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._GpibNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._GpibNumericUpDown.Name = "_GpibNumericUpDown";
            this._GpibNumericUpDown.Size = new System.Drawing.Size(65, 21);
            this._GpibNumericUpDown.TabIndex = 2;
            this._GpibNumericUpDown.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "透明传输时GPIB地址:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._Dht11RadioButton);
            this.groupBox2.Controls.Add(this._18b20RadioButton);
            this.groupBox2.Controls.Add(this._Dht22RadioButton);
            this.groupBox2.Location = new System.Drawing.Point(24, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "温湿度传感器";
            // 
            // _Dht11RadioButton
            // 
            this._Dht11RadioButton.AutoSize = true;
            this._Dht11RadioButton.Location = new System.Drawing.Point(91, 21);
            this._Dht11RadioButton.Name = "_Dht11RadioButton";
            this._Dht11RadioButton.Size = new System.Drawing.Size(57, 17);
            this._Dht11RadioButton.TabIndex = 1;
            this._Dht11RadioButton.TabStop = true;
            this._Dht11RadioButton.Text = "DHT11";
            this._Dht11RadioButton.UseVisualStyleBackColor = true;
            // 
            // _18b20RadioButton
            // 
            this._18b20RadioButton.AutoSize = true;
            this._18b20RadioButton.Location = new System.Drawing.Point(17, 21);
            this._18b20RadioButton.Name = "_18b20RadioButton";
            this._18b20RadioButton.Size = new System.Drawing.Size(68, 17);
            this._18b20RadioButton.TabIndex = 0;
            this._18b20RadioButton.TabStop = true;
            this._18b20RadioButton.Text = "DS18B20";
            this._18b20RadioButton.UseVisualStyleBackColor = true;
            // 
            // _Dht22RadioButton
            // 
            this._Dht22RadioButton.AutoSize = true;
            this._Dht22RadioButton.Location = new System.Drawing.Point(154, 21);
            this._Dht22RadioButton.Name = "_Dht22RadioButton";
            this._Dht22RadioButton.Size = new System.Drawing.Size(57, 17);
            this._Dht22RadioButton.TabIndex = 2;
            this._Dht22RadioButton.TabStop = true;
            this._Dht22RadioButton.Text = "DHT22";
            this._Dht22RadioButton.UseVisualStyleBackColor = true;
            // 
            // _TcpNumericUpDown
            // 
            this._TcpNumericUpDown.Location = new System.Drawing.Point(75, 176);
            this._TcpNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this._TcpNumericUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._TcpNumericUpDown.Name = "_TcpNumericUpDown";
            this._TcpNumericUpDown.Size = new System.Drawing.Size(65, 21);
            this._TcpNumericUpDown.TabIndex = 1;
            this._TcpNumericUpDown.Value = new decimal(new int[] {
            6035,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "TCP端口";
            // 
            // _DhcpGroupBox
            // 
            this._DhcpGroupBox.Controls.Add(this.label8);
            this._DhcpGroupBox.Controls.Add(this.label2);
            this._DhcpGroupBox.Controls.Add(this._MacTextBox);
            this._DhcpGroupBox.Controls.Add(this._MaskAddressControl);
            this._DhcpGroupBox.Controls.Add(this.label3);
            this._DhcpGroupBox.Controls.Add(this._GatwayAddressControl);
            this._DhcpGroupBox.Controls.Add(this.label4);
            this._DhcpGroupBox.Controls.Add(this._IpAddressControl);
            this._DhcpGroupBox.Location = new System.Drawing.Point(24, 42);
            this._DhcpGroupBox.Name = "_DhcpGroupBox";
            this._DhcpGroupBox.Size = new System.Drawing.Size(235, 128);
            this._DhcpGroupBox.TabIndex = 4;
            this._DhcpGroupBox.TabStop = false;
            this._DhcpGroupBox.Text = "DHCP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Mac地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "IP地址";
            // 
            // _MacTextBox
            // 
            this._MacTextBox.Location = new System.Drawing.Point(69, 18);
            this._MacTextBox.Name = "_MacTextBox";
            this._MacTextBox.ReadOnly = true;
            this._MacTextBox.Size = new System.Drawing.Size(147, 21);
            this._MacTextBox.TabIndex = 2;
            // 
            // _MaskAddressControl
            // 
            this._MaskAddressControl.AllowInternalTab = false;
            this._MaskAddressControl.AutoHeight = true;
            this._MaskAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._MaskAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._MaskAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._MaskAddressControl.Location = new System.Drawing.Point(69, 99);
            this._MaskAddressControl.MinimumSize = new System.Drawing.Size(90, 21);
            this._MaskAddressControl.Name = "_MaskAddressControl";
            this._MaskAddressControl.ReadOnly = false;
            this._MaskAddressControl.Size = new System.Drawing.Size(147, 21);
            this._MaskAddressControl.TabIndex = 5;
            this._MaskAddressControl.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "网关";
            // 
            // _GatwayAddressControl
            // 
            this._GatwayAddressControl.AllowInternalTab = false;
            this._GatwayAddressControl.AutoHeight = true;
            this._GatwayAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._GatwayAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._GatwayAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._GatwayAddressControl.Location = new System.Drawing.Point(69, 72);
            this._GatwayAddressControl.MinimumSize = new System.Drawing.Size(90, 21);
            this._GatwayAddressControl.Name = "_GatwayAddressControl";
            this._GatwayAddressControl.ReadOnly = false;
            this._GatwayAddressControl.Size = new System.Drawing.Size(147, 21);
            this._GatwayAddressControl.TabIndex = 4;
            this._GatwayAddressControl.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "掩码";
            // 
            // _IpAddressControl
            // 
            this._IpAddressControl.AllowInternalTab = false;
            this._IpAddressControl.AutoHeight = true;
            this._IpAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._IpAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._IpAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._IpAddressControl.Location = new System.Drawing.Point(69, 45);
            this._IpAddressControl.MinimumSize = new System.Drawing.Size(90, 21);
            this._IpAddressControl.Name = "_IpAddressControl";
            this._IpAddressControl.ReadOnly = false;
            this._IpAddressControl.Size = new System.Drawing.Size(147, 21);
            this._IpAddressControl.TabIndex = 3;
            this._IpAddressControl.Text = "...";
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Location = new System.Drawing.Point(157, 399);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(67, 30);
            this._ConfirmButton.TabIndex = 1;
            this._ConfirmButton.Text = "写入配置";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            // 
            // _CancelButton
            // 
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(229, 399);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(67, 30);
            this._CancelButton.TabIndex = 0;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _Usart1NumberBox
            // 
            this._Usart1NumberBox.Location = new System.Drawing.Point(151, 24);
            this._Usart1NumberBox.Maximum = new decimal(new int[] {
            65535000,
            0,
            0,
            0});
            this._Usart1NumberBox.Minimum = new decimal(new int[] {
            4799,
            0,
            0,
            0});
            this._Usart1NumberBox.Name = "_Usart1NumberBox";
            this._Usart1NumberBox.Size = new System.Drawing.Size(65, 21);
            this._Usart1NumberBox.TabIndex = 6;
            this._Usart1NumberBox.Value = new decimal(new int[] {
            115200,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "USB串口波特率";
            // 
            // _Usart2NumberBox
            // 
            this._Usart2NumberBox.Location = new System.Drawing.Point(151, 51);
            this._Usart2NumberBox.Maximum = new decimal(new int[] {
            65535000,
            0,
            0,
            0});
            this._Usart2NumberBox.Minimum = new decimal(new int[] {
            4799,
            0,
            0,
            0});
            this._Usart2NumberBox.Name = "_Usart2NumberBox";
            this._Usart2NumberBox.Size = new System.Drawing.Size(65, 21);
            this._Usart2NumberBox.TabIndex = 8;
            this._Usart2NumberBox.Value = new decimal(new int[] {
            115200,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "WIFI模块前置串口波特率";
            // 
            // _UsartGroupBox
            // 
            this._UsartGroupBox.Controls.Add(this._UsartSwitchCheckBox);
            this._UsartGroupBox.Controls.Add(this.label6);
            this._UsartGroupBox.Controls.Add(this.label7);
            this._UsartGroupBox.Controls.Add(this._Usart2NumberBox);
            this._UsartGroupBox.Controls.Add(this._Usart1NumberBox);
            this._UsartGroupBox.Location = new System.Drawing.Point(24, 207);
            this._UsartGroupBox.Name = "_UsartGroupBox";
            this._UsartGroupBox.Size = new System.Drawing.Size(235, 84);
            this._UsartGroupBox.TabIndex = 10;
            this._UsartGroupBox.TabStop = false;
            // 
            // _UsartSwitchCheckBox
            // 
            this._UsartSwitchCheckBox.AutoSize = true;
            this._UsartSwitchCheckBox.Location = new System.Drawing.Point(9, 1);
            this._UsartSwitchCheckBox.Name = "_UsartSwitchCheckBox";
            this._UsartSwitchCheckBox.Size = new System.Drawing.Size(98, 17);
            this._UsartSwitchCheckBox.TabIndex = 10;
            this._UsartSwitchCheckBox.Text = "串口数据互转";
            this._UsartSwitchCheckBox.UseVisualStyleBackColor = true;
            // 
            // _DefaultSettingButton
            // 
            this._DefaultSettingButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._DefaultSettingButton.Location = new System.Drawing.Point(12, 399);
            this._DefaultSettingButton.Name = "_DefaultSettingButton";
            this._DefaultSettingButton.Size = new System.Drawing.Size(67, 30);
            this._DefaultSettingButton.TabIndex = 2;
            this._DefaultSettingButton.Text = "还原默认";
            this._DefaultSettingButton.UseVisualStyleBackColor = true;
            // 
            // CareParameterDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(314, 444);
            this.Controls.Add(this._DefaultSettingButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._MainGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(120, 149);
            this.Name = "CareParameterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Care参数设置";
            this._MainGroupBox.ResumeLayout(false);
            this._MainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._GpibNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._TcpNumericUpDown)).EndInit();
            this._DhcpGroupBox.ResumeLayout(false);
            this._DhcpGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._Usart1NumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._Usart2NumberBox)).EndInit();
            this._UsartGroupBox.ResumeLayout(false);
            this._UsartGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _MainGroupBox;
        private System.Windows.Forms.GroupBox _DhcpGroupBox;
        private System.Windows.Forms.RadioButton _DhcpDisableRadioButton;
        private System.Windows.Forms.RadioButton _DhcpEnableRadioButton;
        private NKnife.GUI.WinForm.IPAddressControl.IpAddressControl _MaskAddressControl;
        private NKnife.GUI.WinForm.IPAddressControl.IpAddressControl _GatwayAddressControl;
        private NKnife.GUI.WinForm.IPAddressControl.IpAddressControl _IpAddressControl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _MacTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.NumericUpDown _GpibNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton _Dht11RadioButton;
        private System.Windows.Forms.RadioButton _18b20RadioButton;
        private System.Windows.Forms.RadioButton _Dht22RadioButton;
        private System.Windows.Forms.NumericUpDown _TcpNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown _Usart2NumberBox;
        private System.Windows.Forms.NumericUpDown _Usart1NumberBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox _UsartGroupBox;
        private System.Windows.Forms.CheckBox _UsartSwitchCheckBox;
        private System.Windows.Forms.Button _DefaultSettingButton;
    }
}