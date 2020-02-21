namespace NKnife.MeterKnife.Workbench.Views
{
    partial class DUTView
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
            this._DUTListView = new System.Windows.Forms.ListView();
            this._NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _DUTListView
            // 
            this._DUTListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader1,
            this.columnHeader11});
            this._DUTListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DUTListView.FullRowSelect = true;
            this._DUTListView.GridLines = true;
            this._DUTListView.HideSelection = false;
            this._DUTListView.Location = new System.Drawing.Point(0, 33);
            this._DUTListView.Name = "_DUTListView";
            this._DUTListView.Size = new System.Drawing.Size(766, 577);
            this._DUTListView.TabIndex = 0;
            this._DUTListView.UseCompatibleStateImageBehavior = false;
            this._DUTListView.View = System.Windows.Forms.View.Details;
            // 
            // _NewToolStripButton
            // 
            this._NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._NewToolStripButton.Name = "_NewToolStripButton";
            this._NewToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._NewToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._NewToolStripButton.Text = "新建";
            // 
            // _EditToolStripButton
            // 
            this._EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._EditToolStripButton.Name = "_EditToolStripButton";
            this._EditToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._EditToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._EditToolStripButton.Text = "修改";
            // 
            // _DeleteToolStripButton
            // 
            this._DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._DeleteToolStripButton.Name = "_DeleteToolStripButton";
            this._DeleteToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._DeleteToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._DeleteToolStripButton.Text = "删除";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 610);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(766, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripButton,
            this._EditToolStripButton,
            this._DeleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(766, 33);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "编号";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "名称";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "分类";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "设计值";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单位";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "标定时间";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "标定值";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "描述";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "照片";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "文件";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "登记时间";
            // 
            // DUTView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(766, 632);
            this.Controls.Add(this._DUTListView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DUTView";
            this.Text = "被测物(DUT)管理";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView _DUTListView;
        private System.Windows.Forms.ToolStripButton _NewToolStripButton;
        private System.Windows.Forms.ToolStripButton _EditToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
    }
}