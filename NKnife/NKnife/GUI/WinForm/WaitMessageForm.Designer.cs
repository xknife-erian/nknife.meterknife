namespace NKnife.GUI.WinForm
{
    partial class WaitMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitMessageForm));
            this._LogoBox = new System.Windows.Forms.PictureBox();
            this._TitleLabel = new System.Windows.Forms.Label();
            this._InfomationTextBox = new System.Windows.Forms.TextBox();
            this._ProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this._LogoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _LogoBox
            // 
            this._LogoBox.Image = ((System.Drawing.Image)(resources.GetObject("_LogoBox.Image")));
            this._LogoBox.Location = new System.Drawing.Point(29, 23);
            this._LogoBox.Name = "_LogoBox";
            this._LogoBox.Size = new System.Drawing.Size(72, 71);
            this._LogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._LogoBox.TabIndex = 0;
            this._LogoBox.TabStop = false;
            // 
            // _TitleLabel
            // 
            this._TitleLabel.AutoSize = true;
            this._TitleLabel.Location = new System.Drawing.Point(120, 23);
            this._TitleLabel.Name = "_TitleLabel";
            this._TitleLabel.Size = new System.Drawing.Size(27, 13);
            this._TitleLabel.TabIndex = 1;
            this._TitleLabel.Text = "Title";
            // 
            // _InfomationTextBox
            // 
            this._InfomationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._InfomationTextBox.Location = new System.Drawing.Point(121, 39);
            this._InfomationTextBox.Multiline = true;
            this._InfomationTextBox.Name = "_InfomationTextBox";
            this._InfomationTextBox.ReadOnly = true;
            this._InfomationTextBox.Size = new System.Drawing.Size(273, 62);
            this._InfomationTextBox.TabIndex = 2;
            this._InfomationTextBox.Text = "Infomation";
            // 
            // _ProgressBar
            // 
            this._ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._ProgressBar.Location = new System.Drawing.Point(0, 130);
            this._ProgressBar.Name = "_ProgressBar";
            this._ProgressBar.Size = new System.Drawing.Size(440, 10);
            this._ProgressBar.TabIndex = 3;
            // 
            // WaitMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(440, 140);
            this.ControlBox = false;
            this.Controls.Add(this._ProgressBar);
            this.Controls.Add(this._InfomationTextBox);
            this.Controls.Add(this._TitleLabel);
            this.Controls.Add(this._LogoBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitMessageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "稍候……";
            ((System.ComponentModel.ISupportInitialize)(this._LogoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _LogoBox;
        private System.Windows.Forms.Label _TitleLabel;
        private System.Windows.Forms.TextBox _InfomationTextBox;
        private System.Windows.Forms.ProgressBar _ProgressBar;

    }
}