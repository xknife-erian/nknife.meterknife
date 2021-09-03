namespace NKnife.MeterKnife.Scpis
{
    partial class ScpiMangerForm
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
            this.customerScpiSubjectPanel1 = new MeterKnife.Scpis.CustomerScpiSubjectPanel();
            this.SuspendLayout();
            // 
            // customerScpiSubjectPanel1
            // 
            this.customerScpiSubjectPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerScpiSubjectPanel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.customerScpiSubjectPanel1.GpibAddress = 0;
            this.customerScpiSubjectPanel1.Location = new System.Drawing.Point(0, 0);
            this.customerScpiSubjectPanel1.Name = "customerScpiSubjectPanel1";
            this.customerScpiSubjectPanel1.Size = new System.Drawing.Size(659, 606);
            this.customerScpiSubjectPanel1.TabIndex = 0;
            // 
            // ScpiMangerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 606);
            this.Controls.Add(this.customerScpiSubjectPanel1);
            this.Name = "ScpiMangerForm";
            this.Text = "ScpiMangerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomerScpiSubjectPanel customerScpiSubjectPanel1;
    }
}