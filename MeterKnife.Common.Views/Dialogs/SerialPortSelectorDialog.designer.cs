
namespace NKnife.WinTool.SerialProtocolDebugger.Views.Dialogs
{
    partial class SerialPortSelectorDialog
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
            this._ListView = new NKnife.Win.Forms.SingleRowCheckedListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._AcceptButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ListView
            // 
            this._ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ListView.CheckBoxes = true;
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._ListView.HideSelection = false;
            this._ListView.Location = new System.Drawing.Point(15, 17);
            this._ListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.ShowGroups = false;
            this._ListView.Size = new System.Drawing.Size(474, 161);
            this._ListView.TabIndex = 2;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 20;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "串口";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "描述";
            this.columnHeader3.Width = 340;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(308, 188);
            this._AcceptButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(87, 38);
            this._AcceptButton.TabIndex = 0;
            this._AcceptButton.Text = "确定(&A)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(402, 188);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 38);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消(&C)";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _refreshButton
            // 
            this._refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._refreshButton.Location = new System.Drawing.Point(16, 188);
            this._refreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(87, 38);
            this._refreshButton.TabIndex = 3;
            this._refreshButton.Text = "刷新(&R)";
            this._refreshButton.UseVisualStyleBackColor = true;
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // SerialPortSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(504, 241);
            this.Controls.Add(this._refreshButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._ListView);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(137, 171);
            this.Name = "SerialPortSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择串口";
            this.ResumeLayout(false);

        }

        #endregion

        private NKnife.Win.Forms.SingleRowCheckedListView _ListView;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button _refreshButton;
    }
}