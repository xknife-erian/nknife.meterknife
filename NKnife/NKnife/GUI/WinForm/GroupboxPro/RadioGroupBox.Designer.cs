namespace NKnife.GUI.WinForm.GroupboxPro
{
	partial class RadioGroupBox
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
			this._RadioButton = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// m_radioButton
			// 
			this._RadioButton.AutoSize = true;
			this._RadioButton.Location = new System.Drawing.Point(0, 0);
			this._RadioButton.Name = "m_radioButton";
			this._RadioButton.Size = new System.Drawing.Size(104, 24);
			this._RadioButton.TabIndex = 0;
			this._RadioButton.TabStop = true;
			this._RadioButton.Text = "radioButton";
			this._RadioButton.UseVisualStyleBackColor = true;
			this._RadioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// RadioGroupBox
			// 
			this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.CheckGroupBox_ControlAdded);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.RadioButton _RadioButton;
	}
}
