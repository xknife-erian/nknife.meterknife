namespace NKnife.MeterKnife.Workbench.Dialogs.Plots
{
    partial class ThemeManagerDialog
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
            this._BottomGridLineGroupBox = new System.Windows.Forms.GroupBox();
            this._BottomAxisGridLineMinorColor = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this._BottomAxisGridLineMajorColor = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.singleLine1 = new NKnife.Win.Forms.SingleLine();
            this._AreaBackground = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this._ViewBackground = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this.label5 = new System.Windows.Forms.Label();
            this._LeftGridLineGroupBox = new System.Windows.Forms.GroupBox();
            this._LeftAxisGridLineMinorColor = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this._LeftAxisGridLineMajorColor = new NKnife.MeterKnife.Workbench.Controls.ThemeColorItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._ThemeListComboBox = new System.Windows.Forms.ComboBox();
            this._NewThemeButton = new System.Windows.Forms.Button();
            this._DeleteThemeButton = new System.Windows.Forms.Button();
            this._EnableThemeButton = new System.Windows.Forms.Button();
            this._CloseButton = new System.Windows.Forms.Button();
            this._BottomGridLineGroupBox.SuspendLayout();
            this._LeftGridLineGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _BottomGridLineGroupBox
            // 
            this._BottomGridLineGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._BottomGridLineGroupBox.Controls.Add(this._BottomAxisGridLineMinorColor);
            this._BottomGridLineGroupBox.Controls.Add(this._BottomAxisGridLineMajorColor);
            this._BottomGridLineGroupBox.Controls.Add(this.label8);
            this._BottomGridLineGroupBox.Controls.Add(this.label9);
            this._BottomGridLineGroupBox.Location = new System.Drawing.Point(19, 222);
            this._BottomGridLineGroupBox.Name = "_BottomGridLineGroupBox";
            this._BottomGridLineGroupBox.Size = new System.Drawing.Size(357, 76);
            this._BottomGridLineGroupBox.TabIndex = 28;
            this._BottomGridLineGroupBox.TabStop = false;
            this._BottomGridLineGroupBox.Text = "底部数轴网格线";
            // 
            // _BottomAxisGridLineMinorColor
            // 
            this._BottomAxisGridLineMinorColor.BackColor = System.Drawing.Color.White;
            this._BottomAxisGridLineMinorColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._BottomAxisGridLineMinorColor.Color = System.Drawing.Color.White;
            this._BottomAxisGridLineMinorColor.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._BottomAxisGridLineMinorColor.Location = new System.Drawing.Point(96, 44);
            this._BottomAxisGridLineMinorColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._BottomAxisGridLineMinorColor.Name = "_BottomAxisGridLineMinorColor";
            this._BottomAxisGridLineMinorColor.Size = new System.Drawing.Size(80, 23);
            this._BottomAxisGridLineMinorColor.TabIndex = 1;
            // 
            // _BottomAxisGridLineMajorColor
            // 
            this._BottomAxisGridLineMajorColor.BackColor = System.Drawing.Color.White;
            this._BottomAxisGridLineMajorColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._BottomAxisGridLineMajorColor.Color = System.Drawing.Color.White;
            this._BottomAxisGridLineMajorColor.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._BottomAxisGridLineMajorColor.Location = new System.Drawing.Point(96, 18);
            this._BottomAxisGridLineMajorColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._BottomAxisGridLineMajorColor.Name = "_BottomAxisGridLineMajorColor";
            this._BottomAxisGridLineMajorColor.Size = new System.Drawing.Size(80, 23);
            this._BottomAxisGridLineMajorColor.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "突出显示";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 48);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "一般显示";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(247, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "线径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "颜色";
            // 
            // singleLine1
            // 
            this.singleLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.singleLine1.Location = new System.Drawing.Point(19, 75);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Size = new System.Drawing.Size(358, 1);
            this.singleLine1.TabIndex = 25;
            this.singleLine1.TabStop = false;
            // 
            // _AreaBackground
            // 
            this._AreaBackground.BackColor = System.Drawing.Color.White;
            this._AreaBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._AreaBackground.Color = System.Drawing.Color.White;
            this._AreaBackground.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._AreaBackground.Location = new System.Drawing.Point(115, 111);
            this._AreaBackground.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._AreaBackground.Name = "_AreaBackground";
            this._AreaBackground.Size = new System.Drawing.Size(80, 23);
            this._AreaBackground.TabIndex = 5;
            // 
            // _ViewBackground
            // 
            this._ViewBackground.BackColor = System.Drawing.Color.White;
            this._ViewBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ViewBackground.Color = System.Drawing.Color.White;
            this._ViewBackground.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._ViewBackground.Location = new System.Drawing.Point(115, 85);
            this._ViewBackground.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._ViewBackground.Name = "_ViewBackground";
            this._ViewBackground.Size = new System.Drawing.Size(80, 23);
            this._ViewBackground.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "主题:";
            // 
            // _LeftGridLineGroupBox
            // 
            this._LeftGridLineGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._LeftGridLineGroupBox.Controls.Add(this._LeftAxisGridLineMinorColor);
            this._LeftGridLineGroupBox.Controls.Add(this._LeftAxisGridLineMajorColor);
            this._LeftGridLineGroupBox.Controls.Add(this.label3);
            this._LeftGridLineGroupBox.Controls.Add(this.label4);
            this._LeftGridLineGroupBox.Location = new System.Drawing.Point(19, 140);
            this._LeftGridLineGroupBox.Name = "_LeftGridLineGroupBox";
            this._LeftGridLineGroupBox.Size = new System.Drawing.Size(357, 76);
            this._LeftGridLineGroupBox.TabIndex = 20;
            this._LeftGridLineGroupBox.TabStop = false;
            this._LeftGridLineGroupBox.Text = "左侧数轴网格线";
            // 
            // _LeftAxisGridLineMinorColor
            // 
            this._LeftAxisGridLineMinorColor.BackColor = System.Drawing.Color.White;
            this._LeftAxisGridLineMinorColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LeftAxisGridLineMinorColor.Color = System.Drawing.Color.White;
            this._LeftAxisGridLineMinorColor.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._LeftAxisGridLineMinorColor.Location = new System.Drawing.Point(96, 44);
            this._LeftAxisGridLineMinorColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._LeftAxisGridLineMinorColor.Name = "_LeftAxisGridLineMinorColor";
            this._LeftAxisGridLineMinorColor.Size = new System.Drawing.Size(80, 23);
            this._LeftAxisGridLineMinorColor.TabIndex = 1;
            // 
            // _LeftAxisGridLineMajorColor
            // 
            this._LeftAxisGridLineMajorColor.BackColor = System.Drawing.Color.White;
            this._LeftAxisGridLineMajorColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LeftAxisGridLineMajorColor.Color = System.Drawing.Color.White;
            this._LeftAxisGridLineMajorColor.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._LeftAxisGridLineMajorColor.Location = new System.Drawing.Point(96, 18);
            this._LeftAxisGridLineMajorColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._LeftAxisGridLineMajorColor.Name = "_LeftAxisGridLineMajorColor";
            this._LeftAxisGridLineMajorColor.Size = new System.Drawing.Size(80, 23);
            this._LeftAxisGridLineMajorColor.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "突出显示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "一般显示";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "图表区";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "图表画框区";
            // 
            // _ThemeListComboBox
            // 
            this._ThemeListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ThemeListComboBox.FormattingEnabled = true;
            this._ThemeListComboBox.Location = new System.Drawing.Point(52, 19);
            this._ThemeListComboBox.Name = "_ThemeListComboBox";
            this._ThemeListComboBox.Size = new System.Drawing.Size(133, 25);
            this._ThemeListComboBox.TabIndex = 0;
            // 
            // _NewThemeButton
            // 
            this._NewThemeButton.Location = new System.Drawing.Point(191, 19);
            this._NewThemeButton.Name = "_NewThemeButton";
            this._NewThemeButton.Size = new System.Drawing.Size(63, 25);
            this._NewThemeButton.TabIndex = 2;
            this._NewThemeButton.Text = "新建";
            this._NewThemeButton.UseVisualStyleBackColor = true;
            // 
            // _DeleteThemeButton
            // 
            this._DeleteThemeButton.Location = new System.Drawing.Point(254, 19);
            this._DeleteThemeButton.Name = "_DeleteThemeButton";
            this._DeleteThemeButton.Size = new System.Drawing.Size(63, 25);
            this._DeleteThemeButton.TabIndex = 3;
            this._DeleteThemeButton.Text = "删除";
            this._DeleteThemeButton.UseVisualStyleBackColor = true;
            // 
            // _EnableThemeButton
            // 
            this._EnableThemeButton.Location = new System.Drawing.Point(317, 19);
            this._EnableThemeButton.Name = "_EnableThemeButton";
            this._EnableThemeButton.Size = new System.Drawing.Size(63, 25);
            this._EnableThemeButton.TabIndex = 1;
            this._EnableThemeButton.Text = "启用";
            this._EnableThemeButton.UseVisualStyleBackColor = true;
            // 
            // _CloseButton
            // 
            this._CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CloseButton.Location = new System.Drawing.Point(287, 304);
            this._CloseButton.Name = "_CloseButton";
            this._CloseButton.Size = new System.Drawing.Size(90, 27);
            this._CloseButton.TabIndex = 30;
            this._CloseButton.Text = "完成(&C)";
            this._CloseButton.UseVisualStyleBackColor = true;
            // 
            // ThemeManagerDialog
            // 
            this.AcceptButton = this._CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(393, 351);
            this.Controls.Add(this._CloseButton);
            this.Controls.Add(this._EnableThemeButton);
            this.Controls.Add(this._DeleteThemeButton);
            this.Controls.Add(this._NewThemeButton);
            this.Controls.Add(this._ThemeListComboBox);
            this.Controls.Add(this._BottomGridLineGroupBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.singleLine1);
            this.Controls.Add(this._AreaBackground);
            this.Controls.Add(this._ViewBackground);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._LeftGridLineGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemeManagerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "主题管理";
            this._BottomGridLineGroupBox.ResumeLayout(false);
            this._BottomGridLineGroupBox.PerformLayout();
            this._LeftGridLineGroupBox.ResumeLayout(false);
            this._LeftGridLineGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _BottomGridLineGroupBox;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _BottomAxisGridLineMinorColor;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _BottomAxisGridLineMajorColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private NKnife.Win.Forms.SingleLine singleLine1;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _AreaBackground;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _ViewBackground;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox _LeftGridLineGroupBox;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _LeftAxisGridLineMinorColor;
        private NKnife.MeterKnife.Workbench.Controls.ThemeColorItem _LeftAxisGridLineMajorColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _ThemeListComboBox;
        private System.Windows.Forms.Button _NewThemeButton;
        private System.Windows.Forms.Button _DeleteThemeButton;
        private System.Windows.Forms.Button _EnableThemeButton;
        private System.Windows.Forms.Button _CloseButton;
    }
}