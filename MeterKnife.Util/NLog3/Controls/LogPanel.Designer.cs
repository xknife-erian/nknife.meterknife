namespace NKnife.NLog3.Controls
{
    sealed partial class LogPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogPanel));
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._LevelToolButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._TraceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DebugMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._WarnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ErrorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._FatalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ClearToolButton = new System.Windows.Forms.ToolStripButton();
            this._LogView = new NLogListView();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            resources.ApplyResources(this._ToolStrip, "_ToolStrip");
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LevelToolButton,
            this._ClearToolButton});
            this._ToolStrip.Name = "_ToolStrip";
            // 
            // _LevelToolButton
            // 
            resources.ApplyResources(this._LevelToolButton, "_LevelToolButton");
            this._LevelToolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._TraceMenuItem,
            this._DebugMenuItem,
            this._InfoMenuItem,
            this._WarnMenuItem,
            this._ErrorMenuItem,
            this._FatalMenuItem});
            this._LevelToolButton.Name = "_LevelToolButton";
            // 
            // _TraceMenuItem
            // 
            resources.ApplyResources(this._TraceMenuItem, "_TraceMenuItem");
            this._TraceMenuItem.Name = "_TraceMenuItem";
            this._TraceMenuItem.Click += new System.EventHandler(this.LevelToolButtonClick);
            // 
            // _DebugMenuItem
            // 
            resources.ApplyResources(this._DebugMenuItem, "_DebugMenuItem");
            this._DebugMenuItem.Name = "_DebugMenuItem";
            this._DebugMenuItem.Click += new System.EventHandler(this.LevelToolButtonClick);
            // 
            // _InfoMenuItem
            // 
            resources.ApplyResources(this._InfoMenuItem, "_InfoMenuItem");
            this._InfoMenuItem.Name = "_InfoMenuItem";
            this._InfoMenuItem.Click += new System.EventHandler(this.LevelToolButtonClick);
            // 
            // _WarnMenuItem
            // 
            resources.ApplyResources(this._WarnMenuItem, "_WarnMenuItem");
            this._WarnMenuItem.Name = "_WarnMenuItem";
            this._WarnMenuItem.Click += new System.EventHandler(this.LevelToolButtonClick);
            // 
            // _ErrorMenuItem
            // 
            resources.ApplyResources(this._ErrorMenuItem, "_ErrorMenuItem");
            this._ErrorMenuItem.Name = "_ErrorMenuItem";
            this._ErrorMenuItem.Click += new System.EventHandler(this.LevelToolButtonClick);
            // 
            // _FatalMenuItem
            // 
            resources.ApplyResources(this._FatalMenuItem, "_FatalMenuItem");
            this._FatalMenuItem.Name = "_FatalMenuItem";
            this._FatalMenuItem.Click += new System.EventHandler(this.LevelToolButtonClick);
            // 
            // _ClearToolButton
            // 
            resources.ApplyResources(this._ClearToolButton, "_ClearToolButton");
            this._ClearToolButton.Name = "_ClearToolButton";
            this._ClearToolButton.Click += new System.EventHandler(this.ClearToolButtonClick);
            // 
            // _LogView
            // 
            resources.ApplyResources(this._LogView, "_LogView");
            this._LogView.FullRowSelect = true;
            this._LogView.GridLines = true;
            this._LogView.MaxRowCount = 120;
            this._LogView.MultiSelect = false;
            this._LogView.Name = "_LogView";
            this._LogView.ShowItemToolTips = true;
            this._LogView.UseCompatibleStateImageBehavior = false;
            this._LogView.View = System.Windows.Forms.View.Details;
            // 
            // LogPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._LogView);
            this.Controls.Add(this._ToolStrip);
            this.Name = "LogPanel";
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NLogListView _LogView;
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ToolStripButton _ClearToolButton;
        private System.Windows.Forms.ToolStripDropDownButton _LevelToolButton;
        private System.Windows.Forms.ToolStripMenuItem _TraceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DebugMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _InfoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _WarnMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ErrorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _FatalMenuItem;
    }
}
