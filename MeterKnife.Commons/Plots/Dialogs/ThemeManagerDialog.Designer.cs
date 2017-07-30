namespace MeterKnife.Plots.Dialogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemeManagerDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._NewThemeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteThemeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._ThemesListBox = new System.Windows.Forms.ListBox();
            this.themePanel1 = new MeterKnife.Plots.Dialogs.ThemePanel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewThemeToolStripButton,
            this._DeleteThemeToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(395, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _NewThemeToolStripButton
            // 
            this._NewThemeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._NewThemeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_NewThemeToolStripButton.Image")));
            this._NewThemeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewThemeToolStripButton.Name = "_NewThemeToolStripButton";
            this._NewThemeToolStripButton.Size = new System.Drawing.Size(60, 22);
            this._NewThemeToolStripButton.Text = "新建主题";
            // 
            // _DeleteThemeToolStripButton
            // 
            this._DeleteThemeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteThemeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteThemeToolStripButton.Image")));
            this._DeleteThemeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteThemeToolStripButton.Name = "_DeleteThemeToolStripButton";
            this._DeleteThemeToolStripButton.Size = new System.Drawing.Size(60, 22);
            this._DeleteThemeToolStripButton.Text = "删除主题";
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.Location = new System.Drawing.Point(0, 25);
            this._SplitContainer.Name = "_SplitContainer";
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._ThemesListBox);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.Controls.Add(this.themePanel1);
            this._SplitContainer.Size = new System.Drawing.Size(395, 316);
            this._SplitContainer.SplitterDistance = 79;
            this._SplitContainer.TabIndex = 2;
            // 
            // _ThemesListBox
            // 
            this._ThemesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ThemesListBox.FormattingEnabled = true;
            this._ThemesListBox.Items.AddRange(new object[] {
            "默认主题"});
            this._ThemesListBox.Location = new System.Drawing.Point(0, 0);
            this._ThemesListBox.Name = "_ThemesListBox";
            this._ThemesListBox.Size = new System.Drawing.Size(79, 316);
            this._ThemesListBox.TabIndex = 0;
            // 
            // themePanel1
            // 
            this.themePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themePanel1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.themePanel1.Location = new System.Drawing.Point(0, 0);
            this.themePanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.themePanel1.Name = "themePanel1";
            this.themePanel1.Size = new System.Drawing.Size(312, 316);
            this.themePanel1.TabIndex = 0;
            // 
            // ThemeManagerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 341);
            this.Controls.Add(this._SplitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ThemeManagerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "主题管理";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.ListBox _ThemesListBox;
        private System.Windows.Forms.ToolStripButton _NewThemeToolStripButton;
        private System.Windows.Forms.ToolStripButton _DeleteThemeToolStripButton;
        private ThemePanel themePanel1;
    }
}