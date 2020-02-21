namespace NKnife.MeterKnife.Workbench.Dialogs.Commands
{
    partial class CommandEditorDialog
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
            NKnife.MeterKnife.Common.Scpi.SCPI scpi1 = new NKnife.MeterKnife.Common.Scpi.SCPI();
            this.label3 = new System.Windows.Forms.Label();
            this._IntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._SlotComboBox = new System.Windows.Forms.ComboBox();
            this._DUTComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._TimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this._TimeGroupBox = new System.Windows.Forms.GroupBox();
            this._LoopGroupBox = new System.Windows.Forms.GroupBox();
            this._InfiniteLoopCheckBox = new System.Windows.Forms.CheckBox();
            this._WorkToFinishCheckBox = new System.Windows.Forms.CheckBox();
            this._LoopCountNmericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this._CommandTabControl = new System.Windows.Forms.TabControl();
            this._ScpiTabPage = new System.Windows.Forms.TabPage();
            this._CareTabPage = new System.Windows.Forms.TabPage();
            this._ScpiRadioButton = new System.Windows.Forms.RadioButton();
            this._CareRadioButton = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._ScpiDetailPanel = new NKnife.MeterKnife.Workbench.Controls.ScpiDetailPanel();
            ((System.ComponentModel.ISupportInitialize)(this._IntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._TimeoutNumericUpDown)).BeginInit();
            this._TimeGroupBox.SuspendLayout();
            this._LoopGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopCountNmericUpDown)).BeginInit();
            this._CommandTabControl.SuspendLayout();
            this._ScpiTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label3.Location = new System.Drawing.Point(117, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "ms";
            // 
            // _IntervalNumericUpDown
            // 
            this._IntervalNumericUpDown.Location = new System.Drawing.Point(21, 49);
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
            this._IntervalNumericUpDown.Size = new System.Drawing.Size(90, 23);
            this._IntervalNumericUpDown.TabIndex = 3;
            this._IntervalNumericUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "等待定时:";
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(307, 520);
            this._ConfirmButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(87, 37);
            this._ConfirmButton.TabIndex = 1;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(407, 520);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 37);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // _SlotComboBox
            // 
            this._SlotComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SlotComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SlotComboBox.FormattingEnabled = true;
            this._SlotComboBox.Location = new System.Drawing.Point(36, 40);
            this._SlotComboBox.Name = "_SlotComboBox";
            this._SlotComboBox.Size = new System.Drawing.Size(209, 25);
            this._SlotComboBox.TabIndex = 3;
            // 
            // _DUTComboBox
            // 
            this._DUTComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DUTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._DUTComboBox.FormattingEnabled = true;
            this._DUTComboBox.Location = new System.Drawing.Point(263, 40);
            this._DUTComboBox.Name = "_DUTComboBox";
            this._DUTComboBox.Size = new System.Drawing.Size(231, 25);
            this._DUTComboBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "接驳器";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "被测物";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label7.Location = new System.Drawing.Point(117, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "ms";
            // 
            // _TimeoutNumericUpDown
            // 
            this._TimeoutNumericUpDown.Location = new System.Drawing.Point(21, 97);
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
            this._TimeoutNumericUpDown.Size = new System.Drawing.Size(90, 23);
            this._TimeoutNumericUpDown.TabIndex = 12;
            this._TimeoutNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "等待超时:";
            // 
            // _TimeGroupBox
            // 
            this._TimeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TimeGroupBox.Controls.Add(this._TimeoutNumericUpDown);
            this._TimeGroupBox.Controls.Add(this.label7);
            this._TimeGroupBox.Controls.Add(this.label6);
            this._TimeGroupBox.Controls.Add(this._IntervalNumericUpDown);
            this._TimeGroupBox.Controls.Add(this.label8);
            this._TimeGroupBox.Controls.Add(this.label3);
            this._TimeGroupBox.Location = new System.Drawing.Point(37, 374);
            this._TimeGroupBox.Name = "_TimeGroupBox";
            this._TimeGroupBox.Size = new System.Drawing.Size(208, 138);
            this._TimeGroupBox.TabIndex = 15;
            this._TimeGroupBox.TabStop = false;
            this._TimeGroupBox.Text = "时长";
            // 
            // _LoopGroupBox
            // 
            this._LoopGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._LoopGroupBox.Controls.Add(this._InfiniteLoopCheckBox);
            this._LoopGroupBox.Controls.Add(this._WorkToFinishCheckBox);
            this._LoopGroupBox.Controls.Add(this._LoopCountNmericUpDown);
            this._LoopGroupBox.Controls.Add(this.label11);
            this._LoopGroupBox.Location = new System.Drawing.Point(263, 374);
            this._LoopGroupBox.Name = "_LoopGroupBox";
            this._LoopGroupBox.Size = new System.Drawing.Size(227, 138);
            this._LoopGroupBox.TabIndex = 16;
            this._LoopGroupBox.TabStop = false;
            this._LoopGroupBox.Text = "循环";
            // 
            // _InfiniteLoopCheckBox
            // 
            this._InfiniteLoopCheckBox.AutoSize = true;
            this._InfiniteLoopCheckBox.Checked = true;
            this._InfiniteLoopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._InfiniteLoopCheckBox.Location = new System.Drawing.Point(30, 80);
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
            this._WorkToFinishCheckBox.Location = new System.Drawing.Point(30, 102);
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
            this._LoopCountNmericUpDown.Location = new System.Drawing.Point(30, 49);
            this._LoopCountNmericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._LoopCountNmericUpDown.Maximum = new decimal(new int[] {
            200000000,
            0,
            0,
            0});
            this._LoopCountNmericUpDown.Name = "_LoopCountNmericUpDown";
            this._LoopCountNmericUpDown.Size = new System.Drawing.Size(101, 23);
            this._LoopCountNmericUpDown.TabIndex = 12;
            this._LoopCountNmericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "循环次数:";
            // 
            // _CommandTabControl
            // 
            this._CommandTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CommandTabControl.Controls.Add(this._ScpiTabPage);
            this._CommandTabControl.Controls.Add(this._CareTabPage);
            this._CommandTabControl.ItemSize = new System.Drawing.Size(62, 28);
            this._CommandTabControl.Location = new System.Drawing.Point(131, 125);
            this._CommandTabControl.Name = "_CommandTabControl";
            this._CommandTabControl.SelectedIndex = 0;
            this._CommandTabControl.Size = new System.Drawing.Size(363, 243);
            this._CommandTabControl.TabIndex = 17;
            // 
            // _ScpiTabPage
            // 
            this._ScpiTabPage.Controls.Add(this._ScpiDetailPanel);
            this._ScpiTabPage.Location = new System.Drawing.Point(4, 32);
            this._ScpiTabPage.Name = "_ScpiTabPage";
            this._ScpiTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._ScpiTabPage.Size = new System.Drawing.Size(355, 207);
            this._ScpiTabPage.TabIndex = 0;
            this._ScpiTabPage.Text = "SCPI指令";
            this._ScpiTabPage.UseVisualStyleBackColor = true;
            // 
            // _CareTabPage
            // 
            this._CareTabPage.Location = new System.Drawing.Point(4, 32);
            this._CareTabPage.Name = "_CareTabPage";
            this._CareTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._CareTabPage.Size = new System.Drawing.Size(355, 207);
            this._CareTabPage.TabIndex = 1;
            this._CareTabPage.Text = "MeterCare指令";
            this._CareTabPage.UseVisualStyleBackColor = true;
            // 
            // _ScpiRadioButton
            // 
            this._ScpiRadioButton.AutoSize = true;
            this._ScpiRadioButton.Checked = true;
            this._ScpiRadioButton.Location = new System.Drawing.Point(36, 125);
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
            this._CareRadioButton.Location = new System.Drawing.Point(36, 146);
            this._CareRadioButton.Name = "_CareRadioButton";
            this._CareRadioButton.Size = new System.Drawing.Size(88, 21);
            this._CareRadioButton.TabIndex = 19;
            this._CareRadioButton.Text = "MeterCare";
            this._CareRadioButton.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(36, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(209, 25);
            this.comboBox1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "仪器";
            // 
            // _ScpiDetailPanel
            // 
            this._ScpiDetailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ScpiDetailPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ScpiDetailPanel.Location = new System.Drawing.Point(3, 3);
            this._ScpiDetailPanel.Name = "_ScpiDetailPanel";
            scpi1.Command = "";
            scpi1.Description = "";
            scpi1.Name = "";
            this._ScpiDetailPanel.Scpi = scpi1;
            this._ScpiDetailPanel.Size = new System.Drawing.Size(349, 201);
            this._ScpiDetailPanel.TabIndex = 0;
            // 
            // CommandEditorDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(533, 573);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this._CareRadioButton);
            this.Controls.Add(this._ScpiRadioButton);
            this.Controls.Add(this._CommandTabControl);
            this.Controls.Add(this._LoopGroupBox);
            this.Controls.Add(this._TimeGroupBox);
            this.Controls.Add(this._DUTComboBox);
            this.Controls.Add(this._SlotComboBox);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(137, 171);
            this.Name = "CommandEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "指令编辑器";
            ((System.ComponentModel.ISupportInitialize)(this._IntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._TimeoutNumericUpDown)).EndInit();
            this._TimeGroupBox.ResumeLayout(false);
            this._TimeGroupBox.PerformLayout();
            this._LoopGroupBox.ResumeLayout(false);
            this._LoopGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopCountNmericUpDown)).EndInit();
            this._CommandTabControl.ResumeLayout(false);
            this._ScpiTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _IntervalNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.ComboBox _SlotComboBox;
        private System.Windows.Forms.ComboBox _DUTComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown _TimeoutNumericUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox _TimeGroupBox;
        private System.Windows.Forms.GroupBox _LoopGroupBox;
        private System.Windows.Forms.CheckBox _InfiniteLoopCheckBox;
        private System.Windows.Forms.CheckBox _WorkToFinishCheckBox;
        private System.Windows.Forms.NumericUpDown _LoopCountNmericUpDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl _CommandTabControl;
        private System.Windows.Forms.TabPage _ScpiTabPage;
        private System.Windows.Forms.TabPage _CareTabPage;
        private System.Windows.Forms.RadioButton _ScpiRadioButton;
        private System.Windows.Forms.RadioButton _CareRadioButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private Controls.ScpiDetailPanel _ScpiDetailPanel;
    }
}