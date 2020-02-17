namespace NKnife.MeterKnife.Workbench.Dialogs.Engineerings
{
    partial class CareCommandEditorDialog
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
            this.label9 = new System.Windows.Forms.Label();
            this._ScpiDescriptionTextBox = new System.Windows.Forms.TextBox();
            this._BrLabel = new System.Windows.Forms.Label();
            this._CommandTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._HexEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this._IntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._Label4 = new System.Windows.Forms.Label();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._SlotComboBox = new System.Windows.Forms.ComboBox();
            this._DUTComboBox = new System.Windows.Forms.ComboBox();
            this._Label1 = new System.Windows.Forms.Label();
            this._Label2 = new System.Windows.Forms.Label();
            this._NewDUTButton = new System.Windows.Forms.Button();
            this._GpibNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._Label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._TimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._Label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._InfiniteLoopCheckBox = new System.Windows.Forms.CheckBox();
            this._WorkToFinishCheckBox = new System.Windows.Forms.CheckBox();
            this._LoopCountNmericUpDown = new System.Windows.Forms.NumericUpDown();
            this._Label6 = new System.Windows.Forms.Label();
            this._CommandTabControl = new System.Windows.Forms.TabControl();
            this._ScpiTabPage = new System.Windows.Forms.TabPage();
            this._ScpiNameTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this._CareTabPage = new System.Windows.Forms.TabPage();
            this._ScpiRadioButton = new System.Windows.Forms.RadioButton();
            this._CareRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this._IntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._GpibNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._TimeoutNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopCountNmericUpDown)).BeginInit();
            this._CommandTabControl.SuspendLayout();
            this._ScpiTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "描述:";
            // 
            // _ScpiDescriptionTextBox
            // 
            this._ScpiDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiDescriptionTextBox.Location = new System.Drawing.Point(85, 110);
            this._ScpiDescriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ScpiDescriptionTextBox.Multiline = true;
            this._ScpiDescriptionTextBox.Name = "_ScpiDescriptionTextBox";
            this._ScpiDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ScpiDescriptionTextBox.Size = new System.Drawing.Size(299, 41);
            this._ScpiDescriptionTextBox.TabIndex = 8;
            this._ScpiDescriptionTextBox.Text = "某某\r\n大规模";
            // 
            // _BrLabel
            // 
            this._BrLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._BrLabel.AutoSize = true;
            this._BrLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this._BrLabel.Location = new System.Drawing.Point(83, 87);
            this._BrLabel.Name = "_BrLabel";
            this._BrLabel.Size = new System.Drawing.Size(115, 17);
            this._BrLabel.TabIndex = 7;
            this._BrLabel.Text = "Ctrl+Enter添加新行";
            // 
            // _CommandTextBox
            // 
            this._CommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CommandTextBox.Location = new System.Drawing.Point(85, 47);
            this._CommandTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CommandTextBox.Multiline = true;
            this._CommandTextBox.Name = "_CommandTextBox";
            this._CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._CommandTextBox.Size = new System.Drawing.Size(230, 40);
            this._CommandTextBox.TabIndex = 0;
            this._CommandTextBox.Text = "READ?\r\nFETC?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "指令:";
            // 
            // _HexEnableCheckBox
            // 
            this._HexEnableCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._HexEnableCheckBox.AutoSize = true;
            this._HexEnableCheckBox.Location = new System.Drawing.Point(321, 46);
            this._HexEnableCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._HexEnableCheckBox.Name = "_HexEnableCheckBox";
            this._HexEnableCheckBox.Size = new System.Drawing.Size(65, 21);
            this._HexEnableCheckBox.TabIndex = 1;
            this._HexEnableCheckBox.Text = "16进制";
            this._HexEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label3.Location = new System.Drawing.Point(160, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "ms";
            // 
            // _IntervalNumericUpDown
            // 
            this._IntervalNumericUpDown.Location = new System.Drawing.Point(94, 25);
            this._IntervalNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._IntervalNumericUpDown.Maximum = new decimal(new int[] {
            7200000,
            0,
            0,
            0});
            this._IntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._IntervalNumericUpDown.Name = "_IntervalNumericUpDown";
            this._IntervalNumericUpDown.Size = new System.Drawing.Size(65, 23);
            this._IntervalNumericUpDown.TabIndex = 3;
            this._IntervalNumericUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // _Label4
            // 
            this._Label4.AutoSize = true;
            this._Label4.Location = new System.Drawing.Point(27, 27);
            this._Label4.Name = "_Label4";
            this._Label4.Size = new System.Drawing.Size(59, 17);
            this._Label4.TabIndex = 5;
            this._Label4.Text = "等待定时:";
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(403, 514);
            this._ConfirmButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(87, 37);
            this._ConfirmButton.TabIndex = 1;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(498, 514);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 37);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _SlotComboBox
            // 
            this._SlotComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SlotComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SlotComboBox.FormattingEnabled = true;
            this._SlotComboBox.Location = new System.Drawing.Point(107, 27);
            this._SlotComboBox.Name = "_SlotComboBox";
            this._SlotComboBox.Size = new System.Drawing.Size(365, 25);
            this._SlotComboBox.TabIndex = 3;
            // 
            // _DUTComboBox
            // 
            this._DUTComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DUTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._DUTComboBox.FormattingEnabled = true;
            this._DUTComboBox.Location = new System.Drawing.Point(107, 68);
            this._DUTComboBox.Name = "_DUTComboBox";
            this._DUTComboBox.Size = new System.Drawing.Size(365, 25);
            this._DUTComboBox.TabIndex = 4;
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Location = new System.Drawing.Point(39, 30);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(56, 17);
            this._Label1.TabIndex = 5;
            this._Label1.Text = "接驳器：";
            // 
            // _Label2
            // 
            this._Label2.AutoSize = true;
            this._Label2.Location = new System.Drawing.Point(39, 71);
            this._Label2.Name = "_Label2";
            this._Label2.Size = new System.Drawing.Size(56, 17);
            this._Label2.TabIndex = 6;
            this._Label2.Text = "被测物：";
            // 
            // _NewDUTButton
            // 
            this._NewDUTButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._NewDUTButton.Location = new System.Drawing.Point(478, 64);
            this._NewDUTButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._NewDUTButton.Name = "_NewDUTButton";
            this._NewDUTButton.Size = new System.Drawing.Size(107, 33);
            this._NewDUTButton.TabIndex = 8;
            this._NewDUTButton.Text = "新建被测物";
            this._NewDUTButton.UseVisualStyleBackColor = true;
            // 
            // _GpibNumericUpDown
            // 
            this._GpibNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._GpibNumericUpDown.Location = new System.Drawing.Point(107, 106);
            this._GpibNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._GpibNumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._GpibNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._GpibNumericUpDown.Name = "_GpibNumericUpDown";
            this._GpibNumericUpDown.Size = new System.Drawing.Size(90, 26);
            this._GpibNumericUpDown.TabIndex = 9;
            this._GpibNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._GpibNumericUpDown.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // _Label3
            // 
            this._Label3.AutoSize = true;
            this._Label3.Location = new System.Drawing.Point(39, 111);
            this._Label3.Name = "_Label3";
            this._Label3.Size = new System.Drawing.Size(72, 17);
            this._Label3.TabIndex = 11;
            this._Label3.Text = "GPIB地址：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label7.Location = new System.Drawing.Point(327, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "ms";
            // 
            // _TimeoutNumericUpDown
            // 
            this._TimeoutNumericUpDown.Location = new System.Drawing.Point(260, 25);
            this._TimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._TimeoutNumericUpDown.Maximum = new decimal(new int[] {
            7200000,
            0,
            0,
            0});
            this._TimeoutNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._TimeoutNumericUpDown.Name = "_TimeoutNumericUpDown";
            this._TimeoutNumericUpDown.Size = new System.Drawing.Size(65, 23);
            this._TimeoutNumericUpDown.TabIndex = 12;
            this._TimeoutNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // _Label5
            // 
            this._Label5.AutoSize = true;
            this._Label5.Location = new System.Drawing.Point(195, 28);
            this._Label5.Name = "_Label5";
            this._Label5.Size = new System.Drawing.Size(59, 17);
            this._Label5.TabIndex = 13;
            this._Label5.Text = "等待超时:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._TimeoutNumericUpDown);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this._Label4);
            this.groupBox2.Controls.Add(this._IntervalNumericUpDown);
            this.groupBox2.Controls.Add(this._Label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(42, 355);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 61);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "时长";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._InfiniteLoopCheckBox);
            this.groupBox3.Controls.Add(this._WorkToFinishCheckBox);
            this.groupBox3.Controls.Add(this._LoopCountNmericUpDown);
            this.groupBox3.Controls.Add(this._Label6);
            this.groupBox3.Location = new System.Drawing.Point(42, 420);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(543, 87);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "循环";
            // 
            // _InfiniteLoopCheckBox
            // 
            this._InfiniteLoopCheckBox.AutoSize = true;
            this._InfiniteLoopCheckBox.Checked = true;
            this._InfiniteLoopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._InfiniteLoopCheckBox.Location = new System.Drawing.Point(191, 27);
            this._InfiniteLoopCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._InfiniteLoopCheckBox.Name = "_InfiniteLoopCheckBox";
            this._InfiniteLoopCheckBox.Size = new System.Drawing.Size(75, 21);
            this._InfiniteLoopCheckBox.TabIndex = 16;
            this._InfiniteLoopCheckBox.Text = "持续采集";
            this._InfiniteLoopCheckBox.UseVisualStyleBackColor = true;
            // 
            // _WorkToFinishCheckBox
            // 
            this._WorkToFinishCheckBox.AutoSize = true;
            this._WorkToFinishCheckBox.Location = new System.Drawing.Point(94, 54);
            this._WorkToFinishCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._WorkToFinishCheckBox.Name = "_WorkToFinishCheckBox";
            this._WorkToFinishCheckBox.Size = new System.Drawing.Size(135, 21);
            this._WorkToFinishCheckBox.TabIndex = 15;
            this._WorkToFinishCheckBox.Text = "本指令优先循环完成";
            this._WorkToFinishCheckBox.UseVisualStyleBackColor = true;
            // 
            // _LoopCountNmericUpDown
            // 
            this._LoopCountNmericUpDown.Enabled = false;
            this._LoopCountNmericUpDown.Location = new System.Drawing.Point(94, 26);
            this._LoopCountNmericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._LoopCountNmericUpDown.Maximum = new decimal(new int[] {
            200000000,
            0,
            0,
            0});
            this._LoopCountNmericUpDown.Name = "_LoopCountNmericUpDown";
            this._LoopCountNmericUpDown.Size = new System.Drawing.Size(90, 23);
            this._LoopCountNmericUpDown.TabIndex = 12;
            this._LoopCountNmericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _Label6
            // 
            this._Label6.AutoSize = true;
            this._Label6.Location = new System.Drawing.Point(27, 28);
            this._Label6.Name = "_Label6";
            this._Label6.Size = new System.Drawing.Size(59, 17);
            this._Label6.TabIndex = 13;
            this._Label6.Text = "循环次数:";
            // 
            // _CommandTabControl
            // 
            this._CommandTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CommandTabControl.Controls.Add(this._ScpiTabPage);
            this._CommandTabControl.Controls.Add(this._CareTabPage);
            this._CommandTabControl.ItemSize = new System.Drawing.Size(62, 28);
            this._CommandTabControl.Location = new System.Drawing.Point(136, 143);
            this._CommandTabControl.Name = "_CommandTabControl";
            this._CommandTabControl.SelectedIndex = 0;
            this._CommandTabControl.Size = new System.Drawing.Size(449, 206);
            this._CommandTabControl.TabIndex = 17;
            // 
            // _ScpiTabPage
            // 
            this._ScpiTabPage.Controls.Add(this._ScpiNameTextBox);
            this._ScpiTabPage.Controls.Add(this.label10);
            this._ScpiTabPage.Controls.Add(this.label9);
            this._ScpiTabPage.Controls.Add(this._CommandTextBox);
            this._ScpiTabPage.Controls.Add(this._ScpiDescriptionTextBox);
            this._ScpiTabPage.Controls.Add(this._HexEnableCheckBox);
            this._ScpiTabPage.Controls.Add(this._BrLabel);
            this._ScpiTabPage.Controls.Add(this.label1);
            this._ScpiTabPage.Location = new System.Drawing.Point(4, 32);
            this._ScpiTabPage.Name = "_ScpiTabPage";
            this._ScpiTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._ScpiTabPage.Size = new System.Drawing.Size(441, 170);
            this._ScpiTabPage.TabIndex = 0;
            this._ScpiTabPage.Text = "SCPI指令";
            this._ScpiTabPage.UseVisualStyleBackColor = true;
            // 
            // _ScpiNameTextBox
            // 
            this._ScpiNameTextBox.Location = new System.Drawing.Point(85, 17);
            this._ScpiNameTextBox.Name = "_ScpiNameTextBox";
            this._ScpiNameTextBox.Size = new System.Drawing.Size(230, 23);
            this._ScpiNameTextBox.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "助记名:";
            // 
            // _CareTabPage
            // 
            this._CareTabPage.Location = new System.Drawing.Point(4, 32);
            this._CareTabPage.Name = "_CareTabPage";
            this._CareTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._CareTabPage.Size = new System.Drawing.Size(441, 170);
            this._CareTabPage.TabIndex = 1;
            this._CareTabPage.Text = "MeterCare指令";
            this._CareTabPage.UseVisualStyleBackColor = true;
            // 
            // _ScpiRadioButton
            // 
            this._ScpiRadioButton.AutoSize = true;
            this._ScpiRadioButton.Checked = true;
            this._ScpiRadioButton.Location = new System.Drawing.Point(42, 147);
            this._ScpiRadioButton.Name = "_ScpiRadioButton";
            this._ScpiRadioButton.Size = new System.Drawing.Size(52, 21);
            this._ScpiRadioButton.TabIndex = 18;
            this._ScpiRadioButton.TabStop = true;
            this._ScpiRadioButton.Text = "SCPI";
            this._ScpiRadioButton.UseVisualStyleBackColor = true;
            // 
            // _CareRadioButton
            // 
            this._CareRadioButton.AutoSize = true;
            this._CareRadioButton.Location = new System.Drawing.Point(42, 168);
            this._CareRadioButton.Name = "_CareRadioButton";
            this._CareRadioButton.Size = new System.Drawing.Size(88, 21);
            this._CareRadioButton.TabIndex = 19;
            this._CareRadioButton.Text = "MeterCare";
            this._CareRadioButton.UseVisualStyleBackColor = true;
            // 
            // CareCommandEditorDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(619, 568);
            this.Controls.Add(this._CareRadioButton);
            this.Controls.Add(this._ScpiRadioButton);
            this.Controls.Add(this._CommandTabControl);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._GpibNumericUpDown);
            this.Controls.Add(this._NewDUTButton);
            this.Controls.Add(this._DUTComboBox);
            this.Controls.Add(this._SlotComboBox);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._Label3);
            this.Controls.Add(this._Label2);
            this.Controls.Add(this._Label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(137, 171);
            this.Name = "CareCommandEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "指令编辑器";
            ((System.ComponentModel.ISupportInitialize)(this._IntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._GpibNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._TimeoutNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopCountNmericUpDown)).EndInit();
            this._CommandTabControl.ResumeLayout(false);
            this._ScpiTabPage.ResumeLayout(false);
            this._ScpiTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _IntervalNumericUpDown;
        private System.Windows.Forms.CheckBox _HexEnableCheckBox;
        private System.Windows.Forms.TextBox _CommandTextBox;
        private System.Windows.Forms.Label _Label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Label _BrLabel;
        private System.Windows.Forms.ComboBox _SlotComboBox;
        private System.Windows.Forms.ComboBox _DUTComboBox;
        private System.Windows.Forms.Label _Label1;
        private System.Windows.Forms.Label _Label2;
        private System.Windows.Forms.Button _NewDUTButton;
        private System.Windows.Forms.NumericUpDown _GpibNumericUpDown;
        private System.Windows.Forms.Label _Label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown _TimeoutNumericUpDown;
        private System.Windows.Forms.Label _Label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox _ScpiDescriptionTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox _InfiniteLoopCheckBox;
        private System.Windows.Forms.CheckBox _WorkToFinishCheckBox;
        private System.Windows.Forms.NumericUpDown _LoopCountNmericUpDown;
        private System.Windows.Forms.Label _Label6;
        private System.Windows.Forms.TabControl _CommandTabControl;
        private System.Windows.Forms.TabPage _ScpiTabPage;
        private System.Windows.Forms.TabPage _CareTabPage;
        private System.Windows.Forms.RadioButton _ScpiRadioButton;
        private System.Windows.Forms.RadioButton _CareRadioButton;
        private System.Windows.Forms.TextBox _ScpiNameTextBox;
        private System.Windows.Forms.Label label10;
    }
}