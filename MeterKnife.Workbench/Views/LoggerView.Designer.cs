using NKnife.NLog3.Controls;

namespace MeterKnife.Workbench.Views
{
    partial class LoggerView
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
            this.SuspendLayout();
            // 
            // logPanel1
            // 
            this._LogPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LogPanel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._LogPanel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
            this._LogPanel.Location = new System.Drawing.Point(0, 0);
            this._LogPanel.Name = "logPanel1";
            this._LogPanel.Size = new System.Drawing.Size(784, 262);
            this._LogPanel.TabIndex = 0;
            this._LogPanel.ToolStripVisible = true;
            // 
            // LoggerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 262);
            this.Controls.Add(this._LogPanel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "LoggerView";
            this.Text = "LoggerView";
            this.ResumeLayout(false);

        }

        #endregion

        private NKnife.NLog3.Controls.LogPanel _LogPanel = LogPanel.Instance;
    }
}