namespace MeterKnife.Scpis
{
    partial class InstrumentScpiGroupTreeDialog
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
            this._Tree = new System.Windows.Forms.TreeView();
            this._CancelButton = new System.Windows.Forms.Button();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _Tree
            // 
            this._Tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Tree.Location = new System.Drawing.Point(12, 14);
            this._Tree.Name = "_Tree";
            this._Tree.Size = new System.Drawing.Size(288, 330);
            this._Tree.TabIndex = 0;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(226, 348);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 27);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(145, 348);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(75, 27);
            this._ConfirmButton.TabIndex = 2;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // MeterScpiGroupTreeDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(312, 386);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._Tree);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MeterScpiGroupTreeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "仪器与指令管理器";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView _Tree;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _ConfirmButton;
    }
}