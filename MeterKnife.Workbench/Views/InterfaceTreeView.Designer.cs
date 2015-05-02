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
            this._MeterTree = new MeterKnife.Common.Controls.Tree.MeterTree();
            this.SuspendLayout();
            // 
            // _MeterTree
            // 
            this._MeterTree.AllowDrop = true;
            this._MeterTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MeterTree.FullRowSelect = true;
            this._MeterTree.ImageIndex = 0;
            this._MeterTree.ItemHeight = 22;
            this._MeterTree.Location = new System.Drawing.Point(1, 1);
            this._MeterTree.Name = "_MeterTree";
            this._MeterTree.SelectedImageIndex = 0;
            this._MeterTree.ShowLines = false;
            this._MeterTree.Size = new System.Drawing.Size(282, 360);
            this._MeterTree.TabIndex = 0;
            // 
            // InterfaceTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 362);
            this.Controls.Add(this._MeterTree);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "InterfaceTreeView";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "仪器列表";
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Controls.Tree.MeterTree _MeterTree;

    }
}