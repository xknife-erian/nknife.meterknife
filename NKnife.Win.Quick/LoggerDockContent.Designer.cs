namespace NKnife.Win.Quick
{
    partial class LoggerDockContent
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
            this.loggerListView1 = new NKnife.NLog.WinForm.LoggerListView();
            this.SuspendLayout();
            // 
            // loggerListView1
            // 
            this.loggerListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerListView1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F);
            this.loggerListView1.Location = new System.Drawing.Point(0, 0);
            this.loggerListView1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.loggerListView1.MaxDisplayCount = ((uint)(250u));
            this.loggerListView1.Name = "loggerListView1";
            this.loggerListView1.Size = new System.Drawing.Size(933, 637);
            this.loggerListView1.TabIndex = 0;
            this.loggerListView1.ToolStripVisible = true;
            // 
            // LoggerDockContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(933, 637);
            this.Controls.Add(this.loggerListView1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LoggerDockContent";
            this.Text = "LoggerDockContent";
            this.ResumeLayout(false);

        }

        #endregion

        private NLog.WinForm.LoggerListView loggerListView1;
    }
}