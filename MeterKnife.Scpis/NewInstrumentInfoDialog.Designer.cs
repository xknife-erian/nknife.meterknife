namespace MeterKnife.Scpis
{
    partial class NewInstrumentInfoDialog
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
            this._CancelButton = new System.Windows.Forms.Button();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._BrandComboBox = new System.Windows.Forms.ComboBox();
            this._NameComboBox = new System.Windows.Forms.ComboBox();
            this._DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._DescriptionTextBox);
            this.groupBox1.Controls.Add(this._NameComboBox);
            this.groupBox1.Controls.Add(this._BrandComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "仪器基本信息";
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(332, 159);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(94, 23);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(232, 159);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(94, 23);
            this._ConfirmButton.TabIndex = 0;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _BrandComboBox
            // 
            this._BrandComboBox.FormattingEnabled = true;
            this._BrandComboBox.Location = new System.Drawing.Point(68, 22);
            this._BrandComboBox.Name = "_BrandComboBox";
            this._BrandComboBox.Size = new System.Drawing.Size(196, 21);
            this._BrandComboBox.TabIndex = 0;
            // 
            // _NameComboBox
            // 
            this._NameComboBox.FormattingEnabled = true;
            this._NameComboBox.Location = new System.Drawing.Point(68, 49);
            this._NameComboBox.Name = "_NameComboBox";
            this._NameComboBox.Size = new System.Drawing.Size(196, 21);
            this._NameComboBox.TabIndex = 1;
            // 
            // _DescriptionTextBox
            // 
            this._DescriptionTextBox.Location = new System.Drawing.Point(68, 77);
            this._DescriptionTextBox.Multiline = true;
            this._DescriptionTextBox.Name = "_DescriptionTextBox";
            this._DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._DescriptionTextBox.Size = new System.Drawing.Size(314, 52);
            this._DescriptionTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "品牌:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "型号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "简介:";
            // 
            // NewInstrumentInfoDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(438, 194);
            this.ControlBox = false;
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewInstrumentInfoDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "新仪器信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _DescriptionTextBox;
        private System.Windows.Forms.ComboBox _NameComboBox;
        private System.Windows.Forms.ComboBox _BrandComboBox;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _ConfirmButton;
    }
}