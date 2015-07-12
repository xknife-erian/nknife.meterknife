namespace MeterKnife.DemoApplication.Dialogs
{
    partial class CustomScpiGroupDialog
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
            this.collapsibleSplitContainer1 = new NKnife.GUI.WinForm.CollapsibleSplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customerScpiCommandPanel1 = new MeterKnife.Common.Controls.CustomerScpiCommandPanel();
            ((System.ComponentModel.ISupportInitialize)(this.collapsibleSplitContainer1)).BeginInit();
            this.collapsibleSplitContainer1.Panel1.SuspendLayout();
            this.collapsibleSplitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // collapsibleSplitContainer1
            // 
            this.collapsibleSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collapsibleSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.collapsibleSplitContainer1.Name = "collapsibleSplitContainer1";
            // 
            // collapsibleSplitContainer1.Panel1
            // 
            this.collapsibleSplitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.collapsibleSplitContainer1.Size = new System.Drawing.Size(624, 442);
            this.collapsibleSplitContainer1.SplitterDistance = 250;
            this.collapsibleSplitContainer1.SplitterWidth = 22;
            this.collapsibleSplitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.customerScpiCommandPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 442);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // customerScpiCommandPanel1
            // 
            this.customerScpiCommandPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerScpiCommandPanel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.customerScpiCommandPanel1.Location = new System.Drawing.Point(3, 17);
            this.customerScpiCommandPanel1.Name = "customerScpiCommandPanel1";
            this.customerScpiCommandPanel1.Size = new System.Drawing.Size(244, 422);
            this.customerScpiCommandPanel1.TabIndex = 0;
            // 
            // CustomScpiGroupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.collapsibleSplitContainer1);
            this.Name = "CustomScpiGroupDialog";
            this.Text = "CustomScpiGroupDialog";
            this.collapsibleSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.collapsibleSplitContainer1)).EndInit();
            this.collapsibleSplitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NKnife.GUI.WinForm.CollapsibleSplitContainer collapsibleSplitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Common.Controls.CustomerScpiCommandPanel customerScpiCommandPanel1;

    }
}