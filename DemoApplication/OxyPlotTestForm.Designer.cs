namespace MeterKnife.DemoApplication
{
    sealed partial class OxyPlotTestForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._Panel = new System.Windows.Forms.Panel();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _Panel
            // 
            this._Panel.Controls.Add(this._SplitContainer);
            this._Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Panel.Location = new System.Drawing.Point(0, 25);
            this._Panel.Name = "_Panel";
            this._Panel.Size = new System.Drawing.Size(624, 417);
            this._Panel.TabIndex = 1;
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.Location = new System.Drawing.Point(0, 0);
            this._SplitContainer.Name = "_SplitContainer";
            this._SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this._SplitContainer.Size = new System.Drawing.Size(624, 417);
            this._SplitContainer.SplitterDistance = 208;
            this._SplitContainer.TabIndex = 0;
            // 
            // OxyPlotTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this._Panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "OxyPlotTestForm";
            this.Text = "OxyPlotTestForm";
            this._Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel _Panel;
        private System.Windows.Forms.SplitContainer _SplitContainer;
    }
}