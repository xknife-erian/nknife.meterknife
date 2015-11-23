
using MeterKnife.Workbench.Controls.Tree;

namespace MeterKnife.Workbench.Views
{
    partial class InterfaceTreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceTreeView));
            this._MeterTree = new MeterKnife.Workbench.Controls.Tree.MeterTree();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MeterTree
            // 
            this._MeterTree.AllowDrop = true;
            this._MeterTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MeterTree.FullRowSelect = true;
            this._MeterTree.ImageIndex = 0;
            this._MeterTree.ItemHeight = 22;
            this._MeterTree.Location = new System.Drawing.Point(1, 26);
            this._MeterTree.Name = "_MeterTree";
            this._MeterTree.SelectedImageIndex = 0;
            this._MeterTree.ShowLines = false;
            this._MeterTree.Size = new System.Drawing.Size(282, 313);
            this._MeterTree.TabIndex = 0;
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Location = new System.Drawing.Point(1, 339);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(282, 22);
            this._StatusStrip.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(1, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(282, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // InterfaceTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 362);
            this.Controls.Add(this._MeterTree);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._StatusStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "InterfaceTreeView";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "仪器列表";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MeterTree _MeterTree;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;

    }
}