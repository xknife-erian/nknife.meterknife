namespace MeterKnife.Common.Winforms.Dialogs
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
            this.singleLine1 = new NKnife.GUI.WinForm.SingleLine();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._WifiGpibNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._LANGpibNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._USBGpibNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._UsartSwitchCheckBox = new System.Windows.Forms.CheckBox();
            this._UsartGroupBox = new System.Windows.Forms.GroupBox();
            this._Usart2NumberBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this._Usart1NumberBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this._DhcpEnableRadioButton = new System.Windows.Forms.RadioButton();
            this._DhcpDisableRadioButton = new System.Windows.Forms.RadioButton();
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
            this._RestoreDefaultButton = new System.Windows.Forms.Button();
            this._MainGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._WifiGpibNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._LANGpibNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._USBGpibNumericUpDown)).BeginInit();
            this._UsartGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._TcpNumericUpDown)).BeginInit();
            this._DhcpGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MainGroupBox
            // 
            this._MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._MainGroupBox.Controls.Add(this.singleLine1);
            this._MainGroupBox.Controls.Add(this.groupBox1);
            this._MainGroupBox.Controls.Add(this._UsartSwitchCheckBox);
            this._MainGroupBox.Controls.Add(this._UsartGroupBox);
            this._MainGroupBox.Controls.Add(this._DhcpEnableRadioButton);
            this._MainGroupBox.Controls.Add(this._DhcpDisableRadioButton);
            this._MainGroupBox.Controls.Add(this.groupBox2);
            this._MainGroupBox.Controls.Add(this._TcpNumericUpDown);
            this._MainGroupBox.Controls.Add(this.label5);
            this._MainGroupBox.Controls.Add(this._DhcpGroupBox);
            this._MainGroupBox.Location = new System.Drawing.Point(12, 13);
            this._MainGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MainGroupBox.Name = "_MainGroupBox";
            this._MainGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MainGroupBox.Size = new System.Drawing.Size(673, 371);
            this._MainGroupBox.TabIndex = 3;
            this._MainGroupBox.TabStop = false;
            // 
            // singleLine1
            // 
            this.singleLine1.Location = new System.Drawing.Point(332, 26);
            this.singleLine1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.singleLine1.Size = new System.Drawing.Size(1, 316);
            this.singleLine1.TabIndex = 12;
            this.singleLine1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._WifiGpibNumericUpDown);
            this.groupBox1.Controls.Add(this._LANGpibNumericUpDown);
            this.groupBox1.Controls.Add(this._USBGpibNumericUpDown);
            this.groupBox1.Location = new System.Drawing.Point(367, 154);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(274, 152);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指令透传时仪器GPIB地址";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(80, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "WIFI:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(86, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "LAN:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "USB:";
            // 
            // _WifiGpibNumericUpDown
            // 
            this._WifiGpibNumericUpDown.Location = new System.Drawing.Point(122, 103);
            this._WifiGpibNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._WifiGpibNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._WifiGpibNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._WifiGpibNumericUpDown.Name = "_WifiGpibNumericUpDown";
            this._WifiGpibNumericUpDown.Size = new System.Drawing.Size(76, 23);
            this._WifiGpibNumericUpDown.TabIndex = 4;
            this._WifiGpibNumericUpDown.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // _LANGpibNumericUpDown
            // 
            this._LANGpibNumericUpDown.Location = new System.Drawing.Point(122, 68);
            this._LANGpibNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._LANGpibNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._LANGpibNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._LANGpibNumericUpDown.Name = "_LANGpibNumericUpDown";
            this._LANGpibNumericUpDown.Size = new System.Drawing.Size(76, 23);
            this._LANGpibNumericUpDown.TabIndex = 3;
            this._LANGpibNumericUpDown.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // _USBGpibNumericUpDown
            // 
            this._USBGpibNumericUpDown.Location = new System.Drawing.Point(122, 34);
            this._USBGpibNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._USBGpibNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._USBGpibNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._USBGpibNumericUpDown.Name = "_USBGpibNumericUpDown";
            this._USBGpibNumericUpDown.Size = new System.Drawing.Size(76, 23);
            this._USBGpibNumericUpDown.TabIndex = 2;
            this._USBGpibNumericUpDown.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // _UsartSwitchCheckBox
            // 
            this._UsartSwitchCheckBox.AutoSize = true;
            this._UsartSwitchCheckBox.Location = new System.Drawing.Point(367, 313);
            this._UsartSwitchCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._UsartSwitchCheckBox.Name = "_UsartSwitchCheckBox";
            this._UsartSwitchCheckBox.Size = new System.Drawing.Size(113, 21);
            this._UsartSwitchCheckBox.TabIndex = 10;
            this._UsartSwitchCheckBox.Text = "转WIFI透传模式";
            this._UsartSwitchCheckBox.UseVisualStyleBackColor = true;
            // 
            // _UsartGroupBox
            // 
            this._UsartGroupBox.Controls.Add(this._Usart2NumberBox);
            this._UsartGroupBox.Controls.Add(this.label6);
            this._UsartGroupBox.Controls.Add(this._Usart1NumberBox);
            this._UsartGroupBox.Controls.Add(this.label7);
            this._UsartGroupBox.Location = new System.Drawing.Point(367, 27);
            this._UsartGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._UsartGroupBox.Name = "_UsartGroupBox";
            this._UsartGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._UsartGroupBox.Size = new System.Drawing.Size(274, 110);
            this._UsartGroupBox.TabIndex = 10;
            this._UsartGroupBox.TabStop = false;
            this._UsartGroupBox.Text = "串口设置";
            // 
            // _Usart2NumberBox
            // 
            this._Usart2NumberBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._Usart2NumberBox.FormattingEnabled = true;
            this._Usart2NumberBox.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "76800",
            "115200",
            "128000",
            "230400"});
            this._Usart2NumberBox.Location = new System.Drawing.Point(150, 65);
            this._Usart2NumberBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Usart2NumberBox.Name = "_Usart2NumberBox";
            this._Usart2NumberBox.Size = new System.Drawing.Size(101, 25);
            this._Usart2NumberBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "USB串口波特率";
            // 
            // _Usart1NumberBox
            // 
            this._Usart1NumberBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._Usart1NumberBox.FormattingEnabled = true;
            this._Usart1NumberBox.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "76800",
            "115200",
            "128000",
            "230400"});
            this._Usart1NumberBox.Location = new System.Drawing.Point(150, 30);
            this._Usart1NumberBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Usart1NumberBox.Name = "_Usart1NumberBox";
            this._Usart1NumberBox.Size = new System.Drawing.Size(101, 25);
            this._Usart1NumberBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "WIFI前置串口波特率";
            // 
            // _DhcpEnableRadioButton
            // 
            this._DhcpEnableRadioButton.AutoSize = true;
            this._DhcpEnableRadioButton.Location = new System.Drawing.Point(31, 25);
            this._DhcpEnableRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._DhcpEnableRadioButton.Name = "_DhcpEnableRadioButton";
            this._DhcpEnableRadioButton.Size = new System.Drawing.Size(83, 21);
            this._DhcpEnableRadioButton.TabIndex = 0;
            this._DhcpEnableRadioButton.Text = "启用DHCP";
            this._DhcpEnableRadioButton.UseVisualStyleBackColor = true;
            // 
            // _DhcpDisableRadioButton
            // 
            this._DhcpDisableRadioButton.AutoSize = true;
            this._DhcpDisableRadioButton.Checked = true;
            this._DhcpDisableRadioButton.Location = new System.Drawing.Point(124, 25);
            this._DhcpDisableRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._DhcpDisableRadioButton.Name = "_DhcpDisableRadioButton";
            this._DhcpDisableRadioButton.Size = new System.Drawing.Size(95, 21);
            this._DhcpDisableRadioButton.TabIndex = 1;
            this._DhcpDisableRadioButton.TabStop = true;
            this._DhcpDisableRadioButton.Text = "不启用DHCP";
            this._DhcpDisableRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._Dht11RadioButton);
            this.groupBox2.Controls.Add(this._18b20RadioButton);
            this.groupBox2.Controls.Add(this._Dht22RadioButton);
            this.groupBox2.Location = new System.Drawing.Point(28, 279);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(274, 65);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "温湿度传感器";
            // 
            // _Dht11RadioButton
            // 
            this._Dht11RadioButton.AutoSize = true;
            this._Dht11RadioButton.Location = new System.Drawing.Point(106, 27);
            this._Dht11RadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Dht11RadioButton.Name = "_Dht11RadioButton";
            this._Dht11RadioButton.Size = new System.Drawing.Size(65, 21);
            this._Dht11RadioButton.TabIndex = 1;
            this._Dht11RadioButton.TabStop = true;
            this._Dht11RadioButton.Text = "DHT11";
            this._Dht11RadioButton.UseVisualStyleBackColor = true;
            // 
            // _18b20RadioButton
            // 
            this._18b20RadioButton.AutoSize = true;
            this._18b20RadioButton.Location = new System.Drawing.Point(20, 27);
            this._18b20RadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._18b20RadioButton.Name = "_18b20RadioButton";
            this._18b20RadioButton.Size = new System.Drawing.Size(78, 21);
            this._18b20RadioButton.TabIndex = 0;
            this._18b20RadioButton.TabStop = true;
            this._18b20RadioButton.Text = "DS18B20";
            this._18b20RadioButton.UseVisualStyleBackColor = true;
            // 
            // _Dht22RadioButton
            // 
            this._Dht22RadioButton.AutoSize = true;
            this._Dht22RadioButton.Location = new System.Drawing.Point(180, 27);
            this._Dht22RadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Dht22RadioButton.Name = "_Dht22RadioButton";
            this._Dht22RadioButton.Size = new System.Drawing.Size(65, 21);
            this._Dht22RadioButton.TabIndex = 2;
            this._Dht22RadioButton.TabStop = true;
            this._Dht22RadioButton.Text = "DHT22";
            this._Dht22RadioButton.UseVisualStyleBackColor = true;
            // 
            // _TcpNumericUpDown
            // 
            this._TcpNumericUpDown.Location = new System.Drawing.Point(87, 237);
            this._TcpNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this._TcpNumericUpDown.Size = new System.Drawing.Size(76, 23);
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
            this.label5.Location = new System.Drawing.Point(27, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 17);
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
            this._DhcpGroupBox.Location = new System.Drawing.Point(28, 58);
            this._DhcpGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._DhcpGroupBox.Name = "_DhcpGroupBox";
            this._DhcpGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._DhcpGroupBox.Size = new System.Drawing.Size(274, 167);
            this._DhcpGroupBox.TabIndex = 4;
            this._DhcpGroupBox.TabStop = false;
            this._DhcpGroupBox.Text = "DHCP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 17);
            this.label8.TabIndex = 6;
            this.label8.Text = "Mac地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "IP地址";
            // 
            // _MacTextBox
            // 
            this._MacTextBox.Location = new System.Drawing.Point(80, 24);
            this._MacTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MacTextBox.Name = "_MacTextBox";
            this._MacTextBox.ReadOnly = true;
            this._MacTextBox.Size = new System.Drawing.Size(171, 23);
            this._MacTextBox.TabIndex = 2;
            // 
            // _MaskAddressControl
            // 
            this._MaskAddressControl.AllowInternalTab = false;
            this._MaskAddressControl.AutoHeight = true;
            this._MaskAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._MaskAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._MaskAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._MaskAddressControl.Location = new System.Drawing.Point(80, 129);
            this._MaskAddressControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._MaskAddressControl.MinimumSize = new System.Drawing.Size(99, 23);
            this._MaskAddressControl.Name = "_MaskAddressControl";
            this._MaskAddressControl.ReadOnly = false;
            this._MaskAddressControl.Size = new System.Drawing.Size(171, 23);
            this._MaskAddressControl.TabIndex = 5;
            this._MaskAddressControl.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
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
            this._GatwayAddressControl.Location = new System.Drawing.Point(80, 94);
            this._GatwayAddressControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._GatwayAddressControl.MinimumSize = new System.Drawing.Size(99, 23);
            this._GatwayAddressControl.Name = "_GatwayAddressControl";
            this._GatwayAddressControl.ReadOnly = false;
            this._GatwayAddressControl.Size = new System.Drawing.Size(171, 23);
            this._GatwayAddressControl.TabIndex = 4;
            this._GatwayAddressControl.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
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
            this._IpAddressControl.Location = new System.Drawing.Point(80, 59);
            this._IpAddressControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._IpAddressControl.MinimumSize = new System.Drawing.Size(99, 23);
            this._IpAddressControl.Name = "_IpAddressControl";
            this._IpAddressControl.ReadOnly = false;
            this._IpAddressControl.Size = new System.Drawing.Size(171, 23);
            this._IpAddressControl.TabIndex = 3;
            this._IpAddressControl.Text = "...";
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(482, 392);
            this._ConfirmButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(98, 39);
            this._ConfirmButton.TabIndex = 1;
            this._ConfirmButton.Text = "写入配置";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(587, 392);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(98, 39);
            this._CancelButton.TabIndex = 0;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _RestoreDefaultButton
            // 
            this._RestoreDefaultButton.Location = new System.Drawing.Point(12, 392);
            this._RestoreDefaultButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._RestoreDefaultButton.Name = "_RestoreDefaultButton";
            this._RestoreDefaultButton.Size = new System.Drawing.Size(107, 39);
            this._RestoreDefaultButton.TabIndex = 4;
            this._RestoreDefaultButton.Text = "还原默认";
            this._RestoreDefaultButton.UseVisualStyleBackColor = true;
            this._RestoreDefaultButton.Click += new System.EventHandler(this._RestoreDefaultButton_Click);
            // 
            // CareParameterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 441);
            this.Controls.Add(this._RestoreDefaultButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._MainGroupBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(137, 183);
            this.Name = "CareParameterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Care参数设置";
            this._MainGroupBox.ResumeLayout(false);
            this._MainGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._WifiGpibNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._LANGpibNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._USBGpibNumericUpDown)).EndInit();
            this._UsartGroupBox.ResumeLayout(false);
            this._UsartGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._TcpNumericUpDown)).EndInit();
            this._DhcpGroupBox.ResumeLayout(false);
            this._DhcpGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _MainGroupBox;
        private System.Windows.Forms.GroupBox _DhcpGroupBox;
        private System.Windows.Forms.RadioButton _DhcpEnableRadioButton;
        private System.Windows.Forms.RadioButton _DhcpDisableRadioButton;
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
        private System.Windows.Forms.NumericUpDown _USBGpibNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton _Dht11RadioButton;
        private System.Windows.Forms.RadioButton _18b20RadioButton;
        private System.Windows.Forms.RadioButton _Dht22RadioButton;
        private System.Windows.Forms.NumericUpDown _TcpNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox _UsartGroupBox;
        private System.Windows.Forms.CheckBox _UsartSwitchCheckBox;
        private System.Windows.Forms.Button _RestoreDefaultButton;
        private NKnife.GUI.WinForm.SingleLine singleLine1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _WifiGpibNumericUpDown;
        private System.Windows.Forms.NumericUpDown _LANGpibNumericUpDown;
        private System.Windows.Forms.ComboBox _Usart2NumberBox;
        private System.Windows.Forms.ComboBox _Usart1NumberBox;
    }
}