namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    partial class InstrumentsListPanel
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._ListHead = new MeterKnife.Views.InstrumentsDiscovery.Controls.InstrumentsListHead();
            this.SuspendLayout();
            // 
            // _ListHead
            // 
            this._ListHead.Count = 0;
            this._ListHead.Dock = System.Windows.Forms.DockStyle.Top;
            this._ListHead.GatewayModel = "GatewayModelLabel";
            this._ListHead.Location = new System.Drawing.Point(0, 0);
            this._ListHead.Name = "_ListHead";
            this._ListHead.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._ListHead.Size = new System.Drawing.Size(410, 32);
            this._ListHead.TabIndex = 0;
            // 
            // InstrumentsListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this._ListHead);
            this.Name = "InstrumentsListPanel";
            this.Size = new System.Drawing.Size(410, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private InstrumentsListHead _ListHead;
    }
}
