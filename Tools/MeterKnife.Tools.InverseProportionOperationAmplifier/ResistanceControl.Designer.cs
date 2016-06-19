namespace MeterKnife.Tools.InverseProportionOperationAmplifier
{
    partial class ResistanceControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._ValueBox = new System.Windows.Forms.NumericUpDown();
            this._UnitLabel = new System.Windows.Forms.Label();
            this.singleLine2 = new NKnife.GUI.WinForm.SingleLine();
            this.singleLine1 = new NKnife.GUI.WinForm.SingleLine();
            ((System.ComponentModel.ISupportInitialize)(this._ValueBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _ValueBox
            // 
            this._ValueBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ValueBox.DecimalPlaces = 3;
            this._ValueBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ValueBox.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._ValueBox.Location = new System.Drawing.Point(13, 4);
            this._ValueBox.Maximum = new decimal(new int[] {
            10001,
            0,
            0,
            0});
            this._ValueBox.Name = "_ValueBox";
            this._ValueBox.Size = new System.Drawing.Size(83, 23);
            this._ValueBox.TabIndex = 0;
            this._ValueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._ValueBox.ThousandsSeparator = true;
            this._ValueBox.Value = new decimal(new int[] {
            10000999,
            0,
            0,
            196608});
            // 
            // _UnitLabel
            // 
            this._UnitLabel.AutoSize = true;
            this._UnitLabel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this._UnitLabel.Location = new System.Drawing.Point(96, 0);
            this._UnitLabel.Name = "_UnitLabel";
            this._UnitLabel.Size = new System.Drawing.Size(18, 17);
            this._UnitLabel.TabIndex = 3;
            this._UnitLabel.Text = "Ω";
            // 
            // singleLine2
            // 
            this.singleLine2.BackColor = System.Drawing.Color.DimGray;
            this.singleLine2.FlatLineSize = 3;
            this.singleLine2.Location = new System.Drawing.Point(96, 15);
            this.singleLine2.Name = "singleLine2";
            this.singleLine2.Size = new System.Drawing.Size(15, 3);
            this.singleLine2.TabIndex = 2;
            this.singleLine2.TabStop = false;
            // 
            // singleLine1
            // 
            this.singleLine1.BackColor = System.Drawing.Color.DimGray;
            this.singleLine1.FlatLineSize = 3;
            this.singleLine1.Location = new System.Drawing.Point(-2, 15);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Size = new System.Drawing.Size(15, 3);
            this.singleLine1.TabIndex = 1;
            this.singleLine1.TabStop = false;
            // 
            // ResistanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.singleLine2);
            this.Controls.Add(this._ValueBox);
            this.Controls.Add(this.singleLine1);
            this.Controls.Add(this._UnitLabel);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ResistanceControl";
            this.Size = new System.Drawing.Size(113, 30);
            ((System.ComponentModel.ISupportInitialize)(this._ValueBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown _ValueBox;
        private NKnife.GUI.WinForm.SingleLine singleLine1;
        private NKnife.GUI.WinForm.SingleLine singleLine2;
        private System.Windows.Forms.Label _UnitLabel;
    }
}
