namespace MeterKnife.Common.Winforms.Dialogs
{
    partial class ExportProgressDialog
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
            this._ProgressBar = new System.Windows.Forms.ProgressBar();
            this._GroupBox = new System.Windows.Forms.GroupBox();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._PathTextBox = new System.Windows.Forms.TextBox();
            this._GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ProgressBar
            // 
            this._ProgressBar.Location = new System.Drawing.Point(15, 23);
            this._ProgressBar.Name = "_ProgressBar";
            this._ProgressBar.Size = new System.Drawing.Size(297, 26);
            this._ProgressBar.TabIndex = 0;
            // 
            // _GroupBox
            // 
            this._GroupBox.Controls.Add(this._ProgressBar);
            this._GroupBox.Location = new System.Drawing.Point(31, 15);
            this._GroupBox.Name = "_GroupBox";
            this._GroupBox.Size = new System.Drawing.Size(327, 61);
            this._GroupBox.TabIndex = 2;
            this._GroupBox.TabStop = false;
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Location = new System.Drawing.Point(265, 127);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(93, 29);
            this._ConfirmButton.TabIndex = 0;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _PathTextBox
            // 
            this._PathTextBox.Location = new System.Drawing.Point(31, 85);
            this._PathTextBox.Multiline = true;
            this._PathTextBox.Name = "_PathTextBox";
            this._PathTextBox.ReadOnly = true;
            this._PathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._PathTextBox.Size = new System.Drawing.Size(327, 36);
            this._PathTextBox.TabIndex = 1;
            // 
            // ExportProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 168);
            this.ControlBox = false;
            this.Controls.Add(this._PathTextBox);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._GroupBox);
            this.MinimumSize = new System.Drawing.Size(120, 149);
            this.Name = "ExportProgressDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "导出";
            this._GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar _ProgressBar;
        private System.Windows.Forms.GroupBox _GroupBox;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.TextBox _PathTextBox;
    }
}