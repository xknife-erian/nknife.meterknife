namespace NKnife.MeterKnife.Workbench.Dialogs.Commands
{
    partial class ScpiDetailDialog
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
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._ScpiTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this._AddToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ScpiNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._CommandTextBox = new System.Windows.Forms.TextBox();
            this._ScpiDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(369, 263);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(86, 35);
            this._CancelButton.TabIndex = 7;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(277, 263);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(86, 35);
            this._AcceptButton.TabIndex = 6;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SplitContainer.Location = new System.Drawing.Point(12, 12);
            this._SplitContainer.Name = "_SplitContainer";
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._ScpiTreeView);
            this._SplitContainer.Panel1.Controls.Add(this.toolStrip3);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.Controls.Add(this._ScpiNameTextBox);
            this._SplitContainer.Panel2.Controls.Add(this.label3);
            this._SplitContainer.Panel2.Controls.Add(this._CommandTextBox);
            this._SplitContainer.Panel2.Controls.Add(this._ScpiDescriptionTextBox);
            this._SplitContainer.Panel2.Controls.Add(this.label1);
            this._SplitContainer.Panel2.Controls.Add(this.label4);
            this._SplitContainer.Panel2.Controls.Add(this.label2);
            this._SplitContainer.Size = new System.Drawing.Size(443, 245);
            this._SplitContainer.SplitterDistance = 144;
            this._SplitContainer.TabIndex = 8;
            // 
            // _ScpiTreeView
            // 
            this._ScpiTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ScpiTreeView.FullRowSelect = true;
            this._ScpiTreeView.HideSelection = false;
            this._ScpiTreeView.LabelEdit = true;
            this._ScpiTreeView.Location = new System.Drawing.Point(0, 33);
            this._ScpiTreeView.Name = "_ScpiTreeView";
            this._ScpiTreeView.Size = new System.Drawing.Size(144, 212);
            this._ScpiTreeView.TabIndex = 5;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddToolStripButton,
            this._DeleteToolStripButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(144, 33);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // _AddToolStripButton
            // 
            this._AddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._AddToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._AddToolStripButton.Name = "_AddToolStripButton";
            this._AddToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._AddToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._AddToolStripButton.Text = "新增";
            // 
            // _DeleteToolStripButton
            // 
            this._DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteToolStripButton.Enabled = false;
            this._DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._DeleteToolStripButton.Name = "_DeleteToolStripButton";
            this._DeleteToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this._DeleteToolStripButton.Size = new System.Drawing.Size(46, 31);
            this._DeleteToolStripButton.Text = "删除";
            // 
            // _ScpiNameTextBox
            // 
            this._ScpiNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiNameTextBox.Location = new System.Drawing.Point(3, 23);
            this._ScpiNameTextBox.Name = "_ScpiNameTextBox";
            this._ScpiNameTextBox.Size = new System.Drawing.Size(289, 23);
            this._ScpiNameTextBox.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "助记名";
            // 
            // _CommandTextBox
            // 
            this._CommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CommandTextBox.Location = new System.Drawing.Point(3, 67);
            this._CommandTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CommandTextBox.Multiline = true;
            this._CommandTextBox.Name = "_CommandTextBox";
            this._CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._CommandTextBox.Size = new System.Drawing.Size(289, 64);
            this._CommandTextBox.TabIndex = 20;
            // 
            // _ScpiDescriptionTextBox
            // 
            this._ScpiDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiDescriptionTextBox.Location = new System.Drawing.Point(3, 167);
            this._ScpiDescriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ScpiDescriptionTextBox.Multiline = true;
            this._ScpiDescriptionTextBox.Name = "_ScpiDescriptionTextBox";
            this._ScpiDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ScpiDescriptionTextBox.Size = new System.Drawing.Size(289, 77);
            this._ScpiDescriptionTextBox.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "指令";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "描述";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(164, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Ctrl+Enter添加新行";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScpiDetailDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(467, 310);
            this.Controls.Add(this._SplitContainer);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScpiDetailDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SCPI编辑";
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel1.PerformLayout();
            this._SplitContainer.Panel2.ResumeLayout(false);
            this._SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton _AddToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteToolStripButton;
        private System.Windows.Forms.TreeView _ScpiTreeView;
        private System.Windows.Forms.TextBox _ScpiNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _CommandTextBox;
        private System.Windows.Forms.TextBox _ScpiDescriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}