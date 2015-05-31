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
            this._MeterTree = new MeterKnife.Workbench.Controls.Tree.MeterTree();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // _MeterTree
            // 
            this._MeterTree.AllowDrop = true;
            this._MeterTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MeterTree.FullRowSelect = true;
            this._MeterTree.ImageIndex = 0;
            this._MeterTree.ItemHeight = 22;
            this._MeterTree.Location = new System.Drawing.Point(1, 25);
            this._MeterTree.Name = "_MeterTree";
            this._MeterTree.SelectedImageIndex = 0;
            this._MeterTree.ShowLines = false;
            this._MeterTree.Size = new System.Drawing.Size(282, 314);
            this._MeterTree.TabIndex = 0;
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Location = new System.Drawing.Point(1, 339);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(282, 22);
            this._StatusStrip.TabIndex = 1;
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Location = new System.Drawing.Point(1, 1);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(282, 24);
            this._MenuStrip.TabIndex = 2;
            // 
            // InterfaceTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 362);
            this.Controls.Add(this._MeterTree);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._MenuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "InterfaceTreeView";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "仪器列表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MeterTree _MeterTree;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.MenuStrip _MenuStrip;

    }
}