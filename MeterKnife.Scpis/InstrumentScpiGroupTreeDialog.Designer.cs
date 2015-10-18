using MeterKnife.Scpis.ScpiTree;

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
            this._Tree = new SubjectFileTree();
            this._CancelButton = new System.Windows.Forms.Button();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._Panel = new System.Windows.Forms.Panel();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Tree
            // 
            this._Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Tree.Location = new System.Drawing.Point(0, 25);
            this._Tree.Name = "_Tree";
            this._Tree.Size = new System.Drawing.Size(312, 321);
            this._Tree.TabIndex = 0;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(228, 351);
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
            this._ConfirmButton.Location = new System.Drawing.Point(147, 351);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(75, 27);
            this._ConfirmButton.TabIndex = 2;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _Panel
            // 
            this._Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Panel.Controls.Add(this._Tree);
            this._Panel.Controls.Add(this._ToolStrip);
            this._Panel.Location = new System.Drawing.Point(0, 0);
            this._Panel.Name = "_Panel";
            this._Panel.Size = new System.Drawing.Size(312, 346);
            this._Panel.TabIndex = 3;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(312, 25);
            this._ToolStrip.TabIndex = 0;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // InstrumentScpiGroupTreeDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(312, 386);
            this.Controls.Add(this._Panel);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstrumentScpiGroupTreeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "仪器与指令管理器";
            this._Panel.ResumeLayout(false);
            this._Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SubjectFileTree _Tree;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Panel _Panel;
        private System.Windows.Forms.ToolStrip _ToolStrip;
    }
}