namespace NKnife.MeterKnife.Workbench
{
    partial class Form1
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exitMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this._separator3 = new System.Windows.Forms.ToolStripSeparator();
            this._openAppMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this._separator2 = new System.Windows.Forms.ToolStripSeparator();
            this._engineeringManagerMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this._dutManagerMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this._currentMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this._separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openAppMenuButton,
            this._separator1,
            this._currentMenuButton,
            this._separator2,
            this._engineeringManagerMenuButton,
            this._dutManagerMenuButton,
            this._separator3,
            this._exitMenuButton});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 154);
            // 
            // _exitMenuButton
            // 
            this._exitMenuButton.Name = "_exitMenuButton";
            this._exitMenuButton.Size = new System.Drawing.Size(180, 22);
            this._exitMenuButton.Text = "退出(&X)";
            // 
            // _separator3
            // 
            this._separator3.Name = "_separator3";
            this._separator3.Size = new System.Drawing.Size(177, 6);
            // 
            // _openAppMenuButton
            // 
            this._openAppMenuButton.Name = "_openAppMenuButton";
            this._openAppMenuButton.Size = new System.Drawing.Size(180, 22);
            this._openAppMenuButton.Text = "主程序(&W)";
            // 
            // _separator2
            // 
            this._separator2.Name = "_separator2";
            this._separator2.Size = new System.Drawing.Size(177, 6);
            // 
            // _engineeringManagerMenuButton
            // 
            this._engineeringManagerMenuButton.Name = "_engineeringManagerMenuButton";
            this._engineeringManagerMenuButton.Size = new System.Drawing.Size(180, 22);
            this._engineeringManagerMenuButton.Text = "工程管理(&E)";
            // 
            // _dutManagerMenuButton
            // 
            this._dutManagerMenuButton.Name = "_dutManagerMenuButton";
            this._dutManagerMenuButton.Size = new System.Drawing.Size(180, 22);
            this._dutManagerMenuButton.Text = "被测物管理(&D)";
            // 
            // _currentMenuButton
            // 
            this._currentMenuButton.Name = "_currentMenuButton";
            this._currentMenuButton.Size = new System.Drawing.Size(180, 22);
            this._currentMenuButton.Text = "测量进程(&C)";
            // 
            // _separator1
            // 
            this._separator1.Name = "_separator1";
            this._separator1.Size = new System.Drawing.Size(177, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _exitMenuButton;
        private System.Windows.Forms.ToolStripMenuItem _openAppMenuButton;
        private System.Windows.Forms.ToolStripSeparator _separator1;
        private System.Windows.Forms.ToolStripMenuItem _currentMenuButton;
        private System.Windows.Forms.ToolStripSeparator _separator2;
        private System.Windows.Forms.ToolStripMenuItem _engineeringManagerMenuButton;
        private System.Windows.Forms.ToolStripMenuItem _dutManagerMenuButton;
        private System.Windows.Forms.ToolStripSeparator _separator3;
    }
}