namespace NKnife.MeterKnife.Plots.Dialogs
{
    partial class SolutionListDialog
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
            this._IndexColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._NameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._DatasCountColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._NameTextBox = new System.Windows.Forms.TextBox();
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _ListView
            // 
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._IndexColumnHeader,
            this._NameColumnHeader,
            this._DatasCountColumnHeader});
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.Location = new System.Drawing.Point(12, 12);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(252, 232);
            this._ListView.TabIndex = 3;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // _IndexColumnHeader
            // 
            this._IndexColumnHeader.Text = "";
            this._IndexColumnHeader.Width = 30;
            // 
            // _NameColumnHeader
            // 
            this._NameColumnHeader.Text = "名称";
            this._NameColumnHeader.Width = 140;
            // 
            // _DatasCountColumnHeader
            // 
            this._DatasCountColumnHeader.Text = "数据项";
            this._DatasCountColumnHeader.Width = 50;
            // 
            // _NameTextBox
            // 
            this._NameTextBox.Location = new System.Drawing.Point(57, 250);
            this._NameTextBox.Name = "_NameTextBox";
            this._NameTextBox.Size = new System.Drawing.Size(207, 21);
            this._NameTextBox.TabIndex = 0;
            // 
            // _CancelButton
            // 
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(180, 277);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(84, 27);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消(&X)";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Location = new System.Drawing.Point(90, 277);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(84, 27);
            this._AcceptButton.TabIndex = 1;
            this._AcceptButton.Text = "确定(&C)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "样式名:";
            // 
            // SolutionListDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(276, 316);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._NameTextBox);
            this.Controls.Add(this._ListView);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SolutionListDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "数据折线样式";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.ColumnHeader _IndexColumnHeader;
        private System.Windows.Forms.ColumnHeader _NameColumnHeader;
        private System.Windows.Forms.ColumnHeader _DatasCountColumnHeader;
        private System.Windows.Forms.TextBox _NameTextBox;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Label label1;
    }
}