namespace MeterKnife.Common.Scpi
{
    partial class ScpiCommandEditorDialog
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
            this._InsertHeadBrButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._RangeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._HexEnableCheckBox = new System.Windows.Forms.CheckBox();
            this._CommandTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._InsertTailBrButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._RangeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._InsertTailBrButton);
            this.groupBox1.Controls.Add(this._InsertHeadBrButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._RangeNumericUpDown);
            this.groupBox1.Controls.Add(this._CommandTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._HexEnableCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指令";
            // 
            // _InsertHeadBrButton
            // 
            this._InsertHeadBrButton.Location = new System.Drawing.Point(57, 72);
            this._InsertHeadBrButton.Name = "_InsertHeadBrButton";
            this._InsertHeadBrButton.Size = new System.Drawing.Size(102, 23);
            this._InsertHeadBrButton.TabIndex = 7;
            this._InsertHeadBrButton.Text = "头部插入新行符";
            this._InsertHeadBrButton.UseVisualStyleBackColor = true;
            this._InsertHeadBrButton.Click += new System.EventHandler(this._AddEnterFlagButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label3.Location = new System.Drawing.Point(134, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "ms";
            // 
            // _RangeNumericUpDown
            // 
            this._RangeNumericUpDown.Location = new System.Drawing.Point(57, 104);
            this._RangeNumericUpDown.Maximum = new decimal(new int[] {
            7200000,
            0,
            0,
            0});
            this._RangeNumericUpDown.Name = "_RangeNumericUpDown";
            this._RangeNumericUpDown.Size = new System.Drawing.Size(77, 21);
            this._RangeNumericUpDown.TabIndex = 3;
            this._RangeNumericUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // _HexEnableCheckBox
            // 
            this._HexEnableCheckBox.AutoSize = true;
            this._HexEnableCheckBox.Location = new System.Drawing.Point(300, 75);
            this._HexEnableCheckBox.Name = "_HexEnableCheckBox";
            this._HexEnableCheckBox.Size = new System.Drawing.Size(62, 17);
            this._HexEnableCheckBox.TabIndex = 1;
            this._HexEnableCheckBox.Text = "16进制";
            this._HexEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // _CommandTextBox
            // 
            this._CommandTextBox.Location = new System.Drawing.Point(57, 31);
            this._CommandTextBox.Multiline = true;
            this._CommandTextBox.Name = "_CommandTextBox";
            this._CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._CommandTextBox.Size = new System.Drawing.Size(298, 37);
            this._CommandTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "周期:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "指令:";
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(233, 169);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(75, 28);
            this._ConfirmButton.TabIndex = 1;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(314, 169);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 28);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _InsertTailBrButton
            // 
            this._InsertTailBrButton.Location = new System.Drawing.Point(161, 72);
            this._InsertTailBrButton.Name = "_InsertTailBrButton";
            this._InsertTailBrButton.Size = new System.Drawing.Size(102, 23);
            this._InsertTailBrButton.TabIndex = 8;
            this._InsertTailBrButton.Text = "尾部增加新行符";
            this._InsertTailBrButton.UseVisualStyleBackColor = true;
            // 
            // ScpiCommandEditorDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(401, 210);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScpiCommandEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "指令编辑器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._RangeNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _RangeNumericUpDown;
        private System.Windows.Forms.CheckBox _HexEnableCheckBox;
        private System.Windows.Forms.TextBox _CommandTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _InsertHeadBrButton;
        private System.Windows.Forms.Button _InsertTailBrButton;
    }
}