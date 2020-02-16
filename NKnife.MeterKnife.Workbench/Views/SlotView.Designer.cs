namespace NKnife.MeterKnife.Workbench.Views
{
    partial class SlotView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlotView));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.listView1 = new System.Windows.Forms.ListView();
            this._NewToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._NewCareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aglient82357ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(208, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(208, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(208, 496);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // _NewToolStripDropDownButton
            // 
            this._NewToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._NewToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewCareToolStripMenuItem,
            this.aglient82357ToolStripMenuItem,
            this.toolStripSeparator1,
            this.串口ToolStripMenuItem,
            this.tCPIPToolStripMenuItem,
            this.uSBToolStripMenuItem});
            this._NewToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("_NewToolStripDropDownButton.Image")));
            this._NewToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripDropDownButton.Name = "_NewToolStripDropDownButton";
            this._NewToolStripDropDownButton.Size = new System.Drawing.Size(45, 22);
            this._NewToolStripDropDownButton.Text = "新建";
            // 
            // _NewCareToolStripMenuItem
            // 
            this._NewCareToolStripMenuItem.Name = "_NewCareToolStripMenuItem";
            this._NewCareToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this._NewCareToolStripMenuItem.Text = "MeterCare";
            // 
            // 串口ToolStripMenuItem
            // 
            this.串口ToolStripMenuItem.Enabled = false;
            this.串口ToolStripMenuItem.Name = "串口ToolStripMenuItem";
            this.串口ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.串口ToolStripMenuItem.Text = "串口";
            // 
            // tCPIPToolStripMenuItem
            // 
            this.tCPIPToolStripMenuItem.Enabled = false;
            this.tCPIPToolStripMenuItem.Name = "tCPIPToolStripMenuItem";
            this.tCPIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tCPIPToolStripMenuItem.Text = "TCPIP";
            // 
            // uSBToolStripMenuItem
            // 
            this.uSBToolStripMenuItem.Enabled = false;
            this.uSBToolStripMenuItem.Name = "uSBToolStripMenuItem";
            this.uSBToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSBToolStripMenuItem.Text = "USB";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // aglient82357ToolStripMenuItem
            // 
            this.aglient82357ToolStripMenuItem.Enabled = false;
            this.aglient82357ToolStripMenuItem.Name = "aglient82357ToolStripMenuItem";
            this.aglient82357ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aglient82357ToolStripMenuItem.Text = "Aglient 82357";
            // 
            // SlotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 543);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SlotView";
            this.Text = "SlotView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripDropDownButton _NewToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _NewCareToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 串口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aglient82357ToolStripMenuItem;
    }
}