namespace MeterKnife.Common.Winforms.Dialogs
{
    partial class InterfaceSelectorDialog
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
            this._SerialRadioButton = new System.Windows.Forms.RadioButton();
            this._LanRadioButton = new System.Windows.Forms.RadioButton();
            this._SerialComboBox = new System.Windows.Forms.ComboBox();
            this._IpAddressControl = new NKnife.GUI.WinForm.IPAddressControl.IpAddressControl();
            this._AcceptButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._PortNumberBox = new System.Windows.Forms.ComboBox();
            this._RefreshButton = new System.Windows.Forms.Button();
            this._PortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._CloseButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PortNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _SerialRadioButton
            // 
            this._SerialRadioButton.AutoSize = true;
            this._SerialRadioButton.Location = new System.Drawing.Point(17, 28);
            this._SerialRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SerialRadioButton.Name = "_SerialRadioButton";
            this._SerialRadioButton.Size = new System.Drawing.Size(50, 21);
            this._SerialRadioButton.TabIndex = 0;
            this._SerialRadioButton.Text = "串口";
            this._SerialRadioButton.UseVisualStyleBackColor = true;
            // 
            // _LanRadioButton
            // 
            this._LanRadioButton.AutoSize = true;
            this._LanRadioButton.Location = new System.Drawing.Point(17, 62);
            this._LanRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._LanRadioButton.Name = "_LanRadioButton";
            this._LanRadioButton.Size = new System.Drawing.Size(50, 21);
            this._LanRadioButton.TabIndex = 3;
            this._LanRadioButton.TabStop = true;
            this._LanRadioButton.Text = "网络";
            this._LanRadioButton.UseVisualStyleBackColor = true;
            // 
            // _SerialComboBox
            // 
            this._SerialComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SerialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SerialComboBox.FormattingEnabled = true;
            this._SerialComboBox.Location = new System.Drawing.Point(82, 27);
            this._SerialComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SerialComboBox.Name = "_SerialComboBox";
            this._SerialComboBox.Size = new System.Drawing.Size(98, 25);
            this._SerialComboBox.TabIndex = 1;
            // 
            // _IpAddressControl
            // 
            this._IpAddressControl.AllowInternalTab = false;
            this._IpAddressControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._IpAddressControl.AutoHeight = true;
            this._IpAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._IpAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._IpAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._IpAddressControl.Location = new System.Drawing.Point(82, 61);
            this._IpAddressControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._IpAddressControl.MinimumSize = new System.Drawing.Size(99, 23);
            this._IpAddressControl.Name = "_IpAddressControl";
            this._IpAddressControl.ReadOnly = false;
            this._IpAddressControl.Size = new System.Drawing.Size(174, 23);
            this._IpAddressControl.TabIndex = 4;
            this._IpAddressControl.Text = "...";
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(280, 128);
            this._AcceptButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(98, 35);
            this._AcceptButton.TabIndex = 0;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._PortNumberBox);
            this.groupBox1.Controls.Add(this._RefreshButton);
            this.groupBox1.Controls.Add(this._PortNumericUpDown);
            this.groupBox1.Controls.Add(this._SerialRadioButton);
            this.groupBox1.Controls.Add(this._LanRadioButton);
            this.groupBox1.Controls.Add(this._SerialComboBox);
            this.groupBox1.Controls.Add(this._IpAddressControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(366, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // _PortNumberBox
            // 
            this._PortNumberBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._PortNumberBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._PortNumberBox.FormattingEnabled = true;
            this._PortNumberBox.Items.AddRange(new object[] {
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
            this._PortNumberBox.Location = new System.Drawing.Point(188, 27);
            this._PortNumberBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._PortNumberBox.Name = "_PortNumberBox";
            this._PortNumberBox.Size = new System.Drawing.Size(68, 25);
            this._PortNumberBox.TabIndex = 2;
            // 
            // _RefreshButton
            // 
            this._RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._RefreshButton.Location = new System.Drawing.Point(264, 26);
            this._RefreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._RefreshButton.Name = "_RefreshButton";
            this._RefreshButton.Size = new System.Drawing.Size(77, 27);
            this._RefreshButton.TabIndex = 6;
            this._RefreshButton.Text = "刷新";
            this._RefreshButton.UseVisualStyleBackColor = true;
            this._RefreshButton.Click += new System.EventHandler(this._RefreshButton_Click);
            // 
            // _PortNumericUpDown
            // 
            this._PortNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._PortNumericUpDown.Location = new System.Drawing.Point(264, 61);
            this._PortNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._PortNumericUpDown.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this._PortNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._PortNumericUpDown.Name = "_PortNumericUpDown";
            this._PortNumericUpDown.Size = new System.Drawing.Size(77, 23);
            this._PortNumericUpDown.TabIndex = 5;
            this._PortNumericUpDown.Value = new decimal(new int[] {
            5025,
            0,
            0,
            0});
            // 
            // _CloseButton
            // 
            this._CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._CloseButton.Location = new System.Drawing.Point(12, 128);
            this._CloseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CloseButton.Name = "_CloseButton";
            this._CloseButton.Size = new System.Drawing.Size(96, 35);
            this._CloseButton.TabIndex = 3;
            this._CloseButton.Text = "关闭";
            this._CloseButton.UseVisualStyleBackColor = true;
            this._CloseButton.Click += new System.EventHandler(this._CloseButton_Click);
            // 
            // InterfaceSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CloseButton;
            this.ClientSize = new System.Drawing.Size(392, 181);
            this.ControlBox = false;
            this.Controls.Add(this._CloseButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._AcceptButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(137, 183);
            this.Name = "InterfaceSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "选择Care所在的位置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PortNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton _SerialRadioButton;
        private System.Windows.Forms.RadioButton _LanRadioButton;
        private System.Windows.Forms.ComboBox _SerialComboBox;
        private NKnife.GUI.WinForm.IPAddressControl.IpAddressControl _IpAddressControl;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown _PortNumericUpDown;
        private System.Windows.Forms.Button _RefreshButton;
        private System.Windows.Forms.Button _CloseButton;
        private System.Windows.Forms.ComboBox _PortNumberBox;
    }
}