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
            this._NewToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._NewCareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aglient82357ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._NewSerialPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._SlotListView = new System.Windows.Forms.ListView();
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
            this._NewToolStripDropDownButton,
            this._EditToolStripButton,
            this._DeleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(208, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _NewToolStripDropDownButton
            // 
            this._NewToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._NewToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewCareToolStripMenuItem,
            this.aglient82357ToolStripMenuItem,
            this.toolStripSeparator1,
            this._NewSerialPortToolStripMenuItem,
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
            // aglient82357ToolStripMenuItem
            // 
            this.aglient82357ToolStripMenuItem.Enabled = false;
            this.aglient82357ToolStripMenuItem.Name = "aglient82357ToolStripMenuItem";
            this.aglient82357ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aglient82357ToolStripMenuItem.Text = "Aglient 82357";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // _NewSerialPortToolStripMenuItem
            // 
            this._NewSerialPortToolStripMenuItem.Enabled = false;
            this._NewSerialPortToolStripMenuItem.Name = "_NewSerialPortToolStripMenuItem";
            this._NewSerialPortToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this._NewSerialPortToolStripMenuItem.Text = "串口";
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
            // _EditToolStripButton
            // 
            this._EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._EditToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_EditToolStripButton.Image")));
            this._EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditToolStripButton.Name = "_EditToolStripButton";
            this._EditToolStripButton.Size = new System.Drawing.Size(36, 22);
            this._EditToolStripButton.Text = "修改";
            // 
            // _DeleteToolStripButton
            // 
            this._DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteToolStripButton.Image")));
            this._DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteToolStripButton.Name = "_DeleteToolStripButton";
            this._DeleteToolStripButton.Size = new System.Drawing.Size(36, 22);
            this._DeleteToolStripButton.Text = "删除";
            // 
            // _SlotListView
            // 
            this._SlotListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SlotListView.HideSelection = false;
            this._SlotListView.Location = new System.Drawing.Point(0, 25);
            this._SlotListView.MultiSelect = false;
            this._SlotListView.Name = "_SlotListView";
            this._SlotListView.Size = new System.Drawing.Size(208, 496);
            this._SlotListView.TabIndex = 2;
            this._SlotListView.UseCompatibleStateImageBehavior = false;
            // 
            // SlotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 543);
            this.Controls.Add(this._SlotListView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SlotView";
            this.Text = "接驳器管理";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView _SlotListView;
        private System.Windows.Forms.ToolStripDropDownButton _NewToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _NewCareToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _NewSerialPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aglient82357ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _EditToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteToolStripButton;
    }
}