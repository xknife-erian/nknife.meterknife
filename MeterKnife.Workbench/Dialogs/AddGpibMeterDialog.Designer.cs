﻿namespace MeterKnife.Workbench.Dialogs
{
    partial class AddGpibMeterDialog
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
            this._GroupBox = new System.Windows.Forms.GroupBox();
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._NumberBox = new System.Windows.Forms.NumericUpDown();
            this._GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _GroupBox
            // 
            this._GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GroupBox.Controls.Add(this._NumberBox);
            this._GroupBox.Controls.Add(this.label1);
            this._GroupBox.Location = new System.Drawing.Point(12, 13);
            this._GroupBox.Name = "_GroupBox";
            this._GroupBox.Size = new System.Drawing.Size(268, 91);
            this._GroupBox.TabIndex = 1;
            this._GroupBox.TabStop = false;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(197, 114);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(83, 28);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(108, 114);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(83, 28);
            this._AcceptButton.TabIndex = 3;
            this._AcceptButton.Text = "确认";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "GPIB Address:";
            // 
            // _NumberBox
            // 
            this._NumberBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this._NumberBox.Location = new System.Drawing.Point(103, 37);
            this._NumberBox.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._NumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._NumberBox.Name = "_NumberBox";
            this._NumberBox.Size = new System.Drawing.Size(144, 25);
            this._NumberBox.TabIndex = 2;
            this._NumberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._NumberBox.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // AddGpibMeterDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(292, 160);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._GroupBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddGpibMeterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加仪器";
            this._GroupBox.ResumeLayout(false);
            this._GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NumberBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _GroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.NumericUpDown _NumberBox;
    }
}