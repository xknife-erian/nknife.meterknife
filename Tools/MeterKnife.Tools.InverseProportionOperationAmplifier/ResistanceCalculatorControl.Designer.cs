namespace MeterKnife.Tools.InverseProportionOperationAmplifier
{
    partial class ResistanceCalculatorControl
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
            this.resistanceControl3 = new MeterKnife.Tools.InverseProportionOperationAmplifier.ResistanceControl();
            this.resistanceControl2 = new MeterKnife.Tools.InverseProportionOperationAmplifier.ResistanceControl();
            this.resistanceControl1 = new MeterKnife.Tools.InverseProportionOperationAmplifier.ResistanceControl();
            this.SuspendLayout();
            // 
            // resistanceControl3
            // 
            this.resistanceControl3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resistanceControl3.Location = new System.Drawing.Point(294, 101);
            this.resistanceControl3.Name = "resistanceControl3";
            this.resistanceControl3.Size = new System.Drawing.Size(112, 30);
            this.resistanceControl3.TabIndex = 2;
            // 
            // resistanceControl2
            // 
            this.resistanceControl2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resistanceControl2.Location = new System.Drawing.Point(184, 101);
            this.resistanceControl2.Name = "resistanceControl2";
            this.resistanceControl2.Size = new System.Drawing.Size(137, 30);
            this.resistanceControl2.TabIndex = 1;
            // 
            // resistanceControl1
            // 
            this.resistanceControl1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resistanceControl1.Location = new System.Drawing.Point(75, 101);
            this.resistanceControl1.Name = "resistanceControl1";
            this.resistanceControl1.Size = new System.Drawing.Size(167, 30);
            this.resistanceControl1.TabIndex = 0;
            // 
            // ResistanceCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resistanceControl3);
            this.Controls.Add(this.resistanceControl2);
            this.Controls.Add(this.resistanceControl1);
            this.Name = "ResistanceCalculatorControl";
            this.Size = new System.Drawing.Size(503, 205);
            this.ResumeLayout(false);

        }

        #endregion

        private ResistanceControl resistanceControl1;
        private ResistanceControl resistanceControl2;
        private ResistanceControl resistanceControl3;
    }
}
