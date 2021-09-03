
namespace NKnife.MeterKnife.Workbench.Dialogs.Slots
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
            this._AcceptButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._RefreshButton = new System.Windows.Forms.Button();
            this._SerialPortListView = new NKnife.Win.Forms.SingleRowCheckedListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._SerialParamsGroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._DTRCheckBox = new System.Windows.Forms.CheckBox();
            this._RTSCheckBox = new System.Windows.Forms.CheckBox();
            this._ParityComboBox = new System.Windows.Forms.ComboBox();
            this._DataBitsComboBox = new System.Windows.Forms.ComboBox();
            this._StopBitsComboBox = new System.Windows.Forms.ComboBox();
            this._BaudRateComboBox = new System.Windows.Forms.ComboBox();
            this._SerialParamsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(308, 330);
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
            this._CancelButton.Location = new System.Drawing.Point(402, 330);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 38);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消(&C)";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _RefreshButton
            // 
            this._RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._RefreshButton.Location = new System.Drawing.Point(16, 330);
            this._RefreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._RefreshButton.Name = "_RefreshButton";
            this._RefreshButton.Size = new System.Drawing.Size(87, 38);
            this._RefreshButton.TabIndex = 3;
            this._RefreshButton.Text = "刷新(&R)";
            this._RefreshButton.UseVisualStyleBackColor = true;
            // 
            // _SerialPortListView
            // 
            this._SerialPortListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SerialPortListView.CheckBoxes = true;
            this._SerialPortListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this._SerialPortListView.FullRowSelect = true;
            this._SerialPortListView.GridLines = true;
            this._SerialPortListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._SerialPortListView.HideSelection = false;
            this._SerialPortListView.Location = new System.Drawing.Point(15, 17);
            this._SerialPortListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SerialPortListView.MultiSelect = false;
            this._SerialPortListView.Name = "_SerialPortListView";
            this._SerialPortListView.ShowGroups = false;
            this._SerialPortListView.Size = new System.Drawing.Size(474, 176);
            this._SerialPortListView.TabIndex = 2;
            this._SerialPortListView.UseCompatibleStateImageBehavior = false;
            this._SerialPortListView.View = System.Windows.Forms.View.Details;
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
            // _SerialParamsGroupBox
            // 
            this._SerialParamsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SerialParamsGroupBox.Controls.Add(this.label5);
            this._SerialParamsGroupBox.Controls.Add(this.label4);
            this._SerialParamsGroupBox.Controls.Add(this.label3);
            this._SerialParamsGroupBox.Controls.Add(this.label2);
            this._SerialParamsGroupBox.Controls.Add(this._DTRCheckBox);
            this._SerialParamsGroupBox.Controls.Add(this._RTSCheckBox);
            this._SerialParamsGroupBox.Controls.Add(this._ParityComboBox);
            this._SerialParamsGroupBox.Controls.Add(this._DataBitsComboBox);
            this._SerialParamsGroupBox.Controls.Add(this._StopBitsComboBox);
            this._SerialParamsGroupBox.Controls.Add(this._BaudRateComboBox);
            this._SerialParamsGroupBox.Location = new System.Drawing.Point(16, 207);
            this._SerialParamsGroupBox.Name = "_SerialParamsGroupBox";
            this._SerialParamsGroupBox.Size = new System.Drawing.Size(473, 116);
            this._SerialParamsGroupBox.TabIndex = 4;
            this._SerialParamsGroupBox.TabStop = false;
            this._SerialParamsGroupBox.Text = "串口参数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "奇偶校验";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "数据位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 45;
            this.label3.Text = "停止位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 44;
            this.label2.Text = "波特率";
            // 
            // _DTRCheckBox
            // 
            this._DTRCheckBox.AutoSize = true;
            this._DTRCheckBox.Location = new System.Drawing.Point(79, 81);
            this._DTRCheckBox.Name = "_DTRCheckBox";
            this._DTRCheckBox.Size = new System.Drawing.Size(51, 21);
            this._DTRCheckBox.TabIndex = 43;
            this._DTRCheckBox.Text = "DTR";
            this._DTRCheckBox.UseVisualStyleBackColor = true;
            // 
            // _RTSCheckBox
            // 
            this._RTSCheckBox.AutoSize = true;
            this._RTSCheckBox.Location = new System.Drawing.Point(30, 81);
            this._RTSCheckBox.Name = "_RTSCheckBox";
            this._RTSCheckBox.Size = new System.Drawing.Size(49, 21);
            this._RTSCheckBox.TabIndex = 42;
            this._RTSCheckBox.Text = "RTS";
            this._RTSCheckBox.UseVisualStyleBackColor = true;
            // 
            // _ParityComboBox
            // 
            this._ParityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ParityComboBox.FormattingEnabled = true;
            this._ParityComboBox.Location = new System.Drawing.Point(348, 50);
            this._ParityComboBox.Name = "_ParityComboBox";
            this._ParityComboBox.Size = new System.Drawing.Size(100, 25);
            this._ParityComboBox.TabIndex = 41;
            // 
            // _DataBitsComboBox
            // 
            this._DataBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._DataBitsComboBox.FormattingEnabled = true;
            this._DataBitsComboBox.Location = new System.Drawing.Point(242, 50);
            this._DataBitsComboBox.Name = "_DataBitsComboBox";
            this._DataBitsComboBox.Size = new System.Drawing.Size(100, 25);
            this._DataBitsComboBox.TabIndex = 40;
            // 
            // _StopBitsComboBox
            // 
            this._StopBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._StopBitsComboBox.FormattingEnabled = true;
            this._StopBitsComboBox.Location = new System.Drawing.Point(136, 50);
            this._StopBitsComboBox.Name = "_StopBitsComboBox";
            this._StopBitsComboBox.Size = new System.Drawing.Size(100, 25);
            this._StopBitsComboBox.TabIndex = 39;
            // 
            // _BaudRateComboBox
            // 
            this._BaudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._BaudRateComboBox.FormattingEnabled = true;
            this._BaudRateComboBox.Location = new System.Drawing.Point(30, 50);
            this._BaudRateComboBox.Name = "_BaudRateComboBox";
            this._BaudRateComboBox.Size = new System.Drawing.Size(100, 25);
            this._BaudRateComboBox.TabIndex = 4;
            // 
            // SerialPortSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(504, 383);
            this.Controls.Add(this._SerialParamsGroupBox);
            this.Controls.Add(this._RefreshButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._SerialPortListView);
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
            this._SerialParamsGroupBox.ResumeLayout(false);
            this._SerialParamsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NKnife.Win.Forms.SingleRowCheckedListView _SerialPortListView;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button _RefreshButton;
        private System.Windows.Forms.GroupBox _SerialParamsGroupBox;
        private System.Windows.Forms.ComboBox _BaudRateComboBox;
        private System.Windows.Forms.ComboBox _StopBitsComboBox;
        private System.Windows.Forms.ComboBox _DataBitsComboBox;
        private System.Windows.Forms.ComboBox _ParityComboBox;
        private System.Windows.Forms.CheckBox _DTRCheckBox;
        private System.Windows.Forms.CheckBox _RTSCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}