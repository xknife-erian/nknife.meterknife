namespace MeterKnife.Workbench.Dialogs
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
            this._PortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._RefreshButton = new System.Windows.Forms.Button();
            this._CloseButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PortNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _SerialRadioButton
            // 
            this._SerialRadioButton.AutoSize = true;
            this._SerialRadioButton.Location = new System.Drawing.Point(15, 27);
            this._SerialRadioButton.Name = "_SerialRadioButton";
            this._SerialRadioButton.Size = new System.Drawing.Size(49, 17);
            this._SerialRadioButton.TabIndex = 0;
            this._SerialRadioButton.Text = "串口";
            this._SerialRadioButton.UseVisualStyleBackColor = true;
            // 
            // _LanRadioButton
            // 
            this._LanRadioButton.AutoSize = true;
            this._LanRadioButton.Location = new System.Drawing.Point(15, 53);
            this._LanRadioButton.Name = "_LanRadioButton";
            this._LanRadioButton.Size = new System.Drawing.Size(49, 17);
            this._LanRadioButton.TabIndex = 1;
            this._LanRadioButton.TabStop = true;
            this._LanRadioButton.Text = "网络";
            this._LanRadioButton.UseVisualStyleBackColor = true;
            // 
            // _SerialComboBox
            // 
            this._SerialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SerialComboBox.FormattingEnabled = true;
            this._SerialComboBox.Location = new System.Drawing.Point(70, 26);
            this._SerialComboBox.Name = "_SerialComboBox";
            this._SerialComboBox.Size = new System.Drawing.Size(75, 21);
            this._SerialComboBox.TabIndex = 2;
            // 
            // _IpAddressControl
            // 
            this._IpAddressControl.AllowInternalTab = false;
            this._IpAddressControl.AutoHeight = true;
            this._IpAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._IpAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._IpAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._IpAddressControl.Location = new System.Drawing.Point(70, 52);
            this._IpAddressControl.MinimumSize = new System.Drawing.Size(90, 21);
            this._IpAddressControl.Name = "_IpAddressControl";
            this._IpAddressControl.ReadOnly = false;
            this._IpAddressControl.Size = new System.Drawing.Size(149, 21);
            this._IpAddressControl.TabIndex = 3;
            this._IpAddressControl.Text = "...";
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Location = new System.Drawing.Point(256, 121);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(84, 27);
            this._AcceptButton.TabIndex = 5;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._RefreshButton);
            this.groupBox1.Controls.Add(this._PortNumericUpDown);
            this.groupBox1.Controls.Add(this._SerialRadioButton);
            this.groupBox1.Controls.Add(this._LanRadioButton);
            this.groupBox1.Controls.Add(this._SerialComboBox);
            this.groupBox1.Controls.Add(this._IpAddressControl);
            this.groupBox1.Location = new System.Drawing.Point(23, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 96);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // _PortNumericUpDown
            // 
            this._PortNumericUpDown.Location = new System.Drawing.Point(226, 52);
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
            this._PortNumericUpDown.Size = new System.Drawing.Size(66, 21);
            this._PortNumericUpDown.TabIndex = 4;
            this._PortNumericUpDown.Value = new decimal(new int[] {
            6035,
            0,
            0,
            0});
            // 
            // _RefreshButton
            // 
            this._RefreshButton.Location = new System.Drawing.Point(151, 25);
            this._RefreshButton.Name = "_RefreshButton";
            this._RefreshButton.Size = new System.Drawing.Size(39, 23);
            this._RefreshButton.TabIndex = 5;
            this._RefreshButton.Text = "刷新";
            this._RefreshButton.UseVisualStyleBackColor = true;
            this._RefreshButton.Click += new System.EventHandler(this._RefreshButton_Click);
            // 
            // _CloseButton
            // 
            this._CloseButton.Location = new System.Drawing.Point(23, 121);
            this._CloseButton.Name = "_CloseButton";
            this._CloseButton.Size = new System.Drawing.Size(84, 27);
            this._CloseButton.TabIndex = 8;
            this._CloseButton.Text = "关闭";
            this._CloseButton.UseVisualStyleBackColor = true;
            this._CloseButton.Click += new System.EventHandler(this._CloseButton_Click);
            // 
            // InterfaceSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CloseButton;
            this.ClientSize = new System.Drawing.Size(367, 169);
            this.ControlBox = false;
            this.Controls.Add(this._CloseButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._AcceptButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(120, 149);
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
    }
}