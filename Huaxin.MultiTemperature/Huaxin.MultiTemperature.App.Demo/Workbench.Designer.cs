namespace Huaxin.MultiTemperature.App.Demo
{
    partial class Workbench
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workbench));
            this._SerialPortComboBox = new System.Windows.Forms.ComboBox();
            this._OpenPortButton = new System.Windows.Forms.Button();
            this._ClosePortButton = new System.Windows.Forms.Button();
            this._PathCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this._AllSelectButton = new System.Windows.Forms.Button();
            this._ClearSelectButton = new System.Windows.Forms.Button();
            this._FuncGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this._IntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._CommandTextBox = new System.Windows.Forms.TextBox();
            this._StartButton = new System.Windows.Forms.Button();
            this._PlotsTabControl = new System.Windows.Forms.TabControl();
            this._LoggerPanel = new System.Windows.Forms.Panel();
            this._BuildPdfPrintButton = new System.Windows.Forms.Button();
            this._FuncGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._IntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _SerialPortComboBox
            // 
            this._SerialPortComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._SerialPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SerialPortComboBox.FormattingEnabled = true;
            this._SerialPortComboBox.Location = new System.Drawing.Point(875, 13);
            this._SerialPortComboBox.Name = "_SerialPortComboBox";
            this._SerialPortComboBox.Size = new System.Drawing.Size(121, 21);
            this._SerialPortComboBox.TabIndex = 0;
            // 
            // _OpenPortButton
            // 
            this._OpenPortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._OpenPortButton.Enabled = false;
            this._OpenPortButton.Location = new System.Drawing.Point(875, 40);
            this._OpenPortButton.Name = "_OpenPortButton";
            this._OpenPortButton.Size = new System.Drawing.Size(55, 25);
            this._OpenPortButton.TabIndex = 1;
            this._OpenPortButton.Text = "Open";
            this._OpenPortButton.UseVisualStyleBackColor = true;
            // 
            // _ClosePortButton
            // 
            this._ClosePortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ClosePortButton.Enabled = false;
            this._ClosePortButton.Location = new System.Drawing.Point(941, 40);
            this._ClosePortButton.Name = "_ClosePortButton";
            this._ClosePortButton.Size = new System.Drawing.Size(55, 25);
            this._ClosePortButton.TabIndex = 2;
            this._ClosePortButton.Text = "Close";
            this._ClosePortButton.UseVisualStyleBackColor = true;
            // 
            // _PathCheckedListBox
            // 
            this._PathCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._PathCheckedListBox.FormattingEnabled = true;
            this._PathCheckedListBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this._PathCheckedListBox.Location = new System.Drawing.Point(875, 81);
            this._PathCheckedListBox.Name = "_PathCheckedListBox";
            this._PathCheckedListBox.Size = new System.Drawing.Size(121, 340);
            this._PathCheckedListBox.TabIndex = 3;
            // 
            // _AllSelectButton
            // 
            this._AllSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._AllSelectButton.Enabled = false;
            this._AllSelectButton.Location = new System.Drawing.Point(941, 427);
            this._AllSelectButton.Name = "_AllSelectButton";
            this._AllSelectButton.Size = new System.Drawing.Size(55, 25);
            this._AllSelectButton.TabIndex = 5;
            this._AllSelectButton.Text = "All";
            this._AllSelectButton.UseVisualStyleBackColor = true;
            // 
            // _ClearSelectButton
            // 
            this._ClearSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ClearSelectButton.Enabled = false;
            this._ClearSelectButton.Location = new System.Drawing.Point(875, 427);
            this._ClearSelectButton.Name = "_ClearSelectButton";
            this._ClearSelectButton.Size = new System.Drawing.Size(55, 25);
            this._ClearSelectButton.TabIndex = 4;
            this._ClearSelectButton.Text = "Clear";
            this._ClearSelectButton.UseVisualStyleBackColor = true;
            // 
            // _FuncGroupBox
            // 
            this._FuncGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._FuncGroupBox.Controls.Add(this._BuildPdfPrintButton);
            this._FuncGroupBox.Controls.Add(this.label1);
            this._FuncGroupBox.Controls.Add(this._IntervalNumericUpDown);
            this._FuncGroupBox.Controls.Add(this._CommandTextBox);
            this._FuncGroupBox.Controls.Add(this._StartButton);
            this._FuncGroupBox.Location = new System.Drawing.Point(489, 13);
            this._FuncGroupBox.Name = "_FuncGroupBox";
            this._FuncGroupBox.Size = new System.Drawing.Size(376, 439);
            this._FuncGroupBox.TabIndex = 7;
            this._FuncGroupBox.TabStop = false;
            this._FuncGroupBox.Text = "配置与功能";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "间隔:";
            // 
            // _IntervalNumericUpDown
            // 
            this._IntervalNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._IntervalNumericUpDown.Location = new System.Drawing.Point(51, 84);
            this._IntervalNumericUpDown.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this._IntervalNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._IntervalNumericUpDown.Name = "_IntervalNumericUpDown";
            this._IntervalNumericUpDown.Size = new System.Drawing.Size(63, 21);
            this._IntervalNumericUpDown.TabIndex = 2;
            this._IntervalNumericUpDown.Value = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            // 
            // _CommandTextBox
            // 
            this._CommandTextBox.Font = new System.Drawing.Font("Tahoma", 12F);
            this._CommandTextBox.Location = new System.Drawing.Point(19, 27);
            this._CommandTextBox.Multiline = true;
            this._CommandTextBox.Name = "_CommandTextBox";
            this._CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._CommandTextBox.Size = new System.Drawing.Size(336, 53);
            this._CommandTextBox.TabIndex = 1;
            this._CommandTextBox.Text = "68 78 79 66 01 04 00 01 00 XX CC DD";
            // 
            // _StartButton
            // 
            this._StartButton.Location = new System.Drawing.Point(281, 84);
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(75, 23);
            this._StartButton.TabIndex = 0;
            this._StartButton.Text = "开始采集";
            this._StartButton.UseVisualStyleBackColor = true;
            // 
            // _PlotsTabControl
            // 
            this._PlotsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._PlotsTabControl.Location = new System.Drawing.Point(13, 459);
            this._PlotsTabControl.Name = "_PlotsTabControl";
            this._PlotsTabControl.SelectedIndex = 0;
            this._PlotsTabControl.Size = new System.Drawing.Size(983, 318);
            this._PlotsTabControl.TabIndex = 8;
            // 
            // _LoggerPanel
            // 
            this._LoggerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._LoggerPanel.Location = new System.Drawing.Point(13, 13);
            this._LoggerPanel.Name = "_LoggerPanel";
            this._LoggerPanel.Size = new System.Drawing.Size(470, 440);
            this._LoggerPanel.TabIndex = 9;
            // 
            // _BuildPdfPrintButton
            // 
            this._BuildPdfPrintButton.Location = new System.Drawing.Point(19, 400);
            this._BuildPdfPrintButton.Name = "_BuildPdfPrintButton";
            this._BuildPdfPrintButton.Size = new System.Drawing.Size(337, 33);
            this._BuildPdfPrintButton.TabIndex = 4;
            this._BuildPdfPrintButton.Text = "生成打印报表";
            this._BuildPdfPrintButton.UseVisualStyleBackColor = true;
            this._BuildPdfPrintButton.Click += new System.EventHandler(this._BuildPdfPrintButton_Click);
            // 
            // Workbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 789);
            this.Controls.Add(this._LoggerPanel);
            this.Controls.Add(this._PlotsTabControl);
            this.Controls.Add(this._FuncGroupBox);
            this.Controls.Add(this._AllSelectButton);
            this.Controls.Add(this._ClearSelectButton);
            this.Controls.Add(this._PathCheckedListBox);
            this.Controls.Add(this._ClosePortButton);
            this.Controls.Add(this._OpenPortButton);
            this.Controls.Add(this._SerialPortComboBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Workbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Huaxin-MultiTemperature-Demo";
            this._FuncGroupBox.ResumeLayout(false);
            this._FuncGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._IntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _SerialPortComboBox;
        private System.Windows.Forms.Button _OpenPortButton;
        private System.Windows.Forms.Button _ClosePortButton;
        private System.Windows.Forms.CheckedListBox _PathCheckedListBox;
        private System.Windows.Forms.Button _AllSelectButton;
        private System.Windows.Forms.Button _ClearSelectButton;
        private System.Windows.Forms.GroupBox _FuncGroupBox;
        private System.Windows.Forms.TabControl _PlotsTabControl;
        private System.Windows.Forms.Panel _LoggerPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _IntervalNumericUpDown;
        private System.Windows.Forms.TextBox _CommandTextBox;
        private System.Windows.Forms.Button _StartButton;
        private System.Windows.Forms.Button _BuildPdfPrintButton;
    }
}

