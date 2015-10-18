using MeterKnife.Scpis.ScpiTree;

namespace MeterKnife.Scpis
{
    partial class InstrumentScpiGroupTreeDialog
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
            this.components = new System.ComponentModel.Container();
            this._CancelButton = new System.Windows.Forms.Button();
            this._ConfirmButton = new System.Windows.Forms.Button();
            this._Panel = new System.Windows.Forms.Panel();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._DeleteInstrumentToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditInstrumentToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._Tree = new MeterKnife.Scpis.ScpiTree.SubjectFileTree();
            this._NewToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._NewInstrumentToolStripButton = new System.Windows.Forms.ToolStripMenuItem();
            this._NewScpiSubjectToolStripButton = new System.Windows.Forms.ToolStripMenuItem();
            this._Panel.SuspendLayout();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(228, 351);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 27);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _ConfirmButton
            // 
            this._ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ConfirmButton.Location = new System.Drawing.Point(147, 351);
            this._ConfirmButton.Name = "_ConfirmButton";
            this._ConfirmButton.Size = new System.Drawing.Size(75, 27);
            this._ConfirmButton.TabIndex = 2;
            this._ConfirmButton.Text = "确定";
            this._ConfirmButton.UseVisualStyleBackColor = true;
            this._ConfirmButton.Click += new System.EventHandler(this._ConfirmButton_Click);
            // 
            // _Panel
            // 
            this._Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Panel.Controls.Add(this._Tree);
            this._Panel.Controls.Add(this._ToolStrip);
            this._Panel.Location = new System.Drawing.Point(0, 0);
            this._Panel.Name = "_Panel";
            this._Panel.Size = new System.Drawing.Size(312, 346);
            this._Panel.TabIndex = 3;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewToolStripButton,
            this._DeleteInstrumentToolStripButton,
            this._EditInstrumentToolStripButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(312, 25);
            this._ToolStrip.TabIndex = 0;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // _DeleteInstrumentToolStripButton
            // 
            this._DeleteInstrumentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteInstrumentToolStripButton.Image = global::MeterKnife.Scpis.Properties.Resources.delete;
            this._DeleteInstrumentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteInstrumentToolStripButton.Name = "_DeleteInstrumentToolStripButton";
            this._DeleteInstrumentToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteInstrumentToolStripButton.Text = "删除仪器";
            this._DeleteInstrumentToolStripButton.Click += new System.EventHandler(this._DeleteInstrumentToolStripButton_Click);
            // 
            // _EditInstrumentToolStripButton
            // 
            this._EditInstrumentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._EditInstrumentToolStripButton.Image = global::MeterKnife.Scpis.Properties.Resources.edit;
            this._EditInstrumentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditInstrumentToolStripButton.Name = "_EditInstrumentToolStripButton";
            this._EditInstrumentToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._EditInstrumentToolStripButton.Text = "编辑仪器";
            this._EditInstrumentToolStripButton.Click += new System.EventHandler(this._EditInstrumentToolStripButton_Click);
            // 
            // _Tree
            // 
            this._Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Tree.FullRowSelect = true;
            this._Tree.ImageIndex = 0;
            this._Tree.ItemHeight = 22;
            this._Tree.Location = new System.Drawing.Point(0, 25);
            this._Tree.Name = "_Tree";
            this._Tree.SelectedImageIndex = 0;
            this._Tree.ShowLines = false;
            this._Tree.Size = new System.Drawing.Size(312, 321);
            this._Tree.TabIndex = 0;
            // 
            // _NewToolStripButton
            // 
            this._NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._NewToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewInstrumentToolStripButton,
            this._NewScpiSubjectToolStripButton});
            this._NewToolStripButton.Image = global::MeterKnife.Scpis.Properties.Resources.add;
            this._NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewToolStripButton.Name = "_NewToolStripButton";
            this._NewToolStripButton.Size = new System.Drawing.Size(29, 22);
            this._NewToolStripButton.Text = "新建仪器";
            // 
            // _NewInstrumentToolStripButton
            // 
            this._NewInstrumentToolStripButton.Name = "_NewInstrumentToolStripButton";
            this._NewInstrumentToolStripButton.Size = new System.Drawing.Size(152, 22);
            this._NewInstrumentToolStripButton.Text = "新建仪器";
            this._NewInstrumentToolStripButton.Click += new System.EventHandler(this._NewInstrumentToolStripButton_Click);
            // 
            // _NewScpiSubjectToolStripButton
            // 
            this._NewScpiSubjectToolStripButton.Name = "_NewScpiSubjectToolStripButton";
            this._NewScpiSubjectToolStripButton.Size = new System.Drawing.Size(152, 22);
            this._NewScpiSubjectToolStripButton.Text = "新建指令集";
            this._NewScpiSubjectToolStripButton.Click += new System.EventHandler(this._NewScpiSubjectToolStripButton_Click);
            // 
            // InstrumentScpiGroupTreeDialog
            // 
            this.AcceptButton = this._ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(312, 386);
            this.Controls.Add(this._Panel);
            this.Controls.Add(this._ConfirmButton);
            this.Controls.Add(this._CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstrumentScpiGroupTreeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "仪器与指令管理器";
            this._Panel.ResumeLayout(false);
            this._Panel.PerformLayout();
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SubjectFileTree _Tree;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _ConfirmButton;
        private System.Windows.Forms.Panel _Panel;
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ToolStripButton _DeleteInstrumentToolStripButton;
        private System.Windows.Forms.ToolStripButton _EditInstrumentToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton _NewToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem _NewInstrumentToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem _NewScpiSubjectToolStripButton;
    }
}