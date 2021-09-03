namespace NKnife.MeterKnife.Workbench.Dialogs.Instruments
{
    partial class ScpiDebugDialog
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
            this._SlotComboBox = new System.Windows.Forms.ComboBox();
            this._LoggerTextBox = new System.Windows.Forms.TextBox();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // _SlotComboBox
            // 
            this._SlotComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._SlotComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SlotComboBox.FormattingEnabled = true;
            this._SlotComboBox.Location = new System.Drawing.Point(0, 0);
            this._SlotComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this._SlotComboBox.Name = "_SlotComboBox";
            this._SlotComboBox.Size = new System.Drawing.Size(369, 25);
            this._SlotComboBox.TabIndex = 0;
            // 
            // _LoggerTextBox
            // 
            this._LoggerTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(0)))));
            this._LoggerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LoggerTextBox.Font = new System.Drawing.Font("Fira Code", 10.25F);
            this._LoggerTextBox.ForeColor = System.Drawing.Color.LightGreen;
            this._LoggerTextBox.Location = new System.Drawing.Point(0, 25);
            this._LoggerTextBox.MaxLength = 327670;
            this._LoggerTextBox.Multiline = true;
            this._LoggerTextBox.Name = "_LoggerTextBox";
            this._LoggerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._LoggerTextBox.Size = new System.Drawing.Size(369, 454);
            this._LoggerTextBox.TabIndex = 1;
            this._LoggerTextBox.Text = "abcdefg,Hijklmn";
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Location = new System.Drawing.Point(0, 479);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(369, 22);
            this._StatusStrip.SizingGrip = false;
            this._StatusStrip.TabIndex = 2;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // ScpiDebugDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(369, 501);
            this.ControlBox = false;
            this.Controls.Add(this._LoggerTextBox);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._SlotComboBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ScpiDebugDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ScpiDebugDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _SlotComboBox;
        private System.Windows.Forms.TextBox _LoggerTextBox;
        private System.Windows.Forms.StatusStrip _StatusStrip;
    }
}