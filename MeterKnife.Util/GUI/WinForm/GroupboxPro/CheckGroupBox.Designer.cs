namespace NKnife.GUI.WinForm.GroupboxPro
{
	partial class CheckGroupBox
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
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._CheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// m_checkBox
			// 
			this._CheckBox.AutoSize = true;
			this._CheckBox.Checked = true;
			this._CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._CheckBox.Location = new System.Drawing.Point(0, 0);
			this._CheckBox.Name = "m_checkBox";
			this._CheckBox.Size = new System.Drawing.Size(104, 24);
			this._CheckBox.TabIndex = 0;
			this._CheckBox.Text = "checkBox";
			this._CheckBox.UseVisualStyleBackColor = true;
			this._CheckBox.CheckStateChanged += new System.EventHandler(this.CheckBoxCheckStateChanged);
			this._CheckBox.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
			// 
			// CheckGroupBox
			// 
			this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.CheckGroupBoxControlAdded);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.CheckBox _CheckBox;
	}
}
