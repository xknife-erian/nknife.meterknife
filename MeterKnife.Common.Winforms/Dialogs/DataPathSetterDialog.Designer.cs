namespace MeterKnife.Common.Winforms.Dialogs
{
    partial class DataPathSetterDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._PathTextbox = new System.Windows.Forms.TextBox();
            this._PathSelectButton = new System.Windows.Forms.Button();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(26, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 52);
            this.label1.TabIndex = 1;
            this.label1.Text = "应用程序未找到您的数据路径设置。可能原因是:\r\n1.这是您第一次使用本应用程序；\r\n2.您已有数据存储位置，但重新安装或升级了应用程序；\r\n3.您迁移了原来的数据" +
    "存储位置。";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._PathSelectButton);
            this.groupBox1.Controls.Add(this._PathTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 133);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择数据存储路径";
            // 
            // _PathTextbox
            // 
            this._PathTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._PathTextbox.Location = new System.Drawing.Point(29, 38);
            this._PathTextbox.Name = "_PathTextbox";
            this._PathTextbox.Size = new System.Drawing.Size(357, 21);
            this._PathTextbox.TabIndex = 0;
            // 
            // _PathSelectButton
            // 
            this._PathSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._PathSelectButton.Location = new System.Drawing.Point(393, 35);
            this._PathSelectButton.Name = "_PathSelectButton";
            this._PathSelectButton.Size = new System.Drawing.Size(49, 26);
            this._PathSelectButton.TabIndex = 2;
            this._PathSelectButton.Text = "浏览";
            this._PathSelectButton.UseVisualStyleBackColor = true;
            this._PathSelectButton.Click += new System.EventHandler(this._PathSelectButton_Click);
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(333, 154);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(75, 25);
            this._ConfirmButton.TabIndex = 3;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(414, 154);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 25);
            this._CancelButton.TabIndex = 4;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // DataPathSetterDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(517, 198);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataPathSetterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DataPathSetter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _PathSelectButton;
        private System.Windows.Forms.TextBox _PathTextbox;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Button _CancelButton;
    }
}