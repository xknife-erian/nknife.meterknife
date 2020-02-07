namespace NKnife.MeterKnife.Plots.Dialogs
{
    partial class DataSeriesListDialog
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
            this._ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._AcceptButton = new System.Windows.Forms.Button();
            this._ModifyButton = new System.Windows.Forms.Button();
            this._AppendButton = new System.Windows.Forms.Button();
            this._DeleteButton = new System.Windows.Forms.Button();
            this._LoadButton = new System.Windows.Forms.Button();
            this._SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ListView
            // 
            this._ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7});
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.Location = new System.Drawing.Point(13, 12);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(445, 206);
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 24;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数据名称";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.Width = 65;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "线宽";
            this.columnHeader4.Width = 65;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "线颜色";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "偏移量";
            this.columnHeader7.Width = 50;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(380, 224);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(78, 27);
            this._AcceptButton.TabIndex = 1;
            this._AcceptButton.Text = "确定(&C)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // _ModifyButton
            // 
            this._ModifyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._ModifyButton.Location = new System.Drawing.Point(127, 224);
            this._ModifyButton.Name = "_ModifyButton";
            this._ModifyButton.Size = new System.Drawing.Size(55, 27);
            this._ModifyButton.TabIndex = 17;
            this._ModifyButton.Text = "修改...";
            this._ModifyButton.UseVisualStyleBackColor = true;
            this._ModifyButton.Click += new System.EventHandler(this._ModifyButton_Click);
            // 
            // _AppendButton
            // 
            this._AppendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._AppendButton.Location = new System.Drawing.Point(13, 224);
            this._AppendButton.Name = "_AppendButton";
            this._AppendButton.Size = new System.Drawing.Size(55, 27);
            this._AppendButton.TabIndex = 18;
            this._AppendButton.Text = "添加...";
            this._AppendButton.UseVisualStyleBackColor = true;
            this._AppendButton.Click += new System.EventHandler(this._AppendButton_Click);
            // 
            // _DeleteButton
            // 
            this._DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._DeleteButton.Location = new System.Drawing.Point(69, 224);
            this._DeleteButton.Name = "_DeleteButton";
            this._DeleteButton.Size = new System.Drawing.Size(55, 27);
            this._DeleteButton.TabIndex = 19;
            this._DeleteButton.Text = "删除";
            this._DeleteButton.UseVisualStyleBackColor = true;
            this._DeleteButton.Click += new System.EventHandler(this._DeleteButton_Click);
            // 
            // _LoadButton
            // 
            this._LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._LoadButton.Location = new System.Drawing.Point(196, 224);
            this._LoadButton.Name = "_LoadButton";
            this._LoadButton.Size = new System.Drawing.Size(55, 27);
            this._LoadButton.TabIndex = 20;
            this._LoadButton.Text = "载入...";
            this._LoadButton.UseVisualStyleBackColor = true;
            this._LoadButton.Click += new System.EventHandler(this._LoadButton_Click);
            // 
            // _SaveButton
            // 
            this._SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._SaveButton.Location = new System.Drawing.Point(252, 224);
            this._SaveButton.Name = "_SaveButton";
            this._SaveButton.Size = new System.Drawing.Size(55, 27);
            this._SaveButton.TabIndex = 21;
            this._SaveButton.Text = "保存...";
            this._SaveButton.UseVisualStyleBackColor = true;
            this._SaveButton.Click += new System.EventHandler(this._SaveButton_Click);
            // 
            // DataSeriesListDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 262);
            this.Controls.Add(this._SaveButton);
            this.Controls.Add(this._LoadButton);
            this.Controls.Add(this._DeleteButton);
            this.Controls.Add(this._AppendButton);
            this.Controls.Add(this._ModifyButton);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._ListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSeriesListDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "数据线选择与样式设置";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button _ModifyButton;
        private System.Windows.Forms.Button _AppendButton;
        private System.Windows.Forms.Button _DeleteButton;
        private System.Windows.Forms.Button _LoadButton;
        private System.Windows.Forms.Button _SaveButton;
    }
}