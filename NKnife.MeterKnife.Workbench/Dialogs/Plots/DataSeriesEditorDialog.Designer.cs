namespace NKnife.MeterKnife.Workbench.Dialogs.Plots
{
    partial class DataSeriesEditorDialog
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
            this.label5 = new System.Windows.Forms.Label();
            this._LineColor = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._ThicknessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._LineStyleComboBox = new System.Windows.Forms.ComboBox();
            this._ExhibitsComboBox = new System.Windows.Forms.ComboBox();
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this._MainGroupBox = new System.Windows.Forms.GroupBox();
            this._OffsetNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._ThicknessNumericUpDown)).BeginInit();
            this._MainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._OffsetNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(332, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "偏移";
            // 
            // _LineColor
            // 
            this._LineColor.BackColor = System.Drawing.Color.White;
            this._LineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LineColor.Color = System.Drawing.Color.White;
            this._LineColor.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._LineColor.Location = new System.Drawing.Point(274, 53);
            this._LineColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._LineColor.Name = "_LineColor";
            this._LineColor.Size = new System.Drawing.Size(57, 25);
            this._LineColor.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "颜色";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "线宽";
            // 
            // _ThicknessNumericUpDown
            // 
            this._ThicknessNumericUpDown.DecimalPlaces = 1;
            this._ThicknessNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._ThicknessNumericUpDown.Location = new System.Drawing.Point(214, 55);
            this._ThicknessNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._ThicknessNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this._ThicknessNumericUpDown.Name = "_ThicknessNumericUpDown";
            this._ThicknessNumericUpDown.Size = new System.Drawing.Size(56, 21);
            this._ThicknessNumericUpDown.TabIndex = 2;
            this._ThicknessNumericUpDown.Value = new decimal(new int[] {
            18,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "数据名称：";
            // 
            // _LineStyleComboBox
            // 
            this._LineStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._LineStyleComboBox.FormattingEnabled = true;
            this._LineStyleComboBox.Location = new System.Drawing.Point(146, 55);
            this._LineStyleComboBox.Name = "_LineStyleComboBox";
            this._LineStyleComboBox.Size = new System.Drawing.Size(63, 21);
            this._LineStyleComboBox.TabIndex = 1;
            // 
            // _ExhibitsComboBox
            // 
            this._ExhibitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ExhibitsComboBox.FormattingEnabled = true;
            this._ExhibitsComboBox.Location = new System.Drawing.Point(20, 55);
            this._ExhibitsComboBox.Name = "_ExhibitsComboBox";
            this._ExhibitsComboBox.Size = new System.Drawing.Size(121, 21);
            this._ExhibitsComboBox.TabIndex = 0;
            // 
            // _CancelButton
            // 
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(364, 125);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(89, 31);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消(&X)";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Location = new System.Drawing.Point(269, 125);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(89, 31);
            this._AcceptButton.TabIndex = 1;
            this._AcceptButton.Text = "确定(&C)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // _MainGroupBox
            // 
            this._MainGroupBox.Controls.Add(this._OffsetNumericUpDown);
            this._MainGroupBox.Controls.Add(this._ExhibitsComboBox);
            this._MainGroupBox.Controls.Add(this._LineStyleComboBox);
            this._MainGroupBox.Controls.Add(this.label1);
            this._MainGroupBox.Controls.Add(this.label2);
            this._MainGroupBox.Controls.Add(this._ThicknessNumericUpDown);
            this._MainGroupBox.Controls.Add(this.label3);
            this._MainGroupBox.Controls.Add(this.label5);
            this._MainGroupBox.Controls.Add(this.label4);
            this._MainGroupBox.Controls.Add(this._LineColor);
            this._MainGroupBox.Location = new System.Drawing.Point(12, 12);
            this._MainGroupBox.Name = "_MainGroupBox";
            this._MainGroupBox.Size = new System.Drawing.Size(441, 107);
            this._MainGroupBox.TabIndex = 0;
            this._MainGroupBox.TabStop = false;
            // 
            // _OffsetNumericUpDown
            // 
            this._OffsetNumericUpDown.DecimalPlaces = 8;
            this._OffsetNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._OffsetNumericUpDown.Location = new System.Drawing.Point(335, 54);
            this._OffsetNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._OffsetNumericUpDown.Name = "_OffsetNumericUpDown";
            this._OffsetNumericUpDown.Size = new System.Drawing.Size(89, 21);
            this._OffsetNumericUpDown.TabIndex = 25;
            // 
            // DataSeriesEditorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(464, 169);
            this.Controls.Add(this._MainGroupBox);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(120, 148);
            this.Name = "DataSeriesEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "数据线样式设置";
            ((System.ComponentModel.ISupportInitialize)(this._ThicknessNumericUpDown)).EndInit();
            this._MainGroupBox.ResumeLayout(false);
            this._MainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._OffsetNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _LineColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _ThicknessNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _LineStyleComboBox;
        private System.Windows.Forms.ComboBox _ExhibitsComboBox;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.GroupBox _MainGroupBox;
        private System.Windows.Forms.NumericUpDown _OffsetNumericUpDown;
    }
}