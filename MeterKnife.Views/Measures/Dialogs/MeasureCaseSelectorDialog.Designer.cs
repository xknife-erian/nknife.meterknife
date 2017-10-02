namespace MeterKnife.Views.Measures.Dialogs
{
    partial class MeasureCaseSelectorDialog
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._InstrumentsTree = new System.Windows.Forms.TreeView();
            this._LeftLabel = new System.Windows.Forms.Label();
            this._MeasureCaseListView = new System.Windows.Forms.ListView();
            this._RightLabel = new System.Windows.Forms.Label();
            this._InstrumentsManagerButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._InstrumentsTree);
            this.splitContainer1.Panel1.Controls.Add(this._LeftLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._MeasureCaseListView);
            this.splitContainer1.Panel2.Controls.Add(this._RightLabel);
            this.splitContainer1.Size = new System.Drawing.Size(614, 391);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // _InstrumentsTree
            // 
            this._InstrumentsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._InstrumentsTree.Location = new System.Drawing.Point(0, 24);
            this._InstrumentsTree.Name = "_InstrumentsTree";
            this._InstrumentsTree.Size = new System.Drawing.Size(204, 367);
            this._InstrumentsTree.TabIndex = 1;
            // 
            // _LeftLabel
            // 
            this._LeftLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._LeftLabel.Location = new System.Drawing.Point(0, 0);
            this._LeftLabel.Name = "_LeftLabel";
            this._LeftLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this._LeftLabel.Size = new System.Drawing.Size(204, 24);
            this._LeftLabel.TabIndex = 1;
            this._LeftLabel.Text = "仪器列表";
            this._LeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _MeasureCaseListView
            // 
            this._MeasureCaseListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MeasureCaseListView.Location = new System.Drawing.Point(0, 24);
            this._MeasureCaseListView.Name = "_MeasureCaseListView";
            this._MeasureCaseListView.Size = new System.Drawing.Size(402, 367);
            this._MeasureCaseListView.TabIndex = 1;
            this._MeasureCaseListView.UseCompatibleStateImageBehavior = false;
            // 
            // _RightLabel
            // 
            this._RightLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._RightLabel.Location = new System.Drawing.Point(0, 0);
            this._RightLabel.Name = "_RightLabel";
            this._RightLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this._RightLabel.Size = new System.Drawing.Size(402, 24);
            this._RightLabel.TabIndex = 1;
            this._RightLabel.Text = "测试案例";
            this._RightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _InstrumentsManagerButton
            // 
            this._InstrumentsManagerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._InstrumentsManagerButton.Location = new System.Drawing.Point(11, 408);
            this._InstrumentsManagerButton.Name = "_InstrumentsManagerButton";
            this._InstrumentsManagerButton.Size = new System.Drawing.Size(87, 27);
            this._InstrumentsManagerButton.TabIndex = 1;
            this._InstrumentsManagerButton.Text = "仪器管理(&M)";
            this._InstrumentsManagerButton.UseVisualStyleBackColor = true;
            this._InstrumentsManagerButton.Click += new System.EventHandler(this.InstrumentsManagerButtonClick);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(539, 409);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 27);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "取消(&X)";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(446, 409);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(87, 27);
            this._AcceptButton.TabIndex = 3;
            this._AcceptButton.Text = "确定(&C)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // MeasureCaseSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(638, 445);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._InstrumentsManagerButton);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MeasureCaseSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "测试案例选择器";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView _InstrumentsTree;
        private System.Windows.Forms.ListView _MeasureCaseListView;
        private System.Windows.Forms.Button _InstrumentsManagerButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Label _LeftLabel;
        private System.Windows.Forms.Label _RightLabel;
    }
}