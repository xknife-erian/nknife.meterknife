namespace NKnife.MeterKnife.Workbench.Dialogs.Instruments
{
    partial class InstrumentDetailDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrumentDetailDialog));
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._FilesTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._AddFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._PhotoListView = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._AddPhotoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeletePhotoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._SubModelTextBox = new System.Windows.Forms.TextBox();
            this._ModelTextBox = new System.Windows.Forms.TextBox();
            this._AbbrNameTextBox = new System.Windows.Forms.TextBox();
            this._DescriptionTextBox = new System.Windows.Forms.TextBox();
            this._NameTextBox = new System.Windows.Forms.TextBox();
            this._NumberTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._UseClassificationComboBox = new System.Windows.Forms.ComboBox();
            this._ManufacturerComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._AutoNumberButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._ScpiTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this._AddScpiToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteScpiToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ScpiNameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this._ScpiCommandTextBox = new System.Windows.Forms.TextBox();
            this._ScpiDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this._NetInfoLabel = new System.Windows.Forms.Label();
            this._GetInstrumentScpiTemplateButton = new System.Windows.Forms.Button();
            this._FilesStatusStrip = new System.Windows.Forms.StatusStrip();
            this._PhotosStatusStrip = new System.Windows.Forms.StatusStrip();
            this._PhotosPathToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._FilesPathToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this._FilesStatusStrip.SuspendLayout();
            this._PhotosStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(369, 453);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 35);
            this._CancelButton.TabIndex = 5;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(288, 453);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(75, 35);
            this._AcceptButton.TabIndex = 4;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._FilesTreeView);
            this.tabPage4.Controls.Add(this._FilesStatusStrip);
            this.tabPage4.Controls.Add(this.toolStrip2);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(428, 379);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "相关文件";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _FilesTreeView
            // 
            this._FilesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._FilesTreeView.Location = new System.Drawing.Point(3, 34);
            this._FilesTreeView.Name = "_FilesTreeView";
            this._FilesTreeView.Size = new System.Drawing.Size(422, 320);
            this._FilesTreeView.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddFileToolStripButton,
            this._DeleteFileToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(422, 31);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _AddFileToolStripButton
            // 
            this._AddFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AddFileToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.add_24px;
            this._AddFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddFileToolStripButton.Name = "_AddFileToolStripButton";
            this._AddFileToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._AddFileToolStripButton.Text = "新增";
            // 
            // _DeleteFileToolStripButton
            // 
            this._DeleteFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteFileToolStripButton.Image")));
            this._DeleteFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteFileToolStripButton.Name = "_DeleteFileToolStripButton";
            this._DeleteFileToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._DeleteFileToolStripButton.Text = "删除";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "新项选择";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.miniToolStrip.Location = new System.Drawing.Point(69, 3);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(405, 25);
            this.miniToolStrip.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._PhotoListView);
            this.tabPage3.Controls.Add(this._PhotosStatusStrip);
            this.tabPage3.Controls.Add(this.toolStrip1);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(428, 379);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "实物照片";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _PhotoListView
            // 
            this._PhotoListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PhotoListView.HideSelection = false;
            this._PhotoListView.Location = new System.Drawing.Point(3, 34);
            this._PhotoListView.Name = "_PhotoListView";
            this._PhotoListView.Size = new System.Drawing.Size(422, 320);
            this._PhotoListView.TabIndex = 0;
            this._PhotoListView.UseCompatibleStateImageBehavior = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddPhotoToolStripButton,
            this._DeletePhotoToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(422, 31);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _AddPhotoToolStripButton
            // 
            this._AddPhotoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AddPhotoToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.add_24px;
            this._AddPhotoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddPhotoToolStripButton.Name = "_AddPhotoToolStripButton";
            this._AddPhotoToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._AddPhotoToolStripButton.Text = "新增";
            // 
            // _DeletePhotoToolStripButton
            // 
            this._DeletePhotoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeletePhotoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeletePhotoToolStripButton.Image")));
            this._DeletePhotoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeletePhotoToolStripButton.Name = "_DeletePhotoToolStripButton";
            this._DeletePhotoToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._DeletePhotoToolStripButton.Text = "删除";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this._SubModelTextBox);
            this.tabPage1.Controls.Add(this._ModelTextBox);
            this.tabPage1.Controls.Add(this._AbbrNameTextBox);
            this.tabPage1.Controls.Add(this._DescriptionTextBox);
            this.tabPage1.Controls.Add(this._NameTextBox);
            this.tabPage1.Controls.Add(this._NumberTextBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this._UseClassificationComboBox);
            this.tabPage1.Controls.Add(this._ManufacturerComboBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this._AutoNumberButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(428, 379);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(221, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "子型号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "型号";
            // 
            // _SubModelTextBox
            // 
            this._SubModelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SubModelTextBox.Location = new System.Drawing.Point(223, 198);
            this._SubModelTextBox.Name = "_SubModelTextBox";
            this._SubModelTextBox.Size = new System.Drawing.Size(183, 23);
            this._SubModelTextBox.TabIndex = 15;
            // 
            // _ModelTextBox
            // 
            this._ModelTextBox.Location = new System.Drawing.Point(21, 198);
            this._ModelTextBox.Name = "_ModelTextBox";
            this._ModelTextBox.Size = new System.Drawing.Size(185, 23);
            this._ModelTextBox.TabIndex = 14;
            // 
            // _AbbrNameTextBox
            // 
            this._AbbrNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._AbbrNameTextBox.Location = new System.Drawing.Point(224, 93);
            this._AbbrNameTextBox.Name = "_AbbrNameTextBox";
            this._AbbrNameTextBox.Size = new System.Drawing.Size(182, 23);
            this._AbbrNameTextBox.TabIndex = 13;
            // 
            // _DescriptionTextBox
            // 
            this._DescriptionTextBox.AcceptsReturn = true;
            this._DescriptionTextBox.AcceptsTab = true;
            this._DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DescriptionTextBox.Location = new System.Drawing.Point(22, 250);
            this._DescriptionTextBox.MaxLength = 327670;
            this._DescriptionTextBox.Multiline = true;
            this._DescriptionTextBox.Name = "_DescriptionTextBox";
            this._DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._DescriptionTextBox.Size = new System.Drawing.Size(384, 108);
            this._DescriptionTextBox.TabIndex = 12;
            // 
            // _NameTextBox
            // 
            this._NameTextBox.Location = new System.Drawing.Point(22, 93);
            this._NameTextBox.Name = "_NameTextBox";
            this._NameTextBox.Size = new System.Drawing.Size(185, 23);
            this._NameTextBox.TabIndex = 4;
            // 
            // _NumberTextBox
            // 
            this._NumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._NumberTextBox.Location = new System.Drawing.Point(22, 41);
            this._NumberTextBox.Name = "_NumberTextBox";
            this._NumberTextBox.Size = new System.Drawing.Size(280, 23);
            this._NumberTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "详细描述";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "用途";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "生产厂商";
            // 
            // _UseClassificationComboBox
            // 
            this._UseClassificationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._UseClassificationComboBox.FormattingEnabled = true;
            this._UseClassificationComboBox.Location = new System.Drawing.Point(224, 144);
            this._UseClassificationComboBox.Name = "_UseClassificationComboBox";
            this._UseClassificationComboBox.Size = new System.Drawing.Size(182, 25);
            this._UseClassificationComboBox.TabIndex = 8;
            // 
            // _ManufacturerComboBox
            // 
            this._ManufacturerComboBox.FormattingEnabled = true;
            this._ManufacturerComboBox.Location = new System.Drawing.Point(22, 144);
            this._ManufacturerComboBox.Name = "_ManufacturerComboBox";
            this._ManufacturerComboBox.Size = new System.Drawing.Size(185, 25);
            this._ManufacturerComboBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "简称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "名称";
            // 
            // _AutoNumberButton
            // 
            this._AutoNumberButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._AutoNumberButton.Location = new System.Drawing.Point(308, 38);
            this._AutoNumberButton.Name = "_AutoNumberButton";
            this._AutoNumberButton.Size = new System.Drawing.Size(98, 29);
            this._AutoNumberButton.TabIndex = 2;
            this._AutoNumberButton.Text = "自动生成";
            this._AutoNumberButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.ItemSize = new System.Drawing.Size(67, 30);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(436, 417);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._SplitContainer);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(428, 379);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "SCPI指令";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.Location = new System.Drawing.Point(3, 3);
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
            this._SplitContainer.Panel2.Controls.Add(this.label9);
            this._SplitContainer.Panel2.Controls.Add(this._ScpiCommandTextBox);
            this._SplitContainer.Panel2.Controls.Add(this._ScpiDescriptionTextBox);
            this._SplitContainer.Panel2.Controls.Add(this.label10);
            this._SplitContainer.Panel2.Controls.Add(this.label11);
            this._SplitContainer.Panel2.Controls.Add(this.label12);
            this._SplitContainer.Size = new System.Drawing.Size(422, 373);
            this._SplitContainer.SplitterDistance = 154;
            this._SplitContainer.TabIndex = 9;
            // 
            // _ScpiTreeView
            // 
            this._ScpiTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ScpiTreeView.FullRowSelect = true;
            this._ScpiTreeView.HideSelection = false;
            this._ScpiTreeView.LabelEdit = true;
            this._ScpiTreeView.Location = new System.Drawing.Point(0, 31);
            this._ScpiTreeView.Name = "_ScpiTreeView";
            this._ScpiTreeView.Size = new System.Drawing.Size(154, 342);
            this._ScpiTreeView.TabIndex = 5;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddScpiToolStripButton,
            this._DeleteScpiToolStripButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(154, 31);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // _AddScpiToolStripButton
            // 
            this._AddScpiToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AddScpiToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.add_24px;
            this._AddScpiToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddScpiToolStripButton.Name = "_AddScpiToolStripButton";
            this._AddScpiToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._AddScpiToolStripButton.Text = "新增";
            // 
            // _DeleteScpiToolStripButton
            // 
            this._DeleteScpiToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteScpiToolStripButton.Enabled = false;
            this._DeleteScpiToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteScpiToolStripButton.Image")));
            this._DeleteScpiToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteScpiToolStripButton.Name = "_DeleteScpiToolStripButton";
            this._DeleteScpiToolStripButton.Size = new System.Drawing.Size(28, 28);
            this._DeleteScpiToolStripButton.Text = "删除";
            // 
            // _ScpiNameTextBox
            // 
            this._ScpiNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiNameTextBox.Location = new System.Drawing.Point(8, 23);
            this._ScpiNameTextBox.Name = "_ScpiNameTextBox";
            this._ScpiNameTextBox.Size = new System.Drawing.Size(253, 23);
            this._ScpiNameTextBox.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 25;
            this.label9.Text = "助记名";
            // 
            // _ScpiCommandTextBox
            // 
            this._ScpiCommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiCommandTextBox.Location = new System.Drawing.Point(8, 67);
            this._ScpiCommandTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ScpiCommandTextBox.Multiline = true;
            this._ScpiCommandTextBox.Name = "_ScpiCommandTextBox";
            this._ScpiCommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ScpiCommandTextBox.Size = new System.Drawing.Size(253, 106);
            this._ScpiCommandTextBox.TabIndex = 20;
            // 
            // _ScpiDescriptionTextBox
            // 
            this._ScpiDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ScpiDescriptionTextBox.Location = new System.Drawing.Point(8, 210);
            this._ScpiDescriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ScpiDescriptionTextBox.Multiline = true;
            this._ScpiDescriptionTextBox.Name = "_ScpiDescriptionTextBox";
            this._ScpiDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ScpiDescriptionTextBox.Size = new System.Drawing.Size(253, 163);
            this._ScpiDescriptionTextBox.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "指令";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 191);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 17);
            this.label11.TabIndex = 24;
            this.label11.Text = "描述";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label12.Location = new System.Drawing.Point(103, 177);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 17);
            this.label12.TabIndex = 22;
            this.label12.Text = "Ctrl+Enter添加新行";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _NetInfoLabel
            // 
            this._NetInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._NetInfoLabel.AutoSize = true;
            this._NetInfoLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this._NetInfoLabel.Location = new System.Drawing.Point(17, 434);
            this._NetInfoLabel.Name = "_NetInfoLabel";
            this._NetInfoLabel.Size = new System.Drawing.Size(11, 17);
            this._NetInfoLabel.TabIndex = 8;
            this._NetInfoLabel.Text = ".";
            // 
            // _GetInstrumentScpiTemplateButton
            // 
            this._GetInstrumentScpiTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._GetInstrumentScpiTemplateButton.Enabled = false;
            this._GetInstrumentScpiTemplateButton.Location = new System.Drawing.Point(16, 453);
            this._GetInstrumentScpiTemplateButton.Name = "_GetInstrumentScpiTemplateButton";
            this._GetInstrumentScpiTemplateButton.Size = new System.Drawing.Size(116, 35);
            this._GetInstrumentScpiTemplateButton.TabIndex = 9;
            this._GetInstrumentScpiTemplateButton.Text = "从云端获取";
            this._GetInstrumentScpiTemplateButton.UseVisualStyleBackColor = true;
            // 
            // _FilesStatusStrip
            // 
            this._FilesStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FilesPathToolStripStatusLabel});
            this._FilesStatusStrip.Location = new System.Drawing.Point(3, 354);
            this._FilesStatusStrip.Name = "_FilesStatusStrip";
            this._FilesStatusStrip.Size = new System.Drawing.Size(422, 22);
            this._FilesStatusStrip.SizingGrip = false;
            this._FilesStatusStrip.TabIndex = 5;
            // 
            // _PhotosStatusStrip
            // 
            this._PhotosStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._PhotosPathToolStripStatusLabel});
            this._PhotosStatusStrip.Location = new System.Drawing.Point(3, 354);
            this._PhotosStatusStrip.Name = "_PhotosStatusStrip";
            this._PhotosStatusStrip.Size = new System.Drawing.Size(422, 22);
            this._PhotosStatusStrip.SizingGrip = false;
            this._PhotosStatusStrip.TabIndex = 5;
            this._PhotosStatusStrip.Text = "statusStrip2";
            // 
            // _PhotosPathToolStripStatusLabel
            // 
            this._PhotosPathToolStripStatusLabel.Name = "_PhotosPathToolStripStatusLabel";
            this._PhotosPathToolStripStatusLabel.Size = new System.Drawing.Size(11, 17);
            this._PhotosPathToolStripStatusLabel.Text = ".";
            // 
            // _FilesPathToolStripStatusLabel
            // 
            this._FilesPathToolStripStatusLabel.Name = "_FilesPathToolStripStatusLabel";
            this._FilesPathToolStripStatusLabel.Size = new System.Drawing.Size(11, 17);
            this._FilesPathToolStripStatusLabel.Text = ".";
            // 
            // InstrumentDetailDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(460, 500);
            this.Controls.Add(this._GetInstrumentScpiTemplateButton);
            this.Controls.Add(this._NetInfoLabel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstrumentDetailDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "仪器详细信息";
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel1.PerformLayout();
            this._SplitContainer.Panel2.ResumeLayout(false);
            this._SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this._FilesStatusStrip.ResumeLayout(false);
            this._FilesStatusStrip.PerformLayout();
            this._PhotosStatusStrip.ResumeLayout(false);
            this._PhotosStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TreeView _FilesTreeView;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView _PhotoListView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _SubModelTextBox;
        private System.Windows.Forms.TextBox _ModelTextBox;
        private System.Windows.Forms.TextBox _AbbrNameTextBox;
        private System.Windows.Forms.TextBox _DescriptionTextBox;
        private System.Windows.Forms.TextBox _NameTextBox;
        private System.Windows.Forms.TextBox _NumberTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _UseClassificationComboBox;
        private System.Windows.Forms.ComboBox _ManufacturerComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _AutoNumberButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label _NetInfoLabel;
        private System.Windows.Forms.Button _GetInstrumentScpiTemplateButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _AddFileToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteFileToolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _AddPhotoToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeletePhotoToolStripButton;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.TreeView _ScpiTreeView;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton _AddScpiToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteScpiToolStripButton;
        private System.Windows.Forms.TextBox _ScpiNameTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox _ScpiCommandTextBox;
        private System.Windows.Forms.TextBox _ScpiDescriptionTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.StatusStrip _FilesStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _FilesPathToolStripStatusLabel;
        private System.Windows.Forms.StatusStrip _PhotosStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _PhotosPathToolStripStatusLabel;
    }
}