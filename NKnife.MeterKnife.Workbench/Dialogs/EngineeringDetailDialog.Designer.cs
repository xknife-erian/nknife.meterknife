namespace NKnife.MeterKnife.Workbench.Dialogs
{
    partial class EngineeringDetailDialog
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("初始化", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("数据采集", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("结束", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("aaa");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("bbb");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "ccc",
            "RES",
            "Care",
            "READ?",
            "Yes",
            "0",
            "500",
            "1000"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("ddd");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("eee");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("fff");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("ggg");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineeringDetailDialog));
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this._TabControl = new System.Windows.Forms.TabControl();
            this._EngineeringTabPage = new System.Windows.Forms.TabPage();
            this._AutomaticNumberGenerationButton = new System.Windows.Forms.Button();
            this._EngDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._EngNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._EngNumberTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._CommandsTabPage = new System.Windows.Forms.TabPage();
            this._CommandsListView = new System.Windows.Forms.ListView();
            this._IndexColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._DUTColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._SlotColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._SCPIColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._IsLoopColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._LoopCountolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._IntervalColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._TimeoutColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._CommandsToolStrip = new System.Windows.Forms.ToolStrip();
            this._CreateCommandStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditCommandStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteCommandStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._UpCommandStripButton = new System.Windows.Forms.ToolStripButton();
            this._DownCommandStripButton = new System.Windows.Forms.ToolStripButton();
            this._GenerateNameOnDUTButton = new System.Windows.Forms.Button();
            this._TabControl.SuspendLayout();
            this._EngineeringTabPage.SuspendLayout();
            this._CommandsTabPage.SuspendLayout();
            this._CommandsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(686, 514);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(86, 35);
            this._CancelButton.TabIndex = 3;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(594, 514);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(86, 35);
            this._AcceptButton.TabIndex = 2;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // _TabControl
            // 
            this._TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TabControl.Controls.Add(this._EngineeringTabPage);
            this._TabControl.Controls.Add(this._CommandsTabPage);
            this._TabControl.ItemSize = new System.Drawing.Size(67, 30);
            this._TabControl.Location = new System.Drawing.Point(12, 12);
            this._TabControl.Multiline = true;
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(760, 496);
            this._TabControl.TabIndex = 4;
            // 
            // _EngineeringTabPage
            // 
            this._EngineeringTabPage.Controls.Add(this._GenerateNameOnDUTButton);
            this._EngineeringTabPage.Controls.Add(this._AutomaticNumberGenerationButton);
            this._EngineeringTabPage.Controls.Add(this._EngDescriptionTextBox);
            this._EngineeringTabPage.Controls.Add(this.label3);
            this._EngineeringTabPage.Controls.Add(this._EngNameTextBox);
            this._EngineeringTabPage.Controls.Add(this.label2);
            this._EngineeringTabPage.Controls.Add(this._EngNumberTextBox);
            this._EngineeringTabPage.Controls.Add(this.label1);
            this._EngineeringTabPage.Location = new System.Drawing.Point(4, 34);
            this._EngineeringTabPage.Name = "_EngineeringTabPage";
            this._EngineeringTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._EngineeringTabPage.Size = new System.Drawing.Size(752, 458);
            this._EngineeringTabPage.TabIndex = 0;
            this._EngineeringTabPage.Text = "工程信息";
            this._EngineeringTabPage.UseVisualStyleBackColor = true;
            // 
            // _AutomaticNumberGenerationButton
            // 
            this._AutomaticNumberGenerationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._AutomaticNumberGenerationButton.Location = new System.Drawing.Point(561, 17);
            this._AutomaticNumberGenerationButton.Name = "_AutomaticNumberGenerationButton";
            this._AutomaticNumberGenerationButton.Size = new System.Drawing.Size(154, 30);
            this._AutomaticNumberGenerationButton.TabIndex = 7;
            this._AutomaticNumberGenerationButton.Text = "自动生成";
            this._AutomaticNumberGenerationButton.UseVisualStyleBackColor = true;
            // 
            // _EngDescriptionTextBox
            // 
            this._EngDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._EngDescriptionTextBox.Location = new System.Drawing.Point(108, 85);
            this._EngDescriptionTextBox.MaxLength = 327670;
            this._EngDescriptionTextBox.Multiline = true;
            this._EngDescriptionTextBox.Name = "_EngDescriptionTextBox";
            this._EngDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._EngDescriptionTextBox.Size = new System.Drawing.Size(607, 350);
            this._EngDescriptionTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "详细描述：";
            // 
            // _EngNameTextBox
            // 
            this._EngNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._EngNameTextBox.Location = new System.Drawing.Point(108, 53);
            this._EngNameTextBox.Name = "_EngNameTextBox";
            this._EngNameTextBox.Size = new System.Drawing.Size(447, 23);
            this._EngNameTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "工程名称：";
            // 
            // _EngNumberTextBox
            // 
            this._EngNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._EngNumberTextBox.Location = new System.Drawing.Point(108, 21);
            this._EngNumberTextBox.Name = "_EngNumberTextBox";
            this._EngNumberTextBox.Size = new System.Drawing.Size(447, 23);
            this._EngNumberTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "工程编号：";
            // 
            // _CommandsTabPage
            // 
            this._CommandsTabPage.Controls.Add(this._CommandsListView);
            this._CommandsTabPage.Controls.Add(this._CommandsToolStrip);
            this._CommandsTabPage.Location = new System.Drawing.Point(4, 34);
            this._CommandsTabPage.Name = "_CommandsTabPage";
            this._CommandsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._CommandsTabPage.Size = new System.Drawing.Size(752, 458);
            this._CommandsTabPage.TabIndex = 1;
            this._CommandsTabPage.Text = "采集指令";
            this._CommandsTabPage.UseVisualStyleBackColor = true;
            // 
            // _CommandsListView
            // 
            this._CommandsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._IndexColumnHeader,
            this._DUTColumnHeader,
            this._SlotColumnHeader,
            this._SCPIColumnHeader,
            this._IsLoopColumnHeader,
            this._LoopCountolumnHeader,
            this._IntervalColumnHeader,
            this._TimeoutColumnHeader});
            this._CommandsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._CommandsListView.FullRowSelect = true;
            this._CommandsListView.GridLines = true;
            listViewGroup1.Header = "初始化";
            listViewGroup1.Name = "_InitializtionListViewGroup";
            listViewGroup2.Header = "数据采集";
            listViewGroup2.Name = "_CollectListViewGroup";
            listViewGroup3.Header = "结束";
            listViewGroup3.Name = "_FinishedListViewGroup";
            this._CommandsListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this._CommandsListView.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup2;
            listViewItem4.Group = listViewGroup2;
            listViewItem5.Group = listViewGroup2;
            listViewItem6.Group = listViewGroup3;
            listViewItem7.Group = listViewGroup3;
            this._CommandsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this._CommandsListView.Location = new System.Drawing.Point(3, 28);
            this._CommandsListView.MultiSelect = false;
            this._CommandsListView.Name = "_CommandsListView";
            this._CommandsListView.Size = new System.Drawing.Size(746, 427);
            this._CommandsListView.TabIndex = 1;
            this._CommandsListView.UseCompatibleStateImageBehavior = false;
            this._CommandsListView.View = System.Windows.Forms.View.Details;
            // 
            // _IndexColumnHeader
            // 
            this._IndexColumnHeader.Text = "序号";
            this._IndexColumnHeader.Width = 40;
            // 
            // _DUTColumnHeader
            // 
            this._DUTColumnHeader.Text = "被测物";
            this._DUTColumnHeader.Width = 80;
            // 
            // _SlotColumnHeader
            // 
            this._SlotColumnHeader.Text = "接驳器";
            // 
            // _SCPIColumnHeader
            // 
            this._SCPIColumnHeader.Text = "指令";
            this._SCPIColumnHeader.Width = 200;
            // 
            // _IsLoopColumnHeader
            // 
            this._IsLoopColumnHeader.Text = "循环";
            // 
            // _LoopCountolumnHeader
            // 
            this._LoopCountolumnHeader.Text = "循环次数";
            // 
            // _IntervalColumnHeader
            // 
            this._IntervalColumnHeader.Text = "循环间隔";
            // 
            // _TimeoutColumnHeader
            // 
            this._TimeoutColumnHeader.Text = "循环超时";
            // 
            // _CommandsToolStrip
            // 
            this._CommandsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._CommandsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CreateCommandStripButton,
            this._EditCommandStripButton,
            this._DeleteCommandStripButton,
            this.toolStripSeparator1,
            this._UpCommandStripButton,
            this._DownCommandStripButton});
            this._CommandsToolStrip.Location = new System.Drawing.Point(3, 3);
            this._CommandsToolStrip.Name = "_CommandsToolStrip";
            this._CommandsToolStrip.Size = new System.Drawing.Size(746, 25);
            this._CommandsToolStrip.TabIndex = 0;
            this._CommandsToolStrip.Text = "toolStrip1";
            // 
            // _CreateCommandStripButton
            // 
            this._CreateCommandStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._CreateCommandStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_CreateCommandStripButton.Image")));
            this._CreateCommandStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CreateCommandStripButton.Name = "_CreateCommandStripButton";
            this._CreateCommandStripButton.Size = new System.Drawing.Size(36, 22);
            this._CreateCommandStripButton.Text = "新建";
            // 
            // _EditCommandStripButton
            // 
            this._EditCommandStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._EditCommandStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_EditCommandStripButton.Image")));
            this._EditCommandStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditCommandStripButton.Name = "_EditCommandStripButton";
            this._EditCommandStripButton.Size = new System.Drawing.Size(36, 22);
            this._EditCommandStripButton.Text = "编辑";
            // 
            // _DeleteCommandStripButton
            // 
            this._DeleteCommandStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteCommandStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteCommandStripButton.Image")));
            this._DeleteCommandStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteCommandStripButton.Name = "_DeleteCommandStripButton";
            this._DeleteCommandStripButton.Size = new System.Drawing.Size(36, 22);
            this._DeleteCommandStripButton.Text = "删除";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _UpCommandStripButton
            // 
            this._UpCommandStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._UpCommandStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_UpCommandStripButton.Image")));
            this._UpCommandStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._UpCommandStripButton.Name = "_UpCommandStripButton";
            this._UpCommandStripButton.Size = new System.Drawing.Size(36, 22);
            this._UpCommandStripButton.Text = "上移";
            // 
            // _DownCommandStripButton
            // 
            this._DownCommandStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DownCommandStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DownCommandStripButton.Image")));
            this._DownCommandStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DownCommandStripButton.Name = "_DownCommandStripButton";
            this._DownCommandStripButton.Size = new System.Drawing.Size(36, 22);
            this._DownCommandStripButton.Text = "下移";
            // 
            // _GenerateNameOnDUTButton
            // 
            this._GenerateNameOnDUTButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._GenerateNameOnDUTButton.Location = new System.Drawing.Point(561, 49);
            this._GenerateNameOnDUTButton.Name = "_GenerateNameOnDUTButton";
            this._GenerateNameOnDUTButton.Size = new System.Drawing.Size(154, 30);
            this._GenerateNameOnDUTButton.TabIndex = 8;
            this._GenerateNameOnDUTButton.Text = "依据被测物生成";
            this._GenerateNameOnDUTButton.UseVisualStyleBackColor = true;
            // 
            // EngineeringDetailDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this._TabControl);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EngineeringDetailDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EngineeringDetailDialog";
            this._TabControl.ResumeLayout(false);
            this._EngineeringTabPage.ResumeLayout(false);
            this._EngineeringTabPage.PerformLayout();
            this._CommandsTabPage.ResumeLayout(false);
            this._CommandsTabPage.PerformLayout();
            this._CommandsToolStrip.ResumeLayout(false);
            this._CommandsToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.TabControl _TabControl;
        private System.Windows.Forms.TabPage _EngineeringTabPage;
        private System.Windows.Forms.TabPage _CommandsTabPage;
        private System.Windows.Forms.TextBox _EngDescriptionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _EngNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _EngNumberTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _AutomaticNumberGenerationButton;
        private System.Windows.Forms.ListView _CommandsListView;
        private System.Windows.Forms.ToolStrip _CommandsToolStrip;
        private System.Windows.Forms.ColumnHeader _IndexColumnHeader;
        private System.Windows.Forms.ColumnHeader _DUTColumnHeader;
        private System.Windows.Forms.ColumnHeader _SlotColumnHeader;
        private System.Windows.Forms.ColumnHeader _SCPIColumnHeader;
        private System.Windows.Forms.ColumnHeader _IntervalColumnHeader;
        private System.Windows.Forms.ColumnHeader _TimeoutColumnHeader;
        private System.Windows.Forms.ColumnHeader _IsLoopColumnHeader;
        private System.Windows.Forms.ColumnHeader _LoopCountolumnHeader;
        private System.Windows.Forms.ToolStripButton _CreateCommandStripButton;
        private System.Windows.Forms.ToolStripButton _EditCommandStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteCommandStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _UpCommandStripButton;
        private System.Windows.Forms.ToolStripButton _DownCommandStripButton;
        private System.Windows.Forms.Button _GenerateNameOnDUTButton;
    }
}