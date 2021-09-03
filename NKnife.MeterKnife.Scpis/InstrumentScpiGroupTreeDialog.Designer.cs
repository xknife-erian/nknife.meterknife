using NKnife.MeterKnife.Scpis.ScpiTree;

namespace NKnife.MeterKnife.Scpis
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
            this._Tree = new SubjectFileTree();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._NewInstrumentToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._NewSubjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._DeleteInstrumentToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteSubjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._EditInstrumentToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._EditSubjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._ExportButton = new System.Windows.Forms.ToolStripButton();
            this._ImportButton = new System.Windows.Forms.ToolStripButton();
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
            this._Panel.Padding = new System.Windows.Forms.Padding(5);
            this._Panel.Size = new System.Drawing.Size(312, 346);
            this._Panel.TabIndex = 3;
            // 
            // _Tree
            // 
            this._Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Tree.FullRowSelect = true;
            this._Tree.ImageIndex = 0;
            this._Tree.ItemHeight = 22;
            this._Tree.Location = new System.Drawing.Point(5, 30);
            this._Tree.Name = "_Tree";
            this._Tree.SelectedImageIndex = 0;
            this._Tree.ShowLines = false;
            this._Tree.Size = new System.Drawing.Size(302, 311);
            this._Tree.TabIndex = 0;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewInstrumentToolStripButton,
            this._NewSubjectToolStripButton,
            this.toolStripSeparator1,
            this._DeleteInstrumentToolStripButton,
            this._DeleteSubjectToolStripButton,
            this.toolStripSeparator2,
            this._EditInstrumentToolStripButton,
            this._EditSubjectToolStripButton,
            this.toolStripSeparator3,
            this._ExportButton,
            this._ImportButton});
            this._ToolStrip.Location = new System.Drawing.Point(5, 5);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(302, 25);
            this._ToolStrip.TabIndex = 0;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // _NewInstrumentToolStripButton
            // 
            this._NewInstrumentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._NewInstrumentToolStripButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.add;
            this._NewInstrumentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewInstrumentToolStripButton.Name = "_NewInstrumentToolStripButton";
            this._NewInstrumentToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._NewInstrumentToolStripButton.Text = "新建仪器";
            this._NewInstrumentToolStripButton.Click += new System.EventHandler(this._NewToolStripButton_Click);
            // 
            // _NewSubjectToolStripButton
            // 
            this._NewSubjectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._NewSubjectToolStripButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.add_subject;
            this._NewSubjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewSubjectToolStripButton.Name = "_NewSubjectToolStripButton";
            this._NewSubjectToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._NewSubjectToolStripButton.Text = "新建功能主题";
            this._NewSubjectToolStripButton.Click += new System.EventHandler(this._NewSubjectToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _DeleteInstrumentToolStripButton
            // 
            this._DeleteInstrumentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteInstrumentToolStripButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.delete;
            this._DeleteInstrumentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteInstrumentToolStripButton.Name = "_DeleteInstrumentToolStripButton";
            this._DeleteInstrumentToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteInstrumentToolStripButton.Text = "删除仪器";
            this._DeleteInstrumentToolStripButton.Click += new System.EventHandler(this._DeleteInstrumentToolStripButton_Click);
            // 
            // _DeleteSubjectToolStripButton
            // 
            this._DeleteSubjectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteSubjectToolStripButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.delete_subject;
            this._DeleteSubjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteSubjectToolStripButton.Name = "_DeleteSubjectToolStripButton";
            this._DeleteSubjectToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteSubjectToolStripButton.Text = "删除功能主题";
            this._DeleteSubjectToolStripButton.Click += new System.EventHandler(this._DeleteSubjectToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _EditInstrumentToolStripButton
            // 
            this._EditInstrumentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._EditInstrumentToolStripButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.edit;
            this._EditInstrumentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditInstrumentToolStripButton.Name = "_EditInstrumentToolStripButton";
            this._EditInstrumentToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._EditInstrumentToolStripButton.Text = "编辑仪器";
            this._EditInstrumentToolStripButton.Click += new System.EventHandler(this._EditInstrumentToolStripButton_Click);
            // 
            // _EditSubjectToolStripButton
            // 
            this._EditSubjectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._EditSubjectToolStripButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.edit_subject;
            this._EditSubjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._EditSubjectToolStripButton.Name = "_EditSubjectToolStripButton";
            this._EditSubjectToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._EditSubjectToolStripButton.Text = "编辑功能主题";
            this._EditSubjectToolStripButton.Click += new System.EventHandler(this._EditSubjectToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _ExportButton
            // 
            this._ExportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ExportButton.Enabled = false;
            this._ExportButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.export;
            this._ExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ExportButton.Name = "_ExportButton";
            this._ExportButton.Size = new System.Drawing.Size(23, 22);
            this._ExportButton.Text = "导出";
            this._ExportButton.Click += new System.EventHandler(this._ExportButton_Click);
            // 
            // _ImportButton
            // 
            this._ImportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ImportButton.Enabled = false;
            this._ImportButton.Image = global::NKnife.MeterKnife.Scpis.Properties.Resources.import;
            this._ImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ImportButton.Name = "_ImportButton";
            this._ImportButton.Size = new System.Drawing.Size(23, 22);
            this._ImportButton.Text = "导入";
            this._ImportButton.Click += new System.EventHandler(this._ImportButton_Click);
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
        private System.Windows.Forms.ToolStripButton _NewInstrumentToolStripButton;
        private System.Windows.Forms.ToolStripButton _NewSubjectToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _DeleteSubjectToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _EditSubjectToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _ExportButton;
        private System.Windows.Forms.ToolStripButton _ImportButton;
    }
}